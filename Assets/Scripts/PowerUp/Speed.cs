using UnityEngine;
using System.Collections;

public class Speed : PowerUP
{
    [SerializeField] private float speed_duration = 3f; // La durée pendant laquelle la vitesse augmente
    [SerializeField] private float speed_boost = 2f; // Le facteur de vitesse final à atteindre
    [SerializeField] private float accelerationFactor = 3f; // Facteur d'accélération pour augmenter l'effet

    public override void Activate(GameObject trigger = null)
    {
        if (trigger != null)
        {
            trigger.GetComponent<PlayerPowerUp>().AddPowerUpToInventory(this);
            Destroy(gameObject); // Détruit le power-up après activation
        }
    }

    public override void OnUse(Player user)
    {
        StartCoroutine(ApplySpeedBoost(user));
    }

    private IEnumerator ApplySpeedBoost(Player user)
    {
        float elapsedTime = 1;
        float startSpeed = user.GetComponent<PlayerMovement>().forwardSpeed;
        float targetSpeed = startSpeed * speed_boost; // Calcul de la vitesse cible

        while (elapsedTime < speed_duration)
        {
            elapsedTime += Time.deltaTime;
            float currentBoost = Mathf.Pow(elapsedTime / speed_duration, accelerationFactor) * (targetSpeed - startSpeed);
            user.GetComponent<PlayerMovement>().forwardSpeed = startSpeed + currentBoost;
            yield return null; // Attend jusqu'au prochain frame
        }

        // Ici, la vitesse est augmentée de manière plus significative vers la fin de la période
        user.GetComponent<PlayerMovement>().forwardSpeed = targetSpeed; // S'assure que la vitesse cible est atteinte

    }
}
