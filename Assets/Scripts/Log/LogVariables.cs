namespace logvariables
{ 
    public class Info
    {
        public int PlayerID{get; set;}
        //0,1,2
        public string DateToday{get; set;}
        public int ConditionID{get; set;}
        public string ControllerStatus{get; set;}
        public int TargetID{get; set;}
        public string StartTime{get;set;}
        public string Endtime{ get; set;}
        public float Distance{get; set;}
        public float Height{get; set;}
        public float Radius{get; set;}  
        public static bool isCSVwritten{get;set;}
        public static string playerHand {get; set;}

        //player별로 가장 처음일 때는 파일을 생성. 그 외에는 csv file에 append.
        public static int infoCount{get; set;}
        public Info
        (
            int aPlayerID,
            string aPlayerHand,
            string aDateToday, 
            int aconditionID, 
            string aController, 
            int aTargetID, 
            string aStartTime, 
            string aEndTime, 
            float adistance,
            float astartBheight,
            float aradius
        )

        {
            //condition id, distnce, start height은 STATIC VARIABLE -> csv header 때문에
            PlayerID = aPlayerID;
            playerHand = aPlayerHand;
            DateToday = aDateToday;
            ConditionID = aconditionID;
            aController = ControllerStatus;
            TargetID = aTargetID;
            StartTime = aStartTime;
            Endtime = aEndTime;
            Distance = adistance;
            Height = astartBheight;
            Radius = aradius;
            
        }
    
    }

    //손에 달린 공의 움직임 tracking 하는 클래스
    public class BallPositionTracking 
    {
        public string x{get; set;}
        public string y{get; set;}
        public string z{get; set;}

        public string currentTime {get; set;}
        public static bool isTargetTouched {get; set;}

        public BallPositionTracking(bool isTouched, string ax, string ay, string az, string current)
        {
            isTargetTouched = isTouched;
            x = ax; y = ay; z = az;
            currentTime = current;
        }
    }

    public class EyePositionTracking
    {
        
    }
    
}

