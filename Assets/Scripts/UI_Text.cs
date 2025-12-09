using UnityEngine;

public class UI_Text : MonoBehaviour
{
    private static UI_Text instance;
    [SerializeField]
    private TMPro.TextMeshProUGUI textComponent;

    public GameObject sim;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SetInactive();
        }
    }
    public void SetText(string text)
    {
        instance.textComponent.text = text;
    }
    public void SetTextPlanta(PlantData plantData)
    {
        string text = $"<b>{plantData.plantName}</b>\n\n{plantData.description}\n\n" +
            $"<b>Tempo para colheita:</b> {plantData.timePerStage} seconds\n" +
            $"<b>Precisa de água todo dia:</b> {(plantData.needsWaterEachDay ? "Sim" : "Não")}\n" +
            $"<b>Valor da semente:</b> {plantData.value} reais\n" +
            $"<b>Valor de venda:</b> {plantData.sellValue} reais/kg";
        SetText(text);
    }
    public void SetActive()
    {
        sim.gameObject.SetActive(true);
    }
    public void SetInactive()
    {
        sim.gameObject.SetActive(false);
    }
}