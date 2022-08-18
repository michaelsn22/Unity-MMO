using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PrimitiveAi : MonoBehaviour
{
    //public GameObject enemySlimePrefab;

    Transform player;
    [SerializeField]
    float enemySpeed, distance;
    Vector3 StartPos;

    public PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        StartPos = transform.position;
        view = GetComponent<PhotonView>();
    }
    
    void LateUpdate()
    {
        if (view.IsMine)
        {
            player = GameObject.Find("Player(Clone)").GetComponent<Transform>();
            distance = Vector3.Distance(transform.position, player.position);
            if (distance <= 8f)
                {
                    //chase
                    chase();
                }
            if (distance > 8f)
                {
                    //go back to spawn/home area
                    goHome();
                }
        }
        
    }


/*
    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.position);
        if (distance <= 8f)
        {
            //chase
            chase();
        }
        if (distance > 8f)
        {
            //go back to spawn/home area
            goHome();
        }
    }
*/
    void chase()
    {
        transform.LookAt(player);
        transform.Translate(0,0, enemySpeed * Time.deltaTime);
        if (distance < 2f)
        {
            //attack
            enemySpeed = 0f;
            //Debug.Log("Enemy should play attack here!");
        }
        else
        {
            enemySpeed = 8f;
        }
    }

    void goHome()
    {
        transform.LookAt(StartPos);
        //cant use translate here gives issues with them going past the area.
        transform.position = Vector3.Lerp(transform.position, StartPos, 0.002f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distance);
    }

    public void StopMovement(int active)
    {
        if (active == 0)
        {
            transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }             
    }
}
