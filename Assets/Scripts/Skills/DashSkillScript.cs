using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSkillScript : MonoBehaviour
{
    public PlayersController m_playersController;
    [SerializeField] private GameObject m_playerGameObject;
    public PlayerMove m_playerMove;
    public PlayerHealthScript m_playerColider;
    public Rigidbody2D rb;
    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    public bool m_isFirstPlayer;
    
    public bool isDashing = false;
    public bool canDash = false;
    public float dashTime;
    public float lastDashTime;
    private Vector2 dashDirection;
    private Vector2 movementDirection;
    

    void Start()
    {
        
    }

    void Update()
    {
        // Проверяем, можно ли выполнить дэш
        if (Time.time >= lastDashTime + dashCooldown)
        {
            canDash = true;
        }

        // Проверяем, что время не остановлено и нажата кнопка даша
        if (Time.timeScale > 0f)
        {
            if(m_isFirstPlayer)
                movementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            else
                movementDirection = new Vector2(Input.GetAxisRaw("Horizontal2"), Input.GetAxisRaw("Vertical2"));

            // Проверяем нажатие кнопки даша
            if (Input.GetKeyDown(KeyCode.Mouse1) && canDash && movementDirection != Vector2.zero)
            {
                StartDash(movementDirection.normalized);
            }
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            if (Time.time < dashTime)
            {
                rb.velocity = dashDirection * dashSpeed; // Убираем Time.deltaTime здесь
            }
            else
            {
                StopDash();
            }
        }
        else
        {
            // Если не в даше, поддерживаем обычное движение
            if(m_isFirstPlayer)
                rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * rb.velocity.magnitude;
            else
                rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal2"), Input.GetAxisRaw("Vertical2")).normalized * rb.velocity.magnitude;
        }
    }

    public void StartDash(Vector2 direction)
    {
        isDashing = true;
        canDash = false;
        m_playerMove.enabled = false;
        m_playerColider.m_canDamage = false;
        if (!m_isFirstPlayer)
        {
            m_playersController.DisactivateSkills(1);
        }
        else
        {
            m_playersController.DisactivateSkills(2);
        }
        
        dashTime = Time.time + dashDuration;
        lastDashTime = Time.time;
        dashDirection = direction;
    }

    public void StopDash()
    {
        isDashing = false;
        m_playerColider.m_canDamage = true;
        if (m_isFirstPlayer && m_playersController.m_isPlayer2Active)
        {
            m_playerMove.enabled = true;
            m_playersController.ActivateSkills(2);
        }
        else if (!m_isFirstPlayer && m_playersController.m_isPlayer1Active)
        {
            m_playerMove.enabled = true;
            m_playersController.ActivateSkills(1);
        }
        
        rb.velocity = Vector2.zero; // Останавливаем движение после даша
    }
}
