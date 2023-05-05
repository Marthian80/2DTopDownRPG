using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject magicBoltPrefab;
    [SerializeField] private Transform magicBoltSpawnPoint;

    private Animator myAnimator;

    readonly int FIRE_HASH = Animator.StringToHash("Fire");

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }

    public void Attack()
    {
        myAnimator.SetTrigger(FIRE_HASH);
    }

    public WeaponInfo GetWeaponInfo()
    {
        return this.weaponInfo;
    }

    public void SpawnStaffProjectileAnimEvent()
    {
        GameObject newMagicBolt = Instantiate(magicBoltPrefab, magicBoltSpawnPoint.position, Quaternion.identity);
        newMagicBolt.GetComponent<MagicBolt>().UpdateBoltRange(this.weaponInfo.WeaponRange);
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerSceenPos = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerSceenPos.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);            
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);            
        }
    }
}
