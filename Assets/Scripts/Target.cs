using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -2;

    public ParticleSystem explosionParticle; // Particle system to play on target destruction
    public int pointValue; // Point value for the target, can be set in the Inspector


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse); // Add random torque to the target
        // Set a random position for the target within specified bounds

        transform.position = RandomSpawnPos(); // Randomize the target's position within a range
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnMouseDown()
    {
        if(gameManager.isGameActive) // Check if the game is active before allowing target interaction
        {
            Destroy(gameObject); // Destroy the target when clicked
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation); // Play the explosion particle effect at the target's position
            gameManager.UpdateScore(pointValue); // Update the score by the target's point value when the target is clicked
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject); // Destroy the target when it goes out of bounds
        if(!gameObject.CompareTag("Bad")) 
        {
            gameManager.GameOver(); // Call the GameOver method in the GameManager script
        }
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }
    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }
    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
}
