using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    Vector3 mousePos;
    public Transform targetBall;
    public bool inArea;

    // Start is called before the first frame update
    void Start()
    {
        inArea = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;
        transform.position = mousePos;
    }
}
