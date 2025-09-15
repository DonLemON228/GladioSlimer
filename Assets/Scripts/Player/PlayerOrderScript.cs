using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using FMODUnity;

public class PlayerOrderScript : MonoBehaviour
{
    [SerializeField] CameraManager m_cameraManager;
    [SerializeField] PlayersController m_playerController;
    [SerializeField] BulletsRunScript m_bulletSpawnerScript;
    [SerializeField] Animator m_wheelAnim;
    [SerializeField] Animator m_deadZoneAnim;
    [Header ("Timer")]
    [SerializeField] float m_playersTime;
    [SerializeField] float m_cooldownBetwenSwaps;
    [SerializeField] TextMeshProUGUI m_timerText;
    [SerializeField] Animator m_cooldownSwapAnim;
    [Header("Sounds")]
    public EventReference m_startRoundSoundEvent;
    public EventReference m_wheelRotationSoundEvent;
    EventInstance m_wheelRotationSoundInstance;
    EventInstance m_startRoundSoundInstance;
    private int m_playerIndex = 0;
    private float timeRemaining = 0f;
    private bool m_isFirstPlayer = false;
    private bool m_canStartTimer = false;
    // Start is called before the first frame update
    void Start()
    {
        ChoosePlayer();
        timeRemaining = m_playersTime;
    }

    public void ChoosePlayer()
    {
        m_wheelAnim.SetTrigger("WheelAppear");
        StartRoundSound();
    }

    public void PlayerChooseAnim()
    {
        m_playerIndex = Random.Range(1, 3);
        if(m_playerIndex == 1)
        {
            m_wheelAnim.SetTrigger("Player1Choose");
        }
        else
        {
            m_wheelAnim.SetTrigger("Player2Choose");
        }

        m_deadZoneAnim.speed = 1;
        m_deadZoneAnim.SetTrigger("Start");
    }

    public void ActivatePlayer()
    {
        if(m_playerIndex == 1)
            m_isFirstPlayer = false;
        else if(m_playerIndex == 2)
            m_isFirstPlayer = true;

        m_playerIndex = 0;
        ChangeCameraTarget(!m_isFirstPlayer);
        StartTimer();
    }

    void InversePlayerChange(bool _isFirstPlayer)
    {
        if (_isFirstPlayer)
        {
            m_playerController.ActivatePlayer(1);
        }
        else
        {
            m_playerController.ActivatePlayer(2);
        }
    }

    void ChangeCameraTarget(bool _isFirstPlayer)
    {
        if (_isFirstPlayer)
        {
            m_cameraManager.SwitchCamera(m_cameraManager.m_player1Camera);
        }
        else
        {
            m_cameraManager.SwitchCamera(m_cameraManager.m_player2Camera);
        }
    }

    void InverseDisactivatePlayer(bool _isFirstPlayer)
    {
        if (_isFirstPlayer)
        {
            m_playerController.DisactivatePlayer(1);
        }
        else
        {
            m_playerController.DisactivatePlayer(2);
        }
    }

    public void StartTimer()
    {
        m_isFirstPlayer = !m_isFirstPlayer;
        InversePlayerChange(m_isFirstPlayer);
        m_bulletSpawnerScript.SpawnAllBullets();
        timeRemaining = m_playersTime;
        m_canStartTimer = true;
    }

    public void StopTimer()
    {
        timeRemaining = 0;
        m_timerText.text = timeRemaining.ToString("F2");
        m_canStartTimer = false;
    }

    IEnumerator CooldownBetweenSwaps()
    {
        m_canStartTimer = false;
        m_deadZoneAnim.speed = 0;
        ChangeCameraTarget(!m_isFirstPlayer);
        InverseDisactivatePlayer(m_isFirstPlayer);
        m_cooldownSwapAnim.SetTrigger("ActivateCooldown");
        yield return new WaitForSeconds(m_cooldownBetwenSwaps);
        m_deadZoneAnim.speed = 1;
        m_deadZoneAnim.SetTrigger("Start");
        StartTimer();
    }
    
    void WheelRotationSound()
    {
        m_wheelRotationSoundInstance = RuntimeManager.CreateInstance(m_wheelRotationSoundEvent);
        m_wheelRotationSoundInstance.start();
        m_wheelRotationSoundInstance.release();
    }
    
    void StartRoundSound()
    {
        m_startRoundSoundInstance = RuntimeManager.CreateInstance(m_startRoundSoundEvent);
        m_startRoundSoundInstance.start();
        m_startRoundSoundInstance.release();
    }

    void Update()
    {
        if (m_canStartTimer)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                m_timerText.text = timeRemaining.ToString("F2");
            }
            else
            {
                m_timerText.text = "0.00";
                StartCoroutine(CooldownBetweenSwaps());
            }
        }
    }
}
