using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public Vector3 _playerSpawnPosition;
    public Vector3 _playerPosition;
    public TextAsset[] _mapsData;
    public int _currentMap;
    public int _clearCount;

    public bool is_gameover = false;
    public Text gameoverText;
    public Text clearCountText;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        gameoverText.gameObject.SetActive(false);
        clearCountText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (is_gameover)
        {
            GameOver();
        }
    }


    public void GameOver()
    {
        string clearCount = "Clear:" + _clearCount.ToString();
        gameoverText.gameObject.SetActive(true);
        clearCountText.gameObject.SetActive(true);

        clearCountText.text = clearCount;
    }
}
