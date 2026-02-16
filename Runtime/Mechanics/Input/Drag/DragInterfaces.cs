using UnityEngine;

namespace TheForge.Mechanics.Input.Drag
{
    public interface IDragPressedHandler
    {
        void OnDragPressed(Vector3 pressedPosition);
    }

    public interface IDragHandler
    {
        void OnDrag(Vector3 dragDirection);
    }
    
    public interface IDragReleasedHandler
    {
        void OnDragReleased(Vector3 releasePosition);
    }
}