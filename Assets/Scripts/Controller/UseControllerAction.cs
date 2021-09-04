using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using gaze;

public class UseControllerAction : MonoBehaviour
{
    public LogManager logScript;
    public ClickManager clickScript;
    public GameObject startB;
    public GameObject targetB;
    public Material whiteM;
    public Material greenM;
    public SteamVR_Input_Sources handType = SteamVR_Input_Sources.Any;
    public SteamVR_Action_Boolean grabPinch;
    // Start is called before the first frame update

    // Update is called once per frame

    private void Start() {}
    private void OnEnable ()
    {
        if (grabPinch != null)
        {
            //grabPinch.AddOnChangeListener(OnTriggerPressedOrReleased,handType);
            grabPinch.AddOnStateDownListener(Press, handType);
        }
    }
 
    private void OnDisable()
    {
        if (grabPinch != null)
        {
            //grabPinch.RemoveOnChangeListener(OnTriggerPressedOrReleased, handType);
            grabPinch.RemoveOnStateDownListener(Press, handType);
            Debug.Log("Trigger Pressed disable");
        }
    }
    private void Press(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        //put your stuff here
        Debug.Log("Trigger Pressed");
        if(GazeVariable.gazeAtTarget)
            {
                Debug.Log("is triggered target");
                //targetB.GetComponent<MeshRenderer>().material = greenM;
                clickScript.onTargetclicked();
                logScript.onTargetClickedLog();

            }

            if(GazeVariable.gazeAtStart)
            {
                Debug.Log("is triggered start");
                //startB.GetComponent<MeshRenderer>().material = whiteM;
                clickScript.onStartClicked();
                logScript.onStartClickedLog();
            }
    }
}
