using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class ForceField : MonoBehaviour
{
    public float attractionRadius = 10f;
    public float attractionForce = 100f;
    public Rigidbody2D playerRb;
    public Animator fieldAnimator;
    public LayerMask ignoredLayers;
    [Header("Sounds")]
    public EventReference m_shieldSoundEvent;
    EventInstance m_shieldSoundInstance;

    private void FixedUpdate()
    {
        // �������� ���������� � ������� ����������
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attractionRadius);

        // ������������ ������ ���������
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                // ���������, ���� �� � ���������� Rigidbody2D
                Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
                if (rb != null && rb != playerRb)
                {
                    // ��������� ����������� ����������
                    Vector2 direction = (Vector2)transform.position - rb.position;

                    // ��������� ���� ���������� � Rigidbody2D
                    rb.AddForce(direction.normalized * attractionForce * Time.fixedDeltaTime, ForceMode2D.Force);
                }
            }
        }
    }
    
    private void SoundPlay()
    {
        m_shieldSoundInstance = RuntimeManager.CreateInstance(m_shieldSoundEvent);
        m_shieldSoundInstance.start();
        m_shieldSoundInstance.release();
    }
    
    private void SoundStop()
    {
        m_shieldSoundInstance.stop(STOP_MODE.ALLOWFADEOUT);
        m_shieldSoundInstance.release();
    }

    public void ResetField()
    {
        fieldAnimator.SetBool("Activate", false);
        SoundStop();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            fieldAnimator.SetBool("Activate", true);
            SoundPlay();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            fieldAnimator.SetBool("Activate", false);
            SoundStop();
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
