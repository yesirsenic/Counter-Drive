using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("초당 이동 거리")]
    public float speed = 5f;

    [Header("Audio")]
    [SerializeField] private AudioSource engineSource;

    private void Awake()
    {
        // AudioSource가 없으면 자동으로 하나 붙여줌 (안전장치)
        if (engineSource == null)
        {
            engineSource = gameObject.AddComponent<AudioSource>();
            SetupAudioSource(engineSource);
        }
    }

    private void Start()
    {
        transform.Rotate(0, 180f, 0);

        if (engineSource != null && engineSource.clip != null)
        {
            engineSource.Play();
        }
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

    // 🔧 AudioSource 기본 세팅
    private void SetupAudioSource(AudioSource source)
    {
        source.playOnAwake = false;
        source.loop = true;

        // ⭐ 핵심
        source.spatialBlend = 1f;          // 3D 사운드
        source.rolloffMode = AudioRolloffMode.Logarithmic;
        source.minDistance = 5f;
        source.maxDistance = 40f;

        source.dopplerLevel = 0f;          // 도플러 싫으면 0
        source.volume = 1f;
    }
}