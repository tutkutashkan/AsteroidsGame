using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public  GameManager game;

    public Text scoreText;


    // Update is called once per frame
    void Update()
    {
        scoreText.text = game.score.ToString();
    }
}
