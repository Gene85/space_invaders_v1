using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    public GameObject _Enemy;

    [SerializeField]
    public GameObject _enemyContainer;

    [SerializeField]
    private GameObject _tripleShotPowerupPrefab;

    private bool _stopSpawning = false;


    void Start()
    {
        //Coroutine coroutine = StartCoroutine(SpawnEnemyRoutine());
        //StopCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }
    void Update()
    {

    }
    IEnumerator SpawnEnemyRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_Enemy, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }

    }
    IEnumerator SpawnPowerupRoutine()
    {
        while (_stopSpawning == false) 
        {
            //every 3 -7 seconds, spawn in powerup
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newPowerUp = Instantiate(_tripleShotPowerupPrefab, posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }
    public void OnplayerDeath()
    {
        _stopSpawning = true;
    }
    
    public void OnPowerUpCollected()
    {
        _stopSpawning = true;
    }
}

