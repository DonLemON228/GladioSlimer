using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillsCardsController : MonoBehaviour
{
    [SerializeField] private PointCountingScript m_pointCountingScript;
    [SerializeField] private Animator m_deadZoneAnim;
    [Header("Cards Filds")] 
    [SerializeField] Image m_background;
    [SerializeField] Image m_logo;
    [SerializeField] TextMeshProUGUI m_title;
    [SerializeField] TextMeshProUGUI m_discrition;
    [SerializeField] private Animator m_cardAnim;
    
    [SerializeField] private List<SkillsCardScriptableObject> m_cardsSOPlayer1;
    [SerializeField] private List<SkillsCardScriptableObject> m_cardsSOPlayer2;

    [SerializeField] private float m_timeBeforeHide;
    
    [Header("Sounds")]
    public EventReference m_activateCardSoundEvent;
    EventInstance m_activateCardSoundInstance;

    private bool m_isCardShowed;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    private void ActivateCardSoundPlay()
    {
        m_activateCardSoundInstance = RuntimeManager.CreateInstance(m_activateCardSoundEvent);
        m_activateCardSoundInstance.start();
        m_activateCardSoundInstance.release();
    }

    public void ShowCard(int _playerNumber, int _skillIndex)
    {
        if (_playerNumber == 1)
        {
            m_background.sprite = m_cardsSOPlayer1[_skillIndex].Bg;
            m_logo.sprite = m_cardsSOPlayer1[_skillIndex].Logo;
            m_title.text = m_cardsSOPlayer1[_skillIndex].Name;
            m_discrition.text = m_cardsSOPlayer1[_skillIndex].Discription;
        }
        else
        {
            m_background.sprite = m_cardsSOPlayer2[_skillIndex].Bg;
            m_logo.sprite = m_cardsSOPlayer2[_skillIndex].Logo;
            m_title.text = m_cardsSOPlayer2[_skillIndex].Name;
            m_discrition.text = m_cardsSOPlayer2[_skillIndex].Discription;
        }
        
        m_cardAnim.SetBool("Show", true);
        ActivateCardSoundPlay();
        StartCoroutine(CooldownBeforeHide());
    }

    public void HideCard()
    {
        m_cardAnim.SetBool("Show", false);
        StartCoroutine(m_pointCountingScript.TpPlayers());
        m_deadZoneAnim.speed = 1;
        m_deadZoneAnim.SetTrigger("Restart");
        m_isCardShowed = false;
    }

    IEnumerator CooldownBeforeHide()
    {
        yield return new WaitForSeconds(m_timeBeforeHide);
        m_isCardShowed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && m_isCardShowed)
        {
            HideCard();
        }
    }
}
