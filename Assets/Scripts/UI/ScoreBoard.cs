using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    public TMP_Text ScoreText;
    public TMP_Text NamesText;
    private int[] board = new int[10];

    void Start(){
        //récupère les anciens scores enregistrés dans les playerprefs
        //prend la valeur de base 0 si les scores n'existent pas encore
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
        for (int i = 0; i <= 9; i++) //on part du début sinon y a que le dernier score qui est remplacé (et de toutes facons si il faut remplacer le premier score, on veut pas remplacer les autres)
        {
            if (PlayerPrefs.GetInt("actual") > board[i]) //si le score du gagnant est supérieur
            {
                if (i < 9)
                {
                    for (int j = 9; j > i; j--) //décale les scores inférieurs au nouveau record
                    {
                        board[j] = board[j-1];
                    }
                }
                board[i] = PlayerPrefs.GetInt("actual"); //remplace le score par un nouveau
                break;
            }
        }
        //modifie ou crée la valeur des scores dans les playerprefs
        PlayerPrefs.SetInt("first", board[0]);
        PlayerPrefs.SetInt("second", board[1]);
        PlayerPrefs.SetInt("third", board[2]);
        PlayerPrefs.SetInt("fourth", board[3]);
        PlayerPrefs.SetInt("fifth", board[4]);
        PlayerPrefs.SetInt("sixth", board[5]);
        PlayerPrefs.SetInt("seventh", board[6]);
        PlayerPrefs.SetInt("eighth", board[7]);
        PlayerPrefs.SetInt("ninth", board[8]);
        PlayerPrefs.SetInt("tenth", board[9]);
        PlayerPrefs.Save();
    }

    void WriteBoard()
    {
        for (int i = 0; i < 10; i++)
        {
            NamesText.text += i+1 + ".\n";
            ScoreText.text += board[i] + "\n";
        }
    }
}