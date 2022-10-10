using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class MoveReverse : MonoBehaviour
{
    GameObject PlayerArmature;
    GridMove gridmove;
    // Start is called before the first frame update
    void Start()
    {
        PlayerArmature = GameObject.Find("PlayerArmature");
        gridmove = PlayerArmature.GetComponent<GridMove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        print("reverse");
        gridmove._operationInterrupt = true;
        
    }
}
