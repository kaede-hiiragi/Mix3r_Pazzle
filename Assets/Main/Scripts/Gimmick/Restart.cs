using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class Restart : MonoBehaviour
{
    public string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        sceneName = SceneManager.GetActiveScene ().name;
    }

    // Update is called once per frame
    void Update()
    {
        var current = Keyboard.current;
        if(current.escapeKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene (sceneName);
        }
    }
}
