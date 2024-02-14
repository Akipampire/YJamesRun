using UnityEngine;

public class Accelerate : PowerUP
{
    public override void Activate(GameObject trigger = null)
    {
        trigger.GetComponent<PlayerPowerUp>().AddPowerUpToInventory(this);
        Destroy(gameObject);
    }

    public override void OnUse(Player user)
    {

    }
}