using UnityEngine;

[CreateAssetMenu(menuName = "Farm/Plant")]
public class PlantData : ScriptableObject
{
    public string plantName;
    public string description;
    public Sprite seedSprite;          // sprite da semente
    public Sprite[] growthSprites;
    public float timePerStage;
    public bool needsWaterEachDay;
    public int value;
    public int sellValue;
}
