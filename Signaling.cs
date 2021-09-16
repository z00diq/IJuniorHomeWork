using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]

public class Signaling : MonoBehaviour
{
    [SerializeField] private Mover _thief;
    [SerializeField] private float _signalingOffDistance;
    [SerializeField] private LayerMask _filterMask;
    [SerializeField] private float _volumeStep;

    private AudioSource _audioSource;
    private Coroutine _coroutine = null;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, _filterMask.value);
       
        if (hit)
        {
            if(_coroutine == null)
            {
                _coroutine = StartCoroutine(DoSignaling());
            }
        }
            
    }

    private IEnumerator DoSignaling()
    {
        _audioSource.volume = 0;
        _audioSource.Play();
        float _distance;
        while (_audioSource.isPlaying)
        {
            _distance = Vector2.MoveTowards(_thief.gameObject.transform.position, _audioSource.gameObject.transform.position, 0.1f).x;
            _audioSource.volume += 1/-_distance*_volumeStep;

            if (Mathf.Abs(transform.position.x - _thief.transform.position.x) > _signalingOffDistance)
            {
                _audioSource.Stop();
                _coroutine = null;
                yield break;
            }

            yield return new WaitForSeconds(0.5f);
        }  
    }
}
