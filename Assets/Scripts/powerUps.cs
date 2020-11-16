using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUps : MonoBehaviour
{

    public enum type { speed, size, fire };
    public Sprite speedSprite;
    public Sprite sizeSprite;
    public Sprite fireSprite;
    public type powerType;

    void Awake()
    {
        int typeNr = (int)Random.Range(0, 3);
        powerType = (type)typeNr;
        if(powerType == type.speed)
        {
            GetComponent<SpriteRenderer>().sprite = speedSprite;
        }
        if(powerType == type.size)
        {
            GetComponent<SpriteRenderer>().sprite = sizeSprite;
        }
        if(powerType == type.fire)
        {
            GetComponent<SpriteRenderer>().sprite = fireSprite;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
