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

        Vector2 _previousPosition;
        Vector2 _currentPosition;
        // Start is called before the first frame update
        void Start()
        {
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<StarterAssetsInputs>();
        }

        // Update is called once per frame
        void Update()
        {
            var current = Keyboard.current; //現在のキーボード情報を取得
            _currentPosition = new Vector2(transform.position.x, transform.position.z);

            if (current == null) //キーボードの接続チェック
            {
                return;
            }

            if(_input.move == Vector2.zero)
            {
                _previousPosition = new Vector2(transform.position.x, transform.position.z);
                if (current.wKey.isPressed && !current.aKey.isPressed && !current.sKey.isPressed && current.dKey.isPressed)
                {
                    _input.move = new Vector2(1, 1);
                }
                else if (current.wKey.isPressed && current.aKey.isPressed && !current.sKey.isPressed && !current.dKey.isPressed)
                {
                    _input.move = new Vector2(-1, 1);
                }
                else if (!current.wKey.isPressed && current.aKey.isPressed && current.sKey.isPressed && !current.dKey.isPressed)
                {
                    _input.move = new Vector2(-1, -1);
                }
                else if (!current.wKey.isPressed && !current.aKey.isPressed && current.sKey.isPressed && current.dKey.isPressed)
                {
                    _input.move = new Vector2(1, -1);
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

            if((_currentPosition-_previousPosition).magnitude >= 1)
            {
                _input.move = Vector2.zero;
            }
        }
    }
}
