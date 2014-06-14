﻿using UnityEngine;
using System.Collections;

public class MonsterDoorController : MonoBehaviour {

	private static float interval = 4.0f;
	private static int delay = 5;
	private static float maxY = 10.0f;
	private static float maxIntensity = 5.0f;

	private float sign = 0;
	private float timeLeft = 0.0f;

	private Vector3 doorInitialPosition;
	public GameObject doorLight;

	void Start()
	{
		doorInitialPosition = transform.localPosition;
	}

	// 0 -> 1 -> 2 -> -1 -> 0

	void Update()
	{
		if (timeLeft > 0)
		{
			if ((sign == 1) || (sign == -1))
			{
				float doorStep = sign * Time.deltaTime * maxY / interval;
				float lightStep = sign * Time.deltaTime * maxIntensity / interval;

				transform.Translate (0, doorStep, 0);
				doorLight.light.intensity += lightStep;
			}

			timeLeft -= Time.deltaTime;
		}
		else
		{
			if (sign == 1)
			{
				sign = 2;
				timeLeft = delay;
				return;
			}

			if (sign == 2)
			{
				sign = -1;
				timeLeft = interval;
				return;
			}

			if (sign == -1)
			{
				transform.localPosition = doorInitialPosition;
				doorLight.light.intensity = 0;
				return;
			}
		}
	}
	
	void OpenDoor ()
	{
		sign = 1;
		timeLeft = interval;
	}

	void CloseDoor ()
	{
		timeLeft = 0;
		sign = -1;
	}
}
