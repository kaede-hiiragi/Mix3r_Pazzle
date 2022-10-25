using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    // Start is called before the first frame update
    static public PlayerHealthController instance;
    public int maxHealth = 3;
    private int health;

    public GameObject warpObject;

    public Image[] healthImages;
    void Start()
    {
        instance = this;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeHealth(int value)
    {
        health += value;
        for (int i = 0; i < maxHealth; i++)
        {
            if (i < health)
            {
                healthImages[i].gameObject.SetActive(true);
            } else
            {
                healthImages[i].gameObject.SetActive(false);
            }
            
        }

        if (health == 0)
        {
            warpObject.SetActive(true);
        }
    }
}
