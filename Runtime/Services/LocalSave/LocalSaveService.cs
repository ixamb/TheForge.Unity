using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace TheForge.Services.LocalSave
{
    [Serializable]
    public sealed class SaveContainer
    {
        public Dictionary<string, object> Values = new(); 
    }

    /// <summary>
    /// A service designed to save and access data locally.
    /// The goal of this service is to offer a more flexible alternative to PlayerPrefs and to be more focused on backup data.
    /// The data will be saved in a JSON file. Upon initialization, the service creates a save.json script if one doesn't already exist.
    /// </summary>
    public sealed class LocalSaveService : Singleton<LocalSaveService, ILocalSaveService>, ILocalSaveService
    {
        private const string FileName = "save.json";
        private SaveContainer _container;

        protected override void Init()
        {
            Load();
        }

        /// <summary>
        /// Inserts an object into the input passed as a parameter.
        /// Note that if an input with the same key already existed in the JSON, its content will be overwritten by the object passed as a parameter.
        /// </summary>
        public void Set<T>(string key, T value, bool autoSave = true)
        {
            _container.Values[key] = value;
            if (autoSave)
                Save();
        }

        /// <summary>
        /// Retrieves the JSON input with the key passed as a parameter. The input type must match the function object type.
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        public T Get<T>(string key, T defaultValue = default)
        {
            if (!_container.Values.TryGetValue(key, out var raw))
                return defaultValue;

            try
            {
                if (raw is T casted)
                    return casted;
                
                var jsonText = JsonConvert.SerializeObject(raw);
                return JsonConvert.DeserializeObject<T>(jsonText);
            }
            catch (Exception e)
            {
                Debug.LogError(
                    $"[LocalSaveService] Failed to retrieve key '{key}' as type {typeof(T).Name}. Error: {e.Message}");
                return defaultValue;
            }
        }

        /// <summary>
        /// Forces saving the JSON, useful if you did not enable automatic saving when calling <c>Set()</c>.
        /// </summary>
        // ReSharper disable Unity.PerformanceAnalysis
        public void Save()
        {
            var path = GetPath();
            var json = JsonConvert.SerializeObject(_container, Formatting.Indented);
            File.WriteAllText(path, json);
            Debug.Log($"Save successful: {path}");
        }
        
        
        /// <summary>
        /// Deletes the whole saved data.
        /// <remarks><b>The operation cannot be undone!</b></remarks>
        /// </summary>
        public void Delete()
        {
            File.Delete(GetPath());
        }

        private void Load()
        {
            var path = GetPath();

            if (!File.Exists(path))
            {
                _container = new SaveContainer();
                return;
            }

            var json = File.ReadAllText(path);

            try
            {
                _container = JsonConvert.DeserializeObject<SaveContainer>(json);
                if (_container == null)
                {
                    Debug.LogWarning("[LocalSaveService] Save file corrupted, creating new container.");
                    _container = new SaveContainer();
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"[LocalSaveService] Failed to load and deserialize save file. Creating new container. Error: {e.Message}");
                _container = new SaveContainer();
            }
        }

        private static string GetPath()
        {
            return Path.Combine(Application.persistentDataPath, FileName);
        }
    }
}