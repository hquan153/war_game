using UnityEngine;

public class Audio : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip collisionAudioClip;

    private const int MAX_SOUNDS_PER_2_FRAME = 3;
    private int currentFrameSounds = 0;
    private int lastFrameCount = -1;

    private void Awake()
    {
        audioSource = transform.GetComponent<AudioSource>();
        collisionAudioClip = Resources.Load<AudioClip>("Audio/Collision");
    }

    public void PlayAudio()
    {
        if (Time.frameCount > lastFrameCount + 1)
        {
            lastFrameCount = Time.frameCount;
            currentFrameSounds = 0;
        }

        if (currentFrameSounds >= MAX_SOUNDS_PER_2_FRAME) return;

        audioSource.pitch = Random.Range(.8f, 1.05f);
        audioSource.PlayOneShot(collisionAudioClip, Random.Range(.15f, .4f));

        currentFrameSounds++;
    }
}