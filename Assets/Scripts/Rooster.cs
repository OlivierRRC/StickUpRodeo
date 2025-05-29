using UnityEngine;
using System.Collections;

public class Rooster : EnemyBase
{

    public GameObject shotPrefab;


    public override void Start()
    {
        base.Start();
        // Begin the targetting loop
        StartCoroutine(TargetLoop());
    }

    public override void Update()
    {
        base.Update();

        // If the enemy isn't alerted, skip the rest of the function.
        if (!alerted)
        {
            return;
        }
        // Otherwise if the rooster is within attack range
        else if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
        {
            // Move a bit closer than the edge of the attack range so the enemy can still attack if the player strafes.
            if (Vector3.Distance(transform.position, player.transform.position) < attackRange - 3)
            {
                agent.destination = transform.position;
            }
                
            // Once attack cooldown reaches its end, fire an egg projectile at the Player.
            if (attackCooldown <= 0)
            {
                GameObject shot = Instantiate(shotPrefab, transform.position-transform.forward+transform.up, Quaternion.identity);
                shot.GetComponent<EnemyProjectile>().damage = damage;
                shot.GetComponent<Rigidbody>().AddForce((player.transform.position - transform.position) * 2, ForceMode.Impulse);
                attackCooldown = 1 / attackSpeed;
            }
            else
            {
                attackCooldown -= Time.deltaTime;
            }
        }
        else
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    IEnumerator TargetLoop()
    {
        // This loop runs once a second, updating NavMesh destination accordingly.
        yield return new WaitForSeconds(1);
        // If the enemy has been alerted, it starts moving towards the player across the NavMesh
        if (alerted == true)
        {
            agent.destination = player.transform.position;
        }
        // Otherwise, it just sits still, patiently waiting.
        else
        {
            agent.destination = transform.position;
        }
        // Reset the loop
        StartCoroutine(TargetLoop());
    }

}
