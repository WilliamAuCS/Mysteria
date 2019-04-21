/*
* Created by William Au
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : Character
{
	#region Variables
	[SerializeField]
	private NPCStat health;
	private float maxHealth = 150f;
	[SerializeField]
	private Canvas canvas;
	private bool isDead;
	BanditAI banditAI;
	private bool canMove = true;
	private float sightDistance = 10f;
	#endregion

	protected override void Start()
	{
		base.Start();
		health.Initialized(maxHealth, maxHealth);
		banditAI = GetComponent<BanditAI>();
		StartCoroutine(CheckDistance());
	}
	protected override void Update()
	{
		base.Update();
		SwapSpriteSide();
		CheckIfDead();
	}
	IEnumerator CheckDistance()
	{
		float distBetween = Vector2.Distance(transform.position, banditAI.target.position);
		if (distBetween < sightDistance)
		{
			canMove = true;
			if (distBetween < 1.5f)
			{
				canMove = false;
				Attack();
			}
			else
			{
				canMove = true;
			}
		}
		else
		{
			canMove = false;
		}
		if (canMove)
		{
			animator.SetBool("isMoving", true);
		}
		else
		{
			animator.SetBool("isMoving", false);
		}
		yield return new WaitForSeconds(0.5f);
		StartCoroutine(CheckDistance());
	}
	private void Attack()
	{
		animator.SetTrigger("attack");
	}
	public void Move(Vector2 dir, ForceMode2D fMode)
	{
		if (canMove)
		{
			rB.AddForce(dir, fMode);
		}
	}
	private void CheckIfDead()
	{
		if (health.MyCurrentValue == 0)
		{
			isDead = true;
			animator.SetBool("isDead", true);
			canvas.enabled = false;
			canMove = false;
			rB.constraints = RigidbodyConstraints2D.FreezeAll;
		}
		else
		{
			isDead = false;
			animator.SetBool("isDead", false);
			canvas.enabled = true;
			canMove = true;
			rB.constraints = RigidbodyConstraints2D.None;
			rB.constraints = RigidbodyConstraints2D.FreezeRotation;
		}
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			Physics2D.IgnoreCollision(collision.gameObject.GetComponent<CompositeCollider2D>(), GetComponent<CompositeCollider2D>());
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "basicAttack")
		{
			Debug.Log("hit");
			health.MyCurrentValue -= 10;
			animator.SetTrigger("tookDamage");
		}
		if (collision.gameObject.tag == "spinJab")
		{
			health.MyCurrentValue -= 15;
			animator.SetTrigger("tookDamage");
		}
	}
}
