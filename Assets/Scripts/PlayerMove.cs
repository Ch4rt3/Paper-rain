using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Header("Configuración de Velocidades")]
    [SerializeField] float velocidadMoto = 8f;
    [SerializeField] float velocidadAvion = 5f;
    [SerializeField] float velocidadBulldozer = 2f;
    
    [Header("Configuración de Salto (Solo Moto)")]
    [SerializeField] float fuerzaSalto = 12f;

    // Componentes del mismo Player
    private Rigidbody2D _body;
    private Animator _animator;
    private PlayerTransform _playerTransform;

    // Acciones para el control
    private InputAction _moveAction;

    public Vector2 CurrentMoveInput { get; private set; }
    
    private bool enSuelo = false; // Controla si estamos tocando el piso o una plataforma

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

        // --- LÓGICA DE SALTO ---
        // Si presionas Espacio, eres Moto y estás tocando algo sólido (suelo/rampa)
        if (Input.GetKeyDown(KeyCode.Space) && _playerTransform != null && _playerTransform.currentForm == PlayerTransform.Form.Moto && enSuelo)
        {
            // Le damos impulso hacia arriba
            _body.linearVelocityY = fuerzaSalto;
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

    // Funciones para detectar si estamos tocando una superficie para poder saltar
    private void OnCollisionStay2D(Collision2D collision)
    {
        enSuelo = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        enSuelo = false;
    }
}