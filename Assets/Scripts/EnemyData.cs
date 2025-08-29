using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public float speed = 5f;
    public float attackInterval = 0.5f;
    public float maxHP = 100;
    public float detectDistance = 15f;
    public float attackDistance = 1f;
    public float damage = 20;
}
