using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using TMPro;

public class PointCountingScript : MonoBehaviour
{
    [SerializeField] private MenuScript m_menuScript;
    [SerializeField] private GameObject m_winButtons;
    [SerializeField] private Animator m_deadZoneAnim;
    [SerializeField] private RandomSkillsActivatorScript m_randomSkillsActivatorScript;
    [SerializeField] private CameraManager m_cameraManager;
    [SerializeField] private PlayerOrderScript m_playerOrderScript;
    [SerializeField] private PlayersController m_playersController;
    [SerializeField] private PlayerHealthScript m_playerHealthScript1;
    [SerializeField] private PlayerHealthScript m_playerHealthScript2;
    [SerializeField] private Animator m_titlesAnim;
    [SerializeField] private TextMeshProUGUI m_player1CountText;
    [SerializeField] private TextMeshProUGUI m_player2CountText;
    [SerializeField] private float m_timeBeforeShowText;
    [SerializeField] private float m_timeBeforePlayersTP;
    [SerializeField] private float m_timeBeforeChoosePlayers;
    [SerializeField] private float m_timeBeforeShowWiner;
    
    [Header("Sounds")]
    public EventReference m_blueSlimedSoundEvent;
    public EventReference m_redSlimedSoundEvent;
    public EventReference m_blueWinsSoundEvent;
    public EventReference m_redWinsSoundEvent;
    EventInstance m_blueSlimedSoundInstance;
    EventInstance m_redSlimedSlimedSoundInstance;
    EventInstance m_blueWinsSlimedSoundInstance;
    EventInstance m_redWinsSlimedSoundInstance;
    
    private int m_player1Count;
    private int m_player2Count;
    private bool m_stopCounting;
    // Start is called before the first frame update
    void Start()
    {
        m_playerHealthScript1.PlayerDead += StartResetRountPlayer1;
        m_playerHealthScript2.PlayerDead += StartResetRountPlayer2;
    }
    
    private void BlueBlobSLimedPlay()
    {
        m_blueSlimedSoundInstance = RuntimeManager.CreateInstance(m_blueSlimedSoundEvent);
        m_blueSlimedSoundInstance.start();
        m_blueSlimedSoundInstance.release();
    }
    
    private void RedBlobSLimedPlay()
    {
        m_redSlimedSlimedSoundInstance = RuntimeManager.CreateInstance(m_redSlimedSoundEvent);
        m_redSlimedSlimedSoundInstance.start();
        m_redSlimedSlimedSoundInstance.release();
    }
    
    private void BlueBlobWinsPlay()
    {
        m_blueWinsSlimedSoundInstance = RuntimeManager.CreateInstance(m_blueWinsSoundEvent);
        m_blueWinsSlimedSoundInstance.start();
        m_blueWinsSlimedSoundInstance.release();
    }
    
    private void RedBlobWinsPlay()
    {
        m_redWinsSlimedSoundInstance = RuntimeManager.CreateInstance(m_redWinsSoundEvent);
        m_redWinsSlimedSoundInstance.start();
        m_redWinsSlimedSoundInstance.release();
    }

    void StartResetRountPlayer1()
    {
        if(!m_stopCounting)
            StartCoroutine(CooldownBeforeNewRoundPlayer1());
    }
    
    void StartResetRountPlayer2()
    {
        if(!m_stopCounting)
            StartCoroutine(CooldownBeforeNewRoundPlayer2());
    }

    IEnumerator CooldownBeforeNewRoundPlayer1()
    {
        m_stopCounting = true;
        m_deadZoneAnim.speed = 0;
        DestroyDamagableObjects();
        m_playersController.DisactivatePlayer(1);
        m_playersController.DisactivatePlayer(2);
        IncreasePlayer1Points();
        yield return new WaitForSeconds(m_timeBeforeShowText);
        BlueBlobSLimedPlay();
        m_titlesAnim.SetTrigger("BlueWin");
        yield return new WaitForSeconds(m_timeBeforePlayersTP);
        if (m_player1Count >= 3)
        {
            StartCoroutine(Player1Wins());
        }
        else
        {
            m_randomSkillsActivatorScript.ChooseSkillPlayer1();
        }
    }
    
    IEnumerator CooldownBeforeNewRoundPlayer2()
    {
        m_stopCounting = true;
        m_deadZoneAnim.speed = 0;
        DestroyDamagableObjects();
        m_playersController.DisactivatePlayer(1);
        m_playersController.DisactivatePlayer(2);
        IncreasePlayer2Points();
        yield return new WaitForSeconds(m_timeBeforeShowText);
        RedBlobSLimedPlay();
        m_titlesAnim.SetTrigger("RedWin");
        yield return new WaitForSeconds(m_timeBeforePlayersTP);
        if (m_player2Count >= 3)
        {
            StartCoroutine(Player2Wins());
        }
        else
        {
            m_randomSkillsActivatorScript.ChooseSkillPlayer2();
        }
    }

    public IEnumerator Player1Wins()
    {
        m_playersController.ResetPlayersPosition();
        m_playerHealthScript1.RespawnPlayer();
        m_playerHealthScript2.RespawnPlayer();
        yield return new WaitForSeconds(m_timeBeforeChoosePlayers);
        m_playerHealthScript1.PlayerLose();
        m_playerHealthScript2.PlayerWin();
        yield return new WaitForSeconds(m_timeBeforeShowWiner);
        RedBlobWinsPlay();
        m_titlesAnim.SetTrigger("RedTotalWin");
        m_winButtons.SetActive(true);
        m_menuScript.UnlockCursor();
    }
    
    public IEnumerator Player2Wins()
    {
        m_playersController.ResetPlayersPosition();
        m_playerHealthScript1.RespawnPlayer();
        m_playerHealthScript2.RespawnPlayer();
        yield return new WaitForSeconds(m_timeBeforeShowWiner);
        m_playerHealthScript2.PlayerLose();
        m_playerHealthScript1.PlayerWin();
        yield return new WaitForSeconds(m_timeBeforeShowWiner);
        BlueBlobWinsPlay();
        m_titlesAnim.SetTrigger("BlueTotalWin");
        m_winButtons.SetActive(true);
        m_menuScript.UnlockCursor();
    }

    public IEnumerator TpPlayers()
    {
        m_playersController.ResetPlayersPosition();
        m_playerHealthScript1.RespawnPlayer();
        m_playerHealthScript2.RespawnPlayer();
        yield return new WaitForSeconds(m_timeBeforeChoosePlayers);
        m_playerOrderScript.ChoosePlayer();
        m_stopCounting = false;
    }

    public void IncreasePlayer1Points()
    {
        m_stopCounting = true;
        m_player1Count++;
        m_player1CountText.text = m_player1Count.ToString();
        m_playerOrderScript.StopTimer();
    }
    
    public void IncreasePlayer2Points()
    {
        m_stopCounting = true;
        m_player2Count++;
        m_player2CountText.text = m_player2Count.ToString();
        m_playerOrderScript.StopTimer();
    }
    
    public void DestroyDamagableObjects()
    {
        // Находим все объекты с указанным тегом
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("DamageDeal");

        // Удаляем каждый найденный объект
        foreach (GameObject obj in objectsToDestroy)
        {
            Destroy(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
