using UnityEngine;

public class RoadScroller : MonoBehaviour
{
    public float speed = 2f;
    public float resetX;
    public float planeWidth;

    float fixedY;
    float fixedZ;

    void Start()
    {
        // Y, Z 고정
        fixedY = transform.position.y;
        fixedZ = transform.position.z;

        // Plane 실제 가로 길이 계산 (스케일 반영)
        MeshRenderer mr = GetComponent<MeshRenderer>();
        planeWidth = mr.bounds.size.x;

        // 왼쪽으로 이만큼 가면 리셋
        resetX = -planeWidth;
    }

    void Update()
    {
        float newX = transform.position.x - speed * Time.deltaTime;

        // 화면 밖으로 나가면 반대편으로 이동
        if (newX <= resetX)
        {
            newX += planeWidth * 2f;
        }

        // X만 이동, Y/Z 고정
        transform.position = new Vector3(newX, fixedY, fixedZ);
    }
}
