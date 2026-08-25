using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public float time = 0;
    float step = 0.01f;
    Text shower;
    void Start()
    {
        shower = gameObject.GetComponent<Text>();
        shower.text = "0:00";
        StartCoroutine(TimeCount());
    }
    IEnumerator TimeCount()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            time += step;
            if(Mathf.Approximately(time - (int)time,  0.6f))
            {
                time += 1;
                time -= 0.6f;
            }
            string s = System.Math.Round(time, 2).ToString().Replace(',', ':');
            if (s.Length == 1)
            {
                s += ':';
            }
            while (s.Length <= 3)
            {
                s += '0';
            }
            shower.text = s;
        }
    }
}
