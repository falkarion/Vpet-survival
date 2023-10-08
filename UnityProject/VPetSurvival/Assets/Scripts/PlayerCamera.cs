using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject Player;
    public float DistanceFromPlayer = 5f;
    public float Angle = 60f;

    // Update is called once per frame
    void Start()
    {
        float o = Mathf.Sin(Angle * Mathf.Deg2Rad) * DistanceFromPlayer;
        float a = Mathf.Cos(Angle * Mathf.Deg2Rad) * DistanceFromPlayer;

        this.transform.position = Player.transform.position + new Vector3(0f, o, -a);
        this.transform.rotation = Quaternion.AngleAxis(Angle, Vector3.right);
    }
}
