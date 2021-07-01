using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D m_rigidbody; 
    public Attributes attributes;

    private Animator m_animator;
    private SpriteRenderer m_spriteRenderer;

    public LayerMask groundLayer;
    public static PlayerController sharedInstance;

    public GameObject weapon = null;
    private Vector3 startPosition;

    private void Awake()
    {
        sharedInstance = this;
        startPosition = this.transform.position;
    }

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public bool flip = false;

    private void FixedUpdate()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if (Input.GetKey(KeyCode.A))
            {
                m_spriteRenderer.flipX = flip = false;
                Walk(false);
            }

            if (Input.GetKey(KeyCode.D))
            {
                m_spriteRenderer.flipX = flip = true;
                Walk(true);
            }

            if (m_rigidbody.velocity.x != 0.0f && m_rigidbody.velocity.y == 0.0f && IsTouchingTheGround()) 
            {
                m_animator.SetBool("isIdle", false);
                m_animator.SetBool("isWalk", true);
                m_animator.SetBool("isJump", false);
            }

            if (m_rigidbody.velocity.x == 0.0f && m_rigidbody.velocity.y != 0.0f && IsTouchingTheGround())
            {
                m_animator.SetBool("isIdle", false);
                m_animator.SetBool("isWalk", false);
                m_animator.SetBool("isJump", true);
            }

            if (m_rigidbody.velocity.x != 0.0f && m_rigidbody.velocity.y != 0.0f && !IsTouchingTheGround())
            {
                m_spriteRenderer.flipX = flip;

                m_animator.SetBool("isIdle", false);
                m_animator.SetBool("isWalk", false);
                m_animator.SetBool("isJump", true);
            }

            if (m_rigidbody.velocity == Vector2.zero)
            {
                m_spriteRenderer.flipX = flip;

                m_animator.SetBool("isIdle", true);
                m_animator.SetBool("isWalk", false);
                m_animator.SetBool("isJump", false);
            }
        }
    }

    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Jump();
            }

            if (Input.GetMouseButtonDown(0) && m_rigidbody.velocity == Vector2.zero && weapon != null)  
            { 
                StartCoroutine("Shoot");
            }
        }
    }

    IEnumerator Shoot()
    {

        this.m_animator.SetBool("isIdle", false);
        this.m_animator.SetBool("isShoot", true);

        Vector3 posPlayer = transform.position;
        GameObject shoot = weapon.CompareTag("Portal") ? ShootController.sharedInstance.at(0) : ShootController.sharedInstance.at(1);

        float absolute = flip ? 1 : -1;
        
        weapon.transform.position = new Vector3(absolute * 0.1f + posPlayer.x, 0.05f + posPlayer.y, 0.0f);
        Instantiate(shoot, new Vector3(absolute * 0.9f + posPlayer.x, posPlayer.y +0.1f, 0.0f), shoot.transform.rotation);

        weapon.SetActive(true);   

        yield return new WaitForSeconds(0.5f);

        this.m_animator.SetBool("isIdle", true);
        this.m_animator.SetBool("isShoot", false);

        weapon.SetActive(false);
    }

    public void StartGame()
    {
        this.m_animator.SetBool("isIdle", true);
        this.m_animator.SetBool("isWalk", false);
        this.m_animator.SetBool("isJump", false);
        this.m_animator.SetBool("isShoot", false);

        this.transform.position = startPosition;
    }

    private void Walk(bool state)
    {
        int absolute = (state) ? 1 : -1;
        m_rigidbody.velocity = new Vector2(attributes.velocity * absolute, m_rigidbody.velocity.y);
    }

    private void Jump()
    {
        if (IsTouchingTheGround())
        {
            m_rigidbody.AddForce(Vector2.up * attributes.jumpForce, ForceMode2D.Impulse);
        }
    }

    private bool IsTouchingTheGround()
    {
        return (Physics2D.Raycast(this.transform.position, Vector2.down, 0.9f, groundLayer));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy")) return;
        GameManager.sharedInstance.SetGameState(GameState.gameOver);
        Debug.Log("Muero");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
