using UnityEngine;

public class Player : PawnBase
{
    [Space(20)]
    [Header("------------------------------------------------")]
    [Space(20)]
    [SerializeField] public Animator animator;
    [SerializeField] private float slowOnHitPercentage = 0.1f;
    [SerializeField] private float recoverPercentage = 0.03f;
    private int tookCoin;
    public void OnHit(string type)
    {
        if (type == "slide") {
            if (!isRolling) Hited();
        }else if(type == "jump") {
            if (!isJumping) Hited();
        }
        else {
            Hited();
        }
    }
    private void Hited() {
        life--;
        slowness = slowOnHitPercentage;
        if (life <= 0) Die();
    }
    private void Die() {
        animator.SetBool("isDead", true);
    }
    private void FixedUpdate()
    {
        //Hit Slowness gestion
        if (slowness != 0) 
            slowness = Mathf.Max(0f, slowness - (recoverPercentage / 60));
        //Side Movement
        if (transform.position.y < 0f) 
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }
}
    