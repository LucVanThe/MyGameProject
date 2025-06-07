using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
   
    private GameManager gameManager;
    private AudioManager audiomanager;
    private PlayerController Player;
    [SerializeField] public float damage = 10f;
    
    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        audiomanager = FindAnyObjectByType<AudioManager>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            
            Destroy(collision.gameObject);
            audiomanager.playCoinSound();
            gameManager.addScore(1);
        }
        
        else if (collision.CompareTag("Enemy"))
        {
            PlayerController player = GetComponent<PlayerController>();
            player.takeDamage(10);
        }
        else if (collision.CompareTag("EnemyBullet"))
        {
            Debug.Log("cham player");
            PlayerController player =GetComponent<PlayerController>();
            player.takeDamage(10);
        }
        else if (collision.CompareTag("Key"))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            Destroy(collision.gameObject);

            //gameManager.gotoNextMap(3);
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else if (collision.CompareTag("Death"))
        {
            gameManager.gameover();
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap"))
        {
            PlayerController player = GetComponent<PlayerController>();
            player.takeDamage(1);
        }
    }

}
