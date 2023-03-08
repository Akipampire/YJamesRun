using UnityEngine;
public class coinPickedUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == Mathf.Log(GameManager.PlayerLayer.value, 2)) {
			GameManager.Instance.PlaySFX(SFXPlayer.SFX_TYPE.Controller);
			other.GetComponent<PlayerScoring>().PlayerTookCoin();
            Destroy(gameObject);
        }
    }
}
