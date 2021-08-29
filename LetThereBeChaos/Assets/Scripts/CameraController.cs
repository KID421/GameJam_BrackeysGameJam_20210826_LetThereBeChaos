using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("�ؼ�")]
    public Transform target;
    [Header("�l�ܳt��"), Range(1, 100)]
    public float speed = 1;
    [Header("����")]
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
