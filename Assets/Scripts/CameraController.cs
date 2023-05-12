using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook CMFreelook;
    
    public void SetInputActive(bool value)
    {
        if(value)
        {
            CMFreelook.m_XAxis.m_InputAxisName = "Mouse X";
            CMFreelook.m_YAxis.m_InputAxisName = "Mouse Y";
        }
        else
        {
            CMFreelook.m_XAxis.m_InputAxisName = "";
            CMFreelook.m_YAxis.m_InputAxisName = "";

            CMFreelook.m_XAxis.m_InputAxisValue = 0;
            CMFreelook.m_YAxis.m_InputAxisValue = 0;
        }
    }
}
