using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PathButtonsChangedToEventMono : MonoBehaviour, I_IN_HIDButtonChangedReference
{

    public TargetDeviceFromPath [] m_observedDevices;

    [System.Serializable]
    public class TargetDeviceFromPath
    {
        public string m_targetPath="/";
        public TargetButtonInDevice[] m_buttonsObserved;

        [System.Serializable]
        public class TargetButtonInDevice
        {
            public string m_buttonName="button1";
            public Eloi.PrimitiveUnityEvent_Bool m_onChanged;
        }
    }

    public HIDButtonChangedReference m_lastReceived;
    public void PushIn(HIDButtonChangedReference buttonChanged)
    {
        m_lastReceived = buttonChanged;
        foreach (var device in m_observedDevices)
        {
            if (device.m_targetPath.Trim() == buttonChanged.m_deviceInfo.m_devicePath.Trim()) {
                foreach (var button in device.m_buttonsObserved)
                {
                    if (button.m_buttonName.Trim().ToLower() == buttonChanged.m_booleanThatChanged.m_givenIdName.Trim().ToLower()) {
                        button.m_onChanged.Invoke(buttonChanged.m_booleanThatChanged.m_value);
                       // Debug.Log("Test " + JsonUtility.ToJson( buttonChanged.m_deviceInfo,true));
                    }
                }
            }
        }
    }

}
