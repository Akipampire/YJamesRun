using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Player thisPawn;
    [SerializeField] private float sideSpeed;
    [SerializeField] private float proximity =  0.1f;
    [SerializeField] private float forwardSpeed = 5;
    [SerializeField] private int[] lanesXCoordinate;
    public int xDirection = 0;
    private int currentLane;

    private void Start()
    {
        currentLane = (int)MathF.Ceiling(lanesXCoordinate.Length/2);
    }
    
    public void OnMovement(InputAction.CallbackContext context) {
        if (!context.performed || thisPawn.isMoving) return;
        var wantedDirection = (int)context.ReadValue<Vector2>().x;
        //Si l'input doit emmener sur une lane non existante
        if (wantedDirection + currentLane < 0 || wantedDirection + currentLane >= lanesXCoordinate.Length) return;
        thisPawn.isMoving = true;
        currentLane += wantedDirection;
        xDirection = wantedDirection;
    }

    private void FixedUpdate()
    {
        if (thisPawn.isMoving && Mathf.Abs(lanesXCoordinate[currentLane] - transform.position.x) < proximity)
        {
            thisPawn.isMoving = false;
            transform.position = new Vector3(lanesXCoordinate[currentLane],transform.position.y,transform.position.z);
            xDirection = 0;
        }
        thisPawn.currentRigidbody.velocity =  new Vector3(xDirection*sideSpeed, thisPawn.jumpForce,forwardSpeed*(1-thisPawn.slowness));
    }

    
}