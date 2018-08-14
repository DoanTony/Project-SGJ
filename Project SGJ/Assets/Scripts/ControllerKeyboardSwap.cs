using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControllerKeyboardSwap : MonoBehaviour {

    public ImprovedStandalone keyboard;
    public ImprovedStandalone joystick;
    public string KeyboardHorizontalAxis;
    public string JoystickHorizontalAxis;
    public bool isKeyboard;
    public bool isJoystick;

	void Update () {
	    if(Input.GetAxis(KeyboardHorizontalAxis) != 0)
        {
            isKeyboard = true;
            isJoystick = false;
        }	
        else if(Input.GetAxis(JoystickHorizontalAxis) != 0)
        {
            isJoystick = true;
            isKeyboard = false;
        }

        if(isKeyboard){
            keyboard.enabled = true;
            joystick.enabled = false;
        }
        else if (isJoystick)
        {
            keyboard.enabled = false;
            joystick.enabled = true;
        }
	}
}
