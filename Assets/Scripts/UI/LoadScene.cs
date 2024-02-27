using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] public static Player LeftPlayer;
    [SerializeField] public static Player RightPlayer;
    [SerializeField] float TimeBeforeListen;
    private IDisposable listen;
    public static int scoreLeft = LeftPlayer.score;
    public static int scoreRight = RightPlayer.score;
    private void OnEnable() {
        StartCoroutine(WaitBeforeListen());
    }

    private IEnumerator WaitBeforeListen() {
        yield return new WaitForSeconds(TimeBeforeListen);
        listen = InputSystem.onAnyButtonPress.Call(ctrl => LoadMenu());
    }


    private void LoadMenu() {
        listen.Dispose();
        SceneManager.LoadScene(1);
    }
}
