using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    [SerializeField] private Transform _left;
    [SerializeField] private Transform _right;
    [SerializeField] private int _coinCount;
    [SerializeField] private Coin _coinPrefab;

    private void Start()
    {
        for (int i = 0; i < _coinCount; i++)
        {
            Coin _currentCoin = Instantiate(_coinPrefab);
            _currentCoin.transform.position = new Vector2(Random.Range(_left.position.x, _right.position.x), transform.position.y);
        } 
    }
}
