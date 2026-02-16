using UnityEngine;

namespace TheForge.Extensions
{
    public static class Vector2Extensions
    {
        public static Vector2Int ToVector2Int(this Vector2 vector)
            => new Vector2Int(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y));
    } 
}