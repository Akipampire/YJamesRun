using System.Collections;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    [SerializeField] private BoxCollider otherPlayer;
    [SerializeField] private BoxCollider Player;
    [SerializeField] private Rigidbody rb;
    private Coroutine launchedInvincibility = null;
    private Invicibility_Sphere previousSphere;
    void Start()
    {
        Physics.IgnoreCollision(otherPlayer, Player, true);
    }

    public Coroutine Invicibility(float duration, Invicibility_Sphere sphere)
    {
        Player.gameObject.layer = LayerMask.NameToLayer("Ignoring");
        Physics.IgnoreLayerCollision(Player.gameObject.layer, LayerMask.NameToLayer("Obstacle"), true);
        Physics.IgnoreLayerCollision(Player.gameObject.layer, LayerMask.NameToLayer("Default"), true);
        if (launchedInvincibility != null)
        {
            if (previousSphere) Destroy(previousSphere.gameObject);
            StopCoroutine(launchedInvincibility);
            launchedInvincibility = null;
        }
        previousSphere = sphere;
        return launchedInvincibility = StartCoroutine(InvicibilityEnd(duration, sphere));
    }

    private IEnumerator InvicibilityEnd(float duration, Invicibility_Sphere sphere)
    {
        yield return new WaitForSeconds(duration);
        Player.gameObject.layer = LayerMask.NameToLayer("Player");
		Physics.IgnoreLayerCollision(Player.gameObject.layer, LayerMask.NameToLayer("Default"), false);
		Physics.IgnoreLayerCollision(Player.gameObject.layer, LayerMask.NameToLayer("Obstacle"), false);        
        Destroy(sphere.gameObject);
    }
}
