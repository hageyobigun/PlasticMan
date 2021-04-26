using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using TMPro;
using Game;
using DG.Tweening;


public class StartButton : MonoBehaviour
{
    [SerializeField] private float flashTime = 1.0f; //点滅周期
    [SerializeField] private TextMeshProUGUI pressText = default;
    [SerializeField] private MoveFloor _moveFloor = default;　　//背景や走る演出クラス
    [SerializeField] private GameObject runningMan = default;　//走るプレイヤー
    private string[] controller = new string[10]; //コントローラー入れるもの

    private bool isRunning;

    void Start()
    {
        isRunning = false;

        //点滅
        var tween = pressText.DOFade(0, flashTime).SetLoops(-1, LoopType.Yoyo);

        //スタート
        this.UpdateAsObservable()
            .Where(_ => Input.GetButtonDown("Enter"))
            .Take(1)
            .Subscribe(_ =>
            {
                tween.Kill(); //停止
                _moveFloor.StopMoving(); //停止
                isRunning = true;
                SoundManager.Instance.PlaySe("TitleButton"); //サウンド
                GameManeger.Instance.currentGameStates.Value = GameState.Start;
            });


        //コントローラーによって表示文字変える
        this.UpdateAsObservable()
            .Subscribe(_ => controller = Input.GetJoystickNames());

        this.UpdateAsObservable()
            .Where(_ => controller.Length <= 0)
            .Subscribe(_ => pressText.text = "PRESS ENTER");//キーボード

        this.UpdateAsObservable()
            .Where(_ => controller.Length == 1)
            .Subscribe(_ => pressText.text = "PRESS BUTTON");//PS4

        //ループじゃないと動かないので
        this.UpdateAsObservable()
            .Where(_ => isRunning)
            .Subscribe(_ => _moveFloor.RunningMan(runningMan));//走り出す

    }
}
