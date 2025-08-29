using System;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : LivingEntity
{
	public enum Status
	{
		Idle,
		Trace,
		Attack,
		Die,
	}

	private NavMeshAgent agent;
	private Animator animator;
	private Collider collider;

	public EnemyData enemyData;
	public LayerMask layer;

	public ParticleSystem hitParticle;

	private Transform target;

	private float lastAttackTime;

	private Status currentStatus;

	public Status CurrentStatus
	{
		get { return currentStatus; }
		set
		{
			var before = currentStatus;
			currentStatus = value;
			switch (currentStatus)
			{
				case Status.Idle:
					animator.SetBool(Defines.animatorMove, false);
					agent.isStopped = true;
					break;
				case Status.Trace:
					animator.SetBool(Defines.animatorMove, true);
					agent.isStopped = false;
					break;
				case Status.Attack:
					animator.SetBool(Defines.animatorMove, false);
					agent.isStopped = true;
					break;
				case Status.Die:
					animator.SetTrigger(Defines.animatorDie);
					agent.isStopped = true;
					collider.enabled = false;
					break;
			}
		}
	}

	public float destroyTime = 3f;

	protected override void Awake()
	{
		base.Awake();
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
		collider = GetComponent<Collider>();

		maxHP = enemyData.maxHP;
		lastAttackTime = Time.time;
	}

	private void Update()
	{
		switch (CurrentStatus)
		{
			case Status.Idle:
				UpdateIdle();
				break;
			case Status.Trace:
				UpdateTrace();
				break;
			case Status.Attack:
				UpdateAttack();
				break;
			case Status.Die:
				UpdateDie();
				break;
		}
	}

	private void UpdateDie()
	{
	}

	private void UpdateAttack()
	{
		if(target != null && Vector3.Distance(transform.position, target.position) > enemyData.attackDistance)
		{
			CurrentStatus = Status.Trace;
			return;
		}

		if(Time.time - lastAttackTime > enemyData.attackInterval)
		{
			var go = target.gameObject.GetComponent<IDamagable>();

			if (go != null)
			{
				go.OnDamage(enemyData.damage, Vector3.zero, Vector3.zero);
			}
		}
	}

	private void UpdateTrace()
	{
		if (target != null && Vector3.Distance(transform.position, target.position) < enemyData.attackDistance)
		{
			CurrentStatus = Status.Attack;
			return;
		}
		if (target == null ||
			(target != null && Vector3.Distance(transform.position, target.position) > enemyData.detectDistance))
		{
			CurrentStatus = Status.Idle;
			return;
		}

		agent.SetDestination(target.position);
	}

	private void UpdateIdle()
	{
		if(target == null)
			target = GetClosedObjectsInSphere(enemyData.detectDistance);

		if (target != null &&
			Vector3.Distance(transform.position, target.position) < enemyData.detectDistance)
		{
			CurrentStatus = Status.Trace;
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		CurrentStatus = Status.Idle;
		collider.enabled = true;
	}

	public override void Die()
	{
		base.Die();
		CurrentStatus = Status.Die;
		Destroy(gameObject, destroyTime);
	}

	public override void OnDamage(float damage, Vector3 hitPos, Vector3 hitNormal)
	{
		base.OnDamage(damage, hitPos, hitNormal);

		hitParticle.transform.position = hitPos;
		hitParticle.transform.forward = hitNormal;
		hitParticle.Play();
	}

	private Transform GetClosedObjectsInSphere(float distance)
	{
		var colliders = Physics.OverlapSphere(transform.position, distance, layer);
		if (colliders.Length == 0)
			return null;

		var minDistObj = colliders[0];
		for(int i = 1; i < colliders.Length; i++)
		{
			var dis = Vector3.Distance(colliders[i].transform.position, transform.position);
			var minObjDis = Vector3.Distance(minDistObj.transform.position, transform.position);
			if (dis < minObjDis)
				minDistObj = colliders[i];
		}

		return minDistObj.gameObject.transform;
	}
}
