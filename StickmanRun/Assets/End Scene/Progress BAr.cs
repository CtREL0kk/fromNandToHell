using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image progressBarImage;
    public float maxProgress = 100f;
    public float currentProgress = 0f;

    public void SetProgress(float progress)
    {
        //currentProgress = Mathf.Clamp(progress, 0f, maxProgress);
        var fillAmount = currentProgress / maxProgress;
        progressBarImage.fillAmount = fillAmount;
    }

    private void Update()
    {
        SetProgress(currentProgress);
    }
}
