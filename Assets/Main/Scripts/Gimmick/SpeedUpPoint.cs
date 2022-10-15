using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

 
public class SpeedUpPoint : MonoBehaviour
{
    public  GameObject _Character;
    private void OnTriggerEnter(Collider other)
    {
        _Character.GetComponent<ThirdPersonController>().MoveSpeed *= 2;

    }

    private void OnTriggerExit(Collider other)
    {
        Invoke(nameof(DownSpeed),2.0f);
    }

    void DownSpeed(){
        _Character.GetComponent<ThirdPersonController>().MoveSpeed /= 2;
    }
}