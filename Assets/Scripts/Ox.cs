using UnityEngine;
using System.Collections;

public class Ox : EnemyBase
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();
        StartCoroutine(TargetLoop());
    }

    public override void Update()
    {
        base.Update();
        agent.destination = player.transform.position;

        if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
        {
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
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<OlivierPlayerMove>().TakeDamage(damage);
            GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
        }
    }

    IEnumerator TargetLoop()
    {
        yield return new WaitForSeconds(1);
        agent.destination = player.transform.position;
        StartCoroutine(TargetLoop());
    }

}
