using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Player : PawnBase
{
    [Space(20)]
    [Header("------------------------------------------------")]
    [Space(20)]
    [SerializeField] public Animator animator;
    [SerializeField] private float slowOnHitPercentage = 0.1f;
    [SerializeField] private float recoverPercentage = 0.03f;
    [SerializeField] public Text scoreText;

    public void OnHit(ESQUIVE_TYPE[] type)
    {
        bool shouldBeHit = true;
        if (!type.Contains(ESQUIVE_TYPE.NOT_ESQUIVABLE)) {
            if (type.Contains(ESQUIVE_TYPE.ROLL))
                if (isRolling) shouldBeHit = false;
		    if(shouldBeHit && type.Contains(ESQUIVE_TYPE.JUMP)) 
                if (isJumping) shouldBeHit = false;
        }
        if (shouldBeHit) Hited();

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
        scoreText.text = "Score : " + score;
        //Hit Slowness gestion
        if (slowness != 0) 
            slowness = Mathf.Max(0f, slowness - (recoverPercentage / 60));
        //Side Movement
        if (transform.position.y < 0f) 
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }
}
