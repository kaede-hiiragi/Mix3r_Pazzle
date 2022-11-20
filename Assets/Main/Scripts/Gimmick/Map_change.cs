using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Map_change : MonoBehaviour
{
    GenerateMap regenerate_map;
    GameObject SpawnPoint;
    public bool _start;
    GameObject game_maneger;
    public GameObject _playerGameObject;
    // Start is called before the first frame update
    void Start()
    {
        game_maneger = GameObject.Find("GameManager");
        SpawnPoint = GameObject.Find("PlayerSpawnPoint");
        regenerate_map = game_maneger.GetComponent<GenerateMap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        System.Random r = new System.Random();
        if (!GameManager.instance.is_gameover)
        {
            GameManager.instance._clearCount += 1;
        }

        GameManager.instance._currentMap = r.Next(0, 10);
        regenerate_map._regenerateAMap = true;
        other.GetComponent<CharacterController>().enabled = false;
        other.gameObject.transform.position = SpawnPoint.transform.position;
        other.GetComponent<GridMove>()._destinationPosition.x = SpawnPoint.transform.position.x;
        other.GetComponent<GridMove>()._destinationPosition.y = SpawnPoint.transform.position.z;
        other.GetComponent<CharacterController>().enabled = true;
        
    }
}
