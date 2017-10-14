/*
 * Author : shenjun
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器控制 --- 通过手柄设备驱动
/// </summary>
public class WeaponControl : HandInteraction
{

    public GameObject bulletPrefab;

    /** 武器发射点 */
    Transform firePos;

    /** 手柄设备 */
    HandDevice hand;

    private void Awake()
    {
        firePos = transform.Find("FirePos");
    }

    public override void OnAttachToHand(HandDevice hand)
    {
        this.hand = hand;
        //Debug.Log("获得来福枪");
    }

    public override void OnDetachFromHand(HandDevice hand)
    {
        this.hand = null;
        Destroy(this.gameObject);
    }

    public override void OnHandAttachedUpdate(HandDevice hand)
    {
        // 按下扳机键 发射子弹
        if(this.hand && this.hand.controller != null && this.hand.controller.GetHairTriggerDown())
        {
            Instantiate(bulletPrefab, firePos.position, firePos.rotation);
            StartCoroutine(FirePulse());
        }
    }

    /// <summary>
    /// 手柄的震动
    /// </summary>
    IEnumerator FirePulse()
    {
        for (int i = 0; i < 5; i++)
        {
            ushort tmp = (ushort)Random.Range(800, 2000);
            hand.controller.TriggerHapticPulse(tmp);
            yield return new WaitForSeconds(0.02f);
        }
    }
}
