using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeChanger : MonoBehaviour
{
    public  float divider = 2000;
    public  float MinSize;
    public  float MaxSize;
    public  float Offset = 500;
    Camera Cam;

    void Start()
    {
        try
        {
            Cam = transform.Find("Main Camera").GetComponent<Camera>();
          
        }
        catch {
            Cam = GetComponent<Camera>();
        }
        Cam.orthographic = true;
    }
    void Update()
    {
        var dist = 1f;
        if (ArbitrScript.Tank2 != null)
        {
            var T1 = ArbitrScript.Tank1.transform.position;
            var T2 = ArbitrScript.Tank2.transform.position;
            var x = T1.x + T2.x;
            var y = T1.y + T2.y;
            transform.position = new Vector3((x/2)+Offset,y/2,transform.position.z);
            dist = Vector2.Distance(T1,T2);
        }
        //else if (ArbitrScript.Tank1 != null)
        //{   
        //    Cam.transform.position = ArbitrScript.Tank1.transform.position;
        //}
        var size = Screen.width / divider * dist;
        Cam.orthographicSize = Mathf.Clamp(size,MaxSize,MinSize);
    }
}
