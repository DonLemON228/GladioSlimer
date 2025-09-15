using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using Unity.VisualScripting;
using UnityEngine;

public class LandMineScript : MonoBehaviour
{
    [SerializeField] private GameObject m_explosionEffect;
    [SerializeField] private float m_timeBeforeExplosion;
    [SerializeField] private CallCameraShake m_callCameraShake;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LandmineExplosion(m_timeBeforeExplosion));
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(LandmineExplosion(0));
        }
    }

    IEnumerator LandmineExplosion(float _timeBeforeExplosion)
    {
        yield return new WaitForSeconds(_timeBeforeExplosion);
        Instantiate(m_explosionEffect, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0f);
        m_callCameraShake.CallShake();
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
