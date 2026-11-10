using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime = 0.15f;

    [Header("Level bounds (world x)")]
    [SerializeField] private float levelLeft = 0f;
    [SerializeField] private float levelRight = 120f;

    private Camera cam;
    private float velocityX;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void Start()
    {
        MoveTo(GoalX());   // open the level already framed, without a slide
    }

    void LateUpdate()
    {
        float x = Mathf.SmoothDamp(transform.position.x, GoalX(), ref velocityX, smoothTime);
        MoveTo(x);
    }

    float GoalX()
    {
        float halfWidth = cam.orthographicSize * cam.aspect;
        return Mathf.Clamp(target.position.x, levelLeft + halfWidth, levelRight - halfWidth);
    }

    void MoveTo(float x)
    {
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
}
