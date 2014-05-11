﻿using UnityEngine;
using System.Collections.Generic;

public class Level1 : LevelSetup {

	public List<Action> Setup()
	{
		List<Action> actions = new List<Action>();

		actions.Add(new MoveCameraAction("Lighthouse"));
		actions.Add(new TextAction(0, "Walk into the light-turret. Rotate left and right to see into the maze."));
		actions.Add(new MoveCameraAction("Key"));
		actions.Add(new TextAction(1, "Collect all the energy to fuel the time machine."));
		actions.Add(new MoveCameraAction("Player2"));
		actions.Add(new TextAction(0, "When all the energy is collected everyone needs to high-five to time jump."));
		actions.Add(new MoveCameraAction("Lighthouse"));
		actions.Add(new TextAction(1, "Hold down your trigger for 1 second to exit the light-turret."));

		return actions;
	}


}
