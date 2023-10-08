using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject Player;
    public Vector3 RelativePosition;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Player.transform.position + RelativePosition;
    }
}
