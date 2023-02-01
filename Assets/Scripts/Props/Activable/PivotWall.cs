using UnityEngine;
using utils;

public class PivotWall : Activable {
    [SerializeField] MeshCollider col;
    [SerializeField] MeshRenderer rend;
    bool isOpen = true;
    public override void Activate() {
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
