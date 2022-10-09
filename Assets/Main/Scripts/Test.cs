using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.aKey.isPressed)
        {
            characterController.enabled = false;
            //　キャラクターの位置を変更する
            transform.position = new Vector3(10, 10, 10);
            //　CharacterControllerコンポーネントを有効化する
            characterController.enabled = true;
        }

    }
}
