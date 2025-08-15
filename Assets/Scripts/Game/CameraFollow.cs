using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 1f, -10f);
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;

    void Update()
    {
        //camera follows player position
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
