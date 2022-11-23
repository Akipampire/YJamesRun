using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
    [SerializeField] private string typeEsquive;
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == Mathf.Log(GameManager.PlayerLayer.value, 2)) {
            other.GetComponent<Player>().OnHit(typeEsquive);
        }
    }
}
