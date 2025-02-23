using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    public float damage = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<OlivierPlayerMove>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
