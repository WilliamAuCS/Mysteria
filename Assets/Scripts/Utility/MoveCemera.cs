using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCemera : MonoBehaviour
{
    private Transform player;
    [SerializeField]
    private Vector3 offset = new Vector3(0, 0, -5);

    private void Start()
    {
		player = FindObjectOfType<Player>().transform;
    }

    private void Update()
    {
        transform.position = player.position + offset;
    }

}
