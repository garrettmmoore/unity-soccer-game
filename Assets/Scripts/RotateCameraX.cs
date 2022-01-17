using UnityEngine;

public class RotateCameraX : MonoBehaviour
{
    private const float Speed = 200;
    public GameObject player;

    // Update is called once per frame
    private void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, horizontalInput * Speed * Time.deltaTime);

        // Move focal point with player
        transform.position = player.transform.position;

    }
}
