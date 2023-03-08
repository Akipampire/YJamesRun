using System.Collections.Generic;
using UnityEngine;
using utils;

public class SFXPlayer : MonoBehaviour
{
	public enum SFX_TYPE {
		Throw = 1,
		Hit = 2,
		OneUp = 3,
		Controller = 4,
		Shield = 5,
		SpeedUp = 6
	}
	[SerializeField] AudioSource audioOuput;
	[SerializeField] List<AudioClip> ThrowAudios;
	[SerializeField] List<AudioClip> HitAudios;
	[SerializeField] List<AudioClip> OneUpAudios;
	[SerializeField] List<AudioClip> ControllerAudios;
	[SerializeField] List<AudioClip> ShieldAudios;
	[SerializeField] List<AudioClip> SpeedUpAudios;

	public void AskSFX(SFX_TYPE type) {
		if(type == SFX_TYPE.Throw) {
			PlayAudio(ThrowAudios.RandomElements());
		}else if(type == SFX_TYPE.Hit) {
			PlayAudio(HitAudios.RandomElements());
		}else if (type == SFX_TYPE.OneUp) {
			PlayAudio(OneUpAudios.RandomElements());
		}else if (type == SFX_TYPE.Controller) {
			PlayAudio(ControllerAudios.RandomElements());
		}else if (type == SFX_TYPE.Shield) {
			PlayAudio(ShieldAudios.RandomElements());
		}else if (type == SFX_TYPE.SpeedUp) {
			PlayAudio(SpeedUpAudios.RandomElements());
		}
	}

	public void PlayAudio(AudioClip audioClip) {
		audioOuput.PlayOneShot(audioClip);
	}
}
