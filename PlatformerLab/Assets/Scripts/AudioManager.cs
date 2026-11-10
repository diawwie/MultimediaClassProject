using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Pitch variation")]
    [Range(0f, 0.2f)]
    [SerializeField] private float pitchVariation = 0.08f;

    private AudioSource sfxSource;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        sfxSource = GetComponent<AudioSource>();
    }

    public void PlaySfx(AudioClip clip)
    {
        if (clip == null) return;

        sfxSource.pitch = 1f + Random.Range(-pitchVariation, pitchVariation);
        sfxSource.PlayOneShot(clip);
    }
}
