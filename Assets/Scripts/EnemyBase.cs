using UnityEngine;
using UnityEngine.AI;
//[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBase : MonoBehaviour
{

    

    public float health = 100f;
    public float damage = 10f;
    public float attackSpeed = 1f;
    public float attackRange = 1f;

    protected float attackCooldown = 0f;
    protected GameObject player;
    protected NavMeshAgent agent;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    virtual public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    virtual public void Update()
    {

    }

    virtual public void FixedUpdate()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
