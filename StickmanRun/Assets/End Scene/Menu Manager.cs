using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [System.Obsolete]
    public void Replay()
    {
        Application.LoadLevel("Game");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}