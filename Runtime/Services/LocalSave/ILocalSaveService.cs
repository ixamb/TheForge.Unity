namespace TheForge.Services.LocalSave
{
    /// <inheritdoc cref="LocalSaveService"/>
    public interface ILocalSaveService : ISingleton
    {
        /// <inheritdoc cref="LocalSaveService.Set"/>
        void Set<T>(string key, T value, bool autoSave = true);
        
        /// <inheritdoc cref="LocalSaveService.Get"/>
        T Get<T>(string key, T defaultValue = default);
        
        /// <inheritdoc cref="LocalSaveService.Save"/>
        void Save();
        
        /// <inheritdoc cref="LocalSaveService.Delete"/>
        void Delete();
    }
}