using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

namespace PauseMenu
{
    public class PauseMenu : MonoBehaviour
    {
        private bool isOnPause;
        [SerializeField] private PostProcessVolume volume;
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject pauseWindow;
        [SerializeField] private GameObject deathMenu;
        [SerializeField] private GameObject settingsWindow;

        public void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape) || deathMenu.activeSelf || settingsWindow.activeSelf) return;
            if (isOnPause)
                Resume();
            else
                Pause();
        }

        public void Resume()
        {
            volume.enabled = true;
            pauseWindow.SetActive(false);
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            isOnPause = false;
        }

        public void Restart()
        {
            SceneManager.LoadScene("Game");
            Time.timeScale = 1f;
        }
        
        private void Pause()
        {
            volume.enabled = false;
            pauseWindow.SetActive(true);
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            isOnPause = true;
        }

        public void BackToMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Start Menu");
        }
    }
}
