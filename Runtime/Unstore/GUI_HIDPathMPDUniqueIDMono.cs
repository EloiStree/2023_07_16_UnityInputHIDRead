using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GUI_HIDPathMPDUniqueIDMono : MonoBehaviour
{
    public string m_pathUniqueIDManufactor;
    public string m_pathUniqueIDDisplay;
    public string m_pathUniqueIDProduct;
    public StringEvent m_onPathUniqueIdPush;


    public void SetUniquePathIdManufactor(string uniqueID)
    {
        m_pathUniqueIDManufactor = uniqueID;
    }
    public void SetUniquePathIdDisplay(string uniqueID)
    {
        m_pathUniqueIDDisplay = uniqueID;
    }
    public void SetUniquePathIdProduct(string uniqueID)
    {
        m_pathUniqueIDProduct = uniqueID;
    }

    [ContextMenu("Push ID Manufactor")]
    public void PushIdManufactor()
    {
        m_onPathUniqueIdPush.Invoke(m_pathUniqueIDManufactor);
    }
    [ContextMenu("Push ID Display")]
    public void PushIdDisplay()
    {
        m_onPathUniqueIdPush.Invoke(m_pathUniqueIDDisplay);
    }
    [ContextMenu("Push ID Product")]
    public void PushIdProduct()
    {
        m_onPathUniqueIdPush.Invoke(m_pathUniqueIDProduct);
    }

    [System.Serializable]
    public class StringEvent : UnityEvent<string> { }
}