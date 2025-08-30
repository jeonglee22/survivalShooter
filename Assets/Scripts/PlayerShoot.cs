using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
	private Gun gun;
	private PlayerInput input;

	public UIManager manager;
	private GameManager gameManager;

	private void Awake()
	{
		gun = GetComponentInChildren<Gun>();
		input = GetComponent<PlayerInput>();
	}

	private void Start()
	{
		gameManager = GameObject.FindWithTag(Defines.gameManagerStr).GetComponent<GameManager>();
	}

	private void Update()
	{
		if (manager.IsPaused || gameManager.IsGameOver) return;

		if (input.Shoot)
			gun.Fire();
	}
}
