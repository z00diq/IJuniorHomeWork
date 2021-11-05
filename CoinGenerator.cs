using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    [SerializeField] private Transform _left;
    [SerializeField] private Transform _right;
    [SerializeField] private int _count;
    [SerializeField] private Coin _prefab;

    private void Start()
    {
        for (int i = 0; i < _count; i++)
        {
            Coin _currentCoin = Instantiate(_prefab);
            _currentCoin.transform.position = new Vector2(Random.Range(_left.position.x, _right.position.x), transform.position.y);
        } 
    }
}
