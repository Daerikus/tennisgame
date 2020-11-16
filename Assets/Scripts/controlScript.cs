using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlScript : MonoBehaviour
{

    public float powerupSpawnTime;
    public GameObject powerup;
    public float xRange, yRange;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnPowerup(powerupSpawnTime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateNewPowerup()
    {
        float X = Random.Range(-xRange, xRange);
        float Y = Random.Range(-yRange, yRange);
        Instantiate(powerup, new Vector3(X, Y, -5f), Quaternion.identity);
    }

    IEnumerator spawnPowerup(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            CreateNewPowerup();
        }
        //yield return null;
    }
}
