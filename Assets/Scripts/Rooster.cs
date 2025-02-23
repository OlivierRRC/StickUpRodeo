using UnityEngine;
using System.Collections;

public class Rooster : EnemyBase
{

    public GameObject shotPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();
        agent.destination = player.transform.position;
        StartCoroutine(TargetLoop());
    }

    public override void Update()
    {
        base.Update();

        if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
        {
            if (attackCooldown <= 0)
            {
                GameObject shot = Instantiate(shotPrefab, transform.position-transform.forward+transform.up, Quaternion.identity);
                shot.GetComponent<EnemyProjectile>().damage = damage;
                shot.GetComponent<Rigidbody>().AddForce(-transform.forward * Vector3.Distance(transform.position, player.transform.position)*2, ForceMode.Impulse);
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
        yield return new WaitForSeconds(1);
        agent.destination = player.transform.position;
        StartCoroutine(TargetLoop());
    }

}
