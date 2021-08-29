using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("目標")]
    public Transform target;
    [Header("追蹤速度"), Range(1, 100)]
    public float speed = 1;
    [Header("限制")]
    public Vector2 v2LimitVertical;
    public Vector2 v2LimitHorizontal;

    private void FixedUpdate()
    {
        Track();
    }

    private void Track()
    {
        if (!target) return;

        Vector3 posCamera = transform.position;
        Vector3 posTarget = target.position;

        posCamera = Vector3.Lerp(posCamera, posTarget, speed * Time.deltaTime); ;
        posCamera.z = -10;

        posCamera.x = Mathf.Clamp(posCamera.x, v2LimitHorizontal.x, v2LimitHorizontal.y);
        posCamera.y = Mathf.Clamp(posCamera.y, v2LimitVertical.x, v2LimitVertical.y);

        transform.position = posCamera;
    }
}
