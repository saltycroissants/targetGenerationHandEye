using UnityEngine;
using System;
using currenttarget;
using extendedtargets;
using logvariables;
using global;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class TestClickManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject flatball = null;
    public GameObject startButton = null;
    
    
    public float RADIUS = 2.0f; //radius of circles' trajectory. NOT radius of the flatball.
    public double CENTER_DISTANCE = 5;
    public Material BALLCOLOR;
    public Material successM;
    public Material errorM;
    public Material whiteM;

    private int ball_ID; //for theta.

    private static int ballsLength = 5;
    private Ball[] ballsArr = new Ball[ballsLength];
  
    HashSet<int> ballIDs = new HashSet<int>();

    private bool isFirst = true;
    private bool isDeleted = false;
    public AudioSource audioSource;
    public AudioSource audioSource2;
    public int conditionId = 1;
    public string playerHandInput = "right";
    
    private int ballInfoCount = 0;

 void Start()
    { 
        setInfoBasic();
        currentTarget.TARGETCOUNT = ballsLength -1;
        currentTarget.isTargetTouched = false;

        Debug.Log("startheight" +Global.StartHeight.ToString());
        Debug.Log("camera" +Camera.main.transform.position.z.ToString());
        Debug.Log("distance" + Global.Distance.ToString());

        setRandomSet();
        setBallsArr();
    }

    public void setRandomSet()
    {  
        System.Random rnd = new System.Random();
        
        for (int i = 0; i < ballsLength; i++) 
        {
            while (!ballIDs.Add(rnd.Next(0,ballsLength)));
        }
        
    }

    public void setBallsArr()
    {
        //initialize balls array
        int c = 0;
        foreach (int id in ballIDs)
        {
            ballsArr[c] = new Ball(id);
            //Debug.Log(id);
            c++;
        }   
    }

    public void setInfoBasic()
    {
        
        Global.Condition = conditionId;
        //camera position must be positive
        double distanceD =  (startButton.transform.position.z - Camera.main.transform.position.z) + CENTER_DISTANCE;
        Global.Distance = (float)distanceD;
        Global.StartHeight = (float)startButton.transform.position.y;
        Global.Radius = RADIUS;
        Global.playerHand = playerHandInput;
        
    }
    public void onStartClicked(){

        if(isStartBeforeTarget())
        {
            //change start button color to red
            Debug.Log("when start is clicked and target was not");
            startButton.GetComponent<MeshRenderer>().material = errorM;
        }
        else{
                audioSource2.Play();
            
                //when first row is done, reset to do second row. 
                setBall(RADIUS, CENTER_DISTANCE, BALLCOLOR, ballsArr);
                isFirst = false;
           
            startButton.SetActive(false);
        }
    }

    public bool isStartBeforeTarget(){
        if(currentTarget.isTargetTouched == false && isFirst == false) return true;
        else return false;
    }

    public void onTargetclicked() {
        if(currentTarget.TARGETCOUNT >= ballsLength && currentTarget.TARGETCOUNT < 0)
        {
            Debug.Log("targetcount ERR : " + currentTarget.TARGETCOUNT.ToString());
        }
        else{
            Debug.Log("on target clicked : " + currentTarget.TARGETCOUNT.ToString());
            currentTarget.isTargetTouched =true;
            GameObject targetNow = currentTarget.currentTargetObject;
            targetNow.GetComponent<MeshRenderer>().material = successM;
            audioSource.Play();
            currentTarget.TARGETCOUNT = currentTarget.TARGETCOUNT -1;
            //yield return new WaitForSeconds(1);
            targetNow.SetActive(false);

            startButton.SetActive(true);
            startButton.GetComponent<MeshRenderer>().material = whiteM;
            ballInfoCount += 1;
        }
    }

    private float toDegree (double angle){
        float degree = (float)(angle * 180.0 / Math.PI) ;
        return degree;
    }

    void setBall(float radius, double distance_from_center, Material ballcolor, Ball[] barray){
        
        //theta is rotation degree, radian 기준, 15도
        //Debug.Log("BALL COUNT IS :" + currentTarget.TARGETCOUNT.ToString());
        double distance = Math.Sqrt( radius * radius + distance_from_center * distance_from_center );
       
        flatball.GetComponent<MeshRenderer>().material = ballcolor;

        ball_ID = barray[currentTarget.TARGETCOUNT].ballID;
        //double theta = (2 * Math.PI / 15) * ball_ID;
        double theta = (Math.PI / 12) * ball_ID;
        double sinValue = Math.Sin(theta);
        double cosValue = Math.Cos(theta);

        float yValue = (float)(radius * sinValue);
        float xValue = (float)(radius * cosValue);
        float zValue = (float)distance_from_center;

        //float zRotation = toDegree(theta);

        barray[currentTarget.TARGETCOUNT].ball = Instantiate(
        flatball,
        startButton.transform.position + new Vector3(xValue ,yValue, zValue),
        Quaternion.Euler(0, 0, 0)
        );
        //Quaternion.Euler(0, 0, zRotation)
        //int ballNameNum = 26 - TARGETCOUNT;
        barray[currentTarget.TARGETCOUNT].ball.name = "Ball" + (ball_ID).ToString();
        currentTarget.currentTargetObject = barray[currentTarget.TARGETCOUNT].ball;
        currentTarget.TARGET_ID = ball_ID;
       
        currentTarget.isTargetTouched = false;
        isDeleted = false; 
        Debug.Log("ball_id:"+ ball_ID.ToString() + "xvalue: " + xValue.ToString() + " yvalue" + yValue.ToString() + "zvalue" +zValue.ToString());
    }


   
    // Update is called once per frame
    void Update()
    {   
        //deactivate ball if the target had been touched
        if(ballInfoCount >= 5)
        {
            SceneManager.LoadScene("End");
        }
      
    }
}
