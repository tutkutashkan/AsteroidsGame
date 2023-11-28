using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ufo : MonoBehaviour
{
    // Start is called before the first frame update

    private new Rigidbody2D rigidbody;

    public float size = 1.0f;

    public float Health = 5.0f;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rigidbody.mass = size;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // check if asteroid collides with bullet
        if(collision.gameObject.tag == "Bullet")
        {

            Health = Health -1;
            if(Health == 0){
                rigidbody.velocity = Vector3.zero;
                rigidbody.angularVelocity = 0.0f;
                Health = 10f;
                gameObject.SetActive(false);
                FindObjectOfType<GameManager>().UfoDestroyed(this);
                FindObjectOfType<AudioManager>().Play("AsteroidDeath");
            }
        }
    }

    public void IncreaseHealth()
    {
        Health += 3.0f;
    }
}
