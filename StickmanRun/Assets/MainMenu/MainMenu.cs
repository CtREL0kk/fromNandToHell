using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Random Scene");
    }

    public void PlayStory()
    {
        SceneManager.LoadScene("Story");
    }
    
    public void PlayPrank()
    {
        SceneManager.LoadScene("Rickroll");
    }

    public void ExitGame()
    {
        Debug.Log("Игра закрылась");
        Application.Quit();
    }
}
