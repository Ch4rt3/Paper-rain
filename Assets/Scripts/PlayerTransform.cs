using UnityEngine;

public class PlayerTransform : MonoBehaviour
{
    public enum Form
    {
        Moto,
        Excavadora,
        Avion
    }

    public Form currentForm = Form.Moto;

    private Animator animator;
    private Rigidbody2D rb;

    private bool isTransforming = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isTransforming)
            return;

        if (Input.GetKeyDown(KeyCode.Z))
            ChangeForm(Form.Moto);

        if (Input.GetKeyDown(KeyCode.X))
            ChangeForm(Form.Excavadora);

        if (Input.GetKeyDown(KeyCode.C))
            ChangeForm(Form.Avion);
    }

    void ChangeForm(Form targetForm)
    {
        if (currentForm == targetForm)
            return;

        isTransforming = true;

        if (currentForm == Form.Moto && targetForm == Form.Excavadora)
            animator.Play("MotoToExcavadora");

        else if (currentForm == Form.Excavadora && targetForm == Form.Moto)
            animator.Play("MotoToExcavadoraReverse");

        else if (currentForm == Form.Moto && targetForm == Form.Avion)
            animator.Play("MotoToAvion");

        else if (currentForm == Form.Avion && targetForm == Form.Moto)
            animator.Play("MotoToAvionReverse");

        else if (currentForm == Form.Excavadora && targetForm == Form.Avion)
            animator.Play("ExcavadoraToAvion");

        else if (currentForm == Form.Avion && targetForm == Form.Excavadora)
            animator.Play("ExcavadoraToAvionReverse");

        currentForm = targetForm;

        Invoke(nameof(FinishTransformation), 0.66f);
        ApplyStats();
    }

    void FinishTransformation()
    {
        isTransforming = false;

        switch (currentForm)
        {
            case Form.Moto:
                animator.Play("MotoIdle");
                break;
            case Form.Excavadora:
                animator.Play("ExcavadoraIdle");
                break;
            case Form.Avion:
                animator.Play("AvionIdle");
                break;
        }
    }

    void ApplyStats()
    {
        switch (currentForm)
        {
            case Form.Moto:
                rb.gravityScale = 2f;
                break;

            case Form.Excavadora:
                rb.gravityScale = 3f;
                break;

            case Form.Avion:
                rb.gravityScale = 0.5f;
                break;
        }
    }
}