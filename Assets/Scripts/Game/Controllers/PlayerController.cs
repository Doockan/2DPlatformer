using Game.Config;
using Game.Utils;
using Game.View;
using UnityEngine;

namespace Game.Controllers
{
    public class PlayerController
    {
        private const string _verticalAxisName = "Vertical";
        private const string _horizontalAxisName = "Horizontal";

        private const float _movingThresh = 0.1f;
        private const float _movingSpeed = 150f;
        private const float _jumpThresh = 0.1f;
        private const float _jumpForce = 350f;
        private const float _animationSpeed = 15f;
        private const float _flyThresh = 1f;

        private Vector3 _leftScale = new Vector3(-1,1,1);
        private Vector3 _rightScale = new Vector3( 1,1,1);

        private readonly LevelObjectView _playerView;
        private readonly SpriteAnimatorController _spriteAnimator;
        private readonly ContactPoller _contactPoller;

        private bool _doJump;
        private bool _doSecondJump;
        private float _xAxisInput;


        public PlayerController(LevelObjectView playerView, SpriteAnimatorController spriteAnimator)
        {
            _playerView = playerView;
            _spriteAnimator = spriteAnimator;
            _contactPoller = new ContactPoller(_playerView.Collider2D);

            _doSecondJump = false;
        }

        public void FixedUpdate()
        {
            _doJump = Input.GetAxis(_verticalAxisName) > 0;
            _xAxisInput = Input.GetAxis(_horizontalAxisName);
            _contactPoller.Update();

            var move = Mathf.Abs(_xAxisInput) > _movingThresh;
        
            if (move) MoveTowards();
            Jump();

            if (_contactPoller.IsGrounded)
            {
                var track = move ? AnimState.Run : AnimState.Idle;
                _spriteAnimator.StartAnimation(_playerView.SpriteRenderer, track, true, _animationSpeed);
            }
            else if (Mathf.Abs(_playerView.Rigidbody2D.velocity.y) > _flyThresh)
            {
                var track = AnimState.Jump;
                _spriteAnimator.StartAnimation(_playerView.SpriteRenderer, track, false, _animationSpeed);
            }
        }

        private void MoveTowards()
        {
            _playerView.transform.localScale = _xAxisInput < 0 ? _leftScale : _rightScale;
        
            var newVelocity = 0f;

            if ((_xAxisInput > 0 || !_contactPoller.HasLeftContacts) &&
                (_xAxisInput < 0 || !_contactPoller.HasRightContacts))
            {
                newVelocity = Time.fixedDeltaTime * _movingSpeed * (_xAxisInput < 0 ? -1 : 1);
            }

            _playerView.Rigidbody2D.velocity = new Vector2(newVelocity, _playerView.Rigidbody2D.velocity.y);
        }

        private void Jump()
        {
            if (_contactPoller.IsGrounded) _doSecondJump = true;
            
            if (_contactPoller.IsGrounded && _doJump && Mathf.Abs(_playerView.Rigidbody2D.velocity.y) <= _jumpThresh)
            {
                _playerView.Rigidbody2D.AddForce(Vector2.up * _jumpForce);
            }

            if (!_contactPoller.IsGrounded && _doSecondJump && Input.GetKey(KeyCode.Space))
            {
                _doSecondJump = false;
                _playerView.Rigidbody2D.velocity = new Vector2(_playerView.Rigidbody2D.velocity.x, 0f);
                _playerView.Rigidbody2D.AddForce(Vector2.up * _jumpForce);
            }
        }
    }
}