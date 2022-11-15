using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Map_change : MonoBehaviour
{
    GenerateMap regenerate_map;
    GameObject StartPoint;
    public bool _start;
    GameObject game_maneger;
    public GameObject _playerGameObject;
    // Start is called before the first frame update
    void Start()
    {
        game_maneger = GameObject.Find("GameManager");
        
        regenerate_map = game_maneger.GetComponent<GenerateMap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        System.Random r = new System.Random();
        GameManager.instance._currentMap = r.Next(0, 10);
        regenerate_map._regenerateAMap = true;
        _start = true;
        
    }
}
