using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    [SerializeField] private CapsuleCollider otherPlayer;
    [SerializeField] private CapsuleCollider Player;
    void Start()
    {
        Physics.IgnoreCollision(otherPlayer,Player,true);
    }

}
