using UnityEngine;
public class Player : MonoBehaviour
{
    public static Player Instance {get;private set;}
    public Bullet bulletPrefab;
    private new Rigidbody2D rigidbody;
    public float thrust = 3.0f;
    public float turn = 1.0f;
    public int livesagainstufo = 3;
    private bool thrusting;
    private bool backwards;
    private float turnDirection;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // player movements
        thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        backwards = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            turnDirection = 1f;
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            turnDirection = -1f;
        } else {
            turnDirection = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
            Shoot();
            FindObjectOfType<AudioManager>().Play("Shooting");
        }
        if(livesagainstufo == 0){
            toDeath();
        }
    }

    private void FixedUpdate()
    {
        if (thrusting) {
            rigidbody.AddForce(transform.up * thrust);
        }
        if (turnDirection != 0f) {
            rigidbody.AddTorque(turn * turnDirection);
        }
        if (backwards){
            rigidbody.AddForce(transform.up * (-thrust));
        }
    }

    private void Shoot()
    {
        // creating bullets when player presses the space button or left click
        Bullet bullet = Instantiate(bulletPrefab, transform.position,transform.rotation);
        bullet.Project(transform.up);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.tag == "ufoBullet"){
            livesagainstufo = livesagainstufo -1;
        }
        // checking if player collided with an asteroid
        if (collision.gameObject.tag == "Asteroid")
        {
            toDeath();
        }
    }

    private void toDeath(){

        livesagainstufo = 3;
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = 0.0f;

        gameObject.SetActive(false);

        FindObjectOfType<GameManager>().Death();
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        FindObjectOfType<ufoAI>().StopShooting();

    }

    public Vector3 GetPosition() {
        return transform.position;
    }

}