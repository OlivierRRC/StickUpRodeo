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
    }

    public override void Update()
    {
        base.Update();
        agent.destination = player.transform.position + new Vector3(randomizedTarget.x, 0, randomizedTarget.y);

        if(Vector3.Distance(transform.position, player.transform.position) < attackRange)
        {
            if (attackCooldown <= 0)
            {
                print("Attack");
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

}
