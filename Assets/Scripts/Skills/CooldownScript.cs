using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownScript : MonoBehaviour
{
    public bool m_canActivate;
    
    // Start is called before the first frame update
    void Start()
    {
        m_canActivate = true;
    }

    public void StartCooldown(float _cooldownTime)
    {
        StartCoroutine(Cooldwon(_cooldownTime));
    }

    IEnumerator Cooldwon(float _cooldownTime)
    {
        m_canActivate = false;
        yield return new WaitForSeconds(_cooldownTime);
        m_canActivate = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
