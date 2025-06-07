using UnityEngine;

public class Enemy : MonoBehaviour

{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float distanse = 3f;
    private Vector3 startpos;
    private bool moveRight = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float leftBound = startpos.x - distanse;
        float rightBound = startpos.x + distanse;
        if (moveRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if(transform.position.x>= rightBound)
            {
                moveRight = false;
                Flip();
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (transform.position.x <= leftBound)
            {
                moveRight = true;
                Flip();
            }
        }
    }
    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale=scale;

    }
}
