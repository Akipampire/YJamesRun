using UnityEngine;
using utils;
public class Target : MonoBehaviour
{
    public Side side;
    private void OnEnable() {
        side = transform.parent.position.x == -10 ? Side.Left : transform.parent.position.x == 0 ? Side.Center : Side.Right;
        GameManager.Instance.NewTarget(this);
    }

    private void OnDestroy() {
        GameManager.Instance.DeleteTarget(this);
    }

    public void Trig() {
        transform.parent.localScale = transform.parent.localScale.UpdateAxis(0, VectorAxis.Y);
    }
}
