using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace extendedtargets{
    public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    public int ballID{get;}
    public GameObject ball{get; set;}
    public Ball(int aBallID){
        ballID = aBallID;
    }
}
}

