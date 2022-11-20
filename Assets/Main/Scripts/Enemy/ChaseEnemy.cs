using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using System;
using System.Linq;

public class ChaseEnemy : MonoBehaviour
{
    public GameObject player;
    private GridMove gridMove;
    private List<List<int>> map;
    private int _height;
    private int _width;
    private Vector2 _center;

    public bool is_move = false;
    private Vector2 player_pos;
    private Vector2 enemy_pos;

    private List<int> dh = new List<int>() { 0, 1, 0, -1 };
    private List<int> dw = new List<int>() { 1, 0, -1, 0 };

    // Start is called before the first frame update
    void Start()
    {
        gridMove = player.GetComponent<GridMove>();
        map = GameObject.Find("GameManager").GetComponent<GenerateMap>().map_data;
        _height = map.Count;
        _width = map[0].Count;
        _center = new Vector2((int)(_width / 2), (int)(_height / 2));
    }

    // Update is called once per frame
    void Update()
    {
        enemy_pos = new Vector2((int)transform.position.x, (int)transform.position.z);
        if (gridMove._isMoving)
        {
            is_move = true;
            player_pos = gridMove._currentPosition;
        }

    }

    
    void LateUpdate()
    {
        if (is_move)
        {
            List<int> enemy_direction = bfs();
            (int direction_w, int direction_h) = (enemy_direction[0], enemy_direction[1]);
            transform.position = new Vector3(direction_w, transform.position.y, direction_h);

            is_move = false;
        }
        /*Vector3 enemyPos = transform.position;
        if (is_move && ((gridMove._destinationPosition - gridMove._currentPosition).magnitude <= 0.1f) || (gridMove._destinationPosition - gridMove._currentPosition).magnitude >= 2.0f)
        {
            Vector3 playerPos = new Vector3(gridMove._destinationPosition.x, this.transform.position.y, gridMove._destinationPosition.y);
            
            Vector3 move = playerPos - enemyPos;

            if (move.x != 0f && move.z != 0f)
            {
                int moveIndex = Random.Range(0, 2);
                Debug.Log(moveIndex);
                if (moveIndex == 0)
                {
                    enemyPos.x += Mathf.Abs(move.x) / move.x;
                } else
                {
                    enemyPos.z += Mathf.Abs(move.z) / move.z;
                }
            } else if (move.x != 0f)
            {
                enemyPos.x += Mathf.Abs(move.x) / move.x;
            } else if (move.z != 0f)
            {
                enemyPos.z += Mathf.Abs(move.z) / move.z;
            }
        }

        transform.position = enemyPos;*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealthController.instance.ChangeHealth(-1);
            Destroy(gameObject);
        }
    }


    private List<int> bfs()
    {
        (int enemy_w, int enemy_h) = transformToGrid(enemy_pos);
        (int player_w, int player_h) = transformToGrid(player_pos);

        List<List<int>> dist = Enumerable.Range(0, 10).Select(x => Enumerable.Range(0, 10).Select(y => -1).ToList()).ToList();
        Queue<List<int>> que = new Queue<List<int>>();
        dist[enemy_h][enemy_w] = 0;
        que.Enqueue(new List<int>{ player_h, player_w});

        while (Convert.ToBoolean(que.Count))
        {
            List<int> d = new List<int> { que.Peek()[0], que.Peek()[1] };
            que.Dequeue();

            for (int i = 0; i < 4; i++)
            {
                int nh = d[0] + dh[i];
                int nw = d[1] + dw[i];
                if (nh < 0 || nh >= _height || nw < 0 || nw >= _width)
                {
                    continue;
                }
                if (dist[nh][nw] != -1)
                {
                    continue;
                }
                if (map[nh][nw] != -1)
                {
                    dist[nh][nw] = dist[d[0]][d[1]] + 1;
                    que.Enqueue(new List<int> { nh, nw });
                }
            }
        }

        if (dist[player_h][player_w] == -1)
        {
            return new List<int> { enemy_w, enemy_h };
        }

        List<List<int>> path = new List<List<int>>();
        int now_h = player_h;
        int now_w = player_w;
        while (now_h != enemy_h && now_w != enemy_w)
        {
            path.Add(new List<int> { now_h, now_w });
            for (int i = 0; i < 4; i++)
            {
                int next_h = now_h + dh[i];
                int next_w = now_w + dh[i];

                if (next_h < 0 || next_h >= _height || next_w < 0 || next_w >= _width)
                {
                    continue;
                }
                
                if (dist[next_h][next_w] + 1 == dist[now_h][now_w])
                {
                    now_h = next_h;
                    now_w = next_w;
                    break;
                }
            }
        }

        return path.Last();
    }

    private Tuple<int, int> transformToGrid(Vector2 trans)
    {
        return new Tuple<int, int>(Mathf.RoundToInt(trans.x) + (int)_center.x, Mathf.RoundToInt(trans.y) + (int)_center.y);
    }

}
