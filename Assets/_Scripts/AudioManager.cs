using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance;
    public AudioSource ambientAudioSource;
    public AudioSource cartPullAudioSource;
    public AudioSource rocketAudioSource;
    public AudioSource footStepAudioSource;
    public AudioSource cartCarImpactAudioSource;
    public AudioSource cartCollectAudioSource;
    public AudioSource explosionAudioSource;
    public AudioClip[] playerSteps;
    public AudioClip[] cartLinkClips;
	// Use this for initialization
	void Start () {
        if (instance == null)
            instance = this;
	}
    public void PlayStopCartPull(bool isPlaying)
    {
        if (isPlaying)
            cartPullAudioSource.Play();
        else
            cartPullAudioSource.Stop();
    }
    public void PlayStopRocketBoost(bool isPlaying)
    {
        if (isPlaying)
            rocketAudioSource.Play();
        else
            rocketAudioSource.Stop();
    }
    public void PlayRandomFootStep()
    {
        int index = Random.Range(0, playerSteps.Length);
        footStepAudioSource.PlayOneShot(playerSteps[index]);
    }
    public void PlayOneShotCarImpact()
    {
        cartCarImpactAudioSource.Play();
    }
    public void PlayOneShotCartCollect()
    {
        int index = Random.Range(0, cartLinkClips.Length);
        cartCollectAudioSource.PlayOneShot(cartLinkClips[index]);
    }
    public void PlayOneShotExplosion()
    {
        explosionAudioSource.Play();
    }
}
