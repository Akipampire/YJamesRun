using System;
using UnityEngine;
using UnityEngine.InputSystem;
using utils;

public class PlayerPowerUp : MonoBehaviour
{
    [SerializeField] private Player thisPawn;
    private PowerUpStruct[] powerUpInventory = new PowerUpStruct[3];

    public void AddPowerUpToInventory(PowerUP InPowerUp) {
        for (int i = 0; i < powerUpInventory.Length; i++) {
            if (powerUpInventory[i].isNull()) {
                powerUpInventory[i] = new PowerUpStruct();
                powerUpInventory[i].OnUse = InPowerUp.OnUse;
                GameManager.Instance.AddPowerUpIcon(thisPawn, InPowerUp,i);
                break;
            }
        }
    }
    public void OnLeftPower(InputAction.CallbackContext context) {
        OnPowerUse(context, 0);
    }
    public void OnCenterPower(InputAction.CallbackContext context) {
        OnPowerUse(context, 1);
    }
    public void OnRightPower(InputAction.CallbackContext context) {
        OnPowerUse(context, 2);
    }
    public void OnPowerUse(InputAction.CallbackContext context, int side) {
        if (!context.performed || powerUpInventory[side].isNull()) return;
        powerUpInventory[side].OnUse(thisPawn);
        GameManager.Instance.AddPowerUpIcon(thisPawn, null, side);
        powerUpInventory[side] = null;
    }
}
