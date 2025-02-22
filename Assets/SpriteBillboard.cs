using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{

    private GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float rotAngle = Mathf.Atan2(transform.position.z - player.transform.position.z, transform.position.x - player.transform.position.x);
        transform.rotation = Quaternion.Inverse( Quaternion.Euler(0, rotAngle*Mathf.Rad2Deg - 90, 0));  
        print(rotAngle);
    }
}
