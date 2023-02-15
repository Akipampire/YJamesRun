using UnityEngine;
public abstract class PowerUP : Activable {
    [SerializeField] public Texture2D image;
    public abstract void OnUse(Player user);
}