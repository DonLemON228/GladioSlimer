using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera[] m_cameras;

    public CinemachineVirtualCamera m_player1Camera;
    public CinemachineVirtualCamera m_player2Camera;
    public CinemachineVirtualCamera m_middleCamera;

    public CinemachineVirtualCamera m_startCamera;
    private CinemachineVirtualCamera m_currentCamera;

    // Start is called before the first frame update
    void Start()
    {
        m_currentCamera = m_startCamera;

        for(int i = 0;  i < m_cameras.Length; i++)
        {
            if (m_cameras[i] == m_currentCamera)
            {
                m_cameras[i].Priority = 20;
            }
            else
            {
                m_cameras[i].Priority = 10;
            }
        }
    }

    public void SwitchCamera(CinemachineVirtualCamera _newcamera)
    {
        m_currentCamera = _newcamera;
        m_currentCamera.Priority = 20;
        for (int i = 0; i < m_cameras.Length; i++)
        {
            if (m_cameras[i] != m_currentCamera)
            {
                m_cameras[i].Priority = 10;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
