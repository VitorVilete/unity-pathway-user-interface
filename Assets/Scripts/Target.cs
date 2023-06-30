using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private Collider targetCollider;
    private float minSpeed = 12, maxSpeed = 16, maxTorque = 10;
    private float xRange = 4, ySpawnPos = -6;
    private GameManager gameManager;
    public int pointValue;
    public ParticleSystem explosionParticle;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        targetRb = GetComponent<Rigidbody>();
        targetCollider = targetRb.GetComponent<Collider>();
        targetCollider.isTrigger = true;
        targetRb.AddForce(randomForce(), ForceMode.Impulse);
        targetRb.AddTorque(randomTorque(),randomTorque(),randomTorque(), ForceMode.Impulse);

        transform.position = randomSpawnPos();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            gameManager.UpdateScore(pointValue);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            Destroy(gameObject);
        }
    }

    //Destroys object when it passes the Sensor object
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CollisionSensor"))
        {
            targetCollider.isTrigger = false;
        }
        else if(other.CompareTag("DestroySensor")) 
        { 
            Destroy(gameObject);
            if (!gameObject.CompareTag("Bad"))
            {
                gameManager.GameOver();
            }
        }
    }

    Vector3 randomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }
    Vector3 randomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

    float randomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

}
