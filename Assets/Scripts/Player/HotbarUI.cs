using UnityEngine;
using UnityEngine.InputSystem;

public class HotbarUI : MonoBehaviour
{
    [Header("References")]
    public Transform slotParent;
    public HotbarSlot slotPrefab;

    [Header("Icons")]
    public Sprite hoeSprite;
    public Sprite waterSprite;

    private PlayerManager player;
    private HotbarSlot[] slots;

    void Awake()
    {
        var p = GameObject.FindGameObjectWithTag("Player");
        if (p == null)
        {
            Debug.LogError("Nenhum objeto com tag Player encontrado na cena!");
            return;
        }

        player = p.GetComponent<PlayerManager>();
    }

    void Start()
    {
        BuildHotbar();
        UpdateVisuals();
    }
    private void Update()
    {
        UpdateVisuals();

    }

    void BuildHotbar()
    {
        int total = player.seeds.Count + 2;

        slots = new HotbarSlot[total];

        // Limpando qualquer slot antigo no editor
        foreach (Transform child in slotParent)
            Destroy(child.gameObject);

        for (int i = 0; i < total; i++)
        {
            HotbarSlot slot = Instantiate(slotPrefab, slotParent);

            // Garantir que cada slot tenha seu PRÓPRIO background
            slot.Initialize(player, this);
            slot.slotIndex = i;

            if (i == 0)
            {
                slot.SetupTool(HotbarSlot.ItemType.Hoe, hoeSprite, i);
            }
            else if (i == 1)
            {
                slot.SetupTool(HotbarSlot.ItemType.Water, waterSprite, i);
            }
            else
            {
                int seedIndex = i - 2;
                var seed = player.seeds[seedIndex];
                slot.SetupSeed(seed.data, seed.amount, i);
            }

            slots[i] = slot;
        }
    }

    public void UpdateVisuals()
    {
        if (slots == null) return;

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].ApplyVisuals();
        }
    }

    // Input System
    public void OnSelectLeft(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        player.selectedIndex--;
        if (player.selectedIndex < 0)
            player.selectedIndex = slots.Length - 1;

        UpdateVisuals();
    }

    public void OnSelectRight(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        player.selectedIndex++;
        if (player.selectedIndex >= slots.Length)
            player.selectedIndex = 0;

        UpdateVisuals();
    }
}
