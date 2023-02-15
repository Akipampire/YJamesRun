using UnityEngine;
using utils;
public class Invicibility_Sphere : MonoBehaviour
{
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private float minAlpha = 0.1f;
    [SerializeField] private float maxAlpha = 0.9f;
    [SerializeField] private float speed = 1.0f;

    private float currentAlpha;
    private bool isIncreasing = true;
    void FixedUpdate() {
        if (isIncreasing)   currentAlpha += speed * Time.deltaTime;
        else   currentAlpha -= speed * Time.deltaTime;

        if (currentAlpha >= maxAlpha) isIncreasing = false;
        else if (currentAlpha <= minAlpha) isIncreasing = true;

        mesh.material.color = mesh.material.color.UpdateColor(currentAlpha,ColorAxis.A);
    }
}
