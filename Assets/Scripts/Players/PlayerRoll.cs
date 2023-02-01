using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRoll : MonoBehaviour
{
    [SerializeField] private Player thisPawn;
    [SerializeField] private float instaFallSpeed = 30f;
    [SerializeField] private float slideDuration = 1f;
    [SerializeField] private Camera cam;
    public void OnSlide(InputAction.CallbackContext context)
    {
        if (!context.performed || thisPawn.isRolling) return;
        if (thisPawn.isJumping)
        {
            thisPawn.currentRigidbody.AddForce(Vector3.down * instaFallSpeed,ForceMode.Force);
            thisPawn.isJumping = false;
            thisPawn.animator.SetBool("isJumping", false);
        }
        ChangeRollState(true);
        StartCoroutine(Sliding());
    }
    private IEnumerator Sliding()
    {
        yield return new WaitForSeconds(slideDuration);
        ChangeRollState(false);
        thisPawn.animator.SetInteger("direction", 0);
    }
    void ChangeRollState(bool newState) {
        thisPawn.isRolling = newState;
        thisPawn.animator.SetBool("isRolling", newState);
    }
}
