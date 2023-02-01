using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 givenPosition = Vector3.zero;
    public float speed = 0;
    private void FixedUpdate() {
        if(givenPosition!=Vector3.zero) transform.position = Vector3.MoveTowards(transform.position, givenPosition,speed/10);
    }
}
