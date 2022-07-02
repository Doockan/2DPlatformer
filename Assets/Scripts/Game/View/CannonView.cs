using System.Collections.Generic;
using UnityEngine;

namespace Game.View
{
    public class CannonView : MonoBehaviour
    {
        public Transform MuzzleTransform;
        public Transform Emitter;
        public List<LevelObjectView> BulletPool;
    }
}