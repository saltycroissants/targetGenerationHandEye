using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using global;
using logvariables;

public class BallController : MonoBehaviour
{
    public LogManager logScript;
    public TestClickManager testclickScript;
    public ClickManager clickScript;

    public GameObject startB;
    public GameObject targetB;

    public Material whiteM;
    public Material greenM;


    void OnTriggerEnter(Collider col)
    {
        //Debug.Log("is triggered");
        if (col.tag == "start")
        {
            
            Debug.Log("is triggered start");
            startB.GetComponent<MeshRenderer>().material = whiteM;

            if(Global.Condition ==0)
            {
                testclickScript.onStartClicked();
            }
            else
            {
                clickScript.onStartClicked();
                logScript.onStartClickedLog();
            }   
        }
            
        if (col.tag == "target")
        {
            Debug.Log("is triggered target");
            targetB.GetComponent<MeshRenderer>().material = greenM;
            
            if(Global.Condition ==0)
            {
                testclickScript.onTargetclicked();
            }
            else
            {
                clickScript.onTargetclicked();
                logScript.onTargetClickedLog();

            }   
        }
    }
    void OnTriggerExit(Collider col)
    {
        Debug.Log("trigger exit");
        if (col.tag == "target")
        {
            BallPositionTracking.isTargetTouched = false;
        }
    }

}