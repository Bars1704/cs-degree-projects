using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatsShower : MonoBehaviour
{
    public GameObject Tank;
    private GameObject Wheels;
    private GameObject Weapon;
    private GameObject Body;

    private Text FinalSpeed;
    private Text FinalHealth;

    void Start()
    {
        FinalHealth = gameObject.transform.Find("FinalHealth").GetComponent<Text>();
        FinalSpeed = gameObject.transform.Find("FinalSpeed").GetComponent<Text>();

        Wheels = Tank.transform.Find("Wheels").gameObject;
        Weapon = Tank.transform.Find("Weapons").gameObject;
        Body = Tank.transform.Find("Body").gameObject;
        OnChange();
    }
    
    public void OnChange()
    {
        Wheels = Tank.transform.Find("Wheels").gameObject;
        Weapon = Tank.transform.Find("Weapons").gameObject;
        Body = Tank.transform.Find("Body").gameObject;

        FinalHealth.text = "Health: " + Tank.GetComponent<Tank>().health.HealtValue;
        FinalSpeed.text = "Speed: " + System.Math.Round(Tank.GetComponent<Tank>().speed,2);
    }

    public void Sumbit()
    {
        if (!ArbitrScript.OneSelected)
        {
            DontDestroyOnLoad(Tank);
            Tank.gameObject.name = "Tank1";
            Tank.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            ArbitrScript.Tank1 = Tank;

        }
        else
        {
            DontDestroyOnLoad(Tank);
            Tank.gameObject.name = "Tank2";
            Tank.SetActive(false);
            ArbitrScript.Tank2 = Tank;
        }

        if (ArbitrScript.BothSelected)
        {
            SceneManager.LoadScene(2);
        }
    }
}
