using System.Collections.Generic;
using Game.Config;
using Game.Controllers;
using Game.View;
using UnityEngine;

namespace Game
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private int _animationSpeed = 15;

        [Header("Player")] [SerializeField] private LevelObjectView _playerView;

        private SpriteAnimationConfig _playerConfig;
        private SpriteAnimatorController _playerAnimator;
        private PlayerController _playerController;

        [Header("Coins")] 
        [SerializeField] private List<LevelObjectView> _coinViews;

        private SpriteAnimationConfig _coinConfig;
        private SpriteAnimatorController _coinAnimator;


        [Header("Cannon")] 
        [SerializeField] private CannonView _cannonView;

        [Header("DeathZone")] 
        [SerializeField] private List<LevelObjectView> _deathZonesView;

        [Header("Parallax")] 
        [SerializeField] private GameObject _backGround;

        [SerializeField] private GameObject _midleGround;
        [SerializeField] private GameObject _frontGround;

        private Camera _camera;
        private ParallaxController _parallaxController;
        private DeathZoneController _deathZoneController;
        private AimingMuzzleController _aimingMuzzleController;
        private CoinsController _coinsController;


        private void Awake()
        {
            _camera = Camera.main;
            _parallaxController = new ParallaxController(_camera, _backGround, _midleGround, _frontGround);

            PlayerInitialization();
            CoinsInitialization();

            _aimingMuzzleController = new AimingMuzzleController(_cannonView.MuzzleTransform, _playerView.Transform);
            _deathZoneController = new DeathZoneController(_playerView, _deathZonesView);
        }

        private void Update()
        {
            _parallaxController.Update();

            _playerAnimator.Update();
            
            _coinAnimator.Update();

            _aimingMuzzleController.Update();
        }

        private void FixedUpdate()
        {
            _playerController.FixedUpdate();
        }

        private void PlayerInitialization()
        {
            _playerConfig = Resources.Load<SpriteAnimationConfig>("PlayerAnimatorConfigs");
            _playerAnimator = new SpriteAnimatorController(_playerConfig);
            _playerController = new PlayerController(_playerView, _playerAnimator);
        }

        private void CoinsInitialization()
        {
            _coinConfig = Resources.Load<SpriteAnimationConfig>("CoinAnimatorConfigs");
            _coinAnimator = new SpriteAnimatorController(_coinConfig);
            _coinsController = new CoinsController(_playerView, _coinViews, _coinAnimator);
        }
    }
}