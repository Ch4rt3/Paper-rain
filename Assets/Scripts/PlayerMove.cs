using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Header("Configuración de Velocidades")]
    [SerializeField] float velocidadMoto = 8f;
    [SerializeField] float velocidadAvion = 5f;
    [SerializeField] float velocidadBulldozer = 2f;

    // Componentes del mismo Player
    private Rigidbody2D _body;
    private Animator _animator;
    private PlayerTransform _playerTransform;

    // Acciones para el control
    private InputAction _moveAction;

    public Vector2 CurrentMoveInput { get; private set; }

    void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerTransform = GetComponent<PlayerTransform>();

        _moveAction = InputSystem.actions["Player/Move"];
    }

    void Update()
    {
        if (_playerTransform != null && _playerTransform.isTransforming)
        {
            CurrentMoveInput = Vector2.zero;
            _body.linearVelocityX = 0;
            _animator.SetBool("IsMoving", false);
            return;
        }

        float velocidadActual = ObtenerVelocidadPorForma();

        Vector2 move = _moveAction.ReadValue<Vector2>();
        CurrentMoveInput = move;

        _body.linearVelocityX = move.x * velocidadActual;

        if (move.x != 0)
        {
            if (move.x > 0)
                transform.localScale = new Vector3(1, 1, 1);
            else
                transform.localScale = new Vector3(-1, 1, 1);
        }

        if (_animator != null)
        {
            _animator.SetBool("IsMoving", Mathf.Abs(move.x) > 0.01f);
        }
    }

    float ObtenerVelocidadPorForma()
    {
        if (_playerTransform == null) return velocidadMoto;

        switch (_playerTransform.currentForm)
        {
            case PlayerTransform.Form.Moto:
                return velocidadMoto;

            case PlayerTransform.Form.Bulldozer:
                return velocidadBulldozer;

            case PlayerTransform.Form.Avion:
                return velocidadAvion;

            default:
                return velocidadMoto;
        }
    }
}