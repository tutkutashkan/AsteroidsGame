using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public Player player;
    public Ufo ufo;
    public float respawnTime = 3.0f;
    public int lives = 3;

    public int ufolives = 3;
    public int score = 0;

    public ParticleSystem explosion;
    private void Update()
    {

    }
    public void AsteroidDestroyed(Asteroid asteroid)
    {
        explosion.transform.position = asteroid.transform.position;
        explosion.Play();
        //scoring system
        if (asteroid.size < 0.2){
            score += 100;
        } else if (asteroid.size < 0.3){
            score += 50;
        } else {
            score += 25;
        }
    }

    public void UfoDestroyed(Ufo ufo)
    {
        explosion.transform.position = ufo.transform.position;
        explosion.Play();
        //scoring system
        score += 500;
        ufolives --;
        lives += 1;
        if (ufolives < 1 )
        {
        } else {
            Invoke(nameof(ufoRespawn),10f);
        }
    }

    public void HealthGain(Health health)
    {
        // gaining a health when bulllet collides with little meteors
        explosion.transform.position = health.transform.position;
        explosion.Play();
        lives += 1;
    }

    public void Death()
    {
        // player's death
        explosion.transform.position = player.transform.position;
        explosion.Play();
        lives --;
        if (lives < 1 )
        {
            // if player has more than 1 live respawn in 5s
            Invoke(nameof(gameOver),5.0f);
        } else {
            Invoke(nameof(Respawn),respawnTime);
        }
    }

    private void Respawn()
    {   
        player.transform.position = Vector3.zero;
        // changing the layer of the player to respawn which is undestructible and change it back in 3s
        player.gameObject.layer = LayerMask.NameToLayer("Respawn");
        player.gameObject.SetActive(true);
        FindObjectOfType<ufoAI>().StartShooting();
        Invoke(nameof(AfterRespawn), 3.0f);
    }

    private void ufoRespawn()
    {   
        Vector3 spawnDirection = Random.insideUnitCircle.normalized * 9.0f;
        Vector3 spawnPoint = spawnDirection;
        ufo.transform.position = spawnPoint;
        ufo.gameObject.SetActive(true);

    }


    private void AfterRespawn()
    {
        player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void gameOver()
    {
        FindObjectOfType<PauseMenu>().Pause();
    }

}
