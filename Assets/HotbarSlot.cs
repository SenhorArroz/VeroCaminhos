using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class HotbarSlot : MonoBehaviour, IPointerClickHandler
{
    public enum ItemType { Hoe, Water, Seed }

    public Image backgroundImage;
    public Image iconImage;
    public TextMeshProUGUI amountText;

    public Color normalColor = Color.white;
    public Color selectedColor = new Color(1f, 0.9f, 0.5f);

    public float normalScale = 1f;
    public float selectedScale = 1.15f;

    [HideInInspector] public ItemType type;
    [HideInInspector] public int slotIndex;

    private PlayerManager player;
    private HotbarUI ui;

    public void Initialize(PlayerManager p, HotbarUI u)
    {
        player = p;
        ui = u;
    }

    public void SetupTool(ItemType t, Sprite icon, int index)
    {
        type = t;
        slotIndex = index;

        iconImage.sprite = icon;
        iconImage.enabled = true;
        amountText.gameObject.SetActive(false);

        ApplyVisuals();
    }

    public void SetupSeed(PlantData data, int amount, int index)
    {
        type = ItemType.Seed;
        slotIndex = index;

        iconImage.sprite = data.seedSprite;
        iconImage.enabled = true;

        amountText.text = amount > 1 ? $"x{amount}" : "";
        amountText.gameObject.SetActive(amount > 0);

        ApplyVisuals();
    }

    public void ApplyVisuals()
    {
        if (player == null) return;

        bool selected = (player.selectedIndex == slotIndex);

        backgroundImage.color = selected ? selectedColor : normalColor;

        float scale = selected ? selectedScale : normalScale;
        transform.localScale = new Vector3(scale, scale, 1f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        player.selectedIndex = slotIndex;
        ui.UpdateVisuals();
    }
}
