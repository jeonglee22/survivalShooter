using System;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamagable
{
	public float maxHP = 100;
	protected float health;

	protected bool isDead;

	private AudioSource audioSource;

	public AudioClip damageClip;
	public AudioClip deathClip;

	public event Action OnDeath;

	protected virtual void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}

	protected virtual void OnEnable()
	{
		health = maxHP;
		isDead = false;
	}

	public virtual void OnDamage(float damage, Vector3 hitPos, Vector3 hitNormal)
	{
		health -= damage;
		audioSource.PlayOneShot(damageClip);

		if (health <= 0)
		{
			isDead = true;
			Die();
		}
	}

	public virtual void Die()
	{
		if(OnDeath != null)
		{
			OnDeath();
		}
		audioSource.PlayOneShot(deathClip);
	}
}
