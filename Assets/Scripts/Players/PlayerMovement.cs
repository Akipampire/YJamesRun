using UnityEngine;
using UnityEngine.InputSystem;
using utils;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Player thisPawn;
    public GameObject leftPlayer;
    public GameObject rightPlayer;
    [SerializeField] private float sideSpeed;
    [SerializeField] private float proximity = 0.1f;
    [SerializeField] public int currentLane;
    [SerializeField] public int[] lanesXCoordinate;
    public int xDirection = 0;
    public float forwardSpeed = 0.04F;
    private int wantedDirection = 0;

    [SerializeField] private float speedIncreaseRate = 0.05F;

    public float ForwardSpeed
    {
        get { return forwardSpeed; }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (thisPawn.isMoving) return;

        wantedDirection = (int)context.ReadValue<Vector2>().x; // renvoie -1 ou +1 en fonction de si on va a droite ou a gauche

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



    public void FixedUpdate()
    {
        if (thisPawn.isMoving && Mathf.Abs(lanesXCoordinate[currentLane] - transform.position.x) < proximity)
            StopMoving();
        else if ((xDirection == 1 && transform.position.x > lanesXCoordinate[currentLane]) || (xDirection == -1 && transform.position.x < lanesXCoordinate[currentLane]))
            StopMoving();

        if (!thisPawn.isMoving && transform.position.x != lanesXCoordinate[currentLane])
        {
            transform.position = new Vector3(lanesXCoordinate[currentLane], 0, transform.position.z);
        }

        float currentForwardSpeed = forwardSpeed + (speedIncreaseRate * Mathf.Pow(Time.time, 1));
        thisPawn.currentRigidbody.velocity = new Vector3(xDirection * sideSpeed, thisPawn.currentRigidbody.velocity.y, currentForwardSpeed * (1 - thisPawn.slowness));
        if (thisPawn.life <= 0) thisPawn.currentRigidbody.velocity = Vector3.zero;
    }

    private void StopMoving()
    {
        thisPawn.animator.SetInteger("direction", 0);
        thisPawn.isMoving = false;
        transform.position = transform.position.UpdateAxis(lanesXCoordinate[currentLane], VectorAxis.X);
        xDirection = 0;

    }



    void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) //(ignoring)
        {
            Player Other = collision.gameObject.GetComponent<Player>();
            PlayerMovement TAMERE = collision.gameObject.GetComponent<PlayerMovement>();
            
            if (Other.isMoving)
            {
                if (TAMERE.xDirection == -1 && currentLane == 1)
                {
                    xDirection = -1;
                    currentLane -= 1;
                }
                else if (TAMERE.xDirection == 1 && currentLane == 1)
                {
                    xDirection = 1;
                    currentLane += 1;
                }
            }
        }
    }
}