using UnityEngine;
using UnityEngine.InputSystem;
using utils;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Player thisPawn;
    [SerializeField] private PlayerMovement movements;
    [SerializeField] private Bullet Sphere;
    public void OnShootLeft(InputAction.CallbackContext context) {
        OnShoot(context, Side.Left);
    }
    public void OnShootCenter(InputAction.CallbackContext context) {
        OnShoot(context, Side.Center);
    }
    public void OnShootRight(InputAction.CallbackContext context) {
        OnShoot(context, Side.Right);
    }
    public void OnShoot(InputAction.CallbackContext context, Side side) {
        if (!context.performed) return;
        Target[] targetsFound = GameManager.Instance.AskForTarget(side, transform);
        if (targetsFound.Length > 0) {
            GameManager.Instance.PlaySFX(SFXPlayer.SFX_TYPE.Throw);
            var newBullet = Instantiate(Sphere,transform.position.UpdateAxis(transform.position.y + 0.5f,VectorAxis.Y), Quaternion.identity);
            newBullet.speed = movements.forwardSpeed * 2;
            newBullet.givenPosition = targetsFound[0].transform.position.UpdateAxis(targetsFound[0].transform.position.y + 0.5f,VectorAxis.Y);
        }
    }
}
