using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;

    void Start()
    {
      
    }


    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.GetComponent<Collider2D>().IsTouchingLayers(_layerMask))
        {
            Destroy(gameObject);
        }
    }
}
