using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SFXType
{
    ButtonClick,
    GameOverBoom,
    GameOver,
    Star1,
    Star2,
    Star3,
    GameClearText,
    PlayerDriveAway
}

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [Header("Audio Source")]
    [SerializeField] private AudioSource sfxSource;

    [Header("SFX Clips")]
    [SerializeField] private List<SFXClipData> sfxClips = new();

    private Dictionary<SFXType, AudioClip> sfxDict;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitDictionary();
    }

    private void InitDictionary()
    {
        sfxDict = new Dictionary<SFXType, AudioClip>();

        foreach (var data in sfxClips)
        {
            if (!sfxDict.ContainsKey(data.type))
                sfxDict.Add(data.type, data.clip);
        }
    }

    // 🔊 핵심 함수
    public void PlayShot(SFXType type, float volume = 1f)
    {
        if (!sfxDict.TryGetValue(type, out AudioClip clip))
        {
            Debug.LogWarning($"SFX not found: {type}");
            return;
        }

        sfxSource.PlayOneShot(clip, volume);
    }

    // ⭐ 엔딩 전용
    public void PlayDriveAway(float fadeDuration = 1.2f)
    {
        StartCoroutine(DriveAwayCoroutine(fadeDuration));
    }

    private IEnumerator DriveAwayCoroutine(float duration)
    {
        if (!sfxDict.TryGetValue(SFXType.PlayerDriveAway, out AudioClip clip))
            yield break;

        sfxSource.clip = clip;
        sfxSource.loop = false;
        sfxSource.volume = 1f;

        sfxSource.Play();

        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            sfxSource.volume = Mathf.Lerp(1f, 0f, t / duration);
            yield return null;
        }

        sfxSource.Stop();
        sfxSource.volume = 1f; // 복구
    }
}