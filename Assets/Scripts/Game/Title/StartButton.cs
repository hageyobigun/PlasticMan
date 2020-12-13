using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using TMPro;
using Game;

public class StartButton : MonoBehaviour
{
    private float deltaTime = 0.0333f;
    [SerializeField] private float angularFrequency = 5f;
    [SerializeField] private TextMeshProUGUI _textMeshPro = null;

    Subject<Unit> scene_move = new Subject<Unit>();

    void Start()
    {
        float time = 0.0f;

        //点滅
        Observable.Interval(TimeSpan.FromSeconds(deltaTime)).Subscribe(_ =>
        {
            time += angularFrequency * deltaTime;
            _textMeshPro.color = new Color(0, 0, 0 ,Mathf.Sin(time) * 0.5f + 0.5f);
        }).AddTo(this);

        //スタート
        this.UpdateAsObservable()
            .Where(_ => Input.GetKeyDown(KeyCode.Return))
            .Subscribe(_ =>
            {
                angularFrequency *= 5; //点滅加速
                scene_move.OnNext(Unit.Default);
            });

        //n秒後にシーン移動
        scene_move
            .Delay(TimeSpan.FromSeconds(1.0f))
            .Subscribe(_ => GameManeger.Instance.currentGameStates.Value = GameState.Start);
    }
}
