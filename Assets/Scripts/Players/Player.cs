using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : PawnBase
{
    [Space(20)]
    [Header("------------------------------------------------")]
    [Space(20)]
    [SerializeField] private float slowOnHitPercentage = 0.1f;
    [SerializeField] private float recoverPercentage = 0.03f;
    public void OnHit(string type)
    {
        if (type == "slide") {
            if (!isSliding) Hited();
        }else if(type == "jump") {
            if (!isJumping) Hited();
        }
        else {
            Hited();
        }
    }
    private void Hited() {
        slowness = slowOnHitPercentage;
    }
    private void FixedUpdate()
    {
        if (slowness != 0) 
            slowness = Mathf.Max(0f, slowness - (recoverPercentage / 60));
        if (transform.position.y < 0f) transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }
}
    