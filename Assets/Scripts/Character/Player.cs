/*
* Created by William Au
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public bool canMove = true;
	private float movementSpeed = 6f;

	[SerializeField]
	private Stat health;
	[SerializeField]
	private Animator swordAnim;
	[SerializeField]
	private Animator swordSpell;

	//[SerializeField]
	//private GameObject spinJabTransform;
	//private GameObject spinJabClone;
	private bool isDead;

	private bool spinJab;

	private float startingHealth = 100f;
	private bool hasSword = true;
	public bool hasSwordEquiped;

	// Use this for initialization
	protected override void Start ()
    {
        base.Start();
		health.Initialized(GlobalControl.Instance.playerHealth, 100);
	}

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
		if (canMove)
		{
			GetInput();
		}
		DebuggingHAX();
		CheckIfDead();
	}
	private void OnDestroy()
	{
		SavePlayer();
	}
	public void SavePlayer()
    {
		GlobalControl.Instance.playerHealth = health.MyCurrentValue;
    }
    //Update for physics movements
    void FixedUpdate ()
    {
		
	}
	private void CheckIfDead()
	{
		if (health.MyCurrentValue == 0)
		{
			isDead = true;
			animator.SetBool("isDead", true);
			canMove = false;
		}
		else
		{
			isDead = false;
			animator.SetBool("isDead", false);
			canMove = true;
		}
	}
	/*private void makeSwordSpell()
	{
		Debug.Log("first");
		if (spinJab)
		{
			Debug.Log("second");
			spinJabClone = Instantiate(spinJabTransform, new Vector3(transform.position.x + 1.34f, transform.position.y + -0.12f, transform.position.z), Quaternion.identity) as GameObject;
			spinJab = false;
		}
	}*/
	private void GetInput()
    {
        if (Input.GetKey(KeyCode.D))
        {
            moveRight = true;
        }
        else
        {
            moveRight = false;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveLeft = true;
        }
        else
        {
            moveLeft = false;
        }
        if (Input.GetKey(KeyCode.W))
        {
            moveUp = true;
        }
        else
        {
            moveUp = false;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDown = true;
        }
        else
        {
            moveDown = false;
        }
        Move();
        if (Input.GetKeyDown(KeyCode.Q) && hasSword && hasSwordEquiped)
        {
            attack = true;
			swordAnim.SetTrigger("attack");
		}
		if (Input.GetKeyDown(KeyCode.X) && hasSword && !hasSwordEquiped)
		{
			animator.SetBool("hasSword", true);
			swordAnim.SetBool("hasSword", true);
			hasSwordEquiped = true;
			//swordAnim.gameObject.SetActive(true);
		}
		else if (Input.GetKeyDown(KeyCode.X) && hasSword && hasSwordEquiped)
		{
			animator.SetBool("hasSword", false);
			swordAnim.SetBool("hasSword", false);
			hasSwordEquiped = false;
			//swordAnim.gameObject.SetActive(false);
		}
		if (Input.GetKeyDown(KeyCode.R) && !hasSwordEquiped)
		{
			swordSpell.SetTrigger("spinJab");
			//spinJab = true;
			//makeSwordSpell();
		}
    }
	private void DebuggingHAX()
	{
		if (Input.GetKeyDown(KeyCode.I))
		{
			health.MyCurrentValue -= 10;
		}
		if (Input.GetKeyDown(KeyCode.O))
		{
			health.MyCurrentValue += 10;
		}
	}
	public void Move()
	{
		if (moveRight)
		{
			rB.velocity = new Vector2(movementSpeed, rB.velocity.y);
			sideFacing = true;
			animator.SetBool("isMoving", true);
		}
		if (moveLeft)
		{
			rB.velocity = new Vector2(-movementSpeed, rB.velocity.y);
			sideFacing = false;
			animator.SetBool("isMoving", true);
		}
		if (moveUp)
		{
			rB.velocity = new Vector2(rB.velocity.x, movementSpeed);
			animator.SetBool("isMoving", true);
		}
		if (moveDown)
		{
			rB.velocity = new Vector2(rB.velocity.x, -movementSpeed);
			animator.SetBool("isMoving", true);
		}
		else if (!moveRight && !moveLeft && !moveUp && !moveDown)
		{
			animator.SetBool("isMoving", false);
		}
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Bandit" || collision.gameObject.tag == "Knight")
		{
			Physics2D.IgnoreCollision(collision.gameObject.GetComponent<CompositeCollider2D>(), GetComponent<CompositeCollider2D>());
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "EnemyBasicAttack")
		{
			Debug.Log("hit");
			health.MyCurrentValue -= 10;
			animator.SetTrigger("tookDamage");
		}
	}
}
