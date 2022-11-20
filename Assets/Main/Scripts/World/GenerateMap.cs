using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GenerateMap : MonoBehaviour
{
    public GameObject[] _component;
    public GameObject _mapParentGameObject;
    public GameObject enemies;
    public bool _regenerateAMap;

    public List<Transform> enemiesTransform;
    public List<GameObject> warpPoints;

    [System.NonSerialized]//“G‚Ì’Tõ—p‚Ìî•ñ
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

    void LateUpdate()
    {
        //“G‚ÌˆÊ’u‚ğæ“¾
        foreach(Transform Obj in _mapParentGameObject.transform)
        {
            if (Obj.gameObject.CompareTag("Enemy"))
            {
                enemiesTransform.Add(Obj.gameObject.transform);
            }
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
                    
                    if (_mapComponent.CompareTag("WarpPoint"))
                    {
                        warpPoints.Add(_mapComponent);
                    }
                }
            }
        }

        //Warp, Enemy‚Ìˆ—
        int index = 1 + _height;
        while (index < _splitedText.Length)
        {
            string[] line = _splitedText[index].Split(' ');
            index++;
            string com = line[0];//ƒRƒ}ƒ“ƒh
            int num = int.Parse(line[1]);//ŒÂ”
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

            if (com == "Enemy" || com == "Enemy")
            {
                for (int i = 0; i < num; i++)
                {
                    string[] lines = _splitedText[index].Split(' ').ToArray();
                    index++;
                    List<Tuple<int, int>> points = new List<Tuple<int, int>>();
                    for (int j = 0; j < lines.Length; j++)
                    {
                        List<int> ints = lines[j].Split(',').Select(int.Parse).ToList();
                        points.Add(new Tuple<int, int>(ints[0], ints[1]));
                    }
                    GameObject enemy = Instantiate(enemies, new Vector3(points[0].Item1 - (int)_center.x, 0, -points[1].Item1 + (int)_center.y), Quaternion.identity);
                    enemy.transform.parent = _mapParentGameObject.transform;
                    enemy.GetComponent<WanderingEnemy>().point = points;
                }
            }
        }


    }
}
