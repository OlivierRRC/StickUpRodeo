
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.LookDev;
//[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBase : MonoBehaviour
{

    
    // Enemy Stats
    public float health = 100f;
    public float damage = 10f;
    public float attackSpeed = 1f;
    public float attackRange = 1f;
    public float sightRange = 1f;
    protected float attackCooldown = 0f;

    // Other things all enemy scripts need access to
    public GameObject gore;
    protected GameObject player;
    protected NavMeshAgent agent;
    // This one is public so that child scripts can access it easily
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


    private void OnDrawGizmosSelected()
    {
        // Draw Gizmos to visualize attack range and sight range in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    public void TakeDamage(float damage)
    {
        // Take Damage and apply a bit of force to the target, then say a damage voice line.
        health -= damage;
        GetComponent<Rigidbody>().AddForce(player.transform.forward * damage/4, ForceMode.Impulse);
        GetComponentInChildren<VoiceLineManager>().playDamageVoiceLine();

        // If taking damage kills the enemy...
        if (health <= 0)
        {
            // Get the player to say a kill line
            player.GetComponentInChildren<VoiceLineManager>().playKillVoiceLine(GetComponentInChildren<VoiceLineManager>().killVoiceLines);
            // Then spawn a gore pile
            var obj = Instantiate(gore);
            obj.transform.position = transform.position + Vector3.up;
            // And finally destroy the enemy object
            Destroy(gameObject);
        }
    }

}
