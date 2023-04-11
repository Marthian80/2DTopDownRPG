using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public bool KnockbackActive { get; private set; }

    [SerializeField] private float knockbackTime = 0.3f;

    private Rigidbody2D rb;    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    public void GetKnockedBack(Transform damageSource, float knockbackThrust)
    {
        KnockbackActive = true;
        Vector2 difference = (transform.position - damageSource.position).normalized * knockbackThrust * rb.mass;
        rb.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(KnockRoutine());
    }

    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(knockbackTime);
        rb.velocity = Vector2.zero;
        KnockbackActive = false;
    }
}
