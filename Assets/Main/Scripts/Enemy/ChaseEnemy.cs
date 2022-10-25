using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class ChaseEnemy : MonoBehaviour
{
    public GameObject player;
    private GridMove gridMove;


    public bool is_move = false;
    // Start is called before the first frame update
    void Start()
    {
        gridMove = player.GetComponent<GridMove>();
    }

    // Update is called once per frame
    void Update()
    {
        is_move = gridMove._isMoving;
    }

    
    void LateUpdate()
    {
        Vector3 enemyPos = transform.position;
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

        transform.position = enemyPos;
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
