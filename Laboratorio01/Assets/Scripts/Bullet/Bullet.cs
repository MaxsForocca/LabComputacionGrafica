using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 10f;
    public int damage = 50;
    public GameObject impactVFX;

    void Start()
    {
        // Autodestrucción por seguridad (evita fugas de memoria)
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (impactVFX != null && collision.contacts.Length > 0)
        {
            ContactPoint contact = collision.contacts[0];

            Quaternion rotation = Quaternion.LookRotation(contact.normal);

            GameObject vfx = Instantiate(impactVFX, contact.point, rotation);
            Destroy(vfx, 2f);
        }
        // Colisión con el enemigo, verificando si tiene componente Health
        Health health = collision.gameObject.GetComponentInParent<Health>();

        if (health != null)
        {
            health.TakeDamage(damage);
            GameManager.Instance.AddScore(1);
        }

        // Destruye la bala SIEMPRE al colisionar
        Destroy(gameObject);
    }
}
