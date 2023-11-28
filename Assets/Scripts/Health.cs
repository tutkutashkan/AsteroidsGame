using UnityEngine;

public class Health : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private new Rigidbody2D rigidbody;
    
    public float speed = 50.0f;
    public float maxLifetime = 30.0f;

    private void Awake() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetTrajectory(Vector2 direction)
    {
        rigidbody.AddForce(direction * speed);

        Destroy(gameObject, maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if little meteros collide with a bullet player gains a live
        if(collision.gameObject.tag == "Bullet" )
        {
            FindObjectOfType<GameManager>().HealthGain(this);
            FindObjectOfType<AudioManager>().Play("HealthGain");
            Destroy(gameObject);
        }
    }
}
