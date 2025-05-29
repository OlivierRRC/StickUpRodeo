using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Rat : EnemyBase
{
    private Vector2 randomizedTarget;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();
        randomizedTarget = Random.insideUnitCircle * 1;
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
        // Otherwise if the rat is within attack range...
        else if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
        {
            // Once attack cooldown reaches its end, the rat takes a swipe attack at the player.
            if (attackCooldown <= 0)
            {
                GetComponent<Animator>().SetTrigger("Attack");
                player.GetComponent<OlivierPlayerMove>().TakeDamage(damage);
                attackCooldown = 1/ attackSpeed;
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
        // If the enemy has been alerted, it starts moving towards a point within melee range of the player
        if (alerted == true)
        {
            agent.destination = player.transform.position + new Vector3(randomizedTarget.x, 0, randomizedTarget.y);
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
