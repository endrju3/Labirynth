﻿using UnityEngine;
using System.Collections.Generic;

public class Level18 : LevelSetup {
	
	public List<Action> Setup()
	{
		List<Action> actions = new List<Action>();
		
		actions.Add(new MoveCameraAction("Drone"));
		actions.Add(new TextAction(4, "The teleoport drone is broken. But..."));
		actions.Add(new MoveCameraAction("Drone"));
		actions.Add(new TextAction(4, "We installed a new abilty! Drop stun bombs by tapping 'action-2'."));
		
		return actions;
	}
	
	
}