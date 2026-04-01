using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheForge.Services.Scenes
{
    /// <summary>
    /// A service that simplifies the usage of scene manipulation.
    /// It is based on the basic <c>UnityEngine.SceneManagement</c> class group.
    /// </summary>
    public sealed class SceneService : ISceneService
    {
        private AsyncOperation _currentOperation;

        async Task ISceneService.LoadSceneAsync(string sceneName, LoadSceneMode loadMode)
        {
            _currentOperation = SceneManager.LoadSceneAsync(sceneName, loadMode);
            if (_currentOperation == null)
                throw new Exception();

            while (!_currentOperation.isDone)
            {
                await Task.Yield();
            }
        }
        
        IEnumerator ISceneService.LoadSceneAsync(string sceneName, LoadSceneMode loadMode, Action onComplete)
        {
            _currentOperation = SceneManager.LoadSceneAsync(sceneName, loadMode);
            yield return new WaitUntil(() => _currentOperation!.isDone);
            onComplete?.Invoke();
        }
        
        async Task ISceneService.UnloadSceneAsync(string sceneName, UnloadSceneOptions unloadMode)
        {
            _currentOperation = SceneManager.UnloadSceneAsync(sceneName, unloadMode);
            if (_currentOperation == null)
                throw new Exception();

            while (!_currentOperation.isDone)
            {
                await Task.Yield();
            }
        }
        
        IEnumerator ISceneService.UnloadSceneAsync(string sceneName, Action onComplete, UnloadSceneOptions unloadMode)
        {
            _currentOperation = SceneManager.UnloadSceneAsync(sceneName, unloadMode);
            yield return new WaitUntil(() => _currentOperation!.isDone);
            onComplete?.Invoke();
        }
        
        float ISceneService.GetOperationProgress() => _currentOperation?.progress ?? 0;
    }
}