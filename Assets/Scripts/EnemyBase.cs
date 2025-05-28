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

    public GameObject gore;

    protected float attackCooldown = 0f;
    protected GameObject player;
    protected NavMeshAgent agent;


    public float sightRange = 1f;
    public bool alerted = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    virtual public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    virtual public void Update()
    {
        if (alerted)
        {
            return;
        }
        else if (Vector3.Distance(transform.position, player.transform.position) < sightRange)
        {
            alerted = true;
        }
    }

    virtual public void FixedUpdate()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        GetComponent<Rigidbody>().AddForce(player.transform.forward * damage/2, ForceMode.Impulse);
        GetComponentInChildren<VoiceLineManager>().playDamageVoiceLine();

        if (health <= 0)
        {
            player.GetComponentInChildren<VoiceLineManager>().playKillVoiceLine(GetComponentInChildren<VoiceLineManager>().killVoiceLines);
            var obj = Instantiate(gore);
            obj.transform.position = transform.position + Vector3.up;
            Destroy(gameObject);
        }
    }

}
