﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject bullet = null;
    [SerializeField] private GameObject fireBullet = null;
    [SerializeField] private GameObject bomb = null;
    [SerializeField] private GameObject barrier = null;

    public void Awake()
    {
        if (barrier != null) barrier.SetActive(false);
    }

    public void BulletAttack()
    {
        Instantiate(bullet, this.transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
    }

    public void FireAttack()
    {
        Instantiate(fireBullet, this.transform.position + new Vector3(0, 1.0f, 0), Quaternion.Euler(0f, 0f, 90f));
    }

    public void BombAttack()
    {
        Instantiate(bomb, this.transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);


    }

    public IEnumerator BarrierGuard(float interval)
    {
        //barrier.GetComponent<SpriteRenderer>().DOFade(1.0f, 0);
        //barrier.GetComponent<SpriteRenderer>().DOFade(0.0f, interval);
        barrier.SetActive(true);
        yield return new WaitForSeconds(interval);
        barrier.SetActive(false);
    }

}
