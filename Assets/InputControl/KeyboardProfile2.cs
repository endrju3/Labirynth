using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InControl
{
	public class KeyboardProfile2 : UnityInputDeviceProfile
	{
		public KeyboardProfile2()
		{
			Name = "Keyboard";
			Meta = "A keyboard profile for 1 player.";
			
			SupportedPlatforms = new[]
			{
				"Windows",
				"Mac",
				"Linux"
			};
			
			Sensitivity = 1.0f;
			LowerDeadZone = 0.0f;
			
			ButtonMappings = new[]
			{
				new InputControlMapping
				{
					Handle = "Action",
					Target = InputControlType.LeftTrigger,
					Source = KeyCodeButton( KeyCode.LeftAlt )
				},
				new InputControlMapping
				{
					Handle = "Action2",
					Target = InputControlType.LeftBumper,
					Source = KeyCodeButton( KeyCode.Space )
				}
			};
			
			AnalogMappings = new[]
			{
				new InputControlMapping
				{
					Handle = "Arrow Keys X",
					Target = InputControlType.LeftStickX,
					Source = KeyCodeAxis( KeyCode.A, KeyCode.D )
				},
				new InputControlMapping
				{
					Handle = "Arrow Keys Y",
					Target = InputControlType.LeftStickY,
					Source = KeyCodeAxis( KeyCode.S, KeyCode.W )
				}
			};
		}
	}
}
