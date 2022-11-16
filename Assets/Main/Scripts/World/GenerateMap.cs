using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GenerateMap : MonoBehaviour
{
    public GameObject[] _component;
    public GameObject _mapParentGameObject;
    public bool _regenerateAMap;

    public List<Transform> enemiesTransform;
    public List<GameObject> warpPoints;

    [System.NonSerialized]//敵の探索用の情報
    public List<List<int>> map_data = new List<List<int>>();
    // Start is called before the first frame update
    void Start()
    {
        generate(GameManager.instance._mapsData[GameManager.instance._currentMap]);
    }

    // Update is called once per frame
    void Update()
    {
        if(_regenerateAMap)
        {
            foreach (Transform child in _mapParentGameObject.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            generate(GameManager.instance._mapsData[GameManager.instance._currentMap]);
            _regenerateAMap = false;
        }
    }

    void generate(TextAsset mapDesign)
    {
        string[] _splitedText;
        int _height;
        int _width;
        Vector2 _center;

        _splitedText = mapDesign.text.Split(char.Parse("\n"));
        
        int[] _mapInfo;
        _mapInfo = _splitedText[0].Split(',').Select(int.Parse).ToArray();

        _width = _mapInfo[0];
        _height = _mapInfo[1];
        _center = new Vector2((int)(_width/2), (int)(_height/2));

        for(int i = 1; i <= _height; i++)
        {
            int[] _aRowOfMap = _splitedText[i].Split(',').Select(int.Parse).ToArray();
            map_data.Add(_aRowOfMap.ToList());
            for (int j = 0; j < _width; j++)
            {
                if(_aRowOfMap[j] != -1)
                {
                    GameObject _mapComponent = Instantiate(_component[_aRowOfMap[j]]);
                    _mapComponent.transform.parent = _mapParentGameObject.transform;
                    _mapComponent.transform.position = new Vector3(j-_center.x, 0, (-i)+_center.y+1);
                    if (_mapComponent.CompareTag("Enemy"))
                    {
                        enemiesTransform.Add(_mapComponent.transform);
                    }
                    if (_mapComponent.CompareTag("WarpPoint"))
                    {
                        warpPoints.Add(_mapComponent);
                    }
                }
            }
        }

        //Warp, Enemyの処理
        int index = 1 + _height;
        while (index < _splitedText.Length)
        {
            string[] line = _splitedText[index].Split(' ');
            index++;
            string com = line[0];//コマンド
            int num = int.Parse(line[1]);//個数
            if (com == "Warp" || com == "warp")
            {
                for (int i = 0; i < num; i++)
                {
                    int[] connect = _splitedText[index].Split(' ').Select(int.Parse).ToArray();
                    index++;
                    GameObject children1 = warpPoints[connect[0] - 1].transform.GetChild(0).gameObject;
                    GameObject children2 = warpPoints[connect[1] - 1].transform.GetChild(0).gameObject;
                    children1.GetComponent<WarpPoint>()._destination = children2;
                    children2.GetComponent<WarpPoint>()._destination = children1;
                }
            }
        }


    }
}
