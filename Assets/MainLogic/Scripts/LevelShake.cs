using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class LevelShake : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform levelTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;
    public float shakeCooldown = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.5f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    void Awake()
    {
        if (levelTransform == null)
        {
            levelTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = levelTransform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            if (shakeCooldown <= 0)
            {
                levelTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
                shakeCooldown = 0.2f;
            }
            else
            {
                shakeCooldown -= Time.deltaTime;
            }

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else if (shakeDuration != 0f)
        {
            shakeDuration = 0f;
            levelTransform.localPosition = originalPos;
        }
    }
}