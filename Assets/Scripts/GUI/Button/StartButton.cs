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
    [SerializeField] private TextMeshProUGUI _textMeshPro = null;
    [SerializeField] private Image whiteImage = null;

    void Start()
    {

        //点滅
        var tween = _textMeshPro.DOFade(0, time).SetLoops(-1, LoopType.Yoyo);

        //スタート
        this.UpdateAsObservable()
            .Where(_ => Input.GetKeyDown(KeyCode.Return))
            .Subscribe(_ =>
            {
                tween.Kill(); //停止
                SoundManager.Instance.PlaySe("TitleButton"); //サウンド
                //画面遷移　画面白くする
                whiteImage.DOFade(1, 1.0f)
                .OnComplete(() => GameManeger.Instance.currentGameStates.Value = GameState.Start);
            });

    }
}
