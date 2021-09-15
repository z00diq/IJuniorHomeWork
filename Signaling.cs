using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Signaling : MonoBehaviour
{
    [SerializeField] private Move _thief;
    [SerializeField] private float _signalingOffDistance;
    [SerializeField] private LayerMask _filterMask;
    [SerializeField] private float volumeStep;

    private AudioSource audioSource;
    private Coroutine coroutine = null;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, _filterMask.value);
       
        if (hit)
        {
            if(coroutine == null)
            {
                coroutine = StartCoroutine(DoSignaling());
            }
        }
            
    }

    private IEnumerator DoSignaling()
    {
        audioSource.volume = 0;
        audioSource.Play();

        while (audioSource.isPlaying)
        {
            if (audioSource.volume == 1)
                volumeStep = -volumeStep;
            else if (audioSource.volume == 0)
                volumeStep = Mathf.Abs(volumeStep);

            audioSource.volume += (volumeStep * Time.deltaTime)/*(Mathf.Abs(transform.position.x - _thief.transform.position.x))*/;

            if (Mathf.Abs(transform.position.x - _thief.transform.position.x) > _signalingOffDistance)
            {
                audioSource.Stop();
                coroutine = null;
                yield break;
            }

            yield return new WaitForSeconds(0.5f);
        }  
    }

}
