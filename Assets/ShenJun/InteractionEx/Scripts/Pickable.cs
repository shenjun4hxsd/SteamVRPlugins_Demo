/*
 * Author : shenjun
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Pickable : HandInteraction {

    public ItemPackage itemPackage;

    private Canvas canvas;

    private void Awake()
    {
        canvas = transform.GetComponentInChildren<Canvas>();
        if(canvas)
        {
            canvas.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 当手柄设备进入到触发区域内 调用一次 Enter
    /// </summary>
    /// <param name="hand"></param>
    public override void OnHandHoverBegin(HandDevice hand)
    {
        if(canvas)
        {
            canvas.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 当手柄设备悬停在物体上时 持续调用 Update
    /// </summary>
    /// <param name="hand"></param>
    public override void OnHandHoverUpdate(HandDevice hand)
    {
        if(hand && hand.controller != null)
        {
            // 按下扳机键
            if(hand.controller.GetHairTriggerDown())
            {
                OnAttachToHand(hand);
            }
        }
    }

    /// <summary>
    /// 当手柄设备离开触发区域 调用一次 Exit
    /// </summary>
    /// <param name="hand"></param>
    public override void OnHandHoverEnd(HandDevice hand)
    {
        if(canvas)
        {
            canvas.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 当该对象被添加到控制设备上 调用一次
    /// </summary>
    /// <param name="hand"></param>
    public override void OnAttachToHand(HandDevice hand)
    {
        // 先把手上现有的武器都去掉
        if(itemPackage.packageType == ItemPackage.ItemPackageType.OneHanded)
        {
            hand.DetachObjFromHand();
        }
        else if(itemPackage.packageType == ItemPackage.ItemPackageType.TwoHanded)
        {
            hand.DetachObjFromHand();
            hand.otherHand.DetachObjFromHand();
        }

        // 然后根据资源包的设置给手上附加道具
        if (itemPackage.packageType == ItemPackage.ItemPackageType.OneHanded)
        {
            GameObject oneHandObj = Instantiate(itemPackage.itemPrefab);
            oneHandObj.transform.SetParent(hand.transform);
            oneHandObj.transform.localPosition = Vector3.zero;
            oneHandObj.transform.localRotation = Quaternion.identity;
            hand.AttachObjToHand(oneHandObj);
        }
        else if (itemPackage.packageType == ItemPackage.ItemPackageType.TwoHanded)
        {
            GameObject oneHandObj = Instantiate(itemPackage.itemPrefab);
            oneHandObj.transform.SetParent(hand.transform);
            oneHandObj.transform.localPosition = Vector3.zero;
            oneHandObj.transform.localRotation = Quaternion.identity;
            hand.AttachObjToHand(oneHandObj);

            GameObject twoHandObj = Instantiate(itemPackage.otherHandItemPrefab);
            twoHandObj.transform.SetParent(hand.otherHand.transform);
            twoHandObj.transform.localPosition = Vector3.zero;
            twoHandObj.transform.localRotation = Quaternion.identity;
            hand.otherHand.AttachObjToHand(twoHandObj);
        }
    }
}
