using System;
using Unity.VisualScripting;
using UnityEngine;

public class AttackHolder : MonoBehaviour
{
    public Action<Transform> OnEnterRange;
    public Action<Transform> OnExitRange;

    public void InitializeAttackHolder(Attack _attack)
    {
        SphereCollider sphereCollider = this.AddComponent<SphereCollider>();
        sphereCollider.radius = _attack.range;
        sphereCollider.isTrigger = true;
    }
    
    public void StartCooldown(Attack _attack)
    {
        Debug.Log("starting cooldown period");
        StartCoroutine(_attack.StartCooldown());
    }

    private void OnTriggerEnter(Collider _other)
    {
        Debug.Log($"target entered {_other.name}");
        OnEnterRange?.Invoke(_other.transform);
    }

    private void OnTriggerExit(Collider _other)
    {
        Debug.Log($"target exited {_other.name}");
        OnExitRange?.Invoke(_other.transform);
    }
}
