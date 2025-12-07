using UnityEngine;
using TMPro; // se usar TMP
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public PlayerManager player;
    public TextMeshProUGUI waterText;
    public TextMeshProUGUI moneyText;

    void Update()
    {
        waterText.text = $"{player.currentWater} / {player.maxWater}";
        moneyText.text = $"${player.money}";
    }
}
