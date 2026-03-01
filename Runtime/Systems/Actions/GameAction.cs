using UnityEngine;
using VContainer;

namespace TheForge.Systems.Actions
{
    public abstract class GameAction : ScriptableObject
    {
        [SerializeField] private Sprite sprite;
        
        protected abstract void Executable();

        public virtual void Init(IObjectResolver resolver) { }
        
        public void Execute()
        {
            Executable();
        }
        
        public Sprite ActionSprite => sprite;
    }
}