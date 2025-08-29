using UnityEngine;

public class PlayerHealth : LivingEntity
{
	public override void Die()
	{
		base.Die();
		//Destroy(gameObject);
	}

	public override void OnDamage(float damage, Vector3 hitPos, Vector3 hitNormal)
	{
		base.OnDamage(damage, hitPos, hitNormal);
	}

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
	}
}
