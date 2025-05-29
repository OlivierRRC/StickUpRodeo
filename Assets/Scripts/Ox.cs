using UnityEngine;
using System.Collections;

public class Ox : EnemyBase
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();
        // Start the targetting Loop
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
        // Otherwise if the Ox is within attack range
        else if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
        {
            // Once attack cooldown reaches its end, the Ox charges at the player extremely fast.
            if (attackCooldown <= 0)
            {
                GetComponent<Rigidbody>().AddForce(-transform.forward * 1000);
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

    private void OnCollisionEnter(Collision collision)
    {
        // If the Ox collides with the player, apply damage and cancel the Ox's attack force.
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<OlivierPlayerMove>().TakeDamage(damage);
            GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
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
