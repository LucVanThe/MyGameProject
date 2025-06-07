using UnityEngine;
using UnityEngine.UI;
public class BasicEnemy : MonoBehaviour
{
    public float speed = 2f; // Tốc độ di chuyển
    public Transform player; // Nhân vật của người chơi
    public float attackRange = 1f; // Khoảng cách để tấn công
    public Transform groundCheck; // Điểm kiểm tra mặt đất
    public LayerMask groundLayer; // Lớp mặt đất
    private bool isGrounded;
    private Rigidbody2D rb;
    [SerializeField] protected float maxHP = 50f;
    protected float currHP;
    [SerializeField] protected Image HPbar;
    [SerializeField] protected float enterDamg = 10f;
    [SerializeField] protected float stayDmg = 5f;
    [SerializeField] private GameObject bloddPrefabs;
  
    void Start()
    {
        currHP = maxHP;
        UpdateHPBar();
        rb = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        MoveToPlayer();

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            GameObject blood = Instantiate(bloddPrefabs, transform.position, Quaternion.identity);
            Destroy(blood, 1f);
            playerbullet bullet = other.GetComponent<playerbullet>();
            currHP -= (bullet.bulletdamage) / 2;
            currHP = Mathf.Max(currHP, 0);
            UpdateHPBar();
            if (currHP <= 0)
            {
                GameManager gameManager = FindAnyObjectByType<GameManager>();
                gameManager.addScore(1);
                Die();
                
            }
        }
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            player.takeDamage(enterDamg);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController p = collision.GetComponent<PlayerController>(); // Lấy component từ Player

            if (p != null)
            {
                p.takeDamage(stayDmg);
            }
        }


    }
    protected void Die()
    {
        Destroy(gameObject);
    }
    protected void UpdateHPBar()
    {
        if (HPbar != null)
        {
            HPbar.fillAmount = currHP / maxHP;
        }
    }
    protected void MoveToPlayer()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.1f, groundLayer);

        if (!isGrounded)
        {
            rb.linearVelocity += Vector2.down * 9.8f * Time.deltaTime; // Giả lập trọng lực nếu chưa rơi nhanh
        }
        if (player == null) return;

        // Tính khoảng cách đến người chơi
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance > 8f)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        // Nếu khoảng cách đủ gần, di chuyển về phía người chơi
        if (distance > attackRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);
            transform.localScale = new Vector3(player.transform.position.x < transform.position.x ? -1 : 1, 1, 1);
        }
        else
        {
            rb.linearVelocity = Vector2.zero; // Dừng lại nếu trong tầm tấn công
        }
    }
}
