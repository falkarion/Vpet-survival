using System;
using UnityEngine;

public class AttackHolder : MonoBehaviour
{
    public Action<Transform> OnEnterRange;
    public Action<Transform> OnExitRange;
    
    private void OnTriggerEnter(Collider other)
    {
        OnEnterRange?.Invoke(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        OnExitRange?.Invoke(other.transform);
    }
}
