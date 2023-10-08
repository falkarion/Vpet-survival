using UnityEngine;

public class TiltToCamera : MonoBehaviour
{
    public Camera Camera;

    private void Awake()
    {
        Camera = Camera.main;
    }

    private void Start()
    {
        this.transform.rotation = Camera.transform.rotation;
    }
}
