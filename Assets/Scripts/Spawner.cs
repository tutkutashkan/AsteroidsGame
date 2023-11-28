using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public Asteroid asteroidPrefab;
    //public Player playerPrefab;
    public Health healthSprite;
    public float trajectoryVariance = 15.0f;
    public float Rate = 4.0f;
    public float meteorRate = 10.0f;
    public int Asteroidamount = 1;
    public int Meteoramount = 1;
    public float Distance = 9.0f;

    private void Start()
    {
        // calling the functions every 2s
        InvokeRepeating(nameof(Asteroidspawn),Rate,Rate);
        InvokeRepeating(nameof(Healthspawn),meteorRate,meteorRate);
    }

    private void Asteroidspawn()
    {
        for (int i = 0; i < Asteroidamount; i++)
        {
            // creating a random direction
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * Distance;
            Vector3 spawnPoint = transform.position + spawnDirection;
            
            // spawning the asteroid in 15 and -15 degrees
            float variance = Random.Range(-trajectoryVariance,trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);
            
            Asteroid asteroid = Instantiate(asteroidPrefab,spawnPoint,rotation);
            asteroid.size = Random.Range(asteroid.minSize,asteroid.maxSize);
            asteroid.SetTrajectory(rotation * -spawnDirection);
        }
    }

    private void Healthspawn()
    {
        for (int i = 0; i < Meteoramount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * Distance;
            Vector3 spawnPoint = transform.position + spawnDirection;
            
            float variance = Random.Range(-trajectoryVariance,trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);
            
            Health health = Instantiate(healthSprite,spawnPoint,rotation);
            health.SetTrajectory(rotation * -spawnDirection);
        }
    }

    // every 5 levels the amount of asteroid spawned in 2s is increased 1 more.
    public void Increaseamount()
    {
        Asteroidamount += 1;
        Meteoramount += 1;
    }
}
