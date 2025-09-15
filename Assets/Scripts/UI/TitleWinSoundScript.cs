using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class TitleWinSoundScript : MonoBehaviour
{
    [Header("Sounds")]
    public EventReference m_appearSoundEvent;
    EventInstance m_appearSoundInstance;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void AppearSound()
    {
        m_appearSoundInstance = RuntimeManager.CreateInstance(m_appearSoundEvent);
        m_appearSoundInstance.start();
        m_appearSoundInstance.release();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
