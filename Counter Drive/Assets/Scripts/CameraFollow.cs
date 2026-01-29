using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public Vector3 offset = new Vector3(0, 6f, -8f);
    public float followSpeed = 5f;
    public float horizontalFollowFactor = 0.6f;

    void LateUpdate()
    {
        if (!target) return;

        Vector3 desiredPos = target.position + offset;
        desiredPos.x = Mathf.Lerp(transform.position.x, desiredPos.x, horizontalFollowFactor);

        transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * followSpeed);
    }
}
