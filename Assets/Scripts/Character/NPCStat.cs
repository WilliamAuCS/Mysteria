/*
* Created by William Au
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCStat : MonoBehaviour 
{
	private Image content;
	private float currentFill;
	private int lerpSpeed = 5;

	private Text healthValue;

	private float myMaxValue { get; set; }
	private float currentValue;

	public float MyCurrentValue
	{
		get
		{
			return currentValue;
		}

		set
		{
			if (value > myMaxValue)
			{
				currentValue = myMaxValue;     //making sure player health does not excede the cap
			}
			else if (value < 0)
			{
				currentValue = 0;              //making sure health does not go below 0
			}
			else
			{
				currentValue = value;
			}
			currentFill = currentValue / myMaxValue;
		}
	}
	// Use this for initialization
	void Start()
	{
		content = GetComponent<Image>();
		healthValue = GetComponentInChildren<Text>();
	}

	// Update is called once per frame
	void Update()
	{
		if (currentFill != content.fillAmount)
		{
			content.fillAmount = Mathf.Lerp(content.fillAmount, currentFill, lerpSpeed * Time.deltaTime);
		}
	}
	public void Initialized(float currentValue, float maxValue)
	{
		myMaxValue = maxValue;
		MyCurrentValue = currentValue;
	}
}
