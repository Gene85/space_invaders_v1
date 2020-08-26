using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    public GameObject _Enemy;
    
    [SerializeField]
    private GameObject _enemyContainer;

    private bool _stopSpawning = false;


    void Start()
    {
        StartCoroutine(SpawnRoutine());
        StopCoroutine(SpawnRoutine());
    }
    void Update()
    {

    }
    IEnumerator SpawnRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_Enemy, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
        
        void onPlayerDeath()
        {
            _stopSpawning = true;
        }
    }
}

