using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{

    public List<GameObject> enemyList;
    
    public GameObject powerupPrefab;

    private float spawnRange = 9;

    public int enemyCount; //Cuenta la cant de enemys
    public int waveNumber; //Cuenta la cant de waves

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;

    public bool isGameActive;

    private int score;

    //private float spawnRate = 1.0f; // Tiempo en el que sea spawnean los enemys

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        SpawnpowerupWave(waveNumber);

        score = 0;
        UpdateScore(0);

        isGameActive = true;
        
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
            if (isGameActive)
            {
                int index = Random.Range(0, enemyList.Count); // Recorre toda la lista de enemys
                Instantiate(enemyList[index], GenerateSpawnPosition(), enemyList[index].transform.rotation);
            }
            
        }
    }

    void SpawnpowerupWave(int powerupToSpawn) //Mothod que spawnea powerups
    {
        for(int i = 0; i < powerupToSpawn; i++)
        {
            if (isGameActive)
            {
                Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length; // Busca los enemys y devuelve la cant. (int)

        if(enemyCount == 0 && isGameActive) // si la cant de enemys es 0 y isGameActive verdadero
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
        int index = Random.Range(0, enemyList.Count);
        if (enemyList[index].transform.position.y < -8)
        {
            score++; //updatea 1 punto si enemyPrefab cae de la platform
            
        }
        
    }
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
