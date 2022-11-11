using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnBase : MonoBehaviour
{
    [Header("Generic Variable")]
    //
    public Rigidbody currentRigidbody;
    //
    public float slowness = 0f;
    public float jumpForce = 0f;
    //Boolean
    public bool isSliding = false;
    public bool isMoving = false;
    public bool isJumping = false;
    public bool isGrounded = false;
}
