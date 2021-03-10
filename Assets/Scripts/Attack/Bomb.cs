using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using Game;

public class Bomb : MonoBehaviour
{

    [SerializeField] private int playerId = 0;    //1:player -1:enemy
    [SerializeField] private float flightTime = 1.0f;  //滞空時間
    [SerializeField] private float jumpPower = 5.0f;
    [SerializeField] private GameObject explosion = null;

    private Sequence sequence;

    public void Start()
    {
        ThrowBomb();
        this.UpdateAsObservable()
            .Where(_ => GameManeger.Instance.currentGameStates.Value == GameState.Pause)//止める
            .Subscribe(_ => sequence.Pause());

        this.UpdateAsObservable()
            .Where(_ => GameManeger.Instance.currentGameStates.Value == GameState.Play)//動かす
            .Subscribe(_ => sequence.Play());

    }

    //爆弾発車
    public void ThrowBomb()
    {
        var startPos = transform.position; // 初期位置
        var endPos = startPos + new Vector3(9.4f * playerId, 0, 0);//爆弾投下位置

        sequence = DOTween.Sequence();

        sequence.Append(this.transform.DOJump(endPos, jumpPower, 1, flightTime)
            .OnComplete(() =>
            {
                Instance_explosion();
                Destroy(gameObject);
            })
        );

    }

    //とりあえず後で改善予定
    private void Instance_explosion()
    {
        SoundManager.Instance.PlaySe("Bomb");
        Instantiate(explosion, new Vector3(transform.position.x, 0f, transform.position.z), Quaternion.identity);
        Instantiate(explosion, new Vector3(transform.position.x, -3.0f, transform.position.z), Quaternion.identity);
        Instantiate(explosion, new Vector3(transform.position.x, -5.4f, transform.position.z), Quaternion.identity);
    }

}
