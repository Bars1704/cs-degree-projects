using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuNav : MonoBehaviour
{
    static public void GoNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void TwoAIPlay()
    {
        GameManager.CurrentPlayMode = PlayMode.TwoAI;
        GoNextScene();
    }
    public void TwoPlayersPlay()
    {
        GameManager.CurrentPlayMode = PlayMode.TwoPlayers;
        GoNextScene();
    }
    public void PlayWithAI()
    {
        GameManager.CurrentPlayMode = PlayMode.WithAI;
        GoNextScene();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
