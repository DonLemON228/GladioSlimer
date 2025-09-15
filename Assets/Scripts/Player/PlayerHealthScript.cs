using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using FMODUnity;

public class PlayerHealthScript : MonoBehaviour
{
    [SerializeField] private bool m_isFirstPlayer;
    [SerializeField] private PlayersController m_playersController;
    [SerializeField] private ForceField m_forceFieldAnim;
    [SerializeField] private CircleCollider2D m_playerColider;
    [SerializeField] private Animator m_forcePhield;
    [SerializeField] private Animator m_playerAnim;
    [SerializeField] private Animator m_explosionAnim;
    [SerializeField] private CallCameraShake m_callCameraShake;
    [Header("Sounds")]
    public EventReference m_playerDeathSoundEvent;
    public EventReference m_yappieSoundEvent;
    EventInstance m_playerDeathSoundInstance;
    EventInstance m_yappieSoundInstance;
    public event Action PlayerDead;
    public bool m_isDead;
    public bool m_canDamage;
    
    // Start is called before the first frame update
    void Start()
    {
        m_canDamage = true;
    }

    public void PlayerDeath()
    {
        if (!m_isDead && m_canDamage)
        {
            m_isDead = true;
            PlayerDead?.Invoke();
            PlayerDeathSound();
            m_forceFieldAnim.ResetField();
            m_playerAnim.SetTrigger("Death");
            m_callCameraShake.CallShake();
            m_playerColider.enabled = false;
            m_forcePhield.SetBool("Activate", false);
            if (m_isFirstPlayer)
                m_playersController.DisactivatePlayer(1);
            else
                m_playersController.DisactivatePlayer(2);
        }
    }

    public void RespawnPlayer()
    {
        m_isDead = false;
        m_playerAnim.SetTrigger("Respawn");
        m_forceFieldAnim.ResetField();
        m_playerAnim.SetBool("isWalking", false);
        m_playerColider.enabled = true;
    }

    public void PlayerLose()
    {
        StartCoroutine(PlayerLoseAnim());
    }
    
    public void PlayerWin()
    {
        StartCoroutine(PlayerWinsAnim());
    }

    IEnumerator PlayerLoseAnim()
    {
        m_explosionAnim.SetTrigger("Explosion");
        yield return new WaitForSeconds(1);
        m_playerAnim.SetTrigger("Lose");
    }
    
    IEnumerator PlayerWinsAnim()
    {
        yield return new WaitForSeconds(1);
        m_playerAnim.SetTrigger("Win");
        YappiePlay();
    }
    
    void PlayerDeathSound()
    {
        m_playerDeathSoundInstance = RuntimeManager.CreateInstance(m_playerDeathSoundEvent);
        m_playerDeathSoundInstance.start();
        m_playerDeathSoundInstance.release();
    }
    
    private void YappiePlay()
    {
        m_yappieSoundInstance = RuntimeManager.CreateInstance(m_yappieSoundEvent);
        m_yappieSoundInstance.start();
        m_yappieSoundInstance.release();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
