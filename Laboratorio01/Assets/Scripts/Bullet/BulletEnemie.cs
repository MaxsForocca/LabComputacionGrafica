using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemie : MonoBehaviour
{
    public float lifeTime = 10f;
    public int damage = 10;
    public GameObject impactVFX;
    void Start()
    {
        // Seguridad: destruir la bala si no colisiona con nada
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // VFX de impacto
        if (impactVFX != null && collision.contacts.Length > 0)
        {
            ContactPoint contact = collision.contacts[0];

            Quaternion rotation = Quaternion.LookRotation(contact.normal);

            GameObject vfx = Instantiate(impactVFX, contact.point, rotation);
            Destroy(vfx, 2f);
        }
        // Si colisiona con el Player
        Health health = collision.gameObject.GetComponentInParent<Health>();

        if (health != null)
        {
            health.TakeDamage(damage);
        }

        // Destruir la bala SIEMPRE
        Destroy(gameObject);
    }
}
