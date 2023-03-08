using System.Collections;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    [SerializeField] private CapsuleCollider otherPlayer;
    [SerializeField] private CapsuleCollider Player;
    [SerializeField] private Rigidbody rb;
    private Coroutine launchedInvincibility = null;
	private Invicibility_Sphere previousSphere;
	void Start()
    {
        Physics.IgnoreCollision(otherPlayer,Player,true);
    }

    public Coroutine Invicibility(float duration, Invicibility_Sphere sphere) {
		Physics.IgnoreLayerCollision(Player.gameObject.layer, LayerMask.NameToLayer("Obstacle"), true);
        if (launchedInvincibility != null) {
            if (previousSphere) Destroy(previousSphere.gameObject);
            StopCoroutine(launchedInvincibility);
            launchedInvincibility = null;
		}
        previousSphere = sphere;
		return launchedInvincibility = StartCoroutine(InvicibilityEnd(duration,sphere));
    }

    private IEnumerator InvicibilityEnd(float duration, Invicibility_Sphere sphere) {
        yield return new WaitForSeconds(duration);
		Physics.IgnoreLayerCollision(Player.gameObject.layer, LayerMask.NameToLayer("Obstacle"), false);
        Destroy(sphere.gameObject);
    }
}
