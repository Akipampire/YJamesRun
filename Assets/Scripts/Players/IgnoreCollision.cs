using System;
using System.Collections;
using TMPro;
using UnityEngine;
using utils;

public class IgnoreCollision : MonoBehaviour
{
    [SerializeField] private CapsuleCollider otherPlayer;
    [SerializeField] private CapsuleCollider Player;
    [SerializeField] private Rigidbody rb;
    void Start()
    {
        Physics.IgnoreCollision(otherPlayer,Player,true);
    }

    public void Invicibility(float duration, Invicibility_Sphere sphere) {
        rb.useGravity = false;
        Player.enabled = false;
        StartCoroutine(InvicibilityEnd(duration,sphere));
    }

    private IEnumerator InvicibilityEnd(float duration, Invicibility_Sphere sphere) {
        yield return new WaitForSeconds(duration);
        Player.enabled = true;
        rb.useGravity = true;
        Destroy(sphere.gameObject);
    }
}
