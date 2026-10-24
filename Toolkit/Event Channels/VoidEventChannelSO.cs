using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Void Event Channel", menuName = "Scriptable Objects/Event Channels/Void")]
public class VoidEventChannelSO : ScriptableObject
{
    public event UnityAction listeners;

    public void Raise()
    {
        listeners?.Invoke();
    }
}
