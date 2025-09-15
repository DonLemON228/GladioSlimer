using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersController : MonoBehaviour
{
    [SerializeField] CameraManager m_cameraManager;
    [SerializeField] private RandomSkillsActivatorScript m_randomSkillsActivatorScript;
    [Header("Player1")]
    [SerializeField] Rigidbody2D m_rigidbodyPlayer1;
    [SerializeField] PlayerMove m_playerMove1;
    public bool m_isPlayer1Active;

    [Header("Player2")]
    [SerializeField] Rigidbody2D m_rigidbodyPlayer2;
    [SerializeField] PlayerMove m_playerMove2;
    public bool m_isPlayer2Active;

    private Vector2 m_positionPlayer1;
    private Vector2 m_positionPlayer2;

    // Start is called before the first frame update
    void Start()
    {
        m_positionPlayer1 = m_playerMove1.transform.position;
        m_positionPlayer2 = m_playerMove2.transform.position;
        DisactivatePlayer(1);
        DisactivatePlayer(2);
    }

    public void DisactivateSkills(int _playerNumber)
    {
        if(_playerNumber == 1)
        {
            foreach (var obj in m_randomSkillsActivatorScript.m_choosedSkillsPlayer1)
            {
                if(!obj.GetComponent<PlayerShootSkill>().m_isDash)
                    obj.enabled = false;
            }
        }
        else
        {
            foreach (var obj in m_randomSkillsActivatorScript.m_choosedSkillsPlayer2)
            {
                if(!obj.GetComponent<PlayerShootSkill>().m_isDash)
                    obj.enabled = false;
            }
        }
    }
    
    public void ActivateSkills(int _playerNumber)
    {
        if(_playerNumber == 1)
        {
            foreach (var obj in m_randomSkillsActivatorScript.m_choosedSkillsPlayer1)
            {
                obj.enabled = true;
            }
        }
        else
        {
            foreach (var obj in m_randomSkillsActivatorScript.m_choosedSkillsPlayer2)
            {
                obj.enabled = true;
            }
        }
    }

    public void DisactivatePlayer(int _playerNumber)
    {
        if(_playerNumber == 1)
        {
            m_rigidbodyPlayer1.bodyType = RigidbodyType2D.Kinematic;
            m_rigidbodyPlayer1.velocity = Vector2.zero;
            m_playerMove1.DeactivateAllDirections();
            m_playerMove1.anim.SetBool("isWalking", false);
            m_playerMove1.enabled = false;
            m_isPlayer1Active = false;
            foreach (var obj in m_randomSkillsActivatorScript.m_choosedSkillsPlayer1)
            {
                obj.enabled = false;
            }
        }
        else
        {
            m_rigidbodyPlayer2.bodyType = RigidbodyType2D.Kinematic;
            m_rigidbodyPlayer2.velocity = Vector2.zero;
            m_playerMove2.DeactivateAllDirections();
            m_playerMove2.anim.SetBool("isWalking", false);
            m_playerMove2.enabled = false;
            m_isPlayer2Active = false;
            foreach (var obj in m_randomSkillsActivatorScript.m_choosedSkillsPlayer2)
            {
                obj.enabled = false;
            }
        }
    }

    public void ActivatePlayer(int _playerNumber)
    {
        if (_playerNumber == 1)
        {
            m_rigidbodyPlayer1.bodyType = RigidbodyType2D.Dynamic;
            m_playerMove1.enabled = true;
            m_isPlayer1Active = true;
            foreach (var obj in m_randomSkillsActivatorScript.m_choosedSkillsPlayer1)
            {
                obj.enabled = true;
            }
        }
        else if(_playerNumber == 2)
        {
            m_rigidbodyPlayer2.bodyType = RigidbodyType2D.Dynamic;
            m_playerMove2.enabled = true;
            m_isPlayer2Active = true;
            foreach (var obj in m_randomSkillsActivatorScript.m_choosedSkillsPlayer2)
            {
                obj.enabled = true;
            }
        }
    }

    public void ResetPlayersPosition()
    {
        m_playerMove1.transform.position = m_positionPlayer1;
        m_playerMove2.transform.position = m_positionPlayer2;
        DisactivatePlayer(1);
        DisactivatePlayer(2);
        m_cameraManager.SwitchCamera(m_cameraManager.m_middleCamera);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ActivatePlayer(1);
            DisactivatePlayer(2);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            ActivatePlayer(2);
            DisactivatePlayer(1);
        }
    }
}
