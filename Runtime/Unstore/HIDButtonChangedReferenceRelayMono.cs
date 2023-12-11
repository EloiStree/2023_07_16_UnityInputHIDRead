using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HIDButtonChangedReferenceRelayMono : MonoBehaviour
{

    public HIDButtonChangedReferenceEvent m_onRelay;
    public HIDButtonChangedReference m_lastReceived;

    public void Push(HIDButtonChangedReference value)
    {
        m_lastReceived = value;
        m_onRelay.Invoke(value);
    }

}
