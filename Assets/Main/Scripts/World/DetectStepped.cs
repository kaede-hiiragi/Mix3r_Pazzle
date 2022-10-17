using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectStepped : MonoBehaviour
{
    // Start is called before the first frame update
    public Material _steppedMaterial;

    private void OnTriggerEnter(Collider other)
    {
        this.GetComponent<Renderer>().material = _steppedMaterial;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
