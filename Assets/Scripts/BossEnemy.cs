using UnityEngine;
using UnityEngine.UI;

public class BossEnemy : MonoBehaviour
{
    [SerializeField]private GameManager gameManager;
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
    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] private Transform firepoint;
    [SerializeField] private float speedDanThuong = 5f;
    [SerializeField] private float speedTron = 3f;
    [SerializeField] private float HoiHP = 100f;
    [SerializeField] private float CDskill = 3f;
    private float nextskill = 0f;
    void Start()
    {
        currHP = maxHP;
        UpdateHPBar();
        rb = GetComponent<Rigidbody2D>();
      //  Destroy(bloddPrefabs, 0.5f);
    }

    void Update()
    {
        MoveToPlayer();
        if(Time.time >= nextskill)
        {
            sudungskill();
        }
    }
    private void BanDanThuong()
    {
       
        if (player != null)
        {
            Debug.Log("Ban dan");
            Vector3 directionToPlayer = (player.transform.position - firepoint.position).normalized;           
            GameObject bullet = Instantiate(bulletPrefabs, firepoint.position, Quaternion.identity);
            bullet.transform.right = directionToPlayer;
            EnemyBullet enemyBullet = bullet.AddComponent<EnemyBullet>();
            enemyBullet.SetMovementDirection(directionToPlayer * speedDanThuong);
        }
        else
        {
            Debug.Log("Khong Ban dan");
        }
    }
    private void BanDanTron()
    {
        const int Sodan = 12;
        float angleStep = 360f / Sodan;
        for(int i = 0; i < Sodan; i++)
        {
            float angle = i * angleStep;
            Vector3 bulletDirection = new Vector3(Mathf.Cos(Mathf.Deg2Rad*angle),Mathf.Sin(Mathf.Rad2Deg*angle),0);
            GameObject bullet = Instantiate(bulletPrefabs, firepoint.position, Quaternion.identity);
            EnemyBullet enemyBullet = bullet.AddComponent<EnemyBullet>();
            enemyBullet.SetMovementDirection(bulletDirection * speedTron);
        }
    }
    private void HoiMau(float HPamount)
    {
        currHP = Mathf.Min(currHP + HPamount, maxHP);
        UpdateHPBar();

    }
    private void DichChuyen()
    {
        if(player != null)
        {
            transform.position = player.transform.position;
        }
       
    }
    private void randomSkill()
    {
        int randomskill = Random.Range(0, 4);
        switch (randomskill)
        {
            case 0: BanDanThuong();break;
            case 1:BanDanTron();break;
            case 2:HoiMau(HoiHP);break;
            case 3:DichChuyen();break;
        }
    }
    private void sudungskill()
    {
        nextskill = Time.time + CDskill;
        randomSkill();
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
                gameManager.addScore(1000);
                Die();
               
            }
        }
        if (other.CompareTag("Player"))
        {
            PlayerController player = GetComponent<PlayerController>();
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
        Debug.Log("Game Win");
        gameManager.GameWin();
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
