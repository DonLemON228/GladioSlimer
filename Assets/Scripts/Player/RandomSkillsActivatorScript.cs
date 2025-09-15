using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSkillsActivatorScript : MonoBehaviour
{
    [SerializeField] private Animator m_playerCardsAnim;
    [SerializeField] private SkillsCardsController m_skillsCardsController;
    
    [Header("Player1")] 
    [SerializeField] private List<PlayerShootSkill> m_player1skills = new List<PlayerShootSkill>();
    [SerializeField] private List<GameObject> m_player1skillsImage1 = new List<GameObject>();
    [SerializeField] private List<GameObject> m_player1skillsImage2 = new List<GameObject>();
    [SerializeField] private KeyCode m_skill1KeyCodePlayer1;
    [SerializeField] private KeyCode m_skill2KeyCodePlayer1;
    public List<PlayerShootSkill> m_choosedSkillsPlayer1 = new List<PlayerShootSkill>();
    private int skillNumberPlayer1 = 1;
    
    [Header("Player2")] 
    [SerializeField] private List<PlayerShootSkill> m_player2skills = new List<PlayerShootSkill>();
    [SerializeField] private List<GameObject> m_player2skillsImage1 = new List<GameObject>();
    [SerializeField] private List<GameObject> m_player2skillsImage2 = new List<GameObject>();
    [SerializeField] private KeyCode m_skill1KeyCodePlayer2;
    [SerializeField] private KeyCode m_skill2KeyCodePlayer2;
    public List<PlayerShootSkill> m_choosedSkillsPlayer2 = new List<PlayerShootSkill>();
    private int skillNumberPlayer2 = 1;

    private int m_currentSkillIndex = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChooseSkillPlayer1()
    {
        m_currentSkillIndex = Random.Range(0, m_player1skills.Count);
        skillNumberPlayer1++;
        if (skillNumberPlayer1 <= 3)
        {
            if (skillNumberPlayer1 == 2)
            {
                while (m_choosedSkillsPlayer1.Contains(m_player1skills[m_currentSkillIndex]))
                    m_currentSkillIndex = Random.Range(0, m_player1skills.Count);
                m_choosedSkillsPlayer1.Add(m_player1skills[m_currentSkillIndex]);
                m_player1skillsImage1[m_currentSkillIndex].SetActive(true);
                m_player1skills[m_currentSkillIndex].m_useKey = m_skill1KeyCodePlayer1;
                m_skillsCardsController.ShowCard(1, m_currentSkillIndex);
            }
            else if (skillNumberPlayer1 > 2)
            {
                while (m_choosedSkillsPlayer1.Contains(m_player1skills[m_currentSkillIndex]))
                    m_currentSkillIndex = Random.Range(0, m_player1skills.Count);
                m_choosedSkillsPlayer1.Add(m_player1skills[m_currentSkillIndex]);
                m_player1skillsImage2[m_currentSkillIndex].SetActive(true);
                m_player1skills[m_currentSkillIndex].m_useKey = m_skill2KeyCodePlayer1;
                m_skillsCardsController.ShowCard(1, m_currentSkillIndex);
            }
        }
        else
        {
            skillNumberPlayer1 = 1;
        }
    }
    
    public void ChooseSkillPlayer2()
    {
        m_currentSkillIndex = Random.Range(0, m_player2skills.Count);
        skillNumberPlayer2++;
        if (skillNumberPlayer2 <= 3)
        {
            if (skillNumberPlayer2 == 2)
            {
                while (m_choosedSkillsPlayer2.Contains(m_player2skills[m_currentSkillIndex]))
                    m_currentSkillIndex = Random.Range(0, m_player2skills.Count);
                m_choosedSkillsPlayer2.Add(m_player2skills[m_currentSkillIndex]);
                m_player2skillsImage1[m_currentSkillIndex].SetActive(true);
                m_player2skills[m_currentSkillIndex].m_useKey = m_skill1KeyCodePlayer2;
                m_skillsCardsController.ShowCard(2, m_currentSkillIndex);
            }
            else if (skillNumberPlayer2 > 2)
            {
                while (m_choosedSkillsPlayer2.Contains(m_player2skills[m_currentSkillIndex]))
                    m_currentSkillIndex = Random.Range(0, m_player2skills.Count);
                m_choosedSkillsPlayer2.Add(m_player2skills[m_currentSkillIndex]);
                m_player2skillsImage2[m_currentSkillIndex].SetActive(true);
                m_player2skills[m_currentSkillIndex].m_useKey = m_skill2KeyCodePlayer2;
                m_skillsCardsController.ShowCard(2, m_currentSkillIndex);
            }
        }
        else
        {
            skillNumberPlayer2 = 1;
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
