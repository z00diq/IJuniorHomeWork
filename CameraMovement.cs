using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject _target;

    private Vector3 _offset;

    private void Start()
    {
        _offset = transform.position - _target.transform.position;
    }

    private void Update()
    {
        transform.position = _target.transform.position + _offset;
    }
}
