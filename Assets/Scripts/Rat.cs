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
        agent.destination = player.transform.position + new Vector3(randomizedTarget.x, 0, randomizedTarget.y);
        StartCoroutine(TargetLoop());
    }

    public override void Update()
    {
        base.Update();

        if(Vector3.Distance(transform.position, player.transform.position) < attackRange)
        {
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
        yield return new WaitForSeconds(1);
        agent.destination = player.transform.position + new Vector3(randomizedTarget.x, 0, randomizedTarget.y);
        StartCoroutine(TargetLoop());
    }

}
