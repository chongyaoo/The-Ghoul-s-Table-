using UnityEngine;

public class GunEffects : MonoBehaviour
{
    public ParticleSystem muzzleFlash;
    public ParticleSystem muzzleSmoke;
    public AudioSource bangSound;

    public void FireEffects()
    {
        if (muzzleFlash) muzzleFlash.Play();
        if (muzzleSmoke) muzzleSmoke.Play();
        if (bangSound && bangSound.clip != null)
            bangSound.PlayOneShot(bangSound.clip, bangSound.volume);
    }
}
