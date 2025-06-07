using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ExplosionEnemy : MonoBehaviour
{

    public float speed = 5f; // Tốc độ di chuyển
    public Transform player; // Nhân vật của người chơi
    public float attackRange = 1f; // Khoảng cách để tấn công
    public Transform groundCheck; // Điểm kiểm tra mặt đất
    public LayerMask groundLayer; // Lớp mặt đất
    private bool isGrounded;
    private Rigidbody2D rb;
    [SerializeField] private GameObject explosionPrefabs;
    [SerializeField] protected float maxHP = 50f;
    protected float currHP;
    [SerializeField] protected Image HPbar;
    [SerializeField] private GameObject bloddPrefabs;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      //  Destroy(bloddPrefabs, 0.5f);
        currHP = maxHP;
        UpdateHPBar();
        rb = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        //Enemy2 enemy = FindFirstObjectByType<Enemy2>();
        //enemy.MoveToPlayer(speed, player, attackRange, groundCheck, groundLayer, isGrounded, rb);
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
                CreateExplosion();
                Destroy(gameObject);
            }
        }
        if (other.CompareTag("Player"))
        {
            CreateExplosion();
            Destroy(gameObject);
        }
    }
    private void CreateExplosion()
    {

        if (explosionPrefabs != null)
        {

            GameObject explosion= Instantiate(explosionPrefabs, transform.position, Quaternion.identity);
            Debug.Log("Explosion được tạo! "+ explosion.name);
        }
        else
        {
            Debug.LogError("exPref chưa được gán trong Inspector!");
        }

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
