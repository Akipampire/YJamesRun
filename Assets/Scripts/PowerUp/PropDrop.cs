using UnityEngine;

public class PropDrop : PowerUP
{
    public GameObject obstaclePrefab; // This field will be set in the Unity Editor

    public override void Activate(GameObject trigger = null) {
        trigger.GetComponent<PlayerPowerUp>().AddPowerUpToInventory(this);
        Destroy(gameObject);
    }

    public override void OnUse(Player user) {
        if (obstaclePrefab != null) {
            // Get the position behind the player
            Vector3 dropPosition = user.transform.position - user.transform.forward * 2f; // Adjust the distance behind the player as needed

            // Drop the obstacle at the calculated position
            Instantiate(obstaclePrefab, dropPosition, Quaternion.identity);

            // Optionally, add sound effect here if needed

            // Reset any relevant game state if needed

            // Destroy the power-up object after use
            Destroy(gameObject);
        } else {
            UnityEngine.Debug.LogError("Obstacle Prefab is not assigned in the inspector for PropDrop power-up.");
        }
    }
}