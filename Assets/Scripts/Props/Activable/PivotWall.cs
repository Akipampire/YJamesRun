using UnityEngine;
using utils;

public class PivotWall : Activable {
    [SerializeField] BoxCollider col;
    [SerializeField] MeshRenderer rend;
    bool isOpen = true;
    public override void Activate(GameObject trigger = null) {
        if (isOpen) {
            rend.enabled = false;
            col.enabled = false;
        } else {
            rend.enabled = true;
            col.enabled = true;
        }
        isOpen = !isOpen;
    }
}
