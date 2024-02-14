using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border_color : MonoBehaviour
{
    public GameObject playerleft;
    public GameObject playerright;
    // Start is called before the first frame update
    void Start()
    {
        ChangeColor(playerleft, Color.blue);
        ChangeColor(playerright, Color.red);
    }

    void ChangeColor(GameObject player, Color newColor)
    {
        Renderer renderer = player.GetComponent<Renderer>();
        if (renderer != null && renderer.material != null)
        {
            renderer.material.color = newColor;
        }
        else
        {
            Debug.LogError("Le GameObject ne contient rien");
        }
    }
}
