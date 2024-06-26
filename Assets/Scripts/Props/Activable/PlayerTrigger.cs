using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] Activable script;

    public void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == Mathf.Log(GameManager.PlayerLayer,2))  script.Activate(other.gameObject);
    }
}
