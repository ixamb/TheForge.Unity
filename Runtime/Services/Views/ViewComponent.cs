using UnityEngine;

namespace TheForge.Services.Views
{
    public abstract class ViewComponent<T> : MonoBehaviour where T : ComponentDto
    {
        public abstract void Initialize(T componentInitParameters);
    }
    
    public abstract class ComponentDto {}
}