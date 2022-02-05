using UnityEngine;

namespace CCGKit
{
    [CreateAssetMenu(
        menuName = "Single-Player CCG Kit/Templates/Status",
        fileName = "Status",
        order = 5)]
    public class StatusTemplate : ScriptableObject
    {
        public string Name;
        public Sprite Sprite;
    }
}