using System;
using System.Collections.Generic;
using Game.View;
using UnityEngine;

namespace Game.Controllers
{
    public class DeathZoneController : IDisposable
    {
        private readonly LevelObjectView _playerView;
        private readonly List<LevelObjectView> _deathZones;
        private Vector2 _startPosirion;

        public DeathZoneController(LevelObjectView playerView, List<LevelObjectView> deathZones)
        {
            _playerView = playerView;
            _deathZones = deathZones;

            _startPosirion = _playerView.transform.position;

            _playerView.OnLevelObjectContact += OnLevelObjectContact;
        }

        private void OnLevelObjectContact(LevelObjectView other)
        {
            if (_deathZones.Contains(other))
            {
                Debug.Log("DEATH!!!!");
                _playerView.Rigidbody2D.position = _startPosirion;
            }
        }

        public void Dispose()
        {
            _playerView.OnLevelObjectContact -= OnLevelObjectContact;
        }
    }
}