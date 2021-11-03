using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform _spawnPoint;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.GetComponent<Collider2D>().IsTouchingLayers(_layerMask))
        {
            transform.position = _spawnPoint.position;
        }
    }

}
