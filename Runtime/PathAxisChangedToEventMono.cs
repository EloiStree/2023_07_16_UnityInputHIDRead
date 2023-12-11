using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathAxisChangedToEventMono : MonoBehaviour
{
    public ListOfAllDeviceAsIdBoolFloatMono m_sourceObserver;
    public TargetDeviceFromPath[] m_observedDevices;

    [System.Serializable]
    public class TargetDeviceFromPath
    {
        public string m_targetPath = "/";
        public TargetAxisInDevice[] m_buttonsObserved;

        [System.Serializable]
        public class TargetAxisInDevice
        {
            public string m_axisName = "stick x";
            public float m_betweenLimitA;
            public float m_betweenLimitB;
            public Eloi.PrimitiveUnityEvent_Bool m_onInLimitChanged;
            public float m_current;
            public float m_previous;
            public bool m_inLimit;
            public bool m_inLimitPrevious;
            public string m_idPath;
            public bool m_isFound;
        }
    }

    public bool m_useUpdateToRefresh=true;
    public void Update()
    {
        if (!m_useUpdateToRefresh)
            return;
        if (m_sourceObserver==null)
            return;
        foreach (var device in m_observedDevices)
        {
            {
                HIDAxisChangedReference axisReference;
                foreach (var button in device.m_buttonsObserved)
                {
                    button.m_idPath = HIDButtonStatic.GetIDPathAndButtonName(device.m_targetPath, button.m_axisName);
                    m_sourceObserver.GetAxisFromPathNameId(device.m_targetPath, button.m_axisName, out button.m_isFound, out axisReference);
                    if (axisReference!=null )
                    {
                        if (button.m_betweenLimitA > button.m_betweenLimitB)
                        {
                            float t = button.m_betweenLimitB;
                            button.m_betweenLimitB = button.m_betweenLimitA;
                            button.m_betweenLimitA = t;
                        }
                        button.m_previous = button.m_current;
                        button.m_current = axisReference.m_axisThatChanged.m_value;
                        if (button.m_previous != button.m_current)
                        {
                            bool isInLimit = button.m_current >= button.m_betweenLimitA && button.m_current <= button.m_betweenLimitB;
                            button.m_inLimitPrevious = button.m_inLimit;
                            button.m_inLimit = isInLimit;
                            if(button.m_inLimitPrevious!= button.m_inLimit)
                                button.m_onInLimitChanged.Invoke(button.m_inLimit);
                            // Debug.Log("Test " + JsonUtility.ToJson( buttonChanged.m_deviceInfo,true));
                        }
                    }
                }
            }
        }
    }

}
