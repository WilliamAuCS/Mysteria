/*
* Created by William Au
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Character 
{
    #region Variables
    private NPCStat health;
	private float maxHealth = 250f;
	#endregion

	protected override void Start () 
	{
		base.Start();
		health.Initialized(maxHealth, maxHealth);
	}
	
	protected override void Update () 
	{
		base.Update();
		SwapSpriteSide();
	}
	
}
