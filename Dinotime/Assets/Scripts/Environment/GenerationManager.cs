using UnityEngine;

public class GenerationManager : MonoBehaviour
{
    [Header("Generation Bounds")]
    public Vector2Int generationBoundSize;
    public Vector2 offset;

    [Header("Generation Settings")]
    public int chunkSize;
    public int fossilFreq; // Per chunkSize blocks, how many are fossils?

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateTiles();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        int x1 = -generationBoundSize.x / 2;
        int x2 = generationBoundSize.x / 2;
        for (int i = x1; i < x2; i++)
        {
            int y1 = generationBoundSize.y / 2;
            int y2 = -generationBoundSize.y / 2;

            for (int j = y1; j >= y2 + 1; j--)
            {
                Gizmos.DrawWireCube(new Vector2(i, j) + offset, new Vector3(1, 1, 1));
            }
        }
    }

    private void GenerateTiles()
    {
        LinearGenerate();
    }

    private void LinearGenerate()
    {
        int x1 = -generationBoundSize.x / 2;
        int x2 = generationBoundSize.x / 2;

        for (int i = x1; i < x2; i++)
        {
            int y1 = generationBoundSize.y / 2;
            int y2 = -generationBoundSize.y / 2;
            for (int j = y1; j >= y2 + 1; j--)
            {
                float isFossil = Random.value;
                Transform tiles = GameObject.Find("UndergroundTiles").transform;
    
                if (isFossil < (float)fossilFreq / chunkSize)
                {
                    GameObject fossil = Resources.Load<GameObject>("Prefabs/Environment/Underground/Fossil");

                    Instantiate(fossil, new Vector2(i, j), Quaternion.identity, tiles);
                }
                else
                {
                    GameObject ground = Resources.Load<GameObject>("Prefabs/Environment/Underground/Ground");

                    Instantiate(ground, new Vector2(i, j), Quaternion.identity, tiles);
                }
            }
        }
    }
}
