using UnityEngine;
using System.Collections;

public class SpawnerWaves : MonoBehaviour
{
    [SerializeField] private GameObject Ghool;
    [SerializeField] private GameObject Golem;
    [SerializeField] private GameObject OrkBerserk;
    [SerializeField] private GameObject Dragon;

    [SerializeField] private Transform spawnPoint; // місце спавну ворогів

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        
        yield return new WaitForSeconds(3f);
        
        yield return StartCoroutine(SpawnEnemies(5, 2f, Ghool));
        
        yield return new WaitForSeconds(5f);

        yield return StartCoroutine(SpawnEnemies(3, 3f, OrkBerserk));

        yield return new WaitForSeconds(8f);

        yield return StartCoroutine(SpawnEnemies(1, 0f, Golem));

        yield return new WaitForSeconds(10f);

        yield return StartCoroutine(SpawnEnemies(1, 0f, Dragon));
    }

    IEnumerator SpawnEnemies(int enemyCount, float spawnInterval, GameObject enemyPrefab)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            if (spawnInterval > 0)
                yield return new WaitForSeconds(spawnInterval);
        }
    }
}