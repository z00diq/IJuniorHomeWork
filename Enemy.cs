using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Camera _camera;

    private void Update()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, _camera.gameObject.transform.position, _speed * Time.deltaTime);
        
        if(gameObject.transform.position == _camera.gameObject.transform.position)
        {
            Destroy(gameObject);
        }
    }

    public void Init(Camera camera)
    {
        _camera = camera;
    }
}
