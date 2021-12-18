using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class EnemyMove : MonoBehaviour
{
    private Transform target;
    private int wavepointIndex = 0;
    private EnemyMovement enemy;
    public bool takeReroute;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<EnemyMovement>();
        if (this.gameObject.tag == "Fly")
        {
            target = Waypoints.fPoints[0];
        }
        else
        {
            if (takeReroute)
            {
                target = Waypoints.wPoints2[0];
            }
            else
            {
                target = Waypoints.wPoints[0];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // direction between 2 points
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.moveSpeed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.15f)
        {
            GetNextWaypoint();
        }
        enemy.moveSpeed = enemy.startSpeed;
    }

    void GetNextWaypoint()
    {
        if (this.gameObject.tag == "Fly")
        {
            //last waypoint in path
            if (wavepointIndex >= Waypoints.fPoints.Length - 1)
            {
                EndPath();
                return;
            }
            wavepointIndex++;
            target = Waypoints.fPoints[wavepointIndex];
        }
        else
        {
            if (takeReroute)
            {
                //last waypoint in path
                if (wavepointIndex >= Waypoints.wPoints2.Length - 1)
                {
                    EndPath();
                    return;
                }
                wavepointIndex++;
                target = Waypoints.wPoints2[wavepointIndex];
            }
            else
            {
                //last waypoint in path
                if (wavepointIndex >= Waypoints.wPoints.Length - 1)
                {
                    EndPath();
                    return;
                }
                wavepointIndex++;
                target = Waypoints.wPoints[wavepointIndex];
            }
        }
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.enemiesAlive--;
        Destroy(gameObject);
    }
}
