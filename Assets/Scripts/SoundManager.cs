using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
	public AudioMixer masterMixer;

	public float pitchSize = 40f;

	public void MusicMixerControl(float volume)
	{
		var pitch = volume * pitchSize - pitchSize;
		Debug.Log(pitch);
		masterMixer.SetFloat(Defines.musicVol, pitch);
	}

	public void EffectMixerControl(float volume)
	{
		var pitch = volume * pitchSize - pitchSize;
		Debug.Log(pitch);
		masterMixer.SetFloat(Defines.effectVol, pitch);
	}

	public void SetMasterVolume(bool b)
	{
		var pitch = (b ? 1f : 0f) * pitchSize - pitchSize;
		Debug.Log(pitch);
		masterMixer.SetFloat(Defines.masterVol, pitch);
	}
}
