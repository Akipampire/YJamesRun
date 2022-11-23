using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class coinPickedUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == Mathf.Log(GameManager.PlayerLayer.value, 2)) {
            other.GetComponent<PlayerScoring>().PlayerTookCoin();
            Destroy(gameObject);
        }
    }
}
