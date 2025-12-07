using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Configurações")]
    public float interactDistance = 0.6f;
    public LayerMask soilLayer;

    private PlayerManager player;
    private InputSystem_Actions actions;
    private Vector2 moveInput;
    private Vector2 facingDir = Vector2.down;

    void Awake()
    {
        player = GetComponent<PlayerManager>();
        actions = new InputSystem_Actions();
    }

    void OnEnable()
    {
        actions.Player.Enable();

        actions.Player.Move.performed += OnMove;
        actions.Player.Move.canceled += OnMoveCanceled;

        actions.Player.Interact.performed += OnInteract;

        actions.Player.SelectRight.performed += OnSelectRight;
        actions.Player.SelectLeft.performed += OnSelectLeft;
    }

    void OnDisable()
    {
        actions.Player.Move.performed -= OnMove;
        actions.Player.Move.canceled -= OnMoveCanceled;

        actions.Player.Interact.performed -= OnInteract;

        actions.Player.SelectRight.performed -= OnSelectRight;
        actions.Player.SelectLeft.performed -= OnSelectLeft;

        actions.Player.Disable();
    }

    // -------------------------------------
    //     MOVIMENTO → para direção
    // -------------------------------------
    private void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();

        if (moveInput.sqrMagnitude > 0.1f)
            facingDir = moveInput.normalized;
    }

    private void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        moveInput = Vector2.zero;
    }

    // -------------------------------------
    //     SELEÇÃO HOTBAR
    // -------------------------------------
    private void OnSelectRight(InputAction.CallbackContext ctx)
    {
        player.SelectNextItem();
    }

    private void OnSelectLeft(InputAction.CallbackContext ctx)
    {
        player.SelectPreviousItem();
    }

    // -------------------------------------
    //     INTERAÇÃO
    // -------------------------------------
    private void OnInteract(InputAction.CallbackContext ctx)
    {
        TryInteract();
    }

    void TryInteract()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            facingDir,
            interactDistance,
            soilLayer
        );

        if (!hit.collider)
            return;

        SoilTile tile = hit.collider.GetComponent<SoilTile>();
        if (!tile)
            return;

        int idx = player.selectedIndex;

        // 0 = Enxada
        if (idx == 0)
        {
            if (!tile.isPlowed)
                tile.Plow();

            return;
        }

        // 1 = Regador
        if (idx == 1)
        {
            if (tile.isPlowed && !tile.isWatered && player.currentWater > 0)
            {
                tile.Water();
                player.UseWater();
            }

            return;
        }

        // 2+ = Sementes
        int seedIndex = idx - 2;

        if (seedIndex >= 0 && seedIndex < player.seeds.Count)
        {
            var seedStack = player.seeds[seedIndex];

            if (seedStack.amount > 0 &&
                tile.isPlowed &&
                !tile.hasSeed)
            {
                tile.Plant(seedStack.data);
                player.ConsumeSeed(seedStack.data, 1);
                return;
            }
        }

        // Colher
        if (tile.CanHarvest())
        {
            tile.Harvest(player);
        }
    }
}
