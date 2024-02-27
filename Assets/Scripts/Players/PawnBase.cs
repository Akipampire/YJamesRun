using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnBase : MonoBehaviour {
    [Header("Generic Variable")]
    //identity
    public string playerName;
    //
    public Rigidbody currentRigidbody;
    //Life
    [SerializeField] public int life;
    [SerializeField] public int score;
    [SerializeField] public int coins;
    //
    public float slowness = 0f;
    //Boolean
    public bool isRolling = false;
    public bool isMoving = false;
    public bool isJumping = false;
    public bool isGrounded = false;
}
