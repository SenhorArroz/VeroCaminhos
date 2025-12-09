using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InteractTrigger : MonoBehaviour
{
    public bool ativarSomenteUmaVez = false;
    public bool ativarPorEntrada = false;

    public UnityEvent onInteract;

    private bool playerDentro = false;
    private bool jaAtivado = false;

    private InputSystem_Actions input;
    private InputAction botaoInteract;

    private void Awake()
    {
        input = new InputSystem_Actions();
        input.Enable(); // ← necessário para evitar input mudo
        botaoInteract = input.Player.Interact;
    }

    private void OnEnable()
    {
        botaoInteract.started += Interagir;
    }

    private void OnDisable()
    {
        botaoInteract.started -= Interagir;
    }

    private void Interagir(InputAction.CallbackContext ctx)
    {
        if (!playerDentro) return;

        if (ativarSomenteUmaVez && jaAtivado)
            return;

        onInteract?.Invoke();
        jaAtivado = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        playerDentro = true;

        if (ativarPorEntrada)
            jaAtivado = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        playerDentro = false;
    }
}
