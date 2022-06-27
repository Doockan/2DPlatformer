using Game.Config;
using Game.Controllers;
using Game.View;
using UnityEngine;

namespace Game
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private int _animationSpeed = 15;
        
        
        
        [Header("Player")]
        [SerializeField] private PlayerView _playerView;
        
        private SpriteAnimatorController _playerAnimator;
        private SpriteAnimationConfig _playerConfig;
        
        [Header("Enemy")]
        [SerializeField] private EnemyView _enemyView;
        
        private SpriteAnimatorController _enemyAnimator;
        private SpriteAnimationConfig _enemyConfig;
        
        [Header("Fireball")]
        [SerializeField] private FireballView _fireballView;
        
        private SpriteAnimatorController _fireballAnimator;
        private SpriteAnimationConfig _fireballConfig;
        
        [Header("Explosion")]
        [SerializeField] private ExplosionView _explosionView;
        
        private SpriteAnimatorController _explosionAnimator;
        private SpriteAnimationConfig _explosionConfig;

        [Header("Parallax")] 
        [SerializeField] private GameObject _backGround;
        [SerializeField] private GameObject _midleGround;
        [SerializeField] private GameObject _frontGround;
        
        private Camera _camera;
        private ParallaxController _parallaxController;
        

        private void Awake()
        {
            _camera = Camera.main;
            _parallaxController = new ParallaxController(_camera, _backGround, _midleGround, _frontGround);
            
            
            PlayerInitialization();
            EnemyInitialization();
            FireballInitialization();
            ExplosionInitialization();
        }

        private void Update()
        {
            _parallaxController.Update();
            
            PlayerMoveAnimation();
            AnimatorsUpdate();
        }

        private void PlayerMoveAnimation()
        {
            if (Input.GetKey(KeyCode.W))
            {
                _playerAnimator.StopAnimation(_playerView.SpriteRenderer);
                _playerAnimator.StartAnimation(_playerView.SpriteRenderer, AnimState.Run, true, _animationSpeed);
            }

            if (Input.GetKey(KeyCode.S))
            {
                _playerAnimator.StopAnimation(_playerView.SpriteRenderer);
                _playerAnimator.StartAnimation(_playerView.SpriteRenderer, AnimState.Idle, true, _animationSpeed);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _playerAnimator.StopAnimation(_playerView.SpriteRenderer);
                _playerAnimator.StartAnimation(_playerView.SpriteRenderer, AnimState.Jump, false, _animationSpeed);
            }
        }

        private void AnimatorsUpdate()
        {
            _playerAnimator.Update();
            _enemyAnimator.Update();
            _fireballAnimator.Update();
            _explosionAnimator.Update();
        }

        private void ExplosionInitialization()
        {
            _explosionConfig = Resources.Load<SpriteAnimationConfig>("ExplosionAnimatorConfigs");
            _explosionAnimator = new SpriteAnimatorController(_explosionConfig);
            _explosionAnimator.StartAnimation(_explosionView.SpriteRenderer, AnimState.Run, true, _animationSpeed);
        }

        private void PlayerInitialization()
        {
            _playerConfig = Resources.Load<SpriteAnimationConfig>("PlayerAnimatorConfigs");
            _playerAnimator = new SpriteAnimatorController(_playerConfig);
            _playerAnimator.StartAnimation(_playerView.SpriteRenderer, AnimState.Idle, true, _animationSpeed);
        }

        private void EnemyInitialization()
        {
            _enemyConfig = Resources.Load<SpriteAnimationConfig>("EnemyAnimatorConfigs");
            _enemyAnimator = new SpriteAnimatorController(_enemyConfig);
            _enemyAnimator.StartAnimation(_enemyView.SpriteRenderer, AnimState.Run, true, _animationSpeed);
        }

        private void FireballInitialization()
        {
            _fireballConfig = Resources.Load<SpriteAnimationConfig>("FireballAnimatorConfigs");
            _fireballAnimator = new SpriteAnimatorController(_fireballConfig);
            _fireballAnimator.StartAnimation(_fireballView.SpriteRenderer, AnimState.Run, true, _animationSpeed);
        }
    }
}