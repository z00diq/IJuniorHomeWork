using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private  Transform _spawns;
    [SerializeField] private Enemy _enemy;

    private List<Transform> _spawnsPoints;

    private void Awake()
    {
        _spawnsPoints = new List<Transform>();

        foreach (Transform spawnpoint in _spawns)
        {
            _spawnsPoints.Add(spawnpoint);
        }

        StartCoroutine(SpawnEnemy());
    }
   
    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Transform _currentSpawn = _spawnsPoints[Random.Range(0, _spawnsPoints.Count)];
            Debug.Log(_currentSpawn);
            Enemy _spawnedEnemy = Instantiate(_enemy);
            _spawnedEnemy.Init(Camera.main);
            _spawnedEnemy.gameObject.transform.position = _currentSpawn.position;
            yield return new WaitForSeconds(2f);
        } 
    }
}
