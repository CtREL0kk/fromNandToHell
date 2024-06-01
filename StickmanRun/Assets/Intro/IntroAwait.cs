using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class IntroAwait : MonoBehaviour
{
    public void Start()
    {
        StartCoroutine(WaitForLevel());
    }

    private IEnumerator WaitForLevel()
    {
        yield return new WaitForSeconds(14);
        SceneManager.LoadScene("Start Menu");
    }
}

