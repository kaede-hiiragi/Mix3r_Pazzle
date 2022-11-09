using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipPazzle : MonoBehaviour
{
    public GameObject[] _targetObjects;
    public GameObject _door;
    public GameObject[] _slaveObjects;

    public Material _offMaterial;
    public Material _onMaterial;

    public bool _currentCondition = false;
    public bool _isMaster = false;
    
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Renderer>().material = _offMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        bool _isSolved = true;
        if(_isMaster)
        {
            foreach(GameObject _slave in _slaveObjects)
            {
                if(_slave.GetComponent<FlipPazzle>()._currentCondition == false)
                {
                    _isSolved = false;
                }   
            }
            
            if(_isSolved)
            {
                print("clear");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(_currentCondition)
        {
            this.GetComponent<Renderer>().material = _offMaterial;
            _currentCondition = !_currentCondition;
        }
        else
        {
            this.GetComponent<Renderer>().material = _onMaterial;
            _currentCondition = !_currentCondition;
        }
        foreach(GameObject _targetObject in _targetObjects)
        {
            if(_targetObject.GetComponent<FlipPazzle>()._currentCondition)
            {
                _targetObject.GetComponent<Renderer>().material = _offMaterial;
                _targetObject.GetComponent<FlipPazzle>()._currentCondition = !_targetObject.GetComponent<FlipPazzle>()._currentCondition;
            }
            else if(!_targetObject.GetComponent<FlipPazzle>()._currentCondition)
            {
                _targetObject.GetComponent<Renderer>().material = _onMaterial;
                _targetObject.GetComponent<FlipPazzle>()._currentCondition = !_targetObject.GetComponent<FlipPazzle>()._currentCondition;
            }
        }
    }

    // private void OnTriggerExit(Collider other)
    // {
    //     this.GetComponent<Renderer>().material = _initialMaterial;
    // }
}
