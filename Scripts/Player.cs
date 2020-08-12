using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //configuration parameters
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f; //factor to multiple speed with
    [SerializeField] float padding = 1f; //padding around the spaceship
    [SerializeField] int health = 200;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab; 
    [SerializeField] float projectileSpeed = 10f; //velocity of bullet
    [SerializeField] float projectileFiringPeriod = 0.1f; //waiting time between two bullets

    Coroutine firingCoroutine; // for firing bullets. 

    float xMin; //min val the spcaship can be moved on x axis so it dosent go out of screen
    float xMax; //max val the spcaship can be moved on x axis so it dosent go out of screen
    float yMin;
    float yMax;

    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.75f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
        
    }

    
    // Update is called once per frame
    void Update()
    {
        Move(); //to move the spaceship
        Fire(); //to fire bullets by spaceship
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);

    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.getDamage(); //reducing health in case of damage
        damageDealer.Hit();
        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
    }

    public int GetHealth()
    {
        return health;
    }
    private void Fire()
    {
        if (Input.GetButtonDown("Fire1")) //if space bar is pressed
        {
            firingCoroutine = StartCoroutine(FireContinuously()); //calling coroutine to fire bullets continuously as long as space bar is pressed
            
        }
        if(Input.GetButtonUp("Fire1")) //as soon as space bar is stopped pressing, stop firing bullets
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously() //coroutine to fire bullets continuously when space bar is pressed down
    {
        while (true)
        {
            //instantiate laser prefeb,with pos and rotation.
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;

            //velocity of bullet
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
            yield return new WaitForSeconds(projectileFiringPeriod); //waiting period between two bullets
        }
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main; //gameCamera has main Camera now. you can access it using the word gameCamera.
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding; //setting xmin as 0 + padding
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding; //setting xmax as 1 - padding
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding; //setting ymin as 0 + padding
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding; //setting ymax as 1 - padding
    }
    private void Move()
    {
        //for movement of spaceship on horizontal axis
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed; //get the amount by which you want to move
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax); //change the x pos of spaceship by deltax times
        //Mathx.Clamp is used to change the position of spaceship only if it is within the screen i.e xmin and xmax.

        //for movement of spaceship on vertical axis
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed; //deltaTime is used so that movement is frame independent.
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos); //fix the final pos 
                                                            //to transformed val of x and previous val of y.




    }
}
