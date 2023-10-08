using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy), typeof(Rigidbody))]
public class EnemyMovement : MonoBehaviour
{
    private Enemy enemy;
    private Rigidbody rb;

    public GameObject Target;




    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        doMovement();
    }

    private void doMovement()
    {
        if(!Target)
        {
            return;
        }

        Vector3 targetDirection = (Target.transform.position - this.transform.position).normalized;
        rb.velocity = targetDirection * enemy.Speed;
    }
}
