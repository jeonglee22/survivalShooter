using UnityEngine;

public class PlayerHealth : LivingEntity
{
	public UIManager uIManager;
	private GameManager gameManager;

	public override void Die()
	{
		base.Die();
		Destroy(gameObject);
		uIManager.SetGameOverUI(true);
		gameManager.IsGameOver = true;
		Time.timeScale = 0f;
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

	private void Start()
	{
		gameManager = GameObject.FindWithTag(Defines.gameManagerStr).GetComponent<GameManager>();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
	}
}
