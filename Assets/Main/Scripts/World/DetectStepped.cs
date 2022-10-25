using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectStepped : MonoBehaviour
{
    // Start is called before the first frame update
    public Material _initialMaterial;
    public Material _steppedMaterial;

    // private void OnTriggerEnter(Collider other)
    // {
    //     this.GetComponent<Renderer>().material = _steppedMaterial;
    // }

    // private void OnTriggerExit(Collider other)
    // {
    //     this.GetComponent<Renderer>().material = _initialMaterial;
    // }

    void Start()
    {
        _initialMaterial = this.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if((GameManager.instance._playerPosition.x == this.transform.position.x || GameManager.instance._playerPosition.z == this.transform.position.z))
        {
            this.GetComponent<Renderer>().material = _steppedMaterial;
        }
        else
        {
            this.GetComponent<Renderer>().material = _initialMaterial;
        }
    }
}
