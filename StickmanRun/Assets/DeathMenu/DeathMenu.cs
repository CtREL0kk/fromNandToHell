using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

namespace DeathWindow
{
    public class DeathWindow : MonoBehaviour
    {
        public void BackToMenu()
        {
            SceneManager.LoadScene("Start Menu");
        }

        public void Restart()
        {
            SceneManager.LoadScene("Game");
        }
    }
}
