 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using CodeMonkey.Utils;

public class AsteroidAI : MonoBehaviour {
     private enum State {
        Roaming,
        ChaseTarget,
    }

    //private Asteroid asteroid;
    private Vector3 startingPosition;
    private Vector3 roamPosition;
    private float nextShotTime;
    public GameObject ufoBullet;
    public float timeBetweenShots;
    public float speed;
    public Transform target;
    public float minimumDistance;
    private State state;

    private void Awake() {

        state = State.Roaming;
    }

    private void Start() {
        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();
    }

    private void Update() {
        switch (state) {
        default:
        case State.Roaming:

            transform.position = Vector2.MoveTowards(transform.position, roamPosition, speed * Time.deltaTime);

            float reachedPositionDistance = 3f;
            if (Vector2.Distance(transform.position, roamPosition) < reachedPositionDistance) {
                // Reached Roam Position
                roamPosition = GetRoamingPosition();
            }
            FindTarget();
            break;

        case State.ChaseTarget:

            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            
            float stopChaseDistance = 5f;
            if (Vector3.Distance(transform.position, target.position) > stopChaseDistance) {
                // Too far, stop chasing
                state = State.Roaming;
            }
            break;
        }
    }

    private void Shooting(){
        Instantiate(ufoBullet,transform.position, Quaternion.identity);
    }

    private void FindTarget() {

        float targetRange = 5f;
        if (Vector2.Distance(transform.position, target.position) < targetRange) {
            // Player within target range
            state = State.ChaseTarget;
        }
    }

    private Vector3 GetRoamingPosition(){
        //Vector3 nextDirection = (UnityEngine.Random.Range(-1f,1f), UnityEngine.Random.Range(-1f,1f)).normalized;
        Vector3 nextDirection = Random.insideUnitCircle.normalized;
        Vector3 nextPoint = nextDirection * Random.Range(13f,8.0f);
        return nextPoint;
    }

}
