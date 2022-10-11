using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;


public class MoveReverse : MonoBehaviour
{
    GameObject PlayerArmature;
    GridMove gridmove;
    List<int> numbers = new List<int>() {1, 2, 3, 4};
    
    // Start is called before the first frame update
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
            gridmove._tempMoveDirectionValue = numbers[0];
        }
        if (current.wKey.isPressed && current.aKey.isPressed && !current.sKey.isPressed && !current.dKey.isPressed)
        {
            gridmove._tempMoveDirectionValue = numbers[1];
        }
        if (!current.wKey.isPressed && current.aKey.isPressed && current.sKey.isPressed && !current.dKey.isPressed)
        {
            gridmove._tempMoveDirectionValue = numbers[2];
        }
        if (!current.wKey.isPressed && !current.aKey.isPressed && current.sKey.isPressed && current.dKey.isPressed)
        {
            gridmove._tempMoveDirectionValue = numbers[3];
        }
    }

    void OnTriggerEnter(Collider other)
    {
        print("reverse");
        var random = new System.Random();
        
        numbers = numbers.OrderBy(x => random.Next()).ToList();
        
        gridmove._operationInterrupt = true;
        
    }
}
