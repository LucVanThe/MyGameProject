using UnityEngine;

public class playerbullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 24f;
    [SerializeField] private float timeDestroy = 0.5f;
    [SerializeField] public float bulletdamage = 10f;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, timeDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        moveBullet();
    }
    void moveBullet()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Enemy2"))
    //    {
    //        Enemy2 enemy2 = collision.GetComponent<Enemy2>();
    //        if (enemy2 != null)
    //        {
    //            enemy2.TakeDamage();
    //        }
    //        Destroy(gameObject);
    //    }
    //}
}
