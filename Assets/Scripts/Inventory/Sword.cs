using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject slashAnimation;    
    [SerializeField] private WeaponInfo weaponInfo;
        
    private Animator myAnimator;
    private Transform slashAnimationStartPosition;
    private Transform weaponCollider;
    private GameObject activeSlashAnimation;

    readonly int ATTACK_HASH = Animator.StringToHash("Attack");

    private void Awake()
    {        
        myAnimator = GetComponent<Animator>();                
    }

    public void Start()
    {
        weaponCollider = PlayerController.Instance.GetWeaponCollider();
        slashAnimationStartPosition = PlayerController.Instance.GetWeaponAnimationStartPoint();
    }

    private void Update()
    {
        MouseFollowWithOffset();        
    }

    public void SwingUpFlipAnimEvent()
    {
        activeSlashAnimation.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (PlayerController.Instance.FacingLeft)
        {
            activeSlashAnimation.GetComponent<SpriteRenderer>().flipX = true;  
        }
    }

    public void SwingDownFlipAnimEvent()
    {
        activeSlashAnimation.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (PlayerController.Instance.FacingLeft)
        {
            activeSlashAnimation.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void DoneAttackingAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }
        
    public void Attack()
    {                
        myAnimator.SetTrigger(ATTACK_HASH);
        weaponCollider.gameObject.SetActive(true);

        activeSlashAnimation = Instantiate(slashAnimation, slashAnimationStartPosition.position, Quaternion.identity);
        activeSlashAnimation.transform.parent = this.transform.parent;
    }

    public WeaponInfo GetWeaponInfo()
    {
        return this.weaponInfo;
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerSceenPos = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerSceenPos.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, angle);
        }
        else 
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
