using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class Enemy : MonoBehaviour
{
    public float Health = 1f;
    public float Damage = 1f;
    public float Speed = 1f;
    public int Presence = 1;
    public Action OnKilled;
    public float DEBUG_TakeDamage = 0f;

    private Rigidbody rb;
    private BoxCollider bc;
    private SpriteRenderer sr;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        bc.size = Vector3.zero;
        bc.center = Vector3.zero;
        bc.bounds.Encapsulate(sr.bounds);
    }


    private void Update()
    {
        TakeDamage(DEBUG_TakeDamage);
        DEBUG_TakeDamage = 0f;
    }


    public void TakeDamage(float _amount)
    {
        Health -= _amount;

        if(Health <= 0f)
        {
            die();
        }
    }

    private void die()
    {
        //Destroy
        Destroy(this.gameObject);
        OnKilled?.Invoke();
    }
}
