using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject itemPrefab;
    public Sprite[] itemSprites;
    public float spawnInterval = 1f;
    public float xRange = 8f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnItem), 0.5f, spawnInterval);
    }

    void SpawnItem()
    {
        // Verifica o Singleton
        if (GameManager.instance == null || GameManager.instance.isGameOver) return;

        if (itemSprites == null || itemSprites.Length == 0) return;

        // Lógica de aleatoriedade
        int randomIndex = Random.Range(0, itemSprites.Length);
        Sprite randomSprite = itemSprites[randomIndex];

        float randomX = Random.Range(-xRange, xRange);
        Vector2 spawnPos = new Vector2(randomX, transform.position.y);

        GameObject newItem = Instantiate(itemPrefab, spawnPos, Quaternion.identity);

        SpriteRenderer sr = newItem.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.sprite = randomSprite;
        }
    }
}