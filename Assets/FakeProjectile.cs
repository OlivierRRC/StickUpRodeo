using UnityEngine;

public class FakeProjectile : MonoBehaviour
{

    Vector3 startPosition;
    public Vector3 endPosition;
    float lerpTime = 0;
    public float speed = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
        Destroy(gameObject, speed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(startPosition, endPosition, lerpTime/speed);
        lerpTime += Time.deltaTime;
    }
}
