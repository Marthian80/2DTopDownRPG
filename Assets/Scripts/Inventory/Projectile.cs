using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private GameObject particleOnHitVFX;
    [SerializeField] private bool isEnemyProjectile = false;
    [SerializeField] private float projectileRange = 10f;

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

    public void SetRange(float projectileRange)
    {
        this.projectileRange = projectileRange;
    }

    public void SetSpeed(float projectileSpeed)
    {
        this.moveSpeed = projectileSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        Indestructable indestructable = collision.gameObject.GetComponent<Indestructable>();
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();

        if (!collision.isTrigger && (enemyHealth || indestructable || player))
        {            
            if ((player && isEnemyProjectile) || (enemyHealth && !isEnemyProjectile))
            {
                player?.TakeDamage(1, transform);
                ProjectileHitDetected();
            }
            else if(!collision.isTrigger && indestructable)
            {
                ProjectileHitDetected();
            }
            
        }        
    }

    private void DetectFireDistance()
    {
        if (Vector3.Distance(transform.position, startPosition) > this.projectileRange)
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
        Instantiate(particleOnHitVFX, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
