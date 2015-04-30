﻿using UnityEngine;
using System.Collections.Generic;

public class Level34 : LevelSetup {
	
	public List<Action> Setup()
	{
		List<Action> actions = new List<Action>();
		
		actions.Add (new ChangeCamAction());
		actions.Add(new MoveCameraAction("Lighthouse"));
		actions.Add(new TextAction(4, "There are often multiple solutions to any problem."));
		
		return actions;
	}
	
	
}
