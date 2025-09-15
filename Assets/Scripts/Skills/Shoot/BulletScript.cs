using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_rb;
    [SerializeField] private Animator m_bulletAnim;
    [SerializeField] private CircleCollider2D m_colider;
    [SerializeField] private float m_timeBeforeDestroy;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BulletTimerDestroy());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealthScript>().PlayerDeath();
            DestroyBulletAnimPlay();
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
    
    public void DestroyBulletAnimPlay()
    {
        m_colider.enabled = false;
        m_rb.bodyType = RigidbodyType2D.Static;
        m_bulletAnim.SetTrigger("Impact");
    }

    IEnumerator BulletTimerDestroy()
    {
        yield return new WaitForSeconds(m_timeBeforeDestroy);
        m_bulletAnim.SetTrigger("Impact");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
