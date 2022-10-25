using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

 
public class SpeedUpPoint : MonoBehaviour
{
    public  GameObject _Character;
    private void OnTriggerEnter(Collider other)
    {
        _Character.GetComponent<ThirdPersonController>().MoveSpeed = 4;
        _Character.GetComponent<ThirdPersonController>().SprintSpeed = 8;
        CancelInvoke();
        Invoke(nameof(DownSpeed),2.0f);

    }

    private void OnTriggerExit(Collider other)
    {
    }

    void DownSpeed(){
        _Character.GetComponent<ThirdPersonController>().MoveSpeed = 2;
        _Character.GetComponent<ThirdPersonController>().SprintSpeed = 4;
    }
}