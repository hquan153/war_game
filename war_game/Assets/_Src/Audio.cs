using UnityEngine;

public class Audio : MonoBehaviour
{
    private AudioSource audioSource;

    private const int MAX_SOUNDS_PER_2_FRAME = 2;
    private const int EXTEND_FRAMES = 1;
    private int currentFrameAudio = 0;
    private int lastFrameCount = -1;

    private float m_volumePercent = .5f;
    public float VolumePercent
    {
        get => m_volumePercent;
        set { m_volumePercent = value / 100; }
    }

    private void Awake()
    {
        audioSource = transform.GetComponent<AudioSource>();
    }

    public void PlayAudio()
    {
        if (Time.frameCount > lastFrameCount + EXTEND_FRAMES)
        {
            lastFrameCount = Time.frameCount;
            currentFrameAudio = 0;
        }

        if (currentFrameAudio >= MAX_SOUNDS_PER_2_FRAME) return;

        audioSource.pitch = Random.Range(.88f, 1.02f);
        audioSource.volume = Random.Range(.36f * VolumePercent, .64f * VolumePercent);
        audioSource.time = .08f;
        audioSource.Play();

        currentFrameAudio++;
    }
}