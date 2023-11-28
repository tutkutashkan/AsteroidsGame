using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public  GameManager game;
    public Asteroid asteroid;
    public Spawner spawner;
    public int level = 1;

    public int is5 = 1;
    public int score = 1000;

    public Text levelText;

    // Update is called once per frame
    void Update()
    {  
        if (game.score >= score*1.5) 
        {
            score = game.score;
            level += 1;
            is5 += 1;
            FindObjectOfType<Asteroid>().Increasespeed();
        }
        // every 5 level call the increaseamount function in Spawner class adn IncreaseHealth function in Ufo class
        if (is5 == 5 )
        {
            is5 = 0;
            FindObjectOfType<Spawner>().Increaseamount();
            FindObjectOfType<Ufo>().IncreaseHealth();
        }
        levelText.text = ("Level: " + level.ToString());
    }

}
