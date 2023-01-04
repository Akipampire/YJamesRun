using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnBase : MonoBehaviour {
    [Header("Generic Variable")]
    //identity
    public string name;
    //
    public Rigidbody currentRigidbody;
    //Life
    [SerializeField] public int life;
    //
    public float slowness = 0f;
    public float jumpForce = 0f;
    //Boolean
    public bool isSliding = false;
    public bool isMoving = false;
    public bool isJumping = false;
    public bool isGrounded = false;
}
