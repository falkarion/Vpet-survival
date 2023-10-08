using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Attacks/BasicAttack")]
public class Attack : Ability
{
    public float Damage;
    public float Cooldown;
    public float range;
    private List<Transform> targets;

    public SphereCollider sphereCollider;
    public AttackHolder AttackHolder;
    
    public void InitializeAttack(Transform _origin)
    {
        GameObject attackHolderObject = new GameObject($"Attack_{name}");
        attackHolderObject.transform.SetParent(_origin);
        AttackHolder = attackHolderObject.AddComponent<AttackHolder>();
        sphereCollider = AttackHolder.AddComponent<SphereCollider>();
        sphereCollider.radius = range;
        sphereCollider.isTrigger = true;
        AttackHolder.OnEnterRange += addTarget;
        AttackHolder.OnExitRange += removeTarget;
    }

    private void addTarget(Transform _other)
    {
        if (targets.Contains(_other))
        {
            return;
        }
        targets.Add(_other);
    }
    
    private void removeTarget(Transform _other)
    {
        if (!targets.Contains(_other))
        {
            return;
        }
        targets.Remove(_other);
    }
}