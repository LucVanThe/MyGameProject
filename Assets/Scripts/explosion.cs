using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float dmg = 25f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (collision.CompareTag("Player"))
        {
            player.takeDamage(dmg);
            
        }
        
    }
    public void DestroyExplosion()
    {
        Destroy(gameObject);
    }
}
