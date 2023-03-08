using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Obstacle : MonoBehaviour {
    [SerializeField] private string typeEsquive;
    private List<Player> playersHit = new List<Player>();
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == Mathf.Log(GameManager.PlayerLayer.value, 2)) {
			Player playerHit = other.GetComponent<Player>();
            if (playersHit.Count == 0 || !playersHit.Contains(playerHit)) { //empêcher de se prendre plusieurs fois l'obstacle
				GameManager.Instance.PlaySFX(SFXPlayer.SFX_TYPE.Hit);
				playersHit.Add(playerHit);
                playersHit.Last().OnHit(typeEsquive);
			}
		}
    }
}
