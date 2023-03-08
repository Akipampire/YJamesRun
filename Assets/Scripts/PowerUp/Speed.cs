using UnityEngine;

public class Speed : PowerUP
{
    [SerializeField] float speed_duration = 3f;
    [SerializeField] float speed_boost = 2f;
    public void Start() {
        GameManager.SPEED_BOOST_DURATION = speed_duration;
    }
    public override void Activate(GameObject trigger = null) {
        trigger.GetComponent<PlayerPowerUp>().AddPowerUpToInventory(this);
        Destroy(gameObject);
    }

    public override void OnUse(Player user) {
        user.GetComponent<PlayerMovement>().forwardSpeed = GameManager.Instance.infiniteForward.maxPlayerSpeed * speed_boost;
        GameManager.Instance.PlaySFX(SFXPlayer.SFX_TYPE.SpeedUp);
        GameManager.Instance.ResetSpeed(user);
    }
}
