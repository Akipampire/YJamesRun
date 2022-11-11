using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSlide : MonoBehaviour
{
    [SerializeField] private Player thisPawn;
    [SerializeField] private float instaFallSpeed = 30f;
    
    public void OnSlide(InputAction.CallbackContext context)
    {
        if (!context.performed || thisPawn.isSliding) return;
        if (thisPawn.isJumping)
        {
            thisPawn.jumpForce = -instaFallSpeed;
        }
        thisPawn.isSliding = true;
    }

    private IEnumerator Sliding()
    {
        yield return null;
    }

    private void FixedUpdate()
    {
        if (thisPawn.isGrounded && thisPawn.isSliding)
        {
            thisPawn.isSliding = false;
            StartCoroutine(Sliding());
        }
    }
}
