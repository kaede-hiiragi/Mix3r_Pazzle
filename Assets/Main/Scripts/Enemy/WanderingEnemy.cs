using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;

public class WanderingEnemy : MonoBehaviour
{
    int m_CurrentWaypointIndex = 0;
    private List<List<int>> map = new List<List<int>>();
    private List<Tuple<int, int>> path = new List<Tuple<int, int>>();
    [System.NonSerialized]
    public List<Tuple<int, int>> point = new List<Tuple<int, int>>();
    private GridMove gridMove;
    public bool is_move = false;

    private int _height;
    private int _width;
    private Vector2 _center;
    // Start is called before the first frame update
    void Start()
    {
        gridMove = GameObject.Find("PlayerArmature").GetComponent<GridMove>();

        m_CurrentWaypointIndex = 0;
        map = GameObject.Find("GameManager").GetComponent<GenerateMap>().map_data;
        _height = map.Count;
        _width = map[0].Count;
        _center = new Vector2((int)(_width / 2), (int)(_height / 2));
    }

    void Update()
    {
        if (gridMove._isMoving)
        {
            is_move = true;
        }

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (point != null)
        {
            Tuple<int, int> enemy_pos = new Tuple<int, int>((int)transform.position.x + (int)_center.x, -(int)transform.position.z + (int)_center.y);//座標をgridに変換
            if (is_move && ((gridMove._destinationPosition - gridMove._currentPosition).magnitude <= 0.1f) || (gridMove._destinationPosition - gridMove._currentPosition).magnitude >= 2.0f)
            {
                if (path.Count == 0)
                {
                    m_CurrentWaypointIndex++;
                    m_CurrentWaypointIndex %= point.Count;
                    path = bfs(enemy_pos, point[m_CurrentWaypointIndex]);//経路生成

                    Move();
                }
                else
                {
                    Move();

                }
                is_move = false;
            }
            
        }
        
    }


    private List<Tuple<int, int>> bfs(Tuple<int, int> now, Tuple<int, int> nxt)　//Bfsで経路生成
    {
        
        List<int> dx = new List<int>() { 1, 0, -1, 0 };
        List<int> dy = new List<int>() { 0, 1, 0, -1 };

        List<List<int>> dist = Enumerable.Range(0, _height).Select(x => Enumerable.Range(0, _width).Select(y => -1).ToList()).ToList();
        Queue<Tuple<int, int>> que = new Queue<Tuple<int, int>>();
        dist[now.Item2][now.Item1] = 0;
        que.Enqueue(new Tuple<int, int>(now.Item1, now.Item2));

        while (Convert.ToBoolean(que.Count))
        {
            (int x, int y) = que.Peek();
            if (x == nxt.Item1 && y == nxt.Item2)
            {
                break;
            }
            que.Dequeue();
            for (int i = 0; i < 4; i++)
            {
                int nx = x + dx[i];
                int ny = y + dy[i];
                if (nx < 0 || nx >= _width || ny < 0 || ny >= _height)
                {
                    continue;
                }

                if (dist[ny][nx] != -1)
                {
                    continue;
                }

                if (map[ny][nx] != -1)
                {
                    dist[ny][nx] = dist[y][x] + 1;
                    que.Enqueue(new Tuple<int, int>( nx, ny ));
                }
            }
        }
        

        List<Tuple<int, int>> res = new List<Tuple<int, int>>();       
        if (dist[nxt.Item2][nxt.Item1] < 0)
        {
            res.Add(now);
            return res;
        }

        (int px, int py) = nxt;
        while (dist[py][px] > 0)
        {
            res.Add(new Tuple<int, int>(px, py));
            for (int i = 0; i < 4; i++)
            {
                int nx = px + dx[i];
                int ny = py + dy[i];
                if (nx < 0 || nx >= _width || ny < 0 || ny >= _height)
                {
                    continue;
                }

                if (dist[ny][nx] == dist[py][px] - 1)
                {
                    px = nx;
                    py = ny;
                }
            }
        }

        res.Reverse();

        return res;
    }

    public void Move()
    {
        if (Mathf.Abs(transform.position.x - gridMove._destinationPosition.x) < 0.2f && Mathf.Abs(transform.position.z - gridMove._destinationPosition.y) < 0.2f)
        {
            //PlayerHealthController.instance.ChangeHealth(-1);
            Destroy(gameObject);
        }

        (int x, int y) = path[0];//gridを座標に変換
        x = x - (int)_center.x;
        y = -y + (int)_center.y;

        if (Mathf.Abs(x - gridMove._destinationPosition.x) < 0.2f && Mathf.Abs(y - gridMove._destinationPosition.y) < 0.2f)
        {
            //PlayerHealthController.instance.ChangeHealth(-1);
            Destroy(gameObject);
        }

        path.RemoveAt(0);
        gameObject.transform.position = new Vector3(x, gameObject.transform.position.y, y);

    }

}
