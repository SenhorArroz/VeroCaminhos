using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    [System.Serializable]
    public class SeedStack
    {
        public PlantData data;
        public int amount;
    }

    [Header("Inventário")]
    public List<SeedStack> seeds = new List<SeedStack>();
    public int currentWater = 10;
    public int maxWater = 10;
    public int money = 0;

    [Header("Hotbar")]
    public int selectedIndex = 0;

    // Sinal vindo do InputSystem — SoilTile só reage quando isso for true no frame
    [HideInInspector]
    public bool didInteractThisFrame = false;

    // -----------------------------
    // IDENTIFICAÇÃO DO ITEM SELECIONADO
    // -----------------------------
    public bool SelectedIsHoe() => selectedIndex == 0;
    public bool SelectedIsWateringCan() => selectedIndex == 1;
    public bool SelectedIsSeed() => selectedIndex >= 2 && selectedIndex < seeds.Count + 2;

    public SeedStack GetSelectedSeed()
    {
        if (!SelectedIsSeed()) return null;
        return seeds[selectedIndex - 2];
    }

    // -----------------------------
    // INVENTÁRIO: sementes, água, dinheiro
    // -----------------------------
    public void ConsumeSeed(PlantData data, int amount)
    {
        foreach (var s in seeds)
        {
            if (s.data == data)
            {
                s.amount -= amount;
                if (s.amount < 0)
                    s.amount = 0;

                return;
            }
        }
    }

    public void GiveMoney(int amount)
    {
        money = Mathf.Max(0, money + amount);
    }

    public void UseWater()
    {
        currentWater = Mathf.Max(0, currentWater - 1);
    }

    public void RefillWater()
    {
        currentWater = maxWater;
    }

    // -----------------------------
    // HOTBAR — mudança circular
    // -----------------------------
    public void SelectNextItem()
    {
        int max = 2 + seeds.Count - 1;

        selectedIndex++;
        if (selectedIndex > max)
            selectedIndex = 0;
    }

    public void SelectPreviousItem()
    {
        int max = 2 + seeds.Count - 1;

        selectedIndex--;
        if (selectedIndex < 0)
            selectedIndex = max;
    }

    // -----------------------------
    // INPUTS DO PLAYER
    // Chamados pelo PlayerInput (Unity Input System)
    // -----------------------------
    public void OnInteract()
    {
        // O SoilTile ou PlayerInteraction só executa coisas
        // QUANDO este bool aciona true neste frame.
        didInteractThisFrame = true;
    }

    public void OnSelectRight()
    {
        SelectNextItem();
    }

    public void OnSelectLeft()
    {
        SelectPreviousItem();
    }

    // -----------------------------
    // RESET A CADA FRAME
    // -----------------------------
    private void LateUpdate()
    {
        // Como o nome sugere: depois de todos scripts Update rodarem,
        // este sinal é apagado. Assim nenhum tile recebe "interagir"
        // por engano em outro frame.
        didInteractThisFrame = false;
    }
}
