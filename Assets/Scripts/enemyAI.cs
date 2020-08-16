﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
	public int maxHealth = 120;
	Animator enemyAnimator;
	int currentHealth;
	Rigidbody2D enemyRigidBody;
	// Start is called before the first frame update
	public AIPath aiPath;
	void Start()
	{
		enemyAnimator = GetComponent<Animator>();
		currentHealth = maxHealth;
		enemyRigidBody = GetComponent<Rigidbody2D>();
		aiPath = GetComponent<AIPath>();
		InvokeRepeating("LaunchProjectile", 2.0f, 0.3f);
	}

	// Update is called once per frame
	void Update()
	{
		if (aiPath.desiredVelocity.x <= 0.01f)
		{
			transform.eulerAngles = new Vector3(0, 0, 0);
			enemyAnimator.SetBool("IsWalking", true);
		}
		else
		{
			transform.eulerAngles = new Vector3(0, 180, 0);
			enemyAnimator.SetBool("IsWalking", true);
		}

		if (aiPath.remainingDistance <= 4)
		{
			EnemyAttack();
		}

	}

	public void TakeDamage(int damage)
	{
		if (damage > 0)
		{
			enemyAnimator.SetTrigger("IsHurt");
		}
		currentHealth -= damage;
		if (currentHealth <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		enemyAnimator.SetBool("IsDeath", true);
		enemyAnimator.SetBool("IsWalking", false);
		GetComponent<Rigidbody2D>().simulated = false;
		this.transform.position = new Vector2(enemyRigidBody.position.x, -5.7f);
	}

	void EnemyAttack()
	{
		enemyAnimator.SetTrigger("Attack");
	}
}