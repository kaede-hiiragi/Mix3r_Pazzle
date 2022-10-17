using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpPoint : MonoBehaviour
{
    public bool _isWarp = true;
    public GameObject _destination;
    private void OnTriggerEnter(Collider other)
    {
        if(_isWarp)
        {
            other.GetComponent<CharacterController>().enabled = false;
            other.gameObject.transform.position = _destination.transform.position;
            other.GetComponent<GridMove>()._destinationPosition.x = _destination.transform.position.x;
            other.GetComponent<GridMove>()._destinationPosition.y = _destination.transform.position.z;
            other.GetComponent<CharacterController>().enabled = true;
            if(_destination.GetComponent<WarpPoint>())
            {
                _destination.GetComponent<WarpPoint>()._isWarp = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(_isWarp == false)
        {
            _isWarp = true;
        }
    }
}