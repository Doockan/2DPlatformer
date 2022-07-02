using System;
using System.Collections.Generic;
using Game.Config;
using Game.View;
using UnityEngine;

namespace Game.Controllers
{
    public class CoinsController : IDisposable
    {
        private const float _animationSpeed = 15f;
        
        private readonly LevelObjectView _playerView;
        private readonly List<LevelObjectView> _coinViews;
        private readonly SpriteAnimatorController _spriteAnimator;


        public CoinsController(LevelObjectView playerView, List<LevelObjectView> coinViews,
            SpriteAnimatorController spriteAnimator)
        {
            _playerView = playerView;
            _coinViews = coinViews;
            _spriteAnimator = spriteAnimator;
            _playerView.OnLevelObjectContact += OnLevelObjectContact;
            
            
            foreach (var coinView in _coinViews)
            {
                var spriteRenderer = coinView.SpriteRenderer;
                _spriteAnimator.StartAnimation(spriteRenderer, AnimState.Idle, true, _animationSpeed);
            }
        }

        private void OnLevelObjectContact(LevelObjectView other)
        {
            if (_coinViews.Contains(other))
            {
                _spriteAnimator.StopAnimation(other.SpriteRenderer);
                GameObject.Destroy(other.gameObject);
            }
        }

        public void Dispose()
        {
            _playerView.OnLevelObjectContact -= OnLevelObjectContact;
        }
    }
}