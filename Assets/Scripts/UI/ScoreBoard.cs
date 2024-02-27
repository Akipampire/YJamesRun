using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    public TMP_Text ScoreText;
    private int[] board;

    void Start(){
        board[0] = PlayerPrefs.GetInt("first", 0);
        board[1] = PlayerPrefs.GetInt("second", 0);
        board[2] = PlayerPrefs.GetInt("third", 0);
        board[3] = PlayerPrefs.GetInt("fourth", 0);
        board[4] = PlayerPrefs.GetInt("fifth", 0);
        board[5] = PlayerPrefs.GetInt("sixth", 0);
        board[6] = PlayerPrefs.GetInt("seventh", 0);
        board[7] = PlayerPrefs.GetInt("eighth", 0);
        board[8] = PlayerPrefs.GetInt("ninth", 0);
        board[9] = PlayerPrefs.GetInt("tenth", 0);
        
        UpdateBoard();
        WriteBoard();
    }

    void UpdateBoard()
    {
        for (int i = 9; i >= 0; i--)
        {
            if (GameManager.EndGame.winner.score > board[i])
            {
                if (i < 9)
                {
                    for (int j = 9; j > i; j++)
                    {
                        board[j] = board[j-1];
                    }
                }
                board[i] = GameManager.EndGame.winner.score;
                break;
            }
        }
    }

    void WriteBoard()
    {
        for (int i = 0; i < 10; i++)
        {
            ScoreText.text += board[i] + "\n";
        }
    }
}