using UnityEngine;

namespace TheForge.Systems.Actions
{
    public abstract class GameAction : ScriptableObject
    {
        [SerializeField] private Sprite sprite;
        
        protected abstract void Executable();

        public void Execute()
        {
            Executable();
        }
        
        public Sprite ActionSprite => sprite;
    }
}