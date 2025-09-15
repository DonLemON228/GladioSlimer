using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }
    public List<CinemachineVirtualCamera> cinemachineVirtualCamera;
    private float shakeTimer;

    private void OnEnable()
    {
        Instance = this;
    }
    
    public void Shake(float intensity,float frequency, float time)
    {
        foreach (var obj in cinemachineVirtualCamera)
        {
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = obj.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
            cinemachineBasicMultiChannelPerlin.m_FrequencyGain = frequency;
            shakeTimer = time;
        }
    }

    private void FixedUpdate()
    {
        if (this.enabled)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                foreach (var obj in cinemachineVirtualCamera)
                {
                    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = obj.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
                    cinemachineBasicMultiChannelPerlin.m_FrequencyGain = 0f;
                }
            }
        }
        
    }
}
