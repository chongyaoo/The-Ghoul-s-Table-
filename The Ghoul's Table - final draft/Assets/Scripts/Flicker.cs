using Seagull.Bar_02.SceneProps;
using UnityEngine;

[RequireComponent(typeof(GlowLight), typeof(AudioSource))]
public class FlickerEffect : MonoBehaviour
{
    public float maxOffDuration = 0.2f;
    public float maxOnDuration = 1f;

    private GlowLight glow;
    private AudioSource audioSource;
    private float timer = 0f;
    private float interval;
    private bool isOn;

    private float nextTimeSoundAllowed = 0f;

    void Awake()
    {
        glow = GetComponent<GlowLight>();
        audioSource = GetComponent<AudioSource>();
        isOn = true;
        interval = Random.Range(0f, maxOnDuration);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            // Toggle glow light
            if (isOn) glow.turnOff();
            else glow.turnOn();

            // Play sound only if current time is beyond the clip's duration
            if (audioSource.clip != null && Time.time >= nextTimeSoundAllowed)
            {
                audioSource.PlayOneShot(audioSource.clip, audioSource.volume);
                nextTimeSoundAllowed = Time.time + audioSource.clip.length;
            }

            isOn = !isOn;
            interval = isOn
                ? Random.Range(0f, maxOnDuration)
                : Random.Range(0f, maxOffDuration);
            timer = 0f;
        }
    }
}
