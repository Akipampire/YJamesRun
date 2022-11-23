using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSlide : MonoBehaviour
{
    [SerializeField] private Player thisPawn;
    [SerializeField] private float instaFallSpeed = 30f;
    [SerializeField] private float slideDuration = 1f;
    [SerializeField] private Camera cam;
    public void OnSlide(InputAction.CallbackContext context)
    {
        if (!context.performed || thisPawn.isSliding) return;
        if (thisPawn.isJumping)
        {
            thisPawn.jumpForce = -instaFallSpeed;
        }
        thisPawn.isSliding = true;
        StartCoroutine(Sliding());
    }

    private IEnumerator Sliding()
    {
        yield return new WaitForSeconds(slideDuration);
        thisPawn.isSliding = false;
    }
}
