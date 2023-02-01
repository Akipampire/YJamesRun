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
        if (!context.performed || thisPawn.isRolling || !IsGrounded()) return;
        thisPawn.currentRigidbody.AddForce(Vector3.up * jumpSpeed,ForceMode.Force);
        thisPawn.isJumping = true;
        thisPawn.animator.SetBool("isJumping", true);
        thisPawn.isGrounded = false;
        thisPawn.animator.SetBool("isGrounded", false);
        StartCoroutine(Jump());
    }
    private IEnumerator Jump()
    {
        yield return new WaitForSeconds(0.33f);
        thisPawn.isJumping = false;
        thisPawn.animator.SetBool("isJumping", false);
    }
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround + 0.001f,LayerHit );
    }
    private void FixedUpdate() 
    {
        if(!thisPawn.isGrounded && !thisPawn.isJumping && IsGrounded())
        {
            thisPawn.isGrounded = true;
            thisPawn.animator.SetBool("isGrounded", true);
            thisPawn.isJumping = false;
        }
    }
}
