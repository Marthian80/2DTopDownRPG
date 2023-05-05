using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private GameObject particleOnHitVFX;

    private WeaponInfo weaponInfo;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        MoveProjectile();
        DetectFireDistance();
    }

    public void SetWeaponInfo(WeaponInfo weaponInfo)
    {
        this.weaponInfo = weaponInfo;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        Indestructable indestructable = collision.gameObject.GetComponent<Indestructable>();

        if (!collision.isTrigger && (enemyHealth || indestructable))
        {            
            ProjectileHitDetected();
        }        
    }

    private void DetectFireDistance()
    {
        if (Vector3.Distance(transform.position, startPosition) > this.weaponInfo.WeaponRange)
        {
            Destroy(gameObject);
        }
    }

    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    private void ProjectileHitDetected()
    {
        Instantiate(particleOnHitVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
