using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using TheForge.Services.Audio.Dto;
using UnityEngine;

namespace TheForge.Services.Audio
{
    /// <summary>
    /// A very simple service for managing multi-canal audio.The AudioSources will be placed as children of the service object.
    /// Useful if manual configuration of audio sources in the space is not required.<br />
    /// Audio sources can be of type Music and SFX, and can be configured independently according to this type.
    /// <remarks>A configuration file must be created to list all audio files playable by the service, along with a key/AudioClip pair.</remarks>
    /// </summary>
    public sealed class AudioService : Singleton<AudioService, IAudioService>, IAudioService
    {
        [SerializeField] private AudioServiceProperties properties;
        
        private readonly List<AudioSourceEntry> _audioSourceEntries = new();

        protected override void Init()
        {
            var audioObject = Instantiate(new GameObject("AudioObject"), transform);
            for (uint i = 0; i < properties.CanalsCount; i++)
            {
                var source = audioObject.AddComponent<AudioSource>();
                _audioSourceEntries.Add(new AudioSourceEntry(source));
            }
        }

        /// <summary>
        /// A function loading an audio onto a free AudioSource, with a set of settings.<br />
        /// The AudioClip will automatically be positioned on a free AudioSource.
        /// A source is considered as being free if it has no audio OR if the audio on it isn't playing.
        /// </summary>
        public void LoadAudio(AudioLoadDto audioLoadDto)
        {
            var clipEntry = properties.GetAudioClipEntry(audioLoadDto.Code);
            if (clipEntry?.AudioClip is null)
                return;
            
            foreach (var entry in _audioSourceEntries.Where(entry => !entry.IsPlaying()))
            {
                entry.PlayAudio(new AudioPlayDto(clipEntry.AudioType, clipEntry.AudioClip, audioLoadDto));
                break;
            };
        }

        /// <summary>
        /// Pause the audio linked to a specified key.
        /// </summary>
        public void PauseAudio(string code) => GetPlayingAudioSourceEntry(code)?.PauseAudio();
        
        /// <summary>
        /// Resume the audio linked to a specified key.
        /// </summary>
        public void ResumeAudio(string code) => GetPlayingAudioSourceEntry(code)?.ResumeAudio();
        
        /// <summary>
        /// Stop the audio linked to a specified key.
        /// </summary>
        public void StopAudio(string code) => GetPlayingAudioSourceEntry(code)?.StopAudio();
        
        /// <summary>
        /// Stop all audios, unrelated to their key or type. 
        /// </summary>
        public void StopAllAudios() => _audioSourceEntries.ForEach(entry => entry.StopAudio());
        
        /// <summary>
        /// Restart the audio linked to a specified key.
        /// </summary>
        public void RestartAudio(string code) => GetPlayingAudioSourceEntry(code)?.RestartAudio();

        /// <summary>
        /// Change the volume of all the audio sources with a specified type.
        /// </summary>
        public void ChangeVolume(AudioType audioType, float volume)
        {
            volume = _audioSourceEntries.Where(entry => entry.AudioType == audioType)
                .Aggregate(volume, (current, entry) => Mathf.Clamp01(current));
        }

        /// <summary>
        /// Change the audio volume linked to a specified key.
        /// </summary>
        public void ChangeVolume(string code, float volume) => GetPlayingAudioSourceEntry(code)?.ChangeVolume(volume);
                
        [CanBeNull] private AudioSourceEntry GetPlayingAudioSourceEntry(string code)
            => _audioSourceEntries.FirstOrDefault(entry => entry.Code.Equals(code));
    }
}