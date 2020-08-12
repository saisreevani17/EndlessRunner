using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int score = 0;
    // Start is called before the first frame update

    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int numberGameSession = FindObjectsOfType<GameSession>().Length;
        if(numberGameSession>1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int scoreValue)
    {
        score = score + scoreValue;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
