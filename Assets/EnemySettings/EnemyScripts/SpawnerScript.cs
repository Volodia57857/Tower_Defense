using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;  // Префаб ворога
    [SerializeField]
    private Transform spawnPoint;    // Точка спавну
    [SerializeField]
    private float spawnInterval = 2f; // Інтервал між спавнами (секунди)
    [SerializeField]
    private int enemyCount = 5;       // Скільки ворогів створити

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
