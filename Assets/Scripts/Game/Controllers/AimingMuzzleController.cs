﻿using UnityEngine;

namespace Game.Controllers
{
    public class AimingMuzzleController
    {
        private readonly Transform _muzzleTransform;
        private readonly Transform _aimTransform;

        public AimingMuzzleController(Transform muzzleTransform, Transform aimTransform)
        {
            _muzzleTransform = muzzleTransform;
            _aimTransform = aimTransform;
        }

        public void Update()
        {
            var dir = _aimTransform.position - _muzzleTransform.position;
            var angle = Vector3.Angle(Vector3.down, dir);
            var axis = Vector3.Cross(Vector3.down, dir);
            _muzzleTransform.rotation = Quaternion.AngleAxis(angle, axis);
        }
    }
}