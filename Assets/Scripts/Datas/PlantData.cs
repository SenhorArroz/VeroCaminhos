using UnityEngine;

[CreateAssetMenu(menuName = "Farm/Plant")]
public class PlantData : ScriptableObject
{
    public string plantName;
    [TextArea]
    public string description;
    public Sprite seedSprite;          // sprite da semente
    public Sprite[] growthSprites;
    public float timePerStage;
    public bool needsWaterEachDay;
    public float value;
    public float sellValue;
}
