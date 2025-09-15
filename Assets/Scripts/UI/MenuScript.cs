using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private Animator m_fadeAnim;
    [SerializeField] private float m_timeBeforeTp;
    [SerializeField] private bool m_menuMusic;
    [SerializeField] private bool m_lockCursor;
    [SerializeField] private bool m_isGameMenu;
    [SerializeField] private bool m_isPrefsStart;

    [Header("RedLemonLinks")] 
    [SerializeField] Button m_tiktokButtonRL;
    [SerializeField] private string m_tikTokLinkRL;
    
    [SerializeField] Button m_youTubeButtonRL;
    [SerializeField] private string m_youTubeLinkRL;
    
    [SerializeField] Button m_tgButtonRL;
    [SerializeField] private string m_tgLinkRL;
    
    [Header("LastBreathLinks")] 
    [SerializeField] Button m_tiktokButtonRLastBreath;
    [SerializeField] private string m_tikTokLinkLastBreath;
    
    [SerializeField] Button m_youTubeButtonLastBreath;
    [SerializeField] private string m_youTubeLinkLastBreath;
    
    [SerializeField] Button m_tgButtonLastBreath;
    [SerializeField] private string m_tgLinkLastBreath;
    
    [Header("Sounds")]
    private Bus theBusASoundPassesThrough;
    public EventReference m_menuOstEvent;
    public EventReference m_gameOstEvent;
    EventInstance m_menuOstInstance;
    EventInstance m_gameOstInstance;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        theBusASoundPassesThrough = RuntimeManager.GetBus("Bus:/");
        theBusASoundPassesThrough.stopAllEvents(STOP_MODE.IMMEDIATE);
        if (!m_isGameMenu)
        {
            m_tiktokButtonRL.onClick.AddListener(() => OpenLink(m_tikTokLinkRL));
            m_youTubeButtonRL.onClick.AddListener(() => OpenLink(m_youTubeLinkRL));
            m_tgButtonRL.onClick.AddListener(() => OpenLink(m_tgLinkRL));
        
            m_tiktokButtonRLastBreath.onClick.AddListener(() => OpenLink(m_tikTokLinkLastBreath));
            m_youTubeButtonLastBreath.onClick.AddListener(() => OpenLink(m_youTubeLinkLastBreath));
            m_tgButtonLastBreath.onClick.AddListener(() => OpenLink(m_tgLinkLastBreath));
        }
        
        if (m_lockCursor)
            LockCursor();
        else
            UnlockCursor();
        
        if (m_menuMusic)
        {
            MenuOstPlay();
        }
        else
        {
            GameOstPlay();
        }
        
    }

    void OpenLink(string _link)
    {
        Application.OpenURL(_link);
    }
    
    private void MenuOstPlay()
    {
        m_menuOstInstance = RuntimeManager.CreateInstance(m_menuOstEvent);
        m_menuOstInstance.start();
        m_menuOstInstance.release();
    }
    
    private void MenuOstStop()
    {
        m_menuOstInstance.stop(STOP_MODE.IMMEDIATE);
        m_menuOstInstance.release();
    }
    
    private void GameOstPlay()
    {
        m_gameOstInstance = RuntimeManager.CreateInstance(m_gameOstEvent);
        m_gameOstInstance.start();
        m_gameOstInstance.release();
    }
    
    private void GameOstStop()
    {
        m_gameOstInstance.stop(STOP_MODE.IMMEDIATE);
        m_gameOstInstance.release();
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void RestartGame()
    {
        StartCoroutine(TimeBeforeTp("GameScene"));
        MenuOstStop();
        GameOstStop();
    }

    public void StartGame()
    {
        if(m_isPrefsStart)
            PlayerPrefs.SetInt("isTutorialPassed", 1);
        
        int isTutorPassed = PlayerPrefs.GetInt("isTutorialPassed");
        
        if (isTutorPassed == 1)
        {
            RestartGame();
        }
        else
        {
            TutorialScene();
        }
    }
    
    public void TutorialScene()
    {
        StartCoroutine(TimeBeforeTp("TutorialMenu"));
        MenuOstStop();
        GameOstStop();
    }
    
    public void GoToMenu()
    {
        StartCoroutine(TimeBeforeTp("Menu"));
        GameOstStop();
        MenuOstStop();
    }
    
    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator TimeBeforeTp(string _sceneName)
    {
        m_fadeAnim.SetTrigger("FadeIn");
        Time.timeScale = 1f;
        yield return new WaitForSeconds(m_timeBeforeTp);
        SceneManager.LoadScene(_sceneName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
