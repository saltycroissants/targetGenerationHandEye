using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject text = new GameObject();
        TextMesh t = text.AddComponent<TextMesh>();
        t.text = "START";
        t.fontSize = 15;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
