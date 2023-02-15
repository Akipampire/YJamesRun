using System.Collections;
using UnityEngine;

public abstract class Activable : MonoBehaviour
{
    public abstract void Activate(GameObject trigger = null);
}
