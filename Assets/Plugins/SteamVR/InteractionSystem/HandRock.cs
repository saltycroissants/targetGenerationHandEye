using UnityEngine;
using Valve.VR.InteractionSystem;


public class HandRock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        foreach(var hand in Player.instance.hands)
        {
           hand.ShowController();
        }
    }
}
