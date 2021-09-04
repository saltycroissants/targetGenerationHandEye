using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using CsvHelper;
using System.Globalization;
using logvariables;
using currenttarget;
using global;
using UnityEngine.SceneManagement;

public class LogManager : MonoBehaviour
{
    public int playerInput = 0;
    public string controllerStatus = "notinitalized";
    // Start is called before the first frame update
    public GameObject controller;
    private int ballsLength = 24;
    private string today = DateTime.Now.ToString("M/d/yyyy");  //setDate
    private string csvlink1, csvlink2;
    private Info[] recordings;
    private List<BallPositionTracking> trackingList= new List<BallPositionTracking>();


    void Start()
    {
        //check if path exists
        Info.infoCount = 0;
        Info.isCSVwritten =false;
        
        recordings = new Info[ballsLength];  

        Debug.Log("today"+today);
        for(int i =0; i < recordings.Length; i++){
            recordings[i] = new Info(playerInput, Global.playerHand, today, Global.Condition, controllerStatus, 0, "none", "none", Global.Distance, Global.StartHeight, Global.Radius);
        }

        setDir();
        checkDirError();
        checkFileExists();
        
    }

    public void setDir()
    {
        csvlink1 = $@"C:\Users\user\Desktop\Eunji\Logs\PID{playerInput}\log_PID{playerInput}_C{Global.Condition}CS#{controllerStatus}.csv";
        csvlink2 = $@"C:\Users\user\Desktop\Eunji\Logs\PID{playerInput}\BallTrackinglog_PID{playerInput}_C{Global.Condition}CS#{controllerStatus}.csv";
    }

    //check if directory exists
    public void checkDirError()
    {
        string dir = $@"C:\Users\user\Desktop\Eunji\Logs\PID{playerInput}";

        if (!Directory.Exists(dir))
        {
            Debug.Log("directory missing for log");
            Application.Quit();
        }
  
    }
    //check if there is a file with same name
    public void checkFileExists()
    {
        if (File.Exists(csvlink1))
        {
            Debug.Log("ERROR : file with same name exists for log");
            Application.Quit();
            SceneManager.LoadScene("ERROR");
        } else if (File.Exists(csvlink2))
        {
            Debug.Log("ERROR : file with same name exists for controller tracking log");
            Application.Quit();
            SceneManager.LoadScene("ERROR");
        }
    }

    

    //get date, player ID, conditionID(constant), trial number on start
    //log should be recorded on button click. -> get target ID, start time on button click.
 
    public void onStartClickedLog()
    {   
        if(Info.infoCount < ballsLength)
        {
        string nowString = DateTime.Now.ToString("hh.mm.ss.ffffff");
        recordings[Info.infoCount].StartTime = nowString;
        }    
    }
        

    //get end time on target click.
    public void onTargetClickedLog()
    {
        //for ball controller tracking
        BallPositionTracking.isTargetTouched = true;

         if(Info.infoCount > ballsLength)
        {
            Application.Quit();
            Debug.Log("clicked afterGameEnd2");
        }
        else
        {
            //record endtime on target click.
            recordings[Info.infoCount].Endtime = DateTime.Now.ToString("hh.mm.ss.ffffff");
            recordings[Info.infoCount].TargetID = currentTarget.TARGET_ID;
            
            Debug.Log(Info.infoCount.ToString());

            if(isInfoEnd())
            {
                Debug.Log("WRITING TO CSV");
                //date, Player ID, condition ID, trial number, target ID, start time, end time
                writeToCSV(csvlink1, csvlink2);
                Info.isCSVwritten = true;
                
            }
            //after endtime record, increment count.
            Info.infoCount += 1;
               
        }
        
    }

    bool isInfoEnd() {
        if(Info.infoCount == (ballsLength-1)) return true;
        else return false;
    }

    //ball position 다른 CSV 파일에 기록하기
    void writeToCSV(string csvlink1, string csvlink2){
        
        using (var writer = new StreamWriter(csvlink1))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteHeader<Info>();
            csv.NextRecord();

            for(int i=0; i< recordings.Length; i++){
                csv.WriteRecord(recordings[i]);
                csv.NextRecord();
            }
        }

        if(Global.Condition < 5)
        {
            using (var writer = new StreamWriter(csvlink2))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteHeader<BallPositionTracking>();
                csv.NextRecord();

                for(int i=0; i< trackingList.Count; i++){
                    csv.WriteRecord(trackingList[i]);
                    csv.NextRecord();
                }
            }
        }
        

    }


    // Update is called once per frame
    void Update()
    {
        if(!Info.isCSVwritten && Global.Condition != 5)
        {
            string nowT = DateTime.Now.ToString("hh.mm.ss.ffffff");
            BallPositionTracking bpt = new BallPositionTracking(true, controller.transform.position.x.ToString(),  controller.transform.position.y.ToString(),  controller.transform.position.z.ToString(), nowT);
            trackingList.Add(bpt);
        }
    }
}
