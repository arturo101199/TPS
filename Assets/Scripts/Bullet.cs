using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject decalPrefab;

    float speed = 50f;
    float timeToDestroy = 3f;

    public Vector3 targetPos { get; set; }
    public bool hit { get; set; }

    void OnEnable()
    {
        Destroy(gameObject, timeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        if(!hit && Vector3.Distance(transform.position, targetPos) <= 0.01f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.GetContact(0);
        Instantiate(decalPrefab, contact.point + contact.normal * 0.0001f, Quaternion.LookRotation(contact.normal));
        Destroy(gameObject);
    }
}
