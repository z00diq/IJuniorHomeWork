using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] private float _speed;

    private Transform[] _points;
    private int _currrentPoint;
    private void Start()
    {
        _points = new Transform[_path.childCount];

        for(int i=0; i < _path.childCount; i++)
        {
            _points[i] = _path.GetChild(i);
        }
    }

    private void Update()
    {
        Transform target = _points[_currrentPoint];
        transform.position = Vector3.MoveTowards(transform.position, target.position,_speed*Time.deltaTime);
        if(transform.position == target.position)
        {
            _currrentPoint++;

            if (_currrentPoint >= _points.Length)
            {
                _currrentPoint = 0;
            }
        }
    }
}
