using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ESQUIVE_TYPE {
    NOT_ESQUIVABLE = 0,
    ROLL = 1,
    JUMP = 2,
    ACTION = 3,
}
public class Obstacle : MonoBehaviour {
    [SerializeField] public ESQUIVE_TYPE[] typeEsquive;
    [SerializeField] public BoxCollider toDesactivate;
	private List<Player> playersHit = new List<Player>();

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == Mathf.Log(GameManager.PlayerLayer.value, 2)) {
			Player playerHit = other.GetComponent<Player>();
            if (playersHit.Count == 0 || !playersHit.Contains(playerHit)) { //empêcher de se prendre plusieurs fois l'obstacle
				GameManager.Instance.PlaySFX(SFXPlayer.SFX_TYPE.Hit);
				playersHit.Add(playerHit);
                playersHit.Last().OnHit(typeEsquive);
                if (toDesactivate) toDesactivate.enabled = false;
			}
		}
    }
	private void OnTriggerExit(Collider other) {
		if (toDesactivate) toDesactivate.enabled = true;
	}
}
