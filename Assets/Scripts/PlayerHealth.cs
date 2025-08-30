using UnityEngine;

public class PlayerHealth : LivingEntity
{
	public UIManager uIManager;

	public override void Die()
	{
		base.Die();
		//Destroy(gameObject);
	}

	public override void OnDamage(float damage, Vector3 hitPos, Vector3 hitNormal)
	{
		if(isDead) return;

		base.OnDamage(damage, hitPos, hitNormal);

		var healthPercent = health / maxHP;
		uIManager.SetHealthSlider(healthPercent);
		uIManager.PanelFliking();
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
