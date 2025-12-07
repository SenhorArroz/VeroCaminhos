using UnityEngine;

public class SoilGridManager : MonoBehaviour
{
    public SoilTile soilPrefab;

    [Header("Tamanho da plantação")]
    public int rows = 4;
    public int cols = 6;

    [Header("Distância entre tiles")]
    public float spacing = 1f;


    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        if (soilPrefab == null)
        {
            Debug.LogError("Soil prefab não atribuído!");
            return;
        }

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                Vector3 pos = new Vector3(
                    x * spacing,
                    -y * spacing, // negativo para ficar “para baixo”
                    0
                );

                Instantiate(soilPrefab, pos + transform.position, Quaternion.identity, transform);
            }
        }
    }
}
