using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiScript : MonoBehaviour
{

    public GameObject ball;
    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        pos.x = transform.position.x;
        pos.z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        pos.y = ball.transform.position.y;
        transform.position = pos;
    }
}
