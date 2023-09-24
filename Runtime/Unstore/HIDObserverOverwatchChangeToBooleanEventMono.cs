using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HIDObserverOverwatchChangeToBooleanEventMono : MonoBehaviour
{

    public HIDObserveUserIntentLinkToCurrentDeviceMono m_overwatchValue;
    public ListOfAllDeviceAsIdBoolFloatMono m_currentDeviceState;


    public void CheckForChangeOnAxis() {

    }

    public void NotificationOfButtonChanged(HIDButtonChangedReference button)
    {

        string path = button.m_uniqueId;
        bool pushed = false;
        pushed = TryToPushFromPath(button, path);
        if (pushed) return;

        HIDButtonStatic.GetProductPathOf(button, out path);
        pushed = TryToPushFromPath(button, path);
        if (pushed) return;

        HIDButtonStatic.GetDisplayPathOf(button, out path);
        pushed = TryToPushFromPath(button, path);
        if (pushed) return;

        HIDButtonStatic.GetManufactorPathOf(button, out path);
        pushed = TryToPushFromPath(button, path);
        if (pushed) return;
    }

    private bool TryToPushFromPath(HIDButtonChangedReference button, string path)
    {
        if (m_overwatchValue.m_buttonDico.ContainsKey(path))
        {
            List<HIDRef_DeviceButtonUniqueID> obsever = m_overwatchValue.m_buttonDico[path];
            Notify(button, obsever);    
            return true;
        }
        return false;
    }

    private void Notify(HIDButtonChangedReference button, List<HIDRef_DeviceButtonUniqueID> obsever)
    {
        bool value = button.m_booleanThatChanged.m_value;
        foreach (var item in obsever)
        {
            Notify(item.m_booleanName, item.m_buttonObserver.m_trueIfPressed ? value : !value);
        }
    }


    public bool m_useDebugLog;
    private void Notify(string booleanName, bool newValue)
    {
        if (m_useDebugLog)
            Debug.Log("BC:" + booleanName + " " + newValue);
        m_history.PushIn(new NamedBoolean(booleanName, newValue));
        m_onBooleanChangedDetected.Invoke(booleanName, newValue);
    }
    public BooleanChangedEvent m_onBooleanChangedDetected;
    [System.Serializable]

    public class BooleanChangedEvent : UnityEvent<string, bool> { }

    [System.Serializable]

    public class NamedBoolean { public string m_boolean; public bool m_newValue;

        public NamedBoolean(string boolean, bool newValue)
        {
            m_boolean = boolean;
            m_newValue = newValue;
        }
    }
    [System.Serializable]
    public class History: Eloi.GenericClampHistory<NamedBoolean>{}
    public History m_history;
}
