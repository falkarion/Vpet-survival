using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class Enemy : MonoBehaviour
{
    public float Health = 1f;
    public float Damage = 1f;
    public float Speed = 1f;
    public float Presence = 1f;

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
}
