/*
* Created by William Au
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour 
{
	#region Variables
	[SerializeField]
	private Transform target;
	#endregion
	
	void Update () 
	{
		transform.position = target.position;
	}
}
