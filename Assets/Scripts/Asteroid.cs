using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite[] sprites;

    public Player player;
    public Level now;
    public float size = 1.0f;
    public float minSize = 0.1f;
    public float maxSize = 0.4f;
    public float speed = 6.0f;
    public float Lifetime = 30.0f;
    private SpriteRenderer spriteRenderer;
    private new Rigidbody2D rigidbody;

    private void Awake() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        spriteRenderer.sprite = sprites[Random.Range(0,sprites.Length)];
        // gives asteroid an angle so that every asteroid can look different
        transform.eulerAngles = new Vector3(0.0f,0.0f,Random.value *360.0f);
        transform.localScale = Vector3.one * size;
        rigidbody.mass = size;
    }


    public void SetTrajectory(Vector2 direction)
    {
        rigidbody.AddForce(direction * speed);

        Destroy(gameObject, Lifetime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // check if asteroid collides with bullet
        if(collision.gameObject.tag == "Bullet")
        {
            // if the half of the asteroid is bigger than minimum size split it
            if ((size * 0.5f) > minSize)
            {
                Split();
                Split();
            }
            FindObjectOfType<GameManager>().AsteroidDestroyed(this);
            FindObjectOfType<AudioManager>().Play("AsteroidDeath");
            if(gameObject.tag == "Asteroid"){
                Destroy(gameObject);
            }
            else if(gameObject.tag == "AsteroidAI" ){
                gameObject.SetActive(false);
            }
        }
    }
    private void Split()

    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Asteroid half = Instantiate(this, position, this.transform.rotation);
        half.size = size *0.5f;
        half.SetTrajectory(Random.insideUnitCircle.normalized * 2);
    }

    //every level the speed of the meteor increased by 0.5
    public void Increasespeed()
    {
        speed += 0.5f;
    }
}
