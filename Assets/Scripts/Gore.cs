using UnityEngine;

public class Gore : MonoBehaviour
{
    public Rigidbody body;
    public GameObject particles;
    public AudioSource audioSource;
    public AudioClip audioClip;
    public float hitForce = 10;

    public int healthMax = 3;

    private int hits = 0;

    public void BoomBoom()
    {
        hits++;

        if(hits >= healthMax)
        {
            //fireworkds
            var obj = Instantiate(particles);
            obj.transform.position = transform.position;

            Destroy(gameObject);
        }
        else
        {
            body.AddForce(Vector3.up * hitForce, ForceMode.Impulse);
            audioSource.PlayOneShot(audioClip);
        }

    }
}
