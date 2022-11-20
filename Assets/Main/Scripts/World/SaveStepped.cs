using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStepped : MonoBehaviour
{
    // Start is called before the first frame update
    public Material _initialMaterial;
    public Material _steppedMaterial;
    public Material _enemyMaterial;

    private List<Transform> enemies;

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

        enemies = GameObject.Find("GameManager").GetComponent<GenerateMap>().enemiesTransform;
    }

    // Update is called once per frame
    void Update()
    {
        bool is_player = GameManager.instance._playerPosition.x == this.transform.position.x && GameManager.instance._playerPosition.z == this.transform.position.z;
        bool is_enemy = false;
        for (int i = 0; i < enemies.Count; i++)
        {
            is_enemy = is_enemy || enemies[i].transform.position.x == transform.position.x || enemies[i].transform.position.z == transform.position.z;
        }

        if (is_player && is_enemy)
        {
            this.GetComponent<Renderer>().material = _steppedMaterial;
        } 
        else if (is_player)
        {
            this.GetComponent<Renderer>().material = _steppedMaterial;
        }
        else if (is_enemy)
        {
            this.GetComponent<Renderer>().material = _enemyMaterial;
        }

    }
}
