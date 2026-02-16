using UnityEngine;

namespace TheForge.Mechanics.Input.ObjectTouch
{
    public interface IObjectTouchReleasedHandler
    {
        void OnObjectTouchReleased(GameObject touchedObject);
    }
}