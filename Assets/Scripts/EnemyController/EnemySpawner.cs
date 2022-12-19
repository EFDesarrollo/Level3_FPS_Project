using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int maxInGame = 50;
    [SerializeField] private string prefabTag = "Enemy";
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private float timeBetweenSpawn;
    [SerializeField] private GameObject gemParticle, gemParticleParent;
    private float Timer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag(prefabTag).Length >= maxInGame)
            return;
        if (Time.time >= Timer)
        {
            Spawn();
            Timer = Time.time + timeBetweenSpawn;
        }
    }
    void Spawn()
    {
        int enemyID = Random.Range(0, prefabs.Length);
        int spawnID = Random.Range(0, spawnPositions.Length);

        //Debug.Log("Spawn" + SpawnPositions[spawnID].position);
        GameObject obj = Instantiate(prefabs[enemyID], spawnPositions[spawnID].position, Quaternion.identity);
    }
}
