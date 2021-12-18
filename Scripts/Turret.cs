using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    private Transform target;
    private EnemyMovement targetEnemy;

    [Header("General")]

    public float range;
    [Tooltip("use Enemy for normal type and Fly for other")]
    public string enemyTag = "Enemy";
    public float fireRate;
    public float turnSpeed;
    [Tooltip("leave empty thx")]
    public Node node = null;

    //used for laser
    [Header("laser stuff")]
    public bool useLaser = false;
    public float slowAmount;
    public LineRenderer lineRend;
    public ParticleSystem impactEffect;
    public int damageOverTime;

    //general stuff
    [Header("general stuff")]
    private float fireCountdown = 0f;
    public Transform partToRotate;
    public GameObject bulletPrefab;
    public Transform firePoint;
 

    



    // Start is called before the first frame update
    void Start()
    {
        //updates target every 0.5 seconds
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            if (useLaser)
            {
                if (lineRend.enabled)
                {
                    lineRend.enabled = false;
                    impactEffect.Stop();
                }
            }
            return;
        }

        LockOnTarget();

        if (useLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountdown <= 0)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }
        
        

    }

    void Laser()
    {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowAmount);

        if (!lineRend.enabled)
        {
            lineRend.enabled = true;
            impactEffect.Play();
        }
        lineRend.SetPosition(0, firePoint.position);
        lineRend.SetPosition(1, target.position);

        //impact effect follows target
        Vector3 dir = firePoint.position - target.position;
        impactEffect.transform.position = target.position + dir.normalized;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);

    }

    void LockOnTarget()
    {
        //making turrets turn smoother
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if(bullet != null)
        {
            bullet.Seek(target);
        }

    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        //save the sortest distance
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            //get the distance to enemy
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {

                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;

            }

        }

        //marks the enemy
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<EnemyMovement>();

        }
        else
        {
            target = null;
        }

    }

    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

    }

    private void OnMouseDown()
    {
        BuildManager.instance.SelectNode(node);
    }

}
