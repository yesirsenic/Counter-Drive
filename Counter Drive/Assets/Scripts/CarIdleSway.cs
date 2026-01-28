using UnityEngine;

public class CarIdleSway : MonoBehaviour
{
    [Header("Sway Settings")]
    public float xAmount = 0.03f;   // 좌우 흔들림 크기
    public float yAmount = 0.01f;   // 위아래 흔들림 (아주 미세)
    public float speed = 1.5f;      // 흔들림 속도

    Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float xOffset = Mathf.Sin(Time.time * speed) * xAmount;
        float yOffset = Mathf.Sin(Time.time * speed * 1.3f) * yAmount;

        transform.localPosition = new Vector3(
            startPos.x + xOffset,
            startPos.y + yOffset,
            startPos.z
        );
    }
}
