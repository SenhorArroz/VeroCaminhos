using UnityEngine;

public class SoilTile : MonoBehaviour
{
    [Header("Estado do Solo")]
    public bool isPlowed;
    public bool isWatered;
    public bool hasSeed;

    [Header("Planta")]
    public PlantData plant;
    public int growthStage;
    public float growthTimer;

    [Header("Sprites / Renderers")]
    public SpriteRenderer plantRenderer;
    public SpriteRenderer soilRenderer;
    public Sprite plowedSprite;
    public Sprite wateredSprite;
    public Sprite defaultSprite;

    void Start()
    {
        if (soilRenderer == null)
            soilRenderer = GetComponent<SpriteRenderer>();

        soilRenderer.sprite = defaultSprite;
    }

    void Update()
    {
        // Crescimento da planta
        if (hasSeed && plant != null)
        {
            growthTimer -= Time.deltaTime;

            if (growthTimer <= 0f)
                AdvanceGrowthStage();
        }
    }

    // -----------------------------------------
    //          REAGE AO ENTRAR NO TRIGGER
    // -----------------------------------------
    private void OnTriggerEnter2D(Collider2D col)
    {
        PlayerManager player = col.GetComponent<PlayerManager>();
        if (player == null)
            return;

        HandlePlayerInteraction(player);
    }

    // Sistema inteligente de reação
    void HandlePlayerInteraction(PlayerManager player)
    {
        int idx = player.selectedIndex;

        // 0 — ENXADA
        if (idx == 0)
        {
            if (!isPlowed)
            {
                Plow();
                return;
            }
        }

        // 1 — REGADOR
        if (idx == 1)
        {
            if (isPlowed && !isWatered && player.currentWater > 0)
            {
                Water();
                player.UseWater();
                return;
            }
        }

        // 2+ — SEMENTES
        int seedIndex = idx - 2;

        if (seedIndex >= 0 && seedIndex < player.seeds.Count)
        {
            var seedStack = player.seeds[seedIndex];

            if (isPlowed && !hasSeed && seedStack.amount > 0)
            {
                Plant(seedStack.data);
                player.ConsumeSeed(seedStack.data, 1);
                return;
            }
        }

        // ÚLTIMO: COLHEER
        if (CanHarvest())
        {
            Harvest(player);
        }
    }

    // -----------------------------------------
    //          FUNCIONALIDADES DO SOLO
    // -----------------------------------------

    public void Plow()
    {
        if (!isPlowed)
        {
            isPlowed = true;
            UpdateVisuals();
        }
    }

    public void Water()
    {
        if (isPlowed)
        {
            isWatered = true;
            UpdateVisuals();
        }
    }

    public void Plant(PlantData newPlant)
    {
        if (isPlowed && !hasSeed)
        {
            plant = newPlant;
            hasSeed = true;

            growthStage = 0;
            growthTimer = plant.timePerStage;

            UpdateVisuals();
        }
    }

    void AdvanceGrowthStage()
    {
        if (plant != null && growthStage < plant.growthSprites.Length - 1)
        {
            growthStage++;
            growthTimer = plant.timePerStage;
            UpdateVisuals();
        }
    }

    public bool CanHarvest()
    {
        return hasSeed &&
               plant != null &&
               growthStage == plant.growthSprites.Length - 1;
    }

    public void Harvest(PlayerManager player)
    {
        if (CanHarvest())
        {
            player.GiveMoney(plant.value);
            ResetTile();
        }
    }

    public void ResetTile()
    {
        isPlowed = false;
        hasSeed = false;
        isWatered = false;
        plant = null;
        growthStage = 0;
        growthTimer = 0f;

        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        // Solo
        if (isPlowed)
            soilRenderer.sprite = isWatered ? wateredSprite : plowedSprite;
        else
            soilRenderer.sprite = defaultSprite;

        // Planta
        if (hasSeed && plant != null)
            plantRenderer.sprite = plant.growthSprites[growthStage];
        else
            plantRenderer.sprite = null;
    }
}
