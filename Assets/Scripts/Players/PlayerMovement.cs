using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections; // Add this to fix the IEnumerator error

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Player thisPawn;
    [SerializeField] private float sideSpeed;
    [SerializeField] private float proximity = 0.1f;
    [SerializeField] private int currentLane;
    [SerializeField] public int[] lanesXCoordinate;
    public int xDirection = 0;
    public float forwardSpeed = 5;
    public float speedIncreasePerSecond = 0.5f; // How much to increase speed by each second

    private void Start()
    {
        StartCoroutine(IncreaseSpeedOverTime());
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (!context.performed || thisPawn.isMoving) return;
        var wantedDirection = (int)context.ReadValue<Vector2>().x;
        if (wantedDirection + currentLane < 0 || wantedDirection + currentLane >= lanesXCoordinate.Length) return;
        thisPawn.isMoving = true;
        currentLane += wantedDirection;
        if (thisPawn.isRolling) SideRoll(wantedDirection);
        xDirection = wantedDirection;
    }

    void SideRoll(int dir)
    {
        thisPawn.animator.SetInteger("direction", dir);
    }

    private void FixedUpdate()
    {
        if (thisPawn.isMoving && Mathf.Abs(lanesXCoordinate[currentLane] - transform.position.x) < proximity)
            StopMoving();
        else if ((xDirection == 1 && transform.position.x > lanesXCoordinate[currentLane]) || (xDirection == -1 && transform.position.x < lanesXCoordinate[currentLane]))
            StopMoving();
        thisPawn.currentRigidbody.velocity = new Vector3(xDirection * sideSpeed, thisPawn.currentRigidbody.velocity.y, forwardSpeed * (1 - thisPawn.slowness));
        if (thisPawn.life <= 0) thisPawn.currentRigidbody.velocity = Vector3.zero;
    }

    private void StopMoving()
    {
        thisPawn.animator.SetInteger("direction", 0);
        thisPawn.isMoving = false;
        transform.position = transform.position.UpdateAxis(lanesXCoordinate[currentLane], VectorAxis.X);
        xDirection = 0;
    }

    private IEnumerator IncreaseSpeedOverTime()
    {
        while (true)
        {
            forwardSpeed += speedIncreasePerSecond;
            yield return new WaitForSeconds(1f);
        }
    }
}
