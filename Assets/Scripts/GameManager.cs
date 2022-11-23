using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public static LayerMask PlayerLayer;
    [SerializeField] private LayerMask _PlayerLayer;


    private void Start() {
        PlayerLayer = _PlayerLayer;
    }
}
