using UnityEngine;

public class GenerationManager : MonoBehaviour
{
    [Header("Generation Bounds")]
    public Vector2Int generationBoundSize;
    public Vector2 offset;

    [Header("Generation Settings")]
    public int chunkSize;
    public int fossilFreq;
    private GameObject[] fossilPrefabs;
    private GameObject[] groundPrefabs;
    private GameObject grassPrefab;

    void Start()
    {
        fossilPrefabs = new GameObject[3];
        groundPrefabs = new GameObject[3];

        for (int i = 0; i < 3; i++)
        {
            fossilPrefabs[i] = Resources.Load<GameObject>($"Prefabs/Environment/Underground/Fossil{i}");
            groundPrefabs[i] = Resources.Load<GameObject>($"Prefabs/Environment/Underground/Ground{i}");
        }

        grassPrefab = Resources.Load<GameObject>("Prefabs/Environment/Underground/Grass");

        GenerateTiles();
    }

    private void GenerateTiles()
    {
        int x1 = -generationBoundSize.x / 2;
        int x2 = generationBoundSize.x / 2;
        int y1 = generationBoundSize.y / 2;
        int y2 = -generationBoundSize.y / 2;

        Transform tilesParent = GameObject.Find("UndergroundTiles").transform;

        for (int i = x1; i < x2; i++)
        {
            Instantiate(grassPrefab, new Vector2(i, y1 + 1) + offset, Quaternion.identity, tilesParent);

            for (int j = y1; j >= y2 + 1; j--)
            {
                float isFossil = Random.value;
                Vector2 spawnPos = new Vector2(i, j) + offset;

                if (isFossil < (float)fossilFreq / chunkSize)
                {
                    int index = Random.Range(0, 3);
                    Instantiate(fossilPrefabs[index], spawnPos, Quaternion.identity, tilesParent);
                }
                else
                {
                    int index = Random.Range(0, 3);
                    Instantiate(groundPrefabs[index], spawnPos, Quaternion.identity, tilesParent);
                }
            }
        }
    }
}