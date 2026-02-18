using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace TheForge.Services.Scenes
{
    /// <inheritdoc cref="SceneService"/>
    public interface ISceneService : ISingleton
    {
        /// <inheritdoc cref="SceneService.LoadSceneAsync(string, LoadSceneMode)"/>
        Task LoadSceneAsync(string sceneName, LoadSceneMode loadMode = LoadSceneMode.Additive);
        
        /// <inheritdoc cref="SceneService.LoadSceneAsync(string, LoadSceneMode, Action)"/>
        IEnumerator LoadSceneAsync(string sceneName, LoadSceneMode loadMode, Action onComplete);
        
        /// <inheritdoc cref="SceneService.UnloadSceneAsync(string, UnloadSceneOptions)"/>
        Task UnloadSceneAsync(string sceneName, UnloadSceneOptions unloadMode = UnloadSceneOptions.None);
        
        /// <inheritdoc cref="SceneService.UnloadSceneAsync(string, Action, UnloadSceneOptions"/>
        IEnumerator UnloadSceneAsync(string sceneName, Action onComplete, UnloadSceneOptions unloadMode = UnloadSceneOptions.None);
        
        /// <inheritdoc cref="SceneService.GetOperationProgress"/>
        float GetOperationProgress();
    }
}