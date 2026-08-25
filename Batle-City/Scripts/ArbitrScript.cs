using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArbitrScript : MonoBehaviour
{
    public KeyCode[][] Controls = new KeyCode[][] { 
    new KeyCode[] {KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.Space },
    new KeyCode[] {KeyCode.W,KeyCode.S,KeyCode.A,KeyCode.D,KeyCode.Q }
};

    public static GameObject Tank1; 
    public static GameObject Tank2;
    public static PlayerSettings settings;
    public static bool OneSelected { get { return (Tank1 != null) || (Tank2 != null); } }
    public static bool BothSelected { get { return (Tank1 != null) && (Tank2 != null); } }
    GameObject Spawner1;
    GameObject Spawner2;

    public void ChangeSound()
    {
        settings.Sound = !settings.Sound;
    }
    public void ChangeMusik()
    {
        settings.Musik = !settings.Musik;
        if (SceneManager.GetActiveScene().buildIndex <= 1)
        {
            GameObject.Find("Main Camera").GetComponent<AudioListener>().enabled= settings.Musik;
        }
    }
    void Start()
    {
        if (settings == null)
        {
            settings = new PlayerSettings();
        }
        Initialize();
        GameObject.Find("Main Camera").GetComponent<AudioListener>().enabled = settings.Musik;
    }
    public void Initialize()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            var spawners = GameObject.FindObjectsOfType<Spawner>();
            Spawner1 = spawners[0].gameObject;
            Spawner2 = spawners[1].gameObject;
            GameObject[] go = GameObject.FindGameObjectsWithTag("Spawner");
            go = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
            foreach (GameObject g in go)
            {
                if (g.activeSelf == false && g.name.Contains("Tank"))
                {
                    g.SetActive(true);
                }
            }
            Tank1.transform.position = Spawner1.transform.position;
            Tank2.transform.position = Spawner2.transform.position;
            var healthBars = FindObjectsOfType<HealthBarScript>();
            healthBars[0].Tank = Tank1;
            healthBars[1].GetComponent<HealthBarScript>().Tank = Tank2;
            InitializeTankKeys( ref Tank1, Controls[0]);
            InitializeTankKeys( ref Tank2, Controls[1]);
            Tank1.transform.Find("Weapons").GetComponent<Weapon>().PlayerId = 1;
            Tank2.transform.Find("Weapons").GetComponent<Weapon>().PlayerId = 2;
            Tank1.GetComponent<Tank>().RespawnPoint = Spawner1;
            Tank2.GetComponent<Tank>().RespawnPoint = Spawner2;
            Spawner1.GetComponent<Spawner>().SpawnMe = Tank1;
            Spawner1.GetComponent<Spawner>().SpawnMe = Tank2;
        }
    }
    void InitializeTankKeys( ref GameObject Tank, KeyCode[] Controls)
    {
        var TankControls = Tank.GetComponent<Tank>();
        TankControls.up = Controls[0];
        TankControls.down = Controls[1];
        TankControls.left = Controls[2];
        TankControls.right = Controls[3];
        Tank.transform.Find("Weapons").GetComponent<Weapon>().shoot = Controls[4];
    }

}
