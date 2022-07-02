using UnityEngine;

namespace Game.Controllers
{
    public class ParallaxController
    {
        private readonly Transform _camera;
        private readonly Transform _backGround;
        private readonly Transform _midleGround;
        private readonly Transform _frontGround;
        
        private Vector3 _cameraStartPosition;
        private float _midleGroundTextureSizeX;
        private float _frontGroundTextureSizeX;

        private Vector2 _coef;

        public ParallaxController(Camera camera, GameObject backGround, GameObject midleGround, GameObject frontGround)
        {
            _camera = camera.transform;
            _backGround = backGround.transform;
            _midleGround = midleGround.transform;
            _frontGround = frontGround.transform;

            _cameraStartPosition = _camera.position;

            _coef = new Vector2(0.3f, 0.3f);

            _midleGroundTextureSizeX = GetTextureSize(midleGround);
            _frontGroundTextureSizeX = GetTextureSize(frontGround);
        }

        public void Update()
        {
            Vector3 deltaMovement = _camera.position - _cameraStartPosition;

            _backGround.position = new Vector3(_camera.position.x, _backGround.position.y);
            _midleGround.position += new Vector3(deltaMovement.x * _coef.x, deltaMovement.y * _coef.y);
            _frontGround.position += new Vector3(deltaMovement.x * -_coef.x, deltaMovement.y * -_coef.y);
            _cameraStartPosition = _camera.position;
            
            if (Mathf.Abs(_camera.position.x - _midleGround.position.x) >= _midleGroundTextureSizeX)
            {
                float offsetPositionX = (_camera.position.x - _midleGround.position.x) % _midleGroundTextureSizeX;
                _midleGround.position = new Vector3(_camera.position.x + offsetPositionX, _midleGround.position.y);
            }
            
            if (Mathf.Abs(_camera.position.x - _frontGround.position.x) >= _frontGroundTextureSizeX)
            {
                float offsetPositionX = (_camera.position.x - _frontGround.position.x) % _frontGroundTextureSizeX;
                _frontGround.position = new Vector3(_camera.position.x + offsetPositionX, _frontGround.position.y);
            }
            
        }

        private float GetTextureSize(GameObject value)
        {
            Sprite sprite = value.GetComponent<SpriteRenderer>().sprite;
            Texture2D texture = sprite.texture;
            float textureUnitSize = texture.width / sprite.pixelsPerUnit;
            return textureUnitSize;
        }
    }
}