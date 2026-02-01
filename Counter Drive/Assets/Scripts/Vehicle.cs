using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("초당 이동 거리")]
    public float speed = 5f;


    private void Start()
    {
        transform.Rotate(0, 180f, 0);

        EngineSoundManager.Instance.SetTarget(transform);

    }

    void Update()
    {
        // Z축 +방향으로 고정 속도 이동
        transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DestroyCollider"))
        {
            GameManager.Instance.aliveCount--;
            GameManager.Instance.CheckClear();

            EngineSoundManager.Instance.ClearTarget(transform);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!GameManager.Instance.isControl)
            return;

        if (collision.gameObject.CompareTag("User"))
        {
            Debug.Log("부딪힘");

            GameManager.Instance.GameOver();
        }
    }
}