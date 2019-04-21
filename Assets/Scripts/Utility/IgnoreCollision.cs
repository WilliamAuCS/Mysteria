/*
* Created by William Au
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour 
{
	#region Variables
	[SerializeField]
	private GameObject self;
	#endregion
	private void Start()
	{
		Transform basicAttack = GetComponent<Transform>();
		Physics2D.IgnoreCollision(basicAttack.gameObject.GetComponent<BoxCollider2D>(), self.GetComponent<Collider2D>());
	}
}
