using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GunData gunData;
    public ParticleSystem shootParticle;

	private AudioSource audioSource;
	private LineRenderer lineRenderer;
	
	private float lastFireTime;
	private Vector3 endPos;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		lineRenderer = GetComponent<LineRenderer>();

		lineRenderer.enabled = false;
		shootParticle.lights.light.enabled = false;
	}

	private void Start()
	{
		lastFireTime = Time.time;
	}

	public void Shoot()
	{
		endPos = new Vector3();
		RaycastHit hit;
		if(Physics.Raycast(transform.position, transform.forward, out hit ,gunData.fireDistance))
		{
			endPos = hit.point;

			if(hit.collider.GetComponent<IDamagable>() != null)
			{
				var obj = hit.collider.GetComponent<IDamagable>();
				obj.OnDamage(gunData.damage, hit.point, hit.normal);
			}
		}
		else
		{
			endPos = transform.position + transform.forward * gunData.fireDistance;
		}
		
		StartCoroutine(CorShootEffect());
	}

	public void Fire()
	{
		if(Time.time - lastFireTime > gunData.fireInterval)
		{
			Shoot();
			lastFireTime = Time.time;
		}
	}

	private IEnumerator CorShootEffect()
	{
		audioSource.PlayOneShot(gunData.shootClip);
		shootParticle.Play();
		shootParticle.lights.light.enabled = true;
		
		lineRenderer.enabled = true;

		yield return new WaitForSeconds(gunData.fireInterval * 0.5f);

		lineRenderer.enabled = false;
		shootParticle.lights.light.enabled = false;
	}

	private void LateUpdate()
	{
		lineRenderer.SetPosition(0, transform.position);
		lineRenderer.SetPosition(1, endPos);
	}
}
