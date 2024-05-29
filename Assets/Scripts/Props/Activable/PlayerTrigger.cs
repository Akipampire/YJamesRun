using UnityEngine;
using System.Collections;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] Activable script;
    private Collider Player;

    public void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player") || other.gameObject.layer == LayerMask.NameToLayer("Ignoring"))
        {
            Player = other;
            IgnorePUpStart();
            script.Activate(other.gameObject);  
        } 
        else if (other.gameObject.layer == LayerMask.NameToLayer("NoPUp"))
        {
            gameObject.SetActive(false);
        }
    }

    private void IgnorePUpStart()
    {
        Player.gameObject.layer = LayerMask.NameToLayer("NoPUp");
    }
}
