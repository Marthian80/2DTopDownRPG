using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThrust = 10f;
    [SerializeField] private float damageRecoveryTime = 0.5f;

    private int currentHealt;
    private bool canTakeDamage;

    private Knockback knockback;
    private Flash flash;

    private void Awake()
    {
        knockback = GetComponent<Knockback>();
        flash = GetComponent<Flash>();
    }

    private void Start()
    {
        currentHealt = maxHealth;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();

        if(enemy && canTakeDamage)
        {
            TakeDamage(1);
            knockback.GetKnockedBack(enemy.transform, knockBackThrust);
            StartCoroutine(flash.FlashRoutine());
        }
    }

    private void TakeDamage(int damage)
    {
        canTakeDamage = false;
        currentHealt -= damage;
        StartCoroutine(DamageRecoveryRoutine());
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }
}
