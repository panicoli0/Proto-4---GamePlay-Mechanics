﻿using System.Collections;
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
    private float spawnRate = 10.0f; // Tiempo en el que sea spawnean los enemys

    public int enemyCount; //Cuenta la cant de enemys
    public int waveNumber; //Cuenta la cant de waves

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;

    public bool isGameActive;

    private int score;


    // Start is called before the first frame update
    void Start()
    {

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

        for (int i = 0; i < enemyToSpawn; i++)
        {
            if (isGameActive)
            {
                int index = Random.Range(0, enemyList.Count); // Recorre toda la lista de enemys
                Instantiate(enemyList[index], GenerateSpawnPosition(), enemyList[index].transform.rotation); //Agarra un ebjeto enemy, le da una pos random
            }

        }
    }

    IEnumerator SpawnTarget() //Spawnea enemys por tiempo segun difficulty elegida
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, enemyList.Count);
            Instantiate(enemyList[index]);
        }
    }

    void SpawnpowerupWave(int powerupToSpawn) //Mothod que spawnea powerups
    {
        for (int i = 0; i < powerupToSpawn; i++)
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

        if (enemyCount == 0 && isGameActive) // si la cant de enemys es 0 y isGameActive verdadero
        {
            waveNumber++; // sumale a waveNumber
            SpawnEnemyWave(waveNumber);
            StartCoroutine(SpawnTarget());
            SpawnpowerupWave(waveNumber - 1);
        }
        waveText.text = "Wave Number: " + waveNumber;

    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Points: " + score;
        int index = Random.Range(0, enemyList.Count);
        if (enemyList[index].transform.position.y < -8)
        {
            score++; //updatea 1 punto si enemyPrefab cae de la platform

        }

    }
    public void GameOver() // Para el juego
    {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        restartButton.gameObject.SetActive(true);

    }

    public void RestartGame() // Resetea el juego
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty) // inicia el game
    {
        spawnRate /= difficulty;
        //Debug.Log("La dificultad selecionada fue: " + difficulty);

        SpawnEnemyWave(waveNumber);
        SpawnpowerupWave(waveNumber);
        StartCoroutine(SpawnTarget());

        score = 0;
        UpdateScore(0);

        isGameActive = true;
        titleScreen.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        waveText.gameObject.SetActive(true);

    }
}