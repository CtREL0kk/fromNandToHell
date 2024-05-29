using UnityEngine;

public class IndicatorAnimator : MonoBehaviour
{
    [SerializeField] float animationSpeed = 1f;
    [SerializeField] float scaleRange = 0.1f;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        var scale = 2 + Mathf.Sin(Time.time * animationSpeed) * scaleRange;
        transform.localScale = originalScale * scale;
    }
}
