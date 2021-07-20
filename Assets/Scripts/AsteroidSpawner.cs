using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] asteroidPrefabs;
    [SerializeField] private float secondsBetweenAsteroids = 1.5f; // the seconds between the asteroids spawn to the screen 
    [SerializeField] private Vector2 forceRange; // the force we add to the asteroids when spawn to screen

    private Camera mainCamera;
    private float timer; // when reach to 0, spawn a new asteroid

    private void Start()
    {
        mainCamera = Camera.main;    
    }


    void Update()
    {
        timer -= Time.deltaTime; // recuing the timer by the seconds pat from the last frame

        if (timer <= 0)
        {
            SpawnAsteroid();
            timer += secondsBetweenAsteroids; // resetting the timer
        }
        
    }

    private void SpawnAsteroid()
    {
        int side = Random.Range(0, 4); //  randomlly choosing the screen's side to spawn a new asteroid 

        Vector2 spawnPoint = Vector2.zero;
        Vector2 spawnDirection = Vector2.zero;

        switch(side)
        {
            case 0: // spawn at the left side of the screen
                spawnPoint.x = 0;
                spawnPoint.y = Random.value;
                spawnDirection = new Vector2(1f, Random.Range(-1f, 1f));
                break;
            case 1: // spawn at the righte of the screen
                spawnPoint.x = 1;
                spawnPoint.y = Random.value;
                spawnDirection = new Vector2(-1f, Random.Range(-1f, 1f));
                break;
            case 2: // spawn a the bottom of the screen
                spawnPoint.x = Random.value; 
                spawnPoint.y = 0;
                spawnDirection = new Vector2(Random.Range(-1f, 1f), 1f);
                break;
            case 3: // spawn a the top of the screen
                spawnPoint.x = Random.value; 
                spawnPoint.y = 1;
                spawnDirection = new Vector2(Random.Range(-1f, 1f), -1f);
                break;
        }
        Vector3 worldSpawnPoint = mainCamera.ViewportToWorldPoint(spawnPoint); // converting the viewport units to world's units , to get the point where the asteroid will be spawnd
        worldSpawnPoint.z = 0;

        // randomlly instantiating an asteriod from the asteroids arrey
        GameObject selectedSteroid = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)]; 
        GameObject asteroidInstance =  Instantiate(
            selectedSteroid,
            worldSpawnPoint,
            Quaternion.Euler(0, 0, Random.Range(0f, 360f)));

        Rigidbody rigidBody = asteroidInstance.GetComponent<Rigidbody>();

        rigidBody.velocity = spawnDirection.normalized * Random.Range(forceRange.x, forceRange.y);
    }
}
