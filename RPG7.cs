using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPG7 : Weapon
{
    [SerializeField] private float _realoadTime;

    private float _delay=0;

    private void Update()
    {
        _delay -= Time.deltaTime;
    }

    public override void Shoot(Transform shootPoint)
    {
        if (_delay <= 0)
        {
            var bullet = Instantiate(Bullet, shootPoint.position, shootPoint.transform.rotation);
            _delay = _realoadTime;
        }
    }


}
