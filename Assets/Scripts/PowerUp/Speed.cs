using UnityEngine;

public class Speed : PowerUP
{
    [SerializeField] float speed_duration = 3f;
    [SerializeField] float speed_boost = 3f;
    public void Start() {
        GameManager.SPEED_BOOST_DURATION = speed_duration;
    }
    public override void Activate(GameObject trigger = null) {
        trigger.GetComponent<PlayerPowerUp>().AddPowerUpToInventory(this);
        Destroy(gameObject);
    }

    public override void OnUse(Player user) {
        user.GetComponent<PlayerMovement>().forwardSpeed *= speed_boost;//GameManager.Instance.infiniteForward.maxPlayerSpeed * speed_boost
        GameManager.Instance.ResetSpeed(user);
    }
}
