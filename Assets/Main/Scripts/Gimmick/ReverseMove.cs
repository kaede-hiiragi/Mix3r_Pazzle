using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReverseMove : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject PlayerArmature;
    GridMove gridmove;
    void Start()
    {
        PlayerArmature = GameObject.Find("PlayerArmature");
        gridmove = PlayerArmature.GetComponent<GridMove>();
    }

    // Update is called once per frame
    void Update()
    {
        var current = Keyboard.current;
        if (current.wKey.isPressed && !current.aKey.isPressed && !current.sKey.isPressed && current.dKey.isPressed)
        {
            gridmove._tempMoveDirectionValue = 3;
        }
        if (current.wKey.isPressed && current.aKey.isPressed && !current.sKey.isPressed && !current.dKey.isPressed)
        {
            gridmove._tempMoveDirectionValue = 4;
        }
        if (!current.wKey.isPressed && current.aKey.isPressed && current.sKey.isPressed && !current.dKey.isPressed)
        {
            gridmove._tempMoveDirectionValue = 1;
        }
        if (!current.wKey.isPressed && !current.aKey.isPressed && current.sKey.isPressed && current.dKey.isPressed)
        {
            gridmove._tempMoveDirectionValue = 2;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        print("reverse");
        
        gridmove._operationInterrupt = true;
    }
        


}
