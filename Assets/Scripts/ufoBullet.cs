using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ufoBullet : MonoBehaviour
{

    private new Rigidbody2D rigidbody;
    public float speed = 500.0f;
    public float maxLifetime = 4.0f;
    Vector3 targetPosition;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start(){
        targetPosition = FindObjectOfType<Player>().transform.position;
    }

    private void Update(){
        if(targetPosition != Vector3.zero){
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            }

        Destroy(gameObject, 3.0f);

        if(transform.position == targetPosition){
            Destroy(gameObject);
        }
        //rigidbody.AddForce(targetPosition*speed);
        //Destroy(gameObject, maxLifetime);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

}
