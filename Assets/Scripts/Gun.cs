using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GunData gunData;
    public ParticleSystem shootParticle;

	private AudioSource audioSource;
	private LineRenderer lineRenderer;
	
	private float lastFireTime;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		lineRenderer = GetComponent<LineRenderer>();

		lineRenderer.enabled = false;
	}

	private void Start()
	{
		lastFireTime = Time.time;
	}

	public void Shoot()
	{
		var endPos = new Vector3();
		RaycastHit hit;
		if(Physics.Raycast(transform.position, transform.forward, out hit ,gunData.fireDistance))
		{
			endPos = hit.point;
		}
		else
		{
			endPos = transform.position + transform.forward * gunData.fireDistance;
		}

		StartCoroutine(CorShootEffect(endPos));
	}

	public void Fire()
	{
		if(Time.time - lastFireTime > gunData.fireInterval)
		{
			Shoot();
			lastFireTime = Time.time;
		}
	}

	private IEnumerator CorShootEffect(Vector3 hitPos)
	{
		audioSource.PlayOneShot(gunData.shootClip);
		shootParticle.Play();
		
		lineRenderer.enabled = true;

		lineRenderer.SetPosition(0, transform.position);
		lineRenderer.SetPosition(1, hitPos);

		yield return new WaitForSeconds(gunData.fireInterval * 0.5f);

		lineRenderer.enabled = false;
	}
}
