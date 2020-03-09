using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContorller : MonoBehaviour
{
    private Rigidbody PlayerRb;
    private GameObject focalPoint;
    private float powerUpStrength = 15.0f;
    public float speed = 5.0f;
    public bool hasPowerup = false;
    public GameObject powerupIndicartor;
    // Start is called before the first frame update
    void Start()
    {
        PlayerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");

        PlayerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);

        powerupIndicartor.transform.position = transform.position + new Vector3(0, -0.5f, 0);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerupIndicartor.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());

        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicartor.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collection)
    {
        
        if(collection.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collection.gameObject.GetComponent<Rigidbody>();
            Vector3 awayfromPlayer = collection.gameObject.transform.position - transform.position;

            enemyRigidbody.AddForce(awayfromPlayer * powerUpStrength, ForceMode.Impulse);
            Debug.Log("Collided with:" + collection.gameObject.name + "with powerup set to" + hasPowerup);
        }


    }
}
