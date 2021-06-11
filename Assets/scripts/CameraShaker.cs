using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public static CameraShaker Instance;
    private Vector3 originalPosition;
    private Coroutine currentShake;

    public CameraShaker()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void Start()
    {
        originalPosition = transform.localPosition;
    }

    public void Shake(float duration, float magnitude)
    {
        if (currentShake != null)
        {
            StopCoroutine(currentShake);
        }
        currentShake = StartCoroutine(shakeScreen(duration, magnitude));
    }

    private IEnumerator shakeScreen(float duration, float magnitude)
    {
        var shakeUntil = Time.time + duration;

        while (Time.time < shakeUntil)
        {
            var newX = Random.Range(-1f, 1f) * magnitude;
            var newY = Random.Range(-1f, 1f) * magnitude;
            transform.position = new Vector3(newX, newY, originalPosition.z);
            yield return new WaitForSeconds(0.02f);
        }
        transform.position = originalPosition;
    }
}
