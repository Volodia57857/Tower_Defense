using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;  
    [SerializeField]
    private Transform spawnPoint;    
    [SerializeField]
    private float spawnInterval = 2f; 
    [SerializeField]
    private int enemyCount = 5;       

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
