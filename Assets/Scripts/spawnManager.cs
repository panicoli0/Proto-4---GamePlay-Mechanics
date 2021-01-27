using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public List<GameObject> enemyList;
    
    public GameObject powerupPrefab;

    private float spawnRange = 9;

    public int enemyCount; // var que cuenta la cant de enemys
    public int waveNumber; //var que cuenta la cant de waves

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI waveText;

    private int score;

    //private float spawnRate = 1.0f; // Tiempo en el que sea spawnean los enemys

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        SpawnpowerupWave(waveNumber);
        //StartCoroutine(SpawnEnemys());

        score = 0;
        UpdateScore(0);
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
           
            int index = Random.Range(0, enemyList.Count); // Recorre toda la lista de enemys
            Instantiate(enemyList[index], GenerateSpawnPosition(), enemyList[index].transform.rotation);
        }
    }

    void SpawnpowerupWave(int powerupToSpawn) //Mothod que spawnea powerups
    {
        for(int i = 0; i < powerupToSpawn; i++)
        {
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }
    }

    //IEnumerator SpawnEnemys() //Spawnea enemys de enemyList por seg
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(spawnRate);
    //        int index = Random.Range(0, enemyList.Count);
    //        Instantiate(enemyList[index]);
    //    }
    //}

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
        //Debug.Log("Wave Number: " + waveNumber);
        waveText.text = "Wave Number: " + waveNumber;
        
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Enemys Destroyed: " + score;
        if (enemyPrefab.transform.position.y < -8)
        {
            score++; //updatea 1 punto si enemyPrefab cae de la platform
            
        }
        
    }
}
