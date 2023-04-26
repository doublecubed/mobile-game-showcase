// ------------------------
// Onur Ereren - April 2023
// ------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IslandGame.Feedback
{
	public class AudioPlayer : MonoBehaviour
	{
		#region REFERENCES

		public AudioSource MusicPlayer;
		public AudioSource EffectPlayer;
		
		[Space(5)]
		
		public AudioClip islandTap;
		public AudioClip characterWalk;
		public AudioClip buttonClick;
		
		#endregion
	
		#region VARIABLES

		private Dictionary<string, AudioClip> _audioClips;

		private int _musicOn;
		
		#endregion

		#region MONOBEHAVIOUR

		private void Start()
		{
			SetUpDictionary();
			SetMusicOnOff();
		}

		#endregion

		#region METHODS

		#region AUDIO

		private void SetUpDictionary()
		{
			_audioClips = new Dictionary<string, AudioClip>();
			_audioClips.Add("islandTap", islandTap);
			_audioClips.Add("characterWalk", characterWalk);
			_audioClips.Add("buttonClick", buttonClick);
		}

		public void PlayClip(string clipName)
		{
			if (_audioClips.ContainsKey(clipName))
			{
				EffectPlayer.PlayOneShot(_audioClips[clipName]);
			}
		}

		public void PlayClip(string clipName, float duration)
		{
			if (_audioClips.ContainsKey(clipName))
			{
				StartCoroutine(PlayWithDuration(_audioClips[clipName], duration));				
			}

		}
		
		#endregion
		
		#region MUSIC
		
		private void SetMusicOnOff()
		{
			_musicOn = PlayerPrefs.GetInt("musicOn", 1);

			if (_musicOn != 1) MusicPlayer.enabled = false;
		}

		public void ToggleMusic()
		{
			if (_musicOn == 1)
			{
				_musicOn = 0;
				MusicPlayer.enabled = false;
			}
			else
			{
				_musicOn = 1;
				MusicPlayer.enabled = true;
			}
		}
		
		#endregion
		
		#endregion

		#region COROUTINES

		private IEnumerator PlayWithDuration(AudioClip clip, float duration)
		{
			EffectPlayer.PlayOneShot(clip);
			yield return new WaitForSeconds(duration);
			EffectPlayer.Stop();
		}
		
		
		#endregion
		
	}	
}


