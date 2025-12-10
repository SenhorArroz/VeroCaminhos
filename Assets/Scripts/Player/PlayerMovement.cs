using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimentação")]
    public float moveSpeed = 4f;

    public bool canMove = false;

    public static PlayerMovement instance;

    private Rigidbody2D rb;
    private Vector2 input;

    // Referência ao Input System gerado
    private InputSystem_Actions actions;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
        rb = GetComponent<Rigidbody2D>();

        // cria o mapa de ações
        actions = new InputSystem_Actions();
    }

    void OnEnable()
    {
        actions.Player.Enable();

        // registra callback do input
        actions.Player.Move.performed += OnMove;
        actions.Player.Move.canceled += OnMoveCanceled;
    }

    void OnDisable()
    {
        actions.Player.Move.performed -= OnMove;
        actions.Player.Move.canceled -= OnMoveCanceled;

        actions.Player.Disable();
    }

    // recebe input do Input System
    private void OnMove(InputAction.CallbackContext ctx)
    {
        if (!canMove) return;
        input = ctx.ReadValue<Vector2>().normalized;
    }

    private void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        input = Vector2.zero;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + input * moveSpeed * Time.fixedDeltaTime);
    }
}
