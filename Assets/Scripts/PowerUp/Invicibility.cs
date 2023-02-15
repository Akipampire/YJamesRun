using UnityEngine;
using utils;

public class Invicibility : PowerUP
{
    [SerializeField] float invicibility_duration = 3f;
    [SerializeField] Invicibility_Sphere sphere;
    public override void Activate(GameObject trigger = null) {
        trigger.GetComponent<PlayerPowerUp>().AddPowerUpToInventory(this);
        Destroy(gameObject);
    }

    public override void OnUse(Player user) {
        user.GetComponent<IgnoreCollision>().Invicibility(invicibility_duration, 
            Instantiate(sphere,user.transform.position.Add(new Vector3(0.2f,1f,0f)), Quaternion.identity, user.transform));
    }
}
