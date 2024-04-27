using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    private IDisposable listen;

    private void Update() {
        if (Input.anyKey) {
            SceneManager.LoadScene(1);
        }
    }
}
