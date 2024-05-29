using UnityEngine;
using UnityEngine.InputSystem;
using utils;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Player thisPawn;
    [SerializeField] private float sideSpeed;
    public float currentForwardSpeed;
    [SerializeField] private float proximity = 0.1f;
    [SerializeField] public int currentLane;
    [SerializeField] public int[] lanesXCoordinate;
    public int xDirection = 0; 
    public float forwardSpeed = 0.04F; 

    [SerializeField] private float speedIncreaseRate = 0.05f;

    public float ForwardSpeed
    {
        get { return forwardSpeed; }
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

        currentForwardSpeed = forwardSpeed + (speedIncreaseRate * Mathf.Pow(Time.fixedDeltaTime, 1));
        thisPawn.currentRigidbody.velocity = new Vector3(xDirection * sideSpeed, thisPawn.currentRigidbody.velocity.y, currentForwardSpeed * (1 - thisPawn.slowness));
        if (thisPawn.life <= 0) thisPawn.currentRigidbody.velocity = Vector3.zero;

        if (xDirection == 0 && transform.position.x != lanesXCoordinate[currentLane])
            transform.position = new Vector3(lanesXCoordinate[currentLane], transform.position.y, transform.position.z);
    }

    private void StopMoving()
    {
        thisPawn.animator.SetInteger("direction", 0);
        thisPawn.isMoving = false;
        transform.position = transform.position.UpdateAxis(lanesXCoordinate[currentLane], VectorAxis.X);
        xDirection = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") || other.gameObject.layer == LayerMask.NameToLayer("Ignoring")) 
        {
            Player otherPlayer = other.gameObject.GetComponent<Player>();
            PlayerMovement otherPM = other.gameObject.GetComponent<PlayerMovement>();

            if (otherPlayer.isMoving && otherPM.xDirection == -1 && currentLane == 1)
            {
                xDirection = -1;
                currentLane = 0;
            }
            else if (otherPlayer.isMoving && otherPM.xDirection == 1 && currentLane == 1)
            {
                xDirection = 1;
                currentLane = 2;
            }
            else if (otherPlayer.isMoving && otherPM.xDirection == 1 && currentLane == 2)
            {
                otherPM.xDirection = -1;
                otherPM.currentLane = 1;
            }
            else if (otherPlayer.isMoving && otherPM.xDirection == -1 && currentLane == 0)
            {
                otherPM.xDirection = 1;
                otherPM.currentLane = 1;
            }
        }
    }
}