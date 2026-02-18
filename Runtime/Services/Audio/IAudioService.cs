using TheForge.Services.Audio.Dto;

namespace TheForge.Services.Audio
{
    /// <inheritdoc cref="AudioService"/>
    public interface IAudioService : ISingleton
    {
        /// <inheritdoc cref="AudioService.LoadAudio"/>
        void LoadAudio(AudioLoadDto audioLoadDto);
        
        /// <inheritdoc cref="AudioService.PauseAudio"/>
        void PauseAudio(string code);
        
        /// <inheritdoc cref="AudioService.ResumeAudio"/>
        void ResumeAudio(string code);
        
        /// <inheritdoc cref="AudioService.StopAudio"/>
        void StopAudio(string code);
        
        /// <inheritdoc cref="AudioService.StopAllAudios"/>
        void StopAllAudios();
        
        /// <inheritdoc cref="AudioService.RestartAudio"/>
        void RestartAudio(string code);
        
        /// <inheritdoc cref="AudioService.ChangeVolume(AudioType, float)"/>
        void ChangeVolume(AudioType audioType, float volume);
        
        /// <inheritdoc cref="AudioService.ChangeVolume(string, float)"/>
        void ChangeVolume(string code, float volume);
    }
}