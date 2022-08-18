using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ObjectMaker : MonoBehaviour
{
    public GameObject thing2bDuplicated;
    float distance;
    Vector3 StartPos;
    Transform player;
    bool guySpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        StartPos = transform.position;
    }
    void LateUpdate()
    {
        player = GameObject.Find("Player(Clone)").GetComponent<Transform>();
        //thing2bDuplicated = GameObject.Find("RandomEnemy");
        //thing2bDuplicated = GameObject.Find("Slime_01_King(Clone)");
        //Debug.Log("found the player!");
        distance = Vector3.Distance(transform.position, player.position);
        if (distance <= 8f)
            {
                if (guySpawned == false)
                {
                    guySpawned = true;
                    //do the duplication.
                    doThatThing();
                    //guySpawned = true;
                    Debug.Log("Method called once!!!");
                    return;
                }
            }
        
    }

    // Update is called once per frame
    void doThatThing()
    {
        /* create a copy of this object at the specified spawn point with no rotation */
        PhotonNetwork.Instantiate(thing2bDuplicated.name, transform.position, Quaternion.identity);
        Debug.Log("One Enemy Instantiated!");
        //Object.Instantiate(thing2bDuplicated, transform.position, Quaternion.identity);
    }
}
