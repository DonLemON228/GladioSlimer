using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallCameraShake : MonoBehaviour
{
    public float shakeIntensity;
    public float shakeFrequency;
    public float shakeTime;

    public void CallShake()
    {
        CameraShake.Instance.Shake(shakeIntensity, shakeFrequency, shakeTime);
    }
}
