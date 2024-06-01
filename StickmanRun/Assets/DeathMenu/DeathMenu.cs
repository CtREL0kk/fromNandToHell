using UnityEngine;
using UnityEngine.SceneManagement;

namespace DeathMenu
{
    public class DeathWindow : MonoBehaviour
    {
        public void BackToMenu()
        {
            SceneManager.LoadScene("Start Menu");
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
