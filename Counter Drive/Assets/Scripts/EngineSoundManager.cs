using UnityEngine;

public class EngineSoundManager : MonoBehaviour
{
    public static EngineSoundManager Instance;

    public AudioSource engineSource;

    public Transform listener; // Camera

    public float minDist = 3f;
    public float maxDist = 25f;

    private Transform targetCar;

    void Awake()
    {
        Instance = this;

        engineSource.loop = true;
        engineSource.spatialBlend = 0f; // 🔥 2D 고정
        engineSource.volume = 0f;
        engineSource.Play();
    }

    void Update()
    {
        if (targetCar == null)
        {
            engineSource.volume = Mathf.Lerp(engineSource.volume, 0f, Time.deltaTime * 5f);
            return;
        }

        float dist = Vector3.Distance(targetCar.position, listener.position);
        float targetVol = Mathf.InverseLerp(maxDist, minDist, dist);
        targetVol = Mathf.Clamp01(targetVol);

        engineSource.volume = Mathf.Lerp(engineSource.volume, targetVol, Time.deltaTime * 8f);
    }

    public void SetTarget(Transform car)
    {
        targetCar = car;
    }

    public void ClearTarget(Transform car)
    {
        if (targetCar == car)
            targetCar = null;
    }
}
