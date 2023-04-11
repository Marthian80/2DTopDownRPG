using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject slashAnimation;
    [SerializeField] private Transform slashAnimationStartPosition;
    [SerializeField] private Transform weaponCollider;
    [SerializeField] private float cooldownTime = 0.5f;

    private PlayerInput playerInput;
    private Animator myAnimator;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;
    private bool attackButtonDown, isAttacking = false;

    private GameObject activeSlashAnimation;

    private void Awake()
    {
        playerInput = new PlayerInput();
        myAnimator = GetComponent<Animator>();
        playerController = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void Start()
    {
        playerInput.Combat.Attack.started += _ => StartAttacking();
        playerInput.Combat.Attack.canceled += _ => StopAttacking();
    }

    private void Update()
    {
        MouseFollowWithOffset();
        Attack();
    }

    public void SwingUpFlipAnimEvent()
    {
        activeSlashAnimation.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (playerController.FacingLeft)
        {
            activeSlashAnimation.GetComponent<SpriteRenderer>().flipX = true;  
        }
    }

    public void SwingDownFlipAnimEvent()
    {
        activeSlashAnimation.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (playerController.FacingLeft)
        {
            activeSlashAnimation.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void DoneAttackingAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    private void StartAttacking()
    {
        attackButtonDown = true;
    }

    private void StopAttacking()
    {
        attackButtonDown = false;
    }

    private void Attack()
    {
        if(attackButtonDown && !isAttacking) 
        {
            isAttacking = true;
            myAnimator.SetTrigger("Attack");
            weaponCollider.gameObject.SetActive(true);

            activeSlashAnimation = Instantiate(slashAnimation, slashAnimationStartPosition.position, Quaternion.identity);
            activeSlashAnimation.transform.parent = this.transform.parent;
            StartCoroutine(AttackCDRoutine());
        }
    }

    private IEnumerator AttackCDRoutine()
    {
        yield return new WaitForSeconds(cooldownTime);
        isAttacking = false;
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerSceenPos = Camera.main.WorldToScreenPoint(playerController.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerSceenPos.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, angle);
        }
        else 
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
