
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class PauseSkript : MonoBehaviour
{
    bool isPause = false;
    public KeyCode PauseKey = KeyCode.Escape;
    public GameObject menupanel;
    public TimerScript ts;
    public void ChangePauseStatus(bool pause)
    {
        isPause = pause;
        menupanel.SetActive(isPause);
        Time.timeScale = isPause ? 0 : 1;
    }
     void Start()
    {
        Time.timeScale = 1;
        menupanel.SetActive(false);
    }

    public void GoToMainMenu()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "SampleScene")
        {
            Session ses = new Session(ts.time);
            ses.Add();
        }
        if (ArbitrScript.Tank1 != null)
            Destroy(ArbitrScript.Tank1.gameObject);
        if (ArbitrScript.Tank2 != null)
            Destroy(ArbitrScript.Tank2.gameObject);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);

    }

    void Update()
    {
        if (Input.GetKeyDown(PauseKey) && !isPause)
        {
            ChangePauseStatus(true);
        }
    }
}
