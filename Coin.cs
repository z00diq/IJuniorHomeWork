using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            Destroy(gameObject);
        }
    }
}
