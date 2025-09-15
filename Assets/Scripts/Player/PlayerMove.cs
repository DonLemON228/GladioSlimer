using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using FMODUnity;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] bool m_isFirstPlayer;
    public float moveSpeed = 5f;
    [SerializeField] private float currentSpeed;
    private float startSpeed;
    private Rigidbody2D rb;
    public Animator anim;

    [Header("Directions")] 
    [SerializeField] GameObject m_upDir;
    [SerializeField] GameObject m_upLeftDir;
    [SerializeField] GameObject m_leftDowmDir;
    [SerializeField] GameObject m_downDir;
    [SerializeField] GameObject m_leftDir;
    [SerializeField]  float delay = 0.5f;
    private float lastInputTime;
    
    [Header("Sounds")]
    public EventReference m_playerWalkSoundEvent;
    EventInstance m_playerWalkSoundInstance;

    private float m_lastInputX;
    private float m_lastInputY;
    private Vector2 movement;

    void Start()
    {
        startSpeed = moveSpeed;
        rb = GetComponent<Rigidbody2D>();
        m_upDir.SetActive(false);
        m_upLeftDir.SetActive(false);
        m_leftDowmDir.SetActive(false);
        m_downDir.SetActive(false);
        m_leftDir.SetActive(false);
    }
    
    private void SetActiveDirection(GameObject activeDirection)
    {
        DeactivateAllDirections(); // Отключаем все стрелки
        activeDirection.SetActive(true); // Активируем нужную стрелку
        lastInputTime = Time.time; // Обновляем время последнего ввода
    }

    public void DeactivateAllDirections()
    {
        m_upDir.SetActive(false);
        m_downDir.SetActive(false);
        m_leftDir.SetActive(false);
        m_upLeftDir.SetActive(false);
        m_leftDowmDir.SetActive(false);
    }

    private void PlayerWalkSound()
    {
        m_playerWalkSoundInstance = RuntimeManager.CreateInstance(m_playerWalkSoundEvent);
        m_playerWalkSoundInstance.start();
        m_playerWalkSoundInstance.release();
    }

    void Update()
    {
        if (Time.timeScale != 0f)
        {
            if (m_isFirstPlayer)
            {
                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");
            }
            else
            {
                movement.x = Input.GetAxisRaw("Horizontal2");
                movement.y = Input.GetAxisRaw("Vertical2");
            }
            
            anim.SetFloat("InputX", movement.x);
            anim.SetFloat("InputY", movement.y);

            if (movement.magnitude > 1)
            {
                movement.Normalize();
            }

            if (movement.magnitude > 0)
            {
                anim.SetBool("isWalking", true);
                m_lastInputX = movement.x;
                m_lastInputY = movement.y;
            }
            else
            {
                anim.SetBool("isWalking", false);
                anim.SetFloat("LastInputX", m_lastInputX);
                anim.SetFloat("LastInputY", m_lastInputY);
            }

            if (movement.x < 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (movement.x > 0)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            
            if (!m_isFirstPlayer)
            {
                if (Time.time - lastInputTime < delay)
                    return;
                
                if(Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
                    SetActiveDirection(m_upDir);
                
                if(!Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
                    SetActiveDirection(m_downDir);
                
                if(!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
                    SetActiveDirection(m_leftDir);
                
                if(!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
                    SetActiveDirection(m_leftDir);
                
                if(Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
                    SetActiveDirection(m_upLeftDir);
                
                if(Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
                    SetActiveDirection(m_upLeftDir);
                
                if(!Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
                    SetActiveDirection(m_leftDowmDir);
                
                if(!Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
                    SetActiveDirection(m_leftDowmDir);
            }
            else
            {
                if (Time.time - lastInputTime < delay)
                    return;
                
                if(Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
                    SetActiveDirection(m_upDir);
                
                if(!Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
                    SetActiveDirection(m_downDir);
                
                if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
                    SetActiveDirection(m_leftDir);
                
                if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
                    SetActiveDirection(m_leftDir);
                
                if(Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
                    SetActiveDirection(m_upLeftDir);
                
                if(Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
                    SetActiveDirection(m_upLeftDir);
                
                if(!Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
                    SetActiveDirection(m_leftDowmDir);
                
                if(!Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
                    SetActiveDirection(m_leftDowmDir);
            }
        }
    }

    void FixedUpdate()
    {
        currentSpeed = moveSpeed;
        rb.MovePosition(rb.position + movement * currentSpeed * Time.fixedDeltaTime);
    }

}
