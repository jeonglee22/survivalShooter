using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : ScriptableObject
{
    public AudioClip shootClip;

    public float fireDistance = 20f;
    public float fireInterval = 0.2f;

    public float ammoMax = 100;
    public float magCapacity = 20;

    public float damage = 20;
}
