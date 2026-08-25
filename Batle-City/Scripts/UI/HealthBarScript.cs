using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public GameObject Tank; 
    private Health health;
    private RectTransform id;                                                                                                                            
    private int MaxHealth;                                                                                                                               
    private float maxWith;                                                                                                                               
    Text Deathes;                                                                                                                                        
    void Start()                                                                                                                                         
    {                                                                                                                                                    
        Initialize();                                                                                                                                    
    }                                                                                                                                                    
     void Initialize()                                                                                                                                   
    {                                                                                                                                                    
        Deathes = gameObject.transform.Find("DeathCounter").GetComponent<Text>();                                                                        
        health = Tank.GetComponent<Health>();                                                                                                            
        id = gameObject.transform.Find("GreenBar").GetComponent<RectTransform>();                                                                        
        MaxHealth = health.HealtValue;                                                                                                                   
        maxWith = id.sizeDelta.x;                                                                                                                        
    }                                                                                                                                                    
    void Update()
    {
        try
        {
            if (health.HealtValue == 0)
            {
                id.sizeDelta = new Vector2(0, id.sizeDelta.y);
                Deathes.text = "Deathes: " + Tank.GetComponent<Tank>().DeathCount;
            }
            else if (health.HealtValue == MaxHealth)
            {
                Deathes.text = "Deathes: " + Tank.GetComponent<Tank>().DeathCount;
                id.sizeDelta = new Vector2(maxWith, id.sizeDelta.y);
            }
            else
            {
                Deathes.text = "Deathes: " + Tank.GetComponent<Tank>().DeathCount;
                float newsize = (maxWith / MaxHealth) * health.HealtValue;
                id.sizeDelta = new Vector2(newsize, id.sizeDelta.y);
            }
        }
        catch (NullReferenceException)
        {
            Initialize();
        }  
    }
}