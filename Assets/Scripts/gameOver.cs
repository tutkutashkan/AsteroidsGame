using UnityEngine;
using UnityEngine.UI;

public class gameOver : MonoBehaviour
{
    public  GameManager game;

    public Text gameOverText;

    void Update()
    {  
        if (game.lives == 0)
        {
            gameOverText.text = "Game Over";
        }
    }
}
