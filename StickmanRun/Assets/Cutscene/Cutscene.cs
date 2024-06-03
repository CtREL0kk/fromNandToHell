using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Cutscene : MonoBehaviour
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
       
        yield return new WaitForSeconds(55);
        SceneManager.LoadScene("Start Menu");
    }
}
