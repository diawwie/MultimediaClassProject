using UnityEngine;

[DefaultExecutionOrder(100)]   // after CameraFollow, so it reads where the camera ends up this frame
public class Parallax : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [Range(0f, 1f)]
    [SerializeField] private float factor = 0.5f;

    private float startX;
    private float camStartX;

    void Start()
    {
        startX = transform.position.x;
        camStartX = cam.position.x;
    }

    void LateUpdate()
    {
        float x = startX + (cam.position.x - camStartX) * factor;
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
}