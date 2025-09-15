using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using FMODUnity;

public class BulletSpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject m_projectileObject;
    [SerializeField] private float m_projectileSpeed;
    [Header("Sounds")]
    public EventReference m_shootSoundEvent;
    EventInstance m_shootDeathSoundInstance;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SpawnBullet()
    {
        ShootSound();
        GameObject bullet = Instantiate(m_projectileObject, transform.position, Quaternion.identity);
        
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        
        Vector2 direction = transform.right;
                
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                
        rb.velocity = direction * m_projectileSpeed;
        Destroy(gameObject);
    }
    
    void ShootSound()
    {
        m_shootDeathSoundInstance = RuntimeManager.CreateInstance(m_shootSoundEvent);
        m_shootDeathSoundInstance.start();
        m_shootDeathSoundInstance.release();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
