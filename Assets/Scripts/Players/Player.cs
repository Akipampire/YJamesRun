using System.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : PawnBase
{
    [Space(20)]
    [Header("------------------------------------------------")]
    [Space(20)]
    [SerializeField] public Animator animator;
    [SerializeField] private float slowOnHitPercentage = 0.1f;
    [SerializeField] private float recoverPercentage = 0.03f;
    [SerializeField] public TMP_Text scoreText;
    [SerializeField] public TMP_Text lifeText;
    public bool Invincible = false;
    private CapsuleCollider capsuleCollider;
    public bool shouldBeHit = true;
    public int hitCount = 0;

    public void OnHit(ESQUIVE_TYPE[] type)
    {
        if (!type.Contains(ESQUIVE_TYPE.NOT_ESQUIVABLE)) {
            if (shouldBeHit && type.Contains(ESQUIVE_TYPE.ROLL))
                if (isRolling) shouldBeHit = false;
		    if(shouldBeHit && type.Contains(ESQUIVE_TYPE.JUMP)) 
                if (isJumping) shouldBeHit = false;
        }
        if (shouldBeHit) {
            hitCount++;
            if (hitCount == 1) {
                Hited();
            }
        }
    }
    private void Hited() {
        Debug.Log("Hited");
        life--;
        slowness = slowOnHitPercentage;
        if (life <= 0){
            Die();
        } else {
            StartInvincibility();
        }
    }

    private Coroutine StartInvincibility() {
        capsuleCollider = gameObject.GetComponent<CapsuleCollider>();
        capsuleCollider.gameObject.layer = LayerMask.NameToLayer("Ignoring");
		Physics.IgnoreLayerCollision(capsuleCollider.gameObject.layer, LayerMask.NameToLayer("Obstacle"), true);
        Invincible = true;
        return StartCoroutine(EndInvincibility());
    }

    private IEnumerator EndInvincibility() {
        Debug.Log("Invincibility Start");
        yield return new WaitForSeconds(2.5f);
        Invincible = false;
        capsuleCollider = gameObject.GetComponent<CapsuleCollider>();
        capsuleCollider.gameObject.layer = LayerMask.NameToLayer("Player");
		Physics.IgnoreLayerCollision(capsuleCollider.gameObject.layer, LayerMask.NameToLayer("Obstacle"), false);
        hitCount = 0;
    }
    private void Die() {
        animator.SetBool("isDead", true);
    }
    private void FixedUpdate()
    {
        scoreText.text = "Score : " + score;
        lifeText.text = "Life : " + life;
        //Hit Slowness gestion
        if (slowness != 0) 
            slowness = Mathf.Max(0f, slowness - (recoverPercentage / 60));
        //Side Movement
        if (transform.position.y < 0f) 
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }
}
