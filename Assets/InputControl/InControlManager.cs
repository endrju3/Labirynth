﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InControl;

public class InControlManager : MonoBehaviour {

	public static InControlManager instance;
	private PlayerSelectionMenuController playerSelectionMenuController;
	private int currentPlayer;
	private bool portalStarted;

	void Start ()
	{
		InputManager.Setup ();

//		List<InputDevice> devicesToDetach = new List<InputDevice> ();
//		if (SystemInfo.operatingSystem.ToUpper().Contains("WINDOWS"))
//		{
//			bool detachNext = false;
//			for (int i = 0; i < InputManager.Devices.Count; i++)
//			{
//				InputDevice input = InputManager.Devices[i];
//				if ((input.Name == "XBox 360 Controller") && (!detachNext))
//				{
//					detachNext = true;
//					continue;
//				}
//				if (detachNext)
//				{
//					devicesToDetach.Add(input);
//					detachNext = false;
//				}
//			}
//		}

		InputManager.AttachDevice( new UnityInputDevice( new KeyboardProfile1() ) );
		InputManager.AttachDevice( new UnityInputDevice( new KeyboardProfile2() ) );
		InputManager.AttachDevice( new UnityInputDevice( new CustomProfile() ) );

//		foreach(InputDevice device in InputManager.Devices)
//		{
//			Debug.Log("Attached device:" + device.Name);
//		}
//		
//		foreach(InputDevice device in devicesToDetach)
//		{
//			InputManager.DetachDevice(device);
//			Debug.Log("Detached device:" + device.Name);
//		}

		instance = this;

		if (Application.loadedLevel == 0)
		{
			currentPlayer = 1;
			LevelFinishedController.instance.setControllers(new List<InputController> ());
			//TextMesh levelScreen = GameObject.Find ("Level").GetComponent<TextMesh>();
			//levelScreen.text = "Zone " + (LevelFinishedController.instance.getLevel() + 1) + "/" + LevelFinishedController.instance.getTotalLevels();
			if (LevelFinishedController.instance.getMaxLevel() == 0 && LevelFinishedController.instance.getEnableAllLevels() == false)
			{
				GameObject.Find ("StickContainer").SetActive(false);
			}
		}

		foreach(InputController inputController in LevelFinishedController.instance.getAllControllers())
		{
			inputController.updatePlayer();
		}
	}

	void Update () 
	{
		InputManager.Update ();

		if ((Application.loadedLevel == 0) && (!portalStarted))
		{
			InputDevice input = InputManager.ActiveDevice;

			if (SplashController.instance.isVisible())
			{
				if ((input.GetControl(InputControlType.LeftTrigger).WasPressed) ||
				    (input.GetControl(InputControlType.LeftBumper).WasPressed) ||
				    (input.GetControl(InputControlType.RightTrigger).WasPressed) ||
				    (input.GetControl(InputControlType.RightBumper).WasPressed) ||
				    (input.GetControl(InputControlType.Action1).WasPressed) ||
				    (input.GetControl(InputControlType.Action2).WasPressed) ||
				    (input.GetControl(InputControlType.Action3).WasPressed) ||
				    (input.GetControl(InputControlType.Start).WasPressed) )
				{
					SplashController.instance.setVisible(false);
					return;
				}
			}

			if (input.GetControl(InputControlType.Select).WasPressed)
			{
				LevelFinishedController.instance.setLevel(0);
				Application.LoadLevel(0);
			}
				
			bool left = false;
			bool right = false;
			if ((input.GetControl(InputControlType.LeftTrigger).WasPressed) || 
			    (input.GetControl(InputControlType.LeftBumper).WasPressed) ||
			    (input.GetControl(InputControlType.LeftStickX).WasPressed) ||
			    (input.GetControl(InputControlType.LeftStickY).WasPressed))
			{
				left = true;
			}
			if ((input.GetControl(InputControlType.RightTrigger).WasPressed) || 
			    (input.GetControl(InputControlType.RightBumper).WasPressed) ||
			    (input.GetControl(InputControlType.RightStickX).WasPressed) ||
			    (input.GetControl(InputControlType.RightStickY).WasPressed))
			{
				right = true;
			}
			if (input.GetControl(InputControlType.Start).WasPressed)
			{
				collectPlayersAndStart();
			}

			if ( (MenuUpdate.instance.getplayersInside() >=1) && (input.GetControl(InputControlType.Start).WasPressed)
			    && (TextBarrier2.instance.getPlayersInPortal() <2) )
			{
				Application.OpenURL("http://tiny.cc/bffnew");
			}

			if ( (MenuExit.instance.getplayersInside() >=1) && (input.GetControl(InputControlType.Start).WasPressed) 
			    && (TextBarrier2.instance.getPlayersInPortal() <2) )
			{
				Application.Quit();
			}

			bool keyboard = input.Meta.Contains("keyboard");
			bool found  = false;

			// hasChanged to avoid key holding
			if (left || right)
			{
				foreach(InputController inputController in LevelFinishedController.instance.getControllers())
				{
					if ((inputController.getDevice() == getDeviceId(input)) && (inputController.isLeft() == left))
					{
						found = true;
					}
				}
				if (!found)
				{
					if (LevelFinishedController.instance.getControllers().Count != 4)
					{
						LevelFinishedController.instance.getControllers().Add(
							new InputController(getDeviceId(input), keyboard, left, playerSelectionMenuController, currentPlayer++));
					}
				}
			}
		}
		else
		{
			if (InputManager.ActiveDevice.GetControl(InputControlType.Select).WasPressed)
			{
				LevelFinishedController.instance.Reset();
				Application.LoadLevel(0);
			}
		}

		foreach(InputController input in LevelFinishedController.instance.getAllControllers())
		{
			input.React();
		}
	}

	public void SetPlayerSelectionMenu(PlayerSelectionMenuController playerSelectionMenuController)
	{
		currentPlayer = 1;
		this.playerSelectionMenuController = playerSelectionMenuController;
	}

	public void RemovePlayerSelectionMenu()
	{
		this.playerSelectionMenuController = null;
	}

	private int getDeviceId(InputDevice inputDevice)
	{
		for (int i = 0; i < InputManager.Devices.Count; i++)
		{
			if (InputManager.Devices[i].Meta == inputDevice.Meta)
			{
				return i;
			}
		}

		throw new UnityException("Can't find device = " + inputDevice.Meta);
	}

	private void collectPlayersAndStart() 
	{
		List<InputController> usedControllers = new List<InputController> ();
		Bounds portalBounds = GameObject.Find ("Portal").renderer.bounds;
		foreach(InputController inputController in LevelFinishedController.instance.getControllers())
		{
			GameObject player = GameObject.Find ("Player" + inputController.getPlayerId());
			if (portalBounds.Intersects(player.renderer.bounds))
			{
				usedControllers.Add(inputController);
			}
		}
		if (usedControllers.Count == 0) 
		{
			return;
		}
		if (usedControllers.Count == 1) 
		{	
			InputController playerController = usedControllers[0];
			int deviceId = playerController.getDevice();
			if (playerController.isKeyboard())
			{
				string profileName = InputManager.Devices[playerController.getDevice()].Meta.Contains ("1 player") ? "2 player" : "1 player";
				for(int i=0 ; i < InputManager.Devices.Count; i++)
				{
					if (InputManager.Devices[i].Meta.Contains (profileName))
					{
						deviceId = i;
						break;
					}
				}
			}
			usedControllers.Add (new InputController (deviceId, 
			                                          playerController.isKeyboard(), 
			                                          !playerController.isLeft(), playerSelectionMenuController, 5));
		}
		portalStarted = true;
		LevelFinishedController.instance.setControllers(usedControllers);
		LevelFinishedController.instance.LevelCounter();
		StartCoroutine(TimePortalController.instance.startTimePortal());
	}
}
