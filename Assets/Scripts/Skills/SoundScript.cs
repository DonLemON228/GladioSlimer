using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    [Header("Sounds")]
    public EventReference m_soundEvent;
    EventInstance m_soundInstance;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    private void SoundPlay()
    {
        m_soundInstance = RuntimeManager.CreateInstance(m_soundEvent);
        m_soundInstance.start();
        m_soundInstance.release();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
