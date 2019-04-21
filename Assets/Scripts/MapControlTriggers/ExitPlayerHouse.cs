using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPlayerHouse : MonoBehaviour
{
	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.name == "Player")
		{
            if (Input.GetKeyDown(KeyCode.E))
            {
                MapControl.Instance.PlayerHouseToStartingArea();
            }
		}
	}
}
