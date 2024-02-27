using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;
public class EndGame : MonoBehaviour
{
    [SerializeField] GameObject childrens;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] float TimeBeforeListen;
    public Player winner;
    public Player loser;
    private IDisposable listen;
    private void OnEnable() {
        childrens.SetActive(true);
        text.text = winner.playerName + " Win !";
        StartCoroutine(WaitBeforeListen());
    }

    private IEnumerator WaitBeforeListen() {
        yield return new WaitForSeconds(TimeBeforeListen);
        listen = InputSystem.onAnyButtonPress.Call(ctrl => LoadMenu());
    }

    private void OnDisable() {
        childrens.SetActive(false);
    }

    private void LoadMenu() {
        listen.Dispose();
        SceneManager.LoadScene(0);
    }
}
