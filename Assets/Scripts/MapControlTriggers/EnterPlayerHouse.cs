using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterPlayerHouse : MonoBehaviour
{
	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.name == "Player")
		{
            if (Input.GetKeyDown(KeyCode.E))
            {
                MapControl.Instance.StartingAreaToPlayerHouse();
            }
		}
	}
}
