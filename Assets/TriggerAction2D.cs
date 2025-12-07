using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))] // Garante que tem um Colisor 2D
public class TriggerAction2D : MonoBehaviour
{
    [Header("Configurações de Ativação")]
    [Tooltip("Ativa automaticamente assim que o Player entra na área.")]
    public bool triggerOnEnter = true;

    [Tooltip("O Player precisa apertar o botão de Interagir enquanto está dentro.")]
    public bool requireInput = false;

    [Tooltip("Se marcado, o evento acontece apenas 1 vez e nunca mais (ex: pegar item único).")]
    public bool triggerOnceForever = false;

    [Header("Input System")]
    [Tooltip("Arraste aqui a Ação de Interact do seu InputActions (ex: 'Gameplay/Interact'). Obrigatório se requireInput = true.")]
    public InputActionReference interactAction;

    [Header("Ação")]
    public UnityEvent onTrigger;

    // Controle interno
    private bool hasBeenTriggered = false;
    private bool isPlayerInside = false;

    private void Start()
    {
        // Garante que o colisor seja Trigger
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnEnable()
    {
        if (interactAction != null)
            interactAction.action.performed += OnInputPerformed;
    }

    private void OnDisable()
    {
        if (interactAction != null)
            interactAction.action.performed -= OnInputPerformed;
    }

    // --- FÍSICA 2D ---
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;

            // Se for ativação automática (sem input)
            if (triggerOnEnter && !requireInput)
            {
                TryExecuteAction();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }
    // -----------------

    private void OnInputPerformed(InputAction.CallbackContext ctx)
    {
        // Só executa se o player estiver dentro E o script exigir input
        if (isPlayerInside && requireInput)
        {
            TryExecuteAction();
        }
    }

    private void TryExecuteAction()
    {
        if (triggerOnceForever && hasBeenTriggered) return;

        onTrigger.Invoke();
        hasBeenTriggered = true;
    }

    // Função extra para resetar se precisar (ex: respawn)
    public void ResetTrigger()
    {
        hasBeenTriggered = false;
    }
}