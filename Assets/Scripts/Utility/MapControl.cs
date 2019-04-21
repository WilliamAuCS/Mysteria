/*
* Created by William Au
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl : MonoBehaviour
{
	public static MapControl Instance;

	#region Variables
	[SerializeField]
	Player player;
    [SerializeField]
    private Animator anim;
	//**Spawn Locations**
	private Vector2 playerHouseExitPosition = new Vector2(-1.7f, -25f);
	private Vector2 playerHouseEnterPosition = new Vector2(-49f, -25.5f);
	private Vector2 startingCaveExitPosition = new Vector2(3.23f, 5f);
	private Vector2 startingCaveEnterPosition = new Vector2(103.85f, -16.2f);

	#endregion

	private void Awake()
	{
		Instance = this;
	}
    private void FadeOut()
    {
        anim.SetTrigger("fadeOut");
    }
	public void StartingCaveExitToStartingArea()
	{
        FadeOut();
        StartCoroutine(StartingCaveExitToStartingAreaHelp());
	}
	private IEnumerator StartingCaveExitToStartingAreaHelp()
	{
		player.canMove = false;
		player.transform.position = startingCaveExitPosition;
		player.sideFacing = false;
		player.moveLeft = true;
		for (int i = 0; i < 12; i++)
		{
			player.Move();
		}
		yield return new WaitForSeconds(0.3f);
		player.moveLeft = false;
		player.canMove = true;
	}
	public void StartingAreaToPlayerHouse()
	{
        FadeOut();
        player.transform.position = playerHouseEnterPosition;
	}
	public void PlayerHouseToStartingArea()
	{
        FadeOut();
        player.transform.position = playerHouseExitPosition;
	}
	public void StartingAreaToStartingCave()
	{
        FadeOut();
        player.transform.position = startingCaveEnterPosition;
	}
}
