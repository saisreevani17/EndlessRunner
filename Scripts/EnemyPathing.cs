using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] WaveConfig waveConfig;
    List<Transform> waypoints; //this list stores the coordinates(tranforms) of waypoints
    //[SerializeField] float moveSpeed = 2f; //speed at which the enemy moves
    int waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWayPoints();
        transform.position = waypoints[waypointIndex].transform.position; //starting position of enemy
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }
    private void Move()
    {
        if(waypointIndex <= waypoints.Count-1)
        {
            var targetPosition = waypoints[waypointIndex].transform.position; //target position the enemy should go
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime; //speed of the frame
            transform.position = Vector2.MoveTowards(
                transform.position, targetPosition, movementThisFrame); // move the enemy

            if(transform.position==targetPosition)
            {
                waypointIndex++; //once it reached waypoint increment waypointindex count
            }
        }
        else
        {
            Destroy(gameObject); //once all the waypoints are done, destroy the enemy
        }
    }
}
