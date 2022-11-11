using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private Player thisPawn;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float fallSpeed;
    [SerializeField] private float distToGround = 1f;
    [SerializeField] private LayerMask LayerHit;
    private RaycastHit hit;
    
    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed || thisPawn.isSliding || !IsGrounded()) return;
        thisPawn.jumpForce = jumpSpeed;
        thisPawn.isJumping = true;
        thisPawn.isGrounded = false;
        StartCoroutine(Jump());
    }
    private IEnumerator Jump()
    {
        yield return new WaitForSeconds(0.33f);
        thisPawn.isJumping = false;
    }
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.001f,LayerHit );
    }
    private void FixedUpdate() 
    {
        if (!thisPawn.isJumping && IsGrounded())
        {
            thisPawn.isGrounded = true;
            thisPawn.jumpForce = 0;
            thisPawn.isJumping = false;
        } else { thisPawn.jumpForce -= jumpSpeed / fallSpeed; }
    }
}
