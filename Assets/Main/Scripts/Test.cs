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
            //�@�L�����N�^�[�̈ʒu��ύX����
            transform.position = new Vector3(10, 10, 10);
            //�@CharacterController�R���|�[�l���g��L��������
            characterController.enabled = true;
        }

    }
}
