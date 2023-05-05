using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBolt : MonoBehaviour
{
    [SerializeField] private float boltGrowTime = 2f;

    private bool isGrowing = true;
    private float boltRange;
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D boltCollider;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boltCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        BoltFaceMouse();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Indestructable>() && !collision.isTrigger)
        {
            isGrowing = false;
        }
    }

    public void UpdateBoltRange(float boltRange)
    {
        this.boltRange = boltRange;
        StartCoroutine(IncreaseBoltLengthRoutine());
    }

    private IEnumerator IncreaseBoltLengthRoutine()
    {
        float timePassed = 0f;

        while(spriteRenderer.size.x < boltRange && isGrowing)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / boltGrowTime;

            spriteRenderer.size = new Vector2(Mathf.Lerp(1f, boltRange, linearT), 1f);
            boltCollider.size = new Vector2(Mathf.Lerp(0.89f, boltRange, linearT), boltCollider.size.y);
            boltCollider.offset = new Vector2(Mathf.Lerp(0f, boltRange, linearT / 2), boltCollider.offset.y);

            yield return null;
        }

        StartCoroutine(GetComponent<SpriteFade>().SlowFadeRoutine());
    }

    private void BoltFaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = transform.position - mousePosition;

        transform.right = -direction;
    }
}
