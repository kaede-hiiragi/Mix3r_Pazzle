using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class ReverseMoveRandom : MonoBehaviour
{GameObject PlayerArmature;
    GridMove gridmove;
    public List<int> numbers = new List<int>() {1, 2, 3, 4};
    int i = 0;
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
        
        if (i < 1000)
        {
            if (current.wKey.isPressed && !current.aKey.isPressed && !current.sKey.isPressed && current.dKey.isPressed)
            {
                gridmove._tempMoveDirectionValue = numbers[0];
                i += 1;
            }
            if (current.wKey.isPressed && current.aKey.isPressed && !current.sKey.isPressed && !current.dKey.isPressed)
            {
                gridmove._tempMoveDirectionValue = numbers[1];
                i += 1;
            }
            if (!current.wKey.isPressed && current.aKey.isPressed && current.sKey.isPressed && !current.dKey.isPressed)
            {
                gridmove._tempMoveDirectionValue = numbers[2];
                i += 1;
            }
            if (!current.wKey.isPressed && !current.aKey.isPressed && current.sKey.isPressed && current.dKey.isPressed)
            {
                gridmove._tempMoveDirectionValue = numbers[3];
                i += 1;
            }
        }
        else
        {
            gridmove._operationInterrupt = false;
            i = 0;
            print("reset");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        print("random");
        var random = new System.Random();
        
        numbers = numbers.OrderBy(x => random.Next()).ToList();
        
        gridmove._operationInterrupt = true;

        i = 0;
        
    }
}
