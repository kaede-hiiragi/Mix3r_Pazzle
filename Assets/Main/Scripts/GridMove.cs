using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StarterAssets
{
    public class GridMove : MonoBehaviour
    {
        private CharacterController _controller;
        private StarterAssetsInputs _input;

        bool _isMoving = false;
        int _currentDirection;
        int _previousDirection;

        Vector2 _firstPositon;
        Vector2 _previousPosition;
        Vector2 _destinationPosition;
        Vector2 _currentPosition;
        // Start is called before the first frame update
        void Start()
        {
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<StarterAssetsInputs>();

            _destinationPosition = new Vector2(transform.position.x, transform.position.z);
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 _playerPosition = transform.position;
            var current = Keyboard.current; //現在のキーボード情報を取得
            _currentPosition = new Vector2(_playerPosition.x, _playerPosition.z);

            if (current == null) //キーボードの接続チェック
            {
                return;
            }

            if(_isMoving == false)
            {
                _previousPosition = new Vector2(Mathf.Floor(_playerPosition.x), Mathf.Floor(_playerPosition.z));
                if (current.wKey.isPressed && !current.aKey.isPressed && !current.sKey.isPressed && current.dKey.isPressed)
                {
                    _destinationPosition += Vector2.right;
                    _input.move = new Vector2(1, 1);
                    _currentDirection = 0;
                    _isMoving = true;
                }
                else if (current.wKey.isPressed && current.aKey.isPressed && !current.sKey.isPressed && !current.dKey.isPressed)
                {
                    _destinationPosition += Vector2.up;
                    _input.move = new Vector2(-1, 1);
                    _currentDirection = 1;
                    _isMoving = true;
                }
                else if (!current.wKey.isPressed && current.aKey.isPressed && current.sKey.isPressed && !current.dKey.isPressed)
                {
                    _destinationPosition += Vector2.left;
                    _input.move = new Vector2(-1, -1);
                    _currentDirection = 2;
                    _isMoving = true;
                }
                else if (!current.wKey.isPressed && !current.aKey.isPressed && current.sKey.isPressed && current.dKey.isPressed)
                {
                    _destinationPosition += Vector2.down;
                    _input.move = new Vector2(1, -1);
                    _currentDirection = 3;
                    _isMoving = true;
                }
            }

            if(current.shiftKey.isPressed)
            {
                _input.sprint = true;
            }
            else
            {
                _input.sprint = false;
            }

            if (_isMoving == true)
            {
                if (!current.wKey.isPressed && !current.aKey.isPressed && !current.sKey.isPressed && !current.dKey.isPressed)
                {
                }
            }

            if (_isMoving == true && ((_destinationPosition-_currentPosition).magnitude <= 0.1f || (_destinationPosition - _currentPosition).magnitude >= 2.0f))
            {
                _input.move = Vector2.zero;
                if ((!current.wKey.isPressed && !current.aKey.isPressed && !current.sKey.isPressed && !current.dKey.isPressed) || _currentDirection != _previousDirection)
                {
                    _controller.enabled = false;
                    transform.position = new Vector3(_destinationPosition.x, transform.position.y, _destinationPosition.y);
                    _controller.enabled = true;
                }

                _isMoving = false;
            }

            _previousDirection = _currentDirection;
        }
        void OnGUI()
        {
            GUILayout.Label($"_isMoving: {_isMoving}");
            GUILayout.Label($"_destinationPosition.x: {_destinationPosition.x}");
            GUILayout.Label($"_destinationPosition.y: {_destinationPosition.y}");
            GUILayout.Label($"_currentPosition.x: {_currentPosition.x}");
            GUILayout.Label($"_currentPosition.y: {_currentPosition.y}");
            GUILayout.Label($"_currentDirection: {_currentDirection}");
            GUILayout.Label($"distance: {(_destinationPosition - _currentPosition).magnitude}");
        }
    }
}
