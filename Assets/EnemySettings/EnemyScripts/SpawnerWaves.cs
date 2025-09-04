using UnityEngine;
using System.Collections;

public class SpawnerWaves : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject Ghool;
    public GameObject Golem;
    public GameObject OrkBerserk;
    public GameObject Necromancer;
    public GameObject Skeleton;
    public GameObject Skeleton_green;
    public GameObject Skeleton_blue;
    public GameObject Dragon;

    [SerializeField] private Transform spawnPoint;

    public IEnumerator SpawnEnemies(int enemyCount, float spawnInterval, GameObject enemyPrefab)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            
            WaveManager.aliveEnemies++;
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}