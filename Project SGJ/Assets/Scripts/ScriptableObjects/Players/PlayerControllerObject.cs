using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Controller/Player", order = 1)]
public class PlayerControllerObject : ScriptableObject
{
    public enum Controls { P1, P2, P3, P4 }
    public Controls control;


    private string defaultHorizontal = "Horizontal_";
    private string defaultVertical= "Vertical_";
    private string defaultDash = "Dash_";
     public string defaultjoystickPrefix = "J_";

    public string horizontalAxe;
    public string horizontalAxeJoystick;
    public string verticalAxe;
    public string verticalAxeJoystick;
    public string dash;

    private void OnEnable()
    {
        switch (control)
        {
            case Controls.P1:
                horizontalAxe = defaultHorizontal + "P1"; 
                horizontalAxeJoystick = defaultjoystickPrefix + defaultHorizontal + "P1";
                verticalAxe = defaultVertical + "P1";
                verticalAxeJoystick = defaultjoystickPrefix + defaultVertical + "P1";
                dash = defaultDash + "P1";
                break;

            case Controls.P2:
                horizontalAxe = defaultHorizontal + "P2";
                horizontalAxeJoystick = defaultjoystickPrefix + defaultHorizontal + "P2";
                verticalAxe = defaultVertical + "P2";
                verticalAxeJoystick = defaultjoystickPrefix + defaultVertical + "P2";
                dash = defaultDash + "P2";
                break;

            case Controls.P3:
                horizontalAxe = defaultHorizontal + "P3";
                horizontalAxeJoystick = defaultjoystickPrefix + defaultHorizontal + "P3";
                verticalAxe = defaultVertical + "P3";
                verticalAxeJoystick = defaultjoystickPrefix + defaultVertical + "P3";
                dash = defaultDash + "P3";
                break;

            case Controls.P4:
                horizontalAxe = defaultHorizontal + "P4";
                horizontalAxeJoystick = defaultjoystickPrefix + defaultHorizontal + "P4";
                verticalAxe = defaultVertical + "P4";
                verticalAxeJoystick = defaultjoystickPrefix + defaultVertical + "P4";
                dash = defaultDash + "P4";
                break;
        }
    }
}