using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float moveJump = 10f;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform groundCheck;
    private Animator animator;
    private bool isGrounded;
    private Rigidbody2D rb;
    private GameManager gameManager;
    private AudioManager audiomanager;
    [SerializeField] protected float maxHP = 100f;
    protected float currHP;
    [SerializeField] protected Image HPbar;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audiomanager = FindAnyObjectByType<AudioManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currHP = maxHP;
        UpdateHPBar();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.IsGameOver()|| gameManager.isGamewin()) return;
        HandlMovement();
        HandlJump();
        UpdateAnimator();
    }
   private void HandlMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity=new Vector2(moveInput*moveSpeed, rb.linearVelocity.y);
        if (moveInput > 0) spriteRenderer.flipX = false;
        else if (moveInput < 0) spriteRenderer.flipX = true;

    }
   private void HandlJump()
    {
        if (Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            audiomanager.playJumpSound();
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, moveJump);
        }
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,0.2f, ground);
    }
    private void UpdateAnimator()
    {
        bool isRunning = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        bool isJumping = !isGrounded;
        animator.SetBool("isRunning",isRunning);
        animator.SetBool("isJumping", isJumping);
    }
    public void takeDamage( float damage)
    {
        currHP -= (damage);
        currHP = Mathf.Max(currHP, 0);
        UpdateHPBar();
        if (currHP <= 0)
        {
            Die();
        }
    }
    public void Heal(float heal)
    {
        if (currHP < maxHP)
        {
            currHP += heal;
            currHP = Mathf.Min(currHP, maxHP);
            UpdateHPBar();
        }
    }
    public void Die()
    {
        gameManager.gameover();
    }
    protected void UpdateHPBar()
    {
        if (HPbar != null)
        {
            HPbar.fillAmount = currHP / maxHP;
        }
    }
}
