using UnityEngine;
using System.Collections;

public class SpawnerWaves : MonoBehaviour
{
    [SerializeField] private GameObject Ghool;
    [SerializeField] private GameObject Golem;
    [SerializeField] private GameObject OrkBerserk;
    [SerializeField] private GameObject Necromancer;
    [SerializeField] private GameObject Skeleton;
    [SerializeField] private GameObject Dragon;

    [SerializeField] private Transform spawnPoint; // місце спавну ворогів

    void Start()
    {
        StartCoroutine(Level2Waves());
    }

    IEnumerator Level1Waves()
    {
        
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(SpawnEnemies(5, 1.8f, Ghool));
        yield return new WaitForSeconds(18f);
        yield return StartCoroutine(SpawnEnemies(10, 1.8f, Ghool));
        yield return StartCoroutine(SpawnEnemies(3, 1f, Skeleton));
        yield return new WaitForSeconds(23f);
        yield return StartCoroutine(SpawnEnemies(4, 1f, Ghool));
        yield return StartCoroutine(SpawnEnemies(1, 0.5f, Golem));
        yield return StartCoroutine(SpawnEnemies(3, 1f, Ghool));
        yield return new WaitForSeconds(23f);
        yield return StartCoroutine(SpawnEnemies(5, 1f, Ghool));
        yield return StartCoroutine(SpawnEnemies(2, 0.5f, Golem));
        yield return StartCoroutine(SpawnEnemies(5, 1f, Ghool));
        yield return StartCoroutine(SpawnEnemies(2, 0.5f, Golem));
        yield return new WaitForSeconds(25f);
        yield return StartCoroutine(SpawnEnemies(4, 1f, Skeleton));
        yield return StartCoroutine(SpawnEnemies(1, 0.5f, OrkBerserk));
        yield return StartCoroutine(SpawnEnemies(3, 1f, Skeleton));

    }

    IEnumerator Level2Waves()
    {
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(SpawnEnemies(10, 1.8f, Ghool));
        yield return new WaitForSeconds(18f);
        yield return StartCoroutine(SpawnEnemies(6, 1.4f, Ghool));
        yield return StartCoroutine(SpawnEnemies(6, 1.4f, Skeleton));
        yield return new WaitForSeconds(18f);
        yield return StartCoroutine(SpawnEnemies(3, 1.5f, Skeleton));
        yield return StartCoroutine(SpawnEnemies(1, 2.5f, Golem));
        yield return StartCoroutine(SpawnEnemies(1, 1.5f, Necromancer));
        yield return new WaitForSeconds(24f);
        yield return StartCoroutine(SpawnEnemies(5, 1.3f, Skeleton));
        yield return StartCoroutine(SpawnEnemies(3, 2.5f, Necromancer));
        yield return new WaitForSeconds(25f);
        yield return StartCoroutine(SpawnEnemies(5, 1.3f, Skeleton));
        yield return StartCoroutine(SpawnEnemies(3, 2.5f, Necromancer));
        yield return StartCoroutine(SpawnEnemies(4, 1.4f, Ghool));
        yield return new WaitForSeconds(26f);
        yield return StartCoroutine(SpawnEnemies(5, 1.3f, Ghool));
        yield return StartCoroutine(SpawnEnemies(2, 2.5f, Necromancer));
        yield return StartCoroutine(SpawnEnemies(1, 2.5f, Golem));
        yield return StartCoroutine(SpawnEnemies(1, 2.5f, OrkBerserk));

    }
    IEnumerator Level3Waves()
    {
        yield return new WaitForSeconds(1f);

    }
    IEnumerator Level4Waves()
    {
        yield return new WaitForSeconds(1f);

    }
    IEnumerator Level5Waves()
    {
        yield return new WaitForSeconds(1f);

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