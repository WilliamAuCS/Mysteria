/*
* Created by William Au
*/

using System.Collections;
using UnityEngine;
using Pathfinding;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class BanditAI : MonoBehaviour 
{
	#region Variables
	//What to chase
	public Transform target;

	//How many times each second we will update our path
	[SerializeField]
	private float updateRate = 2f;

	//Caching
	private Seeker seeker;
	private Rigidbody2D rb;

	//The calculated path
	[SerializeField]
	private Path path;

	//The AI's speed per second
	[SerializeField]
	private float speed = 300f;
	[SerializeField]
	private ForceMode2D fMode;
	[HideInInspector]
	public bool pathIsEnded;

	[SerializeField]
	Character character;
	private bool[] checkIfTrue = { false, false, false };
	private int trueCounter = 0;

	//The waypoint we are currently moving towards
	private int currentWaypoint = 0;

	private bool searchingForPlayer;

	//The max distance from the AI to a waypoint for it to continue to the next waypoint
	[SerializeField]
	private float nextWaypointDistance = 3f;

	Bandit bandit;
	#endregion

	private void Start()
	{
		bandit = GetComponent<Bandit>();
		seeker = GetComponent<Seeker>();
		rb = GetComponent<Rigidbody2D>();

		if (target == null)
		{
			if (!searchingForPlayer)
			{
				searchingForPlayer = true;
				StartCoroutine(SearchForPlayer());
			}
			return;
		}
		//Start a new path to the target position, return the result to the OnPathComplete method???
		//seeker.StartPath(transform.position, target.position, OnPathComplete);

		StartCoroutine(UpdatePath());
	}
	IEnumerator SearchForPlayer()
	{
		GameObject sResult = GameObject.FindGameObjectWithTag("Player");
		if (sResult == null)
		{
			yield return new WaitForSeconds(0.5f);
			StartCoroutine(SearchForPlayer());
		}
		else
		{
			target = sResult.transform;
			searchingForPlayer = false;
			StartCoroutine(UpdatePath());
			yield return false;
		}
	}
	IEnumerator UpdatePath()
	{
		if (target == null)
		{
			if (!searchingForPlayer)
			{
				searchingForPlayer = true;
				StartCoroutine(SearchForPlayer());
			}
			yield return false;
		}
		seeker.StartPath(transform.position, target.position, OnPathComplete);

		yield return new WaitForSeconds(1f / updateRate);
		StartCoroutine(UpdatePath());
	}
	public void OnPathComplete(Path p)
	{
		//Debug.Log("We got a path. Did it have an error?" + p.error);
		if (!p.error)
		{
			path = p;
			currentWaypoint = 0;
		}
	}
	private void FixedUpdate()
	{
		if (target == null)
		{
			if (!searchingForPlayer)
			{
				searchingForPlayer = true;
				StartCoroutine(SearchForPlayer());
			}
			return;
		}
		//Always look towards player?
		if (path == null)
		{
			return;
		}
		if (currentWaypoint >= path.vectorPath.Count)
		{
			if (pathIsEnded)
			{
				return;
			}
			//Debug.Log("End of path reached");
			pathIsEnded = true;
			return;
		}
		pathIsEnded = false;

		//Direction to the next waypoint
		Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
		CheckSide(dir);
		dir *= speed * Time.fixedDeltaTime;

		//Move the AI
		bandit.Move(dir, fMode);

		float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
		if (dist < nextWaypointDistance)
		{
			currentWaypoint++;
			return;
		}
	}
	private void CheckSide(Vector3 dir)
	{
		int isTrue = 0;
		int isFalse = 0;
		if (dir.x > 0f)
		{
			checkIfTrue[trueCounter] = true;
		}
		else
		{
			checkIfTrue[trueCounter] = false;
		}
		++trueCounter;
		if (trueCounter == 3)
		{
			for (int i = 0; i < 3; i++)
			{
				if (checkIfTrue[i] == true)
				{
					++isTrue;
				}
				else
				{
					++isFalse;
				}
			}
			if (isTrue == 3)
			{
				character.sideFacing = true;
			}
			else if (isFalse == 3)
			{
				character.sideFacing = false;
			}
			isTrue = 0;
			isFalse = 0;
			trueCounter = 0;
		}
	}
}
