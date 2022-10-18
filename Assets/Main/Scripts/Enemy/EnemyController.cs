using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    private GridMove gridMove;
    public GameObject _destination;

    PlayerHealthController playerHealthController;

    public bool is_move = false;
    private float firstPosY;
    // Start is called before the first frame update
    void Start()
    {
        gridMove = player.GetComponent<GridMove>();
        playerHealthController = player.GetComponent<PlayerHealthController>();
        firstPosY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        is_move = gridMove._isMoving;
        if (transform.position.y < firstPosY - 1.0f)
        {
            player.GetComponent<CharacterController>().enabled = false;
            player.gameObject.transform.position = _destination.transform.position;
            player.GetComponent<GridMove>()._destinationPosition.x = _destination.transform.position.x;
            player.GetComponent<GridMove>()._destinationPosition.y = _destination.transform.position.z;
            player.GetComponent<CharacterController>().enabled = true;
            if (_destination.GetComponent<WarpPoint>())
            {
                _destination.GetComponent<WarpPoint>()._isWarp = false;
            }
            Destroy(gameObject);        
        }
    }

    
    void LateUpdate()
    {
        Vector3 enemyPos = transform.position;
        if (is_move && ((gridMove._destinationPosition - gridMove._currentPosition).magnitude <= 0.1f) || (gridMove._destinationPosition - gridMove._currentPosition).magnitude >= 2.0f)
        {
            Vector3 playerPos = new Vector3(gridMove._destinationPosition.x, this.transform.position.y, gridMove._destinationPosition.y);
            
            Vector3 move = playerPos - enemyPos;
            Debug.Log(playerPos);

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealthController.ChangeHealth(-1);
            Destroy(gameObject);
        }
    }

}
