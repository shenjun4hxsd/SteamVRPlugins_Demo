/*
 * Author : shenjun
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAction : MonoBehaviour {

    public GameObject effectPrefab;
    public AudioClip hitSound;

    public float moveSpeed;

	void Start () {

        Destroy(this.gameObject, 5f);
		
	}
	
	void Update () {

        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Destroy(this.gameObject);

            Vector3 hitPoint = other.ClosestPoint(transform.position);

            if(effectPrefab)
            {
                // 攻击特效
                Instantiate(effectPrefab, hitPoint, Quaternion.identity);
            }

            if(hitSound)
            {
                AudioSource.PlayClipAtPoint(hitSound, hitPoint);
            }
        }
    }
}
