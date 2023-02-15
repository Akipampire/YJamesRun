using System;
using UnityEngine;
using UnityEngine.InputSystem;
using utils;

public class PlayerPowerUp : MonoBehaviour
{
    [SerializeField] private Player thisPawn;
    [SerializeField] private GameObject[] powerUpInventory;

    public void AddPowerUpToInventory(GameObject InPowerUp) {
        for (int i = 0; i < powerUpInventory.Length; i++) {
            if (powerUpInventory[i].isNull()) {
                powerUpInventory[i] = InPowerUp;
                GameManager.Instance.AddPowerUpIcon(thisPawn, InPowerUp,i);
            }
        }
    }
    public void OnLeftPower(InputAction.CallbackContext context) {
        OnShoot(context, Side.Left);
    }
    public void OnCenterPower(InputAction.CallbackContext context) {
        OnShoot(context, Side.Center);
    }
    public void OnRightPower(InputAction.CallbackContext context) {
        OnShoot(context, Side.Right);
    }
    public void OnShoot(InputAction.CallbackContext context, Side side) {
        if (!context.performed || powerUpInventory[(int)side].isNull()) return;

    }
}
