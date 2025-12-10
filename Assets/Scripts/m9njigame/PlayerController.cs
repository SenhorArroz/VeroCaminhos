using UnityEngine;
using UnityEngine.InputSystem; // Necessário

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float xLimit = 8f;

    public static PlayerController instance;
    // Referência à classe gerada pelo Input System
    private InputSystem_Actions controls;
    private Vector2 moveInput;

    // Inicialização da instância dos controles
    void Awake()
    {
            controls = new InputSystem_Actions();

        // Configura o callback para ler o valor quando mover
        // O contexto (ctx) contém o valor do Vector2
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();

        // Quando soltar a tecla, o valor volta a ser zero
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }
    void OnEnable()
    {
        controls.Player.Enable();
    }

    void OnDisable()
    {
        controls.Player.Disable();
    }

    void Update()
    {
            transform.Translate(Vector2.right * moveInput.x * speed * Time.deltaTime);

            // Limite da tela (Clamp)
            float xPos = Mathf.Clamp(transform.position.x, -xLimit, xLimit);
            transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
    }
}