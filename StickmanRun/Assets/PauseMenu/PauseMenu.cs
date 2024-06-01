using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

namespace PauseMenu
{
    public class PauseMenu : MonoBehaviour
    {
        private bool isOnPause;
        private AudioSource musicAudioSource;
        private AudioSource soundsAudioSource;
        private PostProcessVolume postProcess;
        private AudioClip espSound;
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject pauseWindow;
        [SerializeField] private GameObject deathMenu;
        [SerializeField] private GameObject settingsWindow;

        public void Start()
        {
            musicAudioSource = GameObject.FindWithTag("MusicAudioSource").GetComponent<AudioSource>();
            soundsAudioSource = GameObject.FindWithTag("SoundsAudioSource").GetComponent<AudioSource>();
            postProcess = GameObject.FindWithTag("MainCamera").GetComponent<PostProcessVolume>();
            espSound = Resources.Load<AudioClip>("Audio/Sounds/Button");
        }

        public void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape) || deathMenu.activeSelf || settingsWindow.activeSelf) return;
            soundsAudioSource.PlayOneShot(espSound);
            if (isOnPause)
                Resume();
            else
                Pause();
        }

        public void Resume()
        {
            musicAudioSource.UnPause();
            postProcess.enabled = true;
            pauseWindow.SetActive(false);
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            isOnPause = false;
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1f;
        }
        
        private void Pause()
        {
            musicAudioSource.Pause();
            postProcess.enabled = false;
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
