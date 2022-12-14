using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;
using System.Linq;

public class GridMove : MonoBehaviour
{
    private CharacterController _controller;
    private StarterAssetsInputs _input;

    [System.NonSerialized]
    public bool _isMoving = false;

    Vector2 _firstPositon;
    Vector2 _previousPosition;
    [System.NonSerialized]
    public Vector2 _destinationPosition;
    [System.NonSerialized]
    public Vector2 _currentPosition;
    [System.NonSerialized]
    public int _moveDirection;

    public int _tempMoveDirectionValue;
    public bool _operationInterrupt = false;
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _input = GetComponent<StarterAssetsInputs>();

        _destinationPosition = new Vector2(transform.position.x, transform.position.z);
        _previousPosition = GameManager.instance._playerSpawnPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 _playerPosition = transform.position;
        var current = Keyboard.current; //���݂̃L�[�{�[�h�����擾
        _currentPosition = new Vector2(_playerPosition.x, _playerPosition.z);

        if (current == null) //�L�[�{�[�h�̐ڑ��`�F�b�N
        {
            return;
        }

        if (_isMoving == false)
        {
            _previousPosition = new Vector2(Mathf.Floor(_playerPosition.x), Mathf.Floor(_playerPosition.z));
            if (current.wKey.isPressed && !current.aKey.isPressed && !current.sKey.isPressed && current.dKey.isPressed)
            {
                if (!_operationInterrupt)
                {
                    _tempMoveDirectionValue = 1;
                }
                gridMoveDirection(_tempMoveDirectionValue);
            }
            else if (current.wKey.isPressed && current.aKey.isPressed && !current.sKey.isPressed && !current.dKey.isPressed)
            {
                if (!_operationInterrupt)
                {
                    _tempMoveDirectionValue = 2;
                }
                gridMoveDirection(_tempMoveDirectionValue);
            }
            else if (!current.wKey.isPressed && current.aKey.isPressed && current.sKey.isPressed && !current.dKey.isPressed)
            {
                if (!_operationInterrupt)
                {
                    _tempMoveDirectionValue = 3;
                }
                gridMoveDirection(_tempMoveDirectionValue);
            }
            else if (!current.wKey.isPressed && !current.aKey.isPressed && current.sKey.isPressed && current.dKey.isPressed)
            {
                if (!_operationInterrupt)
                {
                    _tempMoveDirectionValue = 4;
                }
                gridMoveDirection(_tempMoveDirectionValue);
            }
        }

        if (current.shiftKey.isPressed)
        {
            _input.sprint = true;
        }
        else
        {
            _input.sprint = false;
        }

        if ((_isMoving == true && (_destinationPosition - _currentPosition).magnitude <= 0.1f) || (_destinationPosition - _currentPosition).magnitude >= 2.0f)
        {
            _input.move = Vector2.zero;
            if (!current.wKey.isPressed && !current.aKey.isPressed && !current.sKey.isPressed && !current.dKey.isPressed)
            {
                _controller.enabled = false;
                transform.position = new Vector3(_destinationPosition.x, GameManager.instance._playerPosition.y, _destinationPosition.y);
                _controller.enabled = true;
            }

            _isMoving = false;
        }

        if (GameManager.instance._playerPosition.y > Mathf.Ceil(_playerPosition.y))
        {
            _controller.enabled = false;
            transform.position = new Vector3(transform.position.x, GameManager.instance._playerPosition.y, transform.position.z);
            _controller.enabled = true;
        }

        GameManager.instance._playerPosition.x = _destinationPosition.x;
        GameManager.instance._playerPosition.z = _destinationPosition.y;
    }
    void OnGUI()
    {
        GUILayout.Label($"_isMoving: {_isMoving}");
        GUILayout.Label($"_destinationPosition.x: {_destinationPosition.x}");
        GUILayout.Label($"_destinationPosition.y: {_destinationPosition.y}");
        GUILayout.Label($"_currentPosition.x: {_currentPosition.x}");
        GUILayout.Label($"_currentPosition.y: {_currentPosition.y}");
        GUILayout.Label($"_previousPosition.x: {_previousPosition.x}");
        GUILayout.Label($"_previousPosition.y: {_previousPosition.y}");
        GUILayout.Label($"distance: {(_destinationPosition - _currentPosition).magnitude}");
    }

    void gridMoveDirection(int moveDirection)
    {
        switch(moveDirection)
        {
            case 1:
                if(isFloor(GameManager.instance._mapsData[GameManager.instance._currentMap], (int)_destinationPosition.x+1, (int)_destinationPosition.y))
                {
                    _destinationPosition += Vector2.right;
                    _input.move = new Vector2(1, 1);
                    _isMoving = true;
                }
                break;
            case 2:
                if(isFloor(GameManager.instance._mapsData[GameManager.instance._currentMap], (int)_destinationPosition.x, (int)_destinationPosition.y+1))
                {
                    _destinationPosition += Vector2.up;
                    _input.move = new Vector2(-1, 1);
                    _isMoving = true;
                }
                break;
            case 3:
                if(isFloor(GameManager.instance._mapsData[GameManager.instance._currentMap], (int)_destinationPosition.x-1, (int)_destinationPosition.y))
                {
                    _destinationPosition += Vector2.left;
                    _input.move = new Vector2(-1, -1);
                    _isMoving = true;
                }
                break;
            case 4:
                if(isFloor(GameManager.instance._mapsData[GameManager.instance._currentMap], (int)_destinationPosition.x, (int)_destinationPosition.y-1))
                {
                    _destinationPosition += Vector2.down;
                    _input.move = new Vector2(1, -1);
                    _isMoving = true;
                }
                break;
        }
    }

    bool isFloor(TextAsset mapDesign, int searchX, int searchY)
    {
        string[] _splitedText;
        int _height;
        int _width;
        Vector2 _center;

        _splitedText = mapDesign.text.Split(char.Parse("\n"));
        
        int[] _mapInfo;
        _mapInfo = _splitedText[0].Split(',').Select(int.Parse).ToArray();

        _width = _mapInfo[0];
        _height = _mapInfo[1];
        _center = new Vector2((int)(_width/2), (int)(_height/2));

        if((searchX >= _width-_center.x)||(searchX < -_center.x)||(searchY >= _height-_center.y)||(searchY < -_center.y))
        {
            return false;
        }

        int[] _aRowOfMap = _splitedText[(-searchY)+(int)(_center.y)+1].Split(',').Select(int.Parse).ToArray();
        if(_aRowOfMap[searchX+(int)(_center.x)] == -1)
        {
            return false;
        }

        return true;
    }
}
