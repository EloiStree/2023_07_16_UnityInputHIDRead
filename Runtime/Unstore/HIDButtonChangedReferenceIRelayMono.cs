
using System.Linq;
using UnityEngine;

public class HIDButtonChangedReferenceIRelayMono : MonoBehaviour
{

    public GameObject[] m_onRelayBroadcast;
    public I_IN_HIDButtonChangedReference[][] m_onRelayBroadcastInterface;
    public HIDButtonChangedReference m_lastReceived;

    public void Push(HIDButtonChangedReference value)
    {
        m_lastReceived = value;
        m_onRelayBroadcastInterface = m_onRelayBroadcast.Select(k => k.GetComponents<I_IN_HIDButtonChangedReference>()).ToArray();
        foreach (I_IN_HIDButtonChangedReference[] items in m_onRelayBroadcastInterface)
        {
            foreach (I_IN_HIDButtonChangedReference item in items)
            {
                if (item != null)
                {
                    item.PushIn(value);
                }
            }
        }
    }

}


public interface I_IN_HIDButtonChangedReference
{
    public void PushIn(HIDButtonChangedReference buttonChanged);
}