using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemyAI enemy))
        {
            transform.position = _spawnPoint.position;
        }
    }

}
