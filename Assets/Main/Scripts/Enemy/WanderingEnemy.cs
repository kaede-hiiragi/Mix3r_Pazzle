using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingEnemy : MonoBehaviour
{
    public Transform[] waypoints;
    int m_CurrentWaypointIndex;
    private List<List<int>> map;


    // Start is called before the first frame update
    void Start()
    {
        m_CurrentWaypointIndex = 0;
        map = GameObject.Find("GameManager").GetComponent<GenerateMap>().map_data;
        for (int i = 0; i < map.Count; i++)
        {
            string tmp = "";
            for (int j = 0; j < map[i].Count; j++)
            {
                tmp = tmp + " " + map[i][j].ToString();
            }
            Debug.Log(tmp);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealthController.instance.ChangeHealth(-1);
            Destroy(gameObject);
        }
    }

}
