using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class PlayerShootSkill : MonoBehaviour
{
    public KeyCode m_useKey;
    [SerializeField] private CooldownScript m_cooldownScript;
    [SerializeField] private PlayerHealthScript m_playerGameObject;
    [SerializeField] private GameObject m_projectileObject;
    [SerializeField] private List<CooldownIconScript> m_cooldownIconScripts;
    [SerializeField] private float m_shootCooldown;
    [SerializeField] List<GameObject> m_directions;
    [SerializeField] private bool m_isFirstPlyaer;
    public bool m_isDash;
    [Header("Dash")]
    public PlayersController m_playersController;
    public PlayerMove m_playerMove;
    public Rigidbody2D rb;
    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    public bool canDash = false;
    public bool isDashing = false;
    public float dashTime;
    public float lastDashTime;
    private Vector2 dashDirection;
    private Vector2 movementDirection;
    
    [Header("Sounds")]
    public EventReference m_shootSoundEvent;
    EventInstance m_shootSoundInstance;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    private void ShootSoundPlay()
    {
        m_shootSoundInstance = RuntimeManager.CreateInstance(m_shootSoundEvent);
        m_shootSoundInstance.start();
        m_shootSoundInstance.release();
    }
    
    void Shoot()
    {
        foreach (GameObject obj in m_directions)
        {
            if (obj.activeInHierarchy)
            {
                GameObject bullet = Instantiate(m_projectileObject, obj.transform.position, Quaternion.identity);
        
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        
                Vector2 direction = obj.transform.up;
                
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
        }

        ShootSoundPlay();
        m_cooldownScript.StartCooldown(m_shootCooldown);
        foreach (var obj in m_cooldownIconScripts)
        {
            if(obj.gameObject.activeInHierarchy)
                obj.StartCooldown(m_shootCooldown);
        }
    }
    
    public void StartDash(Vector2 direction)
    {
        isDashing = true;
        canDash = false;
        m_playerMove.enabled = false;
        m_playerGameObject.gameObject.layer = LayerMask.NameToLayer("PlayerInDash");
        m_playerGameObject.m_canDamage = false;
        if (!m_isFirstPlyaer)
        {
            m_playersController.DisactivateSkills(1);
        }
        else
        {
            m_playersController.DisactivateSkills(2);
        }
        ShootSoundPlay();

        dashTime = Time.time + dashDuration;
        lastDashTime = Time.time;
        dashDirection = direction;
        m_cooldownScript.StartCooldown(m_shootCooldown);
        foreach (var obj in m_cooldownIconScripts)
        {
            if(obj.gameObject.activeInHierarchy)
                obj.StartCooldown(m_shootCooldown);
        }
    }

    public void StopDash()
    {
        isDashing = false;
        m_playerGameObject.m_canDamage = true;
        m_playerGameObject.gameObject.layer = LayerMask.NameToLayer("Player");
        if (m_isFirstPlyaer && m_playersController.m_isPlayer2Active)
        {
            m_playerMove.enabled = true;
            m_playersController.ActivateSkills(2);
        }
        else if (!m_isFirstPlyaer && m_playersController.m_isPlayer1Active)
        {
            m_playerMove.enabled = true;
            m_playersController.ActivateSkills(1);
        }
        
        rb.velocity = Vector2.zero; // Останавливаем движение после даша
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isFirstPlyaer)
        {
            if (!m_isDash)
            {
                if (Input.GetKey(m_useKey) && m_cooldownScript.m_canActivate)
                {
                    Shoot();
                }
            }
            else
            {
                if (Time.time >= lastDashTime + dashCooldown)
                {
                    canDash = true;
                }

                // Проверяем, что время не остановлено и нажата кнопка даша
                if (Time.timeScale > 0f)
                {
                    if(m_isFirstPlyaer)
                        movementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                    else
                        movementDirection = new Vector2(Input.GetAxisRaw("Horizontal2"), Input.GetAxisRaw("Vertical2"));

                    // Проверяем нажатие кнопки даша
                    if (Input.GetKeyDown(m_useKey) && canDash && m_cooldownScript.m_canActivate && movementDirection != Vector2.zero)
                    {
                        StartDash(movementDirection.normalized);
                    }
                }
            }
            
        }
        else
        {
            if (!m_isDash)
            {
                if (Input.GetKey(m_useKey) && m_cooldownScript.m_canActivate)
                {
                    Shoot();
                }
            }
            else
            {
                if (Time.time >= lastDashTime + dashCooldown)
                {
                    canDash = true;
                }

                // Проверяем, что время не остановлено и нажата кнопка даша
                if (Time.timeScale > 0f)
                {
                    if(m_isFirstPlyaer)
                        movementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                    else
                        movementDirection = new Vector2(Input.GetAxisRaw("Horizontal2"), Input.GetAxisRaw("Vertical2"));

                    // Проверяем нажатие кнопки даша
                    if (Input.GetKeyDown(m_useKey) && m_cooldownScript.m_canActivate && canDash && movementDirection != Vector2.zero)
                    {
                        StartDash(movementDirection.normalized);
                    }
                }
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
            if(m_isFirstPlyaer)
                rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * rb.velocity.magnitude;
            else
                rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal2"), Input.GetAxisRaw("Vertical2")).normalized * rb.velocity.magnitude;
        }
    }
}
