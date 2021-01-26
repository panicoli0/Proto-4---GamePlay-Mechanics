using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    private float spawnRange = 9;

    public int enemyCount; // var que cuenta la cant de enemys
    public int waveNumber = 1; //var que cuenta la cant de waves

    public int powerupCount;
    

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        SpawnpowerupWave(waveNumber);
        
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;
    }

    void SpawnEnemyWave(int enemyToSpawn) //Method que spwnea enemys
    {
        for(int i = 0; i < enemyToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }
    void SpawnpowerupWave(int powerupToSpawn) //Mothod que spawnea powerups
    {
        for(int i = 0; i < powerupToSpawn; i++)
        {
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }
    }
    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length; // Busca los enemys y devuelve la cant. (int)

        if(enemyCount == 0) // si la cant de enemys es 0
        {
            waveNumber++; // sumale a waveNumber
            SpawnEnemyWave(waveNumber);
            SpawnpowerupWave(waveNumber - 1);
        }
       
    }
}
