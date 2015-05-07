﻿using UnityEngine;
using System;
using System.Collections.Generic;

public class Zap : PuzzleTemplate
{
	//0 = wall/post , 2 = space, 3 = block, 4 = mummy, 5 = key 6 = sleepingMonster, 7 = vertical trigger, 8 = horozontal wall trigger, 9 = new mazes trigger
	public Zap() :base()
	{
		sizeX = 9;
		sizeZ = 9;
		
		extX = 2;
		extZ = 2;
		internalSize = sizeZ - 2; 
		
		templateSizeX = 17;
		templateSizeZ = 17;
		template = new int[17,17]
		{
			{2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
			{2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
			{2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
			{2,2,2,0,0,0,2,2,2,2,2,2,2,2,2,2,2},
			{2,2,2,0,5,0,2,2,2,2,2,2,2,2,2,2,2},
			{2,0,0,0,2,0,0,0,2,2,2,2,2,2,2,2,2},
			{2,0,2,2,6,2,3,2,2,2,2,2,2,2,2,2,2},
			{2,0,0,0,2,0,0,0,2,2,2,0,0,0,2,2,2},
			{2,2,2,0,2,0,2,2,2,2,2,0,5,0,2,2,2},
			{2,2,2,0,2,0,2,2,2,0,0,0,2,0,0,0,2},
			{2,2,2,0,2,0,2,2,2,2,3,2,6,2,2,0,2},
			{2,2,2,0,2,0,2,2,2,0,0,0,2,0,0,0,2},
			{2,2,2,0,2,0,2,2,2,2,2,0,2,0,2,2,2},
			{2,2,2,0,2,0,0,0,2,0,0,0,2,0,2,2,2},
			{2,2,2,0,2,2,2,0,2,0,2,2,2,0,2,2,2},
			{2,2,2,0,0,0,0,0,2,0,0,0,0,0,2,2,2},
			{2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2}
		};
		
		extededTemplate = new int[sizeZ * 2 -1, sizeX * 2 -1]; 
		init ();
	}
}