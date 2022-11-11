using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIsGrounded : MonoBehaviour
{
    private bool isGrounded = true;
    public int groundLayer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == groundLayer) isGrounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == groundLayer) isGrounded = false;
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }
}
