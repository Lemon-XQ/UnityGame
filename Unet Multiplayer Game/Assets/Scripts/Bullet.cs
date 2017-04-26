using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public void OnCollisionEnter(Collision collision)
    {
        GameObject hit = collision.gameObject;
        hit.GetComponent<Health>().TakeDamage(10);
        Destroy(this.gameObject);
    }
}
