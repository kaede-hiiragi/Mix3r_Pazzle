using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlainController : MonoBehaviour
{
    public GameObject player;
    PlayerHealthController playerHealthController;
    // Start is called before the first frame update
    void Start()
    {
        playerHealthController = player.GetComponent<PlayerHealthController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerHealthController.ChangeHealth(-1);
        }
    }
}
