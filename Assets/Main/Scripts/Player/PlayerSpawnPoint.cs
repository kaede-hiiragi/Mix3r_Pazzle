using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    public GameObject _playerGameObject;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance._playerSpawnPosition = new Vector3(Mathf.Floor(this.transform.position.x), Mathf.Floor(this.transform.position.y), Mathf.Floor(this.transform.position.z));
        GameManager.instance._playerPosition = GameManager.instance._playerSpawnPosition;
        _playerGameObject.GetComponent<CharacterController>().enabled = false;
        _playerGameObject.transform.position = GameManager.instance._playerSpawnPosition;
        _playerGameObject.GetComponent<CharacterController>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
