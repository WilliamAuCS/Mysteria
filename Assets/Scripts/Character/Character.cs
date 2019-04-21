/*
* Created by William Au
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public bool sideFacing = true;     //true is facing right
    protected bool moveRight;
    public bool moveLeft;
    protected bool moveUp;
    protected bool moveDown;
    protected Rigidbody2D rB;
    protected Animator animator;
	private SpriteRenderer sR;

	private Vector2 curPos;
	private Vector2 lastPos;
	protected bool isMoving;

    protected bool attack;

    // Use this for initialization
    protected virtual void Start ()
    {
        rB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
		sR = GetComponent<SpriteRenderer>();
		//lastPos = transform.position;
	}
	
	// Update is called once per frame
	protected virtual void Update ()
    {
        if (attack)
        {
            MakeAttack();
        }
		//CheckIfMoving();
		SwapSpriteSide();
	}
	protected void SwapSpriteSide()
	{
		if (sideFacing)
		{
			transform.localScale = new Vector2(1f, 1f);
		}
		else
		{
			transform.localScale = new Vector2(-1f, 1f);
		}
	}
	private void CheckIfMoving()
	{
		curPos = transform.position;
		if (curPos == lastPos)
		{
			isMoving = false;
			animator.SetBool("isMoving", false);
		}
		else
		{
			isMoving = true;
			animator.SetBool("isMoving", true);
		}
	}
	private void MakeAttack()
    {
        animator.SetTrigger("attack");
        attack = false;
    }
}
