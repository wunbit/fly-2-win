using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnPool : MonoBehaviour
{
    public int columnPoolSize = 5;
    public GameObject columnPrefab;
    private GameControl GCInstance;
    public float spawnRate = 4f;
    public float columnYMin = -1f;
    public float columnYMax = 3.5f;
    private GameObject[] columns;
    public Vector2 objectPoolPosition = new Vector2 (-15f, -25f);
    public float timeSinceLastSpawned;
    public float spawnXPosition = 10f;
    private int currentColumn = 0;
    // Start is called before the first frame update
    void Awake()
    {
        GCInstance = GameObject.Find("GameControl").GetComponent<GameControl>();
    }
    void Start()
    {
        columns = new GameObject[columnPoolSize];
        for (int i = 0; i < columnPoolSize; i++)
        {
            columns[i] = (GameObject) Instantiate(columnPrefab, objectPoolPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawned += Time.deltaTime;

        if (GCInstance.gameOver == false && timeSinceLastSpawned >= spawnRate)
        {
            timeSinceLastSpawned = 0;
            float spawnYPosition = Random.Range (columnYMin, columnYMax);
            columns[currentColumn].transform.position = new Vector2 (spawnXPosition, spawnYPosition);
            currentColumn++;
            if (currentColumn >= columnPoolSize)
            {
                currentColumn = 0;
            }
        }
    }
}
