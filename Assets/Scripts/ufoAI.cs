 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using CodeMonkey.Utils;

public class ufoAI : MonoBehaviour {

    private enum State {
        Roaming,
        ChaseTarget,
        ShootingTarget,
        //GoingBackToStart,
    }
    private Asteroid asteroid;
    private Vector3 startingPosition;
    private Vector3 roamPosition;
    private float nextShotTime;
    public GameObject ufoBullet;
    public float timeBetweenShots;
    public float speed;
    public Transform target;
    public float minimumDistance;
    private State state;
    public bool shooting = true;

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

            float attackRange = 3.0f;
            if (Vector3.Distance(transform.position, target.position) < attackRange) {
                // attack
                state = State.ShootingTarget;
            }
    
            float stopChaseDistance = 5f;
            if (Vector3.Distance(transform.position, target.position) > stopChaseDistance) {
                // Too far, stop chasing
                state = State.Roaming;
            }
            break;
            
        case State.ShootingTarget:

            if(Time.time > nextShotTime && shooting){
                Instantiate(ufoBullet,transform.position,Quaternion.identity);
                FindObjectOfType<AudioManager>().Play("Shooting");
                nextShotTime = Time.time + timeBetweenShots;
            }

            float goBack = 2.0f;
            if (Vector3.Distance(transform.position, target.position) < goBack) {
                // Retreat
                transform.position = Vector2.MoveTowards(transform.position, target.position, (-speed*2) * Time.deltaTime);
            }

            if (Vector3.Distance(transform.position, target.position) > 3.0f) {
                state = State.ChaseTarget;
            }
            break;
        }
    }

    private void FindTarget() {

        float targetRange = 5f;
        if (Vector2.Distance(transform.position, target.position) < targetRange) {
            // Player within target range
            state = State.ChaseTarget;
        }
    }

    private Vector3 GetRoamingPosition(){
        Vector3 nextDirection = Random.insideUnitCircle.normalized;
        Vector3 nextPoint = nextDirection * Random.Range(14f,9.0f);
        return nextPoint;
    }

    public void StopShooting()
    {
        shooting = false;
    }
    public void StartShooting()
    {
        shooting = true;
    }

}
