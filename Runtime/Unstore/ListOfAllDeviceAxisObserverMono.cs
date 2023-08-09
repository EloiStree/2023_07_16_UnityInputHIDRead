using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOfAllDeviceAxisObserverMono : MonoBehaviour
{
    public HIDObserveUserIntentLinkToCurrentDeviceMono m_observer;

    public ListOfAllDeviceAsIdBoolFloatMono m_sourceObservered;
    public bool m_useUpdateToCheckChange;

    public void Update()
    {
        CheckForAxisChange();
        InvokeRepeating("PingIamHereIncode", 0, 5);
    }

    
    private void PingIamHereIncode()
    {
        throw new NotImplementedException();
    }
    private void CheckForAxisChange()
    {
    }
}
