using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public GameObject powerUpIndicator;
    public bool hasPowerup;

    private float powerupStrength = 15.0f;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);

        powerUpIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0); // Cuando agarras el powerUp que el indicator siga al Player
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpConuntdownRoutine());
            powerUpIndicator.gameObject.SetActive(true);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if  (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            
            //Debug.Log("Collide with: " + collision.gameObject.name + "with a power up set to" + hasPowerup);

            enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }

    }
    IEnumerator PowerUpConuntdownRoutine()
    {
        yield return new WaitForSeconds(5); //Updatea una funcion fuera de Update()
        hasPowerup = false;
        powerUpIndicator.gameObject.SetActive(false);
    }
}
