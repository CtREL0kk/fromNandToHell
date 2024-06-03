using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Rickroll : MonoBehaviour
{
    public void Start()
    {
        StartCoroutine(WaitForLevel());
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("Start Menu");
    }

    private IEnumerator WaitForLevel()
    {
       
        yield return new WaitForSeconds(30);
        SceneManager.LoadScene("Start Menu");
    }
}
