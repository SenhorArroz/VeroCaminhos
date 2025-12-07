using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider))] // Garante que o objeto tenha um Collider
public class TriggerInteract : MonoBehaviour
{
    [Header("Configurações")]
    [Tooltip("Tag do objeto que vai ativar o trigger (ex: Player)")]
    [SerializeField] private string playerTag = "Player";

    [Tooltip("Se marcado, a interação só funciona uma única vez.")]
    [SerializeField] private bool acionarApenasUmaVez = false;

    [Tooltip("Se marcado, o objeto se desativa após a interação.")]
    [SerializeField] private bool desativarAoAtivar = false;

    [Header("Eventos")]
    public UnityEvent aoInteragir;

    // Variáveis Internas
    private InputSystem_Actions _controls; // Classe gerada pelo Input System
    private bool _jogadorNoTrigger = false;
    private bool _jaFoiAcionado = false;

    private void Awake()
    {
        _controls = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        _controls.Player.Enable();
        _controls.Player.Interact.performed += OnInteractPerformed;
    }

    private void OnDisable()
    {
        _controls.Player.Interact.performed -= OnInteractPerformed;
        _controls.Player.Disable();
    }

    // Chamado quando o botão é apertado
    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        // 1. Verifica se o jogador está dentro da área
        if (!_jogadorNoTrigger) return;

        // 2. Verifica se é para acionar apenas uma vez e se já foi feito
        if (acionarApenasUmaVez && _jaFoiAcionado) return;

        // 3. Executa o evento
        aoInteragir.Invoke();
        Debug.Log("sim");
        _jaFoiAcionado = true;

        // 4. Desativa o objeto se necessário
        if (desativarAoAtivar)
        {
            gameObject.SetActive(false);
        }
    }

    // --- Lógica do Trigger ---

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            _jogadorNoTrigger = true;
            // Opcional: Aqui você pode ativar uma UI (ex: "Aperte E")
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            _jogadorNoTrigger = false;
            // Opcional: Aqui você desativa a UI
        }
    }
}