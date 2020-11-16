using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ballmove : MonoBehaviour
{

    Rigidbody2D rb;
    float xSpeed;
    float ySpeed;
    public float maxYSpeed;
    public GameObject player;
    public GameObject ai;
    public enum type { speed, size, fire };
    bool hasPowerup = false;
    float magnitude;
    public int playerScore;
    public Text pScoreText;
    public int aiScore;
    public Text aScoreText;
    public Text puText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        xSpeed = 10.0f;
        ySpeed = 5.0f;
        magnitude = 1f;
        playerScore = 0;
        aiScore = 0;
        puText.text = null;
    }

    void Update()
    {
        if(Mathf.Abs(ySpeed) > maxYSpeed*magnitude)
        {
            if(ySpeed > 0)
            {
                ySpeed = maxYSpeed*magnitude;
            } else
            {
                ySpeed = -maxYSpeed*magnitude;
            }
        }
        rb.velocity = new Vector2(xSpeed * magnitude, ySpeed * magnitude);
        pScoreText.text = ""+playerScore;
        aScoreText.text = "" + aiScore;
    }

    void powerUp(type powerType)
    {
        if(powerType == type.fire)
        {
            Debug.Log("FIRE");
            StartCoroutine(fire());
        }
        if(powerType == type.size)
        {
            Debug.Log("SIZE");
            StartCoroutine(resize());
        }
        if (powerType == type.speed)
        {
            Debug.Log("SPEED");
            StartCoroutine(speed());
        }
    }

    GameObject GetClosestPowerup (string str)
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag(str);

        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach(GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if(curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("wall"))
        {
            xSpeed *= -1;
            if (col.name == "leftEdge")
            {
                Debug.Log("AI won");
                //Destroy(gameObject);
                aiScore++;
                StartCoroutine(ResetBall());
            }
            if (col.name == "rightEdge")
            {
                Debug.Log("Player won");
                //Destroy(gameObject);
                playerScore++;
                StartCoroutine(ResetBall());
            }
        }
        if(col.CompareTag("flat"))
        {
            ySpeed *= -1;
        }
        if (xSpeed > 0 && col.CompareTag("ai"))
        {
            xSpeed *= -1;
        }
        if (col.CompareTag("powerup"))
        {
            if (!hasPowerup)
            {
                hasPowerup = true;
                GameObject powerToken = GetClosestPowerup("powerup");
                type _powerType = (type)powerToken.GetComponent<powerUps>().powerType;
                powerUp(_powerType);
                Destroy(powerToken);
            }
        }
        if(xSpeed < 0 && col.CompareTag("Player"))
        {
            
            if (col.name == "Top")
            {
                ySpeed += 5;
                xSpeed *= -1;
            }
            if(col.name == "Center")
            {
                xSpeed *= -1;
            }
            if(col.name == "Bottom")
            {
                ySpeed -= 5;
                xSpeed *= -1;
            }
        }
    }

    IEnumerator ResetBall ()
    {
        transform.position = new Vector3(0, 0, -5f);
        magnitude = 0;
        yield return new WaitForSeconds(5.0f);
        magnitude = 1;
        hasPowerup = false;
    }

    IEnumerator resize()
    {
        transform.localScale *= .5f;
        puText.text = "TINY";
        yield return new WaitForSeconds(5.0f);
        transform.localScale *= 2f;
        puText.text = null;
        hasPowerup = false;
    }

    IEnumerator speed()
    {
        magnitude = 2f;
        puText.text = "SPEED";
        yield return new WaitForSeconds(5.0f);
        magnitude = 1f;
        puText.text = null;
        hasPowerup = false;
    }

    IEnumerator fire()
    {
        aiScript _aiScript = ai.GetComponent<aiScript>();
        _aiScript.enabled = false;
        puText.text = "FIRE";
        yield return new WaitForSeconds(3.0f);
        _aiScript.enabled = true;
        puText.text = null;
        hasPowerup = false;
    }
}
