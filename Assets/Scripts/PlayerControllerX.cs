using System.Collections;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody _playerRb;
    private const float Speed = 500;
    private GameObject _focalPoint;

    public bool hasPowerUp;
    public GameObject powerUpIndicator;
    public int powerUpDuration = 5;

    public bool hasSpeedBoost;
    public int speedBoostDuration = 5;
    public ParticleSystem smokeParticlePrefab;

    private const float NormalStrength = 10; // how hard to hit enemy without powerUp
    private const float PowerUpStrength = 25; // how hard to hit enemy with powerUp

    private void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        _focalPoint = GameObject.Find("Focal Point");
        smokeParticlePrefab = _focalPoint.GetComponentInChildren<ParticleSystem>();
    }

    private void FixedUpdate()
    {
        // Move the player in the direction that our camera is pointing in
        var forwardInput = Input.GetAxis("Vertical");
        var horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.Space) && hasSpeedBoost == false)
        {
            // Give player a speed boost
            _playerRb.AddForce(_focalPoint.transform.forward * speedBoostDuration, ForceMode.Impulse);

            // Start particle effect
            smokeParticlePrefab.Play();

            // The smoke particle effect follows the player's position
            smokeParticlePrefab.transform.position = transform.position + new Vector3(0, -0.5f, 0);

            hasSpeedBoost = true;
            StartCoroutine(SpeedBoostCooldown());
        }

        _playerRb.AddForce(_focalPoint.transform.forward * (forwardInput * Speed * Time.deltaTime));
        _playerRb.AddForce(_focalPoint.transform.right * (horizontalInput * Speed * Time.deltaTime));

        // The powerUpIndicator follows the player's position
        powerUpIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        powerUpIndicator.transform.Rotate(new Vector3(0, 2, 0));

    }

    // If Player collides with powerUp, activate powerUp
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            powerUpIndicator.SetActive(true);

            Destroy(other.gameObject);
            StartCoroutine(PowerUpCooldown());
        }
    }

    // Coroutine to count down powerUp duration
    private IEnumerator PowerUpCooldown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerUp = false;
        powerUpIndicator.SetActive(false);
    }

    // Coroutine to count down powerUp duration
    private IEnumerator SpeedBoostCooldown()
    {
        yield return new WaitForSeconds(2);
        smokeParticlePrefab.Stop();
        hasSpeedBoost = false;
    }

    // If Player collides with enemy
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            var awayFromPlayer = collision.gameObject.transform.position - transform.position ;
           
            if (hasPowerUp) // if have powerUp hit enemy with powerUp force
            {
                enemyRigidbody.AddForce(awayFromPlayer * PowerUpStrength, ForceMode.Impulse);
            }
            else // if no powerUp, hit enemy with normal strength
            {
                enemyRigidbody.AddForce(awayFromPlayer * NormalStrength, ForceMode.Impulse);
            }


        }
    }



}
