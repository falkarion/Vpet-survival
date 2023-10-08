using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Abilities/Attacks/BasicAttack")]
public class Attack : Ability
{
    public float Damage;
    public float Cooldown;
    public float range;
    private List<Transform> targets;

    public SphereCollider sphereCollider;
    public AttackHolder AttackHolder;

    public bool ready = true;
    private Transform origin;
    
    public void InitializeAttack(Transform _origin)
    {
        GameObject attackHolderObject = new GameObject($"Attack_{name}");
        attackHolderObject.transform.SetParent(_origin);
        origin = _origin;
        AttackHolder = attackHolderObject.AddComponent<AttackHolder>();
        AttackHolder.OnEnterRange += addTarget;
        AttackHolder.OnExitRange += removeTarget;
    }

    private void addTarget(Transform _other)
    {
        Debug.Log($"Adding Target {_other.name}");
        if (targets.Contains(_other))
        {
            return;
        }
        targets.Add(_other);
        
        Debug.Log($"attack ready: {ready}");
        if (ready)
        {
            attack();
        }
    }

    private void removeTarget(Transform _other)
    {
        if (!targets.Contains(_other))
        {
            return;
        }
        targets.Remove(_other);
    }

    private void attack()
    {
        int closestID = GetClosestTargetID(origin);
        Transform closestTarget = targets[closestID];
        
        Debug.Log($"Attacking: {closestTarget.name}");
        
        //spawn projectile at origin

        AttackHolder.StartCooldown(this);
    }
    
    private int GetClosestTargetID(Transform _origin)
    {
        float smallestDistance = float.MaxValue;
        int smallestDistanceID = -1;
        for (int i = 0; i < targets.Count; i++)
        {
            float checkDistance = Vector3.Distance(targets[i].position, _origin.position);
            if (smallestDistance > checkDistance)
            {
                smallestDistance = checkDistance;
                smallestDistanceID = i;
            }
        }

        return smallestDistanceID;
    }

    public IEnumerator StartCooldown()
    {
        Debug.Log("Cooldown Started");
        ready = false;
        yield return new WaitForSeconds(Cooldown);
        ready = true;
        Debug.Log("Attack Ready");

    }
}