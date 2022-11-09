using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class playerFollow : MonoBehaviour
{
    public Transform player;
    public float speed = 4f;


    private Rigidbody rig;
    
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 towards = new Vector3(player.rotation.x, player.rotation.y, player.rotation.z-90.0f);
        Vector3 pos = Vector3.MoveTowards(transform.position, player.position, speed * Time.fixedDeltaTime);
        rig.MovePosition(pos);
        transform.LookAt(towards);
    }
}
