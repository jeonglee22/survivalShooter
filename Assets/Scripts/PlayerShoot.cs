using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
	private Gun gun;
	private PlayerInput input;

	private void Awake()
	{
		gun = GetComponentInChildren<Gun>();
		input = GetComponent<PlayerInput>();
	}

	private void Update()
	{
		if (input.Shoot)
			gun.Fire();
	}
}
