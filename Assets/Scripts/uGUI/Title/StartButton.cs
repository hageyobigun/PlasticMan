using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using TMPro;
using Game;
using DG.Tweening;
using UnityEngine.UI;


public class StartButton : MonoBehaviour
{
    [SerializeField] private float time = 2.0f;
    [SerializeField] private TextMeshProUGUI _textMeshPro = default;
    [SerializeField] private MoveFloor _moveFloor = default;
    [SerializeField] private GameObject runningMan = default;
    private string[] controller = new string[10]; //コントローラー入れるもの

    private bool isRunning;

    void Start()
    {
        isRunning = false;

        //点滅
        var tween = _textMeshPro.DOFade(0, time).SetLoops(-1, LoopType.Yoyo);

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
            .Subscribe(_ => _textMeshPro.text = "PRESS ENTER");//キーボード

        this.UpdateAsObservable()
            .Where(_ => controller.Length == 1)
            .Subscribe(_ => _textMeshPro.text = "PRESS BUTTON");//PS4


        this.UpdateAsObservable()
            .Where(_ => isRunning)
            .Subscribe(_ => _moveFloor.RunningMan(runningMan));//走り出す

    }
}
