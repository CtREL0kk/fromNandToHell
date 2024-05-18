using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeAmount = 0.7f;

    private Vector3 originalPosition;
    private float currentShakeAmount;
    private float shakeTimer = 0f;
    private float fadeDuration;

    void Update()
    {
        if (shakeTimer > 0)
        {
            Vector2 shakeOffset = UnityEngine.Random.insideUnitCircle * currentShakeAmount;
            transform.position = originalPosition + new Vector3(shakeOffset.x, shakeOffset.y, 0);

            currentShakeAmount = Mathf.Lerp(shakeAmount, 0f, 1 - shakeTimer / fadeDuration);
            shakeTimer -= Time.deltaTime;
        }
    }

    public void ShakeCamera(float fadeDurationInput)
    {
        fadeDuration = fadeDurationInput;
        shakeTimer = fadeDuration;
        currentShakeAmount = shakeAmount;
        originalPosition = transform.position;
    }
}
