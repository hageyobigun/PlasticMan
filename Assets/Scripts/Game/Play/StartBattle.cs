using UnityEngine;
using TMPro;
using Game;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;

public class StartBattle : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI startText = default;

    // Start is called before the first frame update
    void Start()
    {
        //シーンの移動が完了したら動かす
        this.UpdateAsObservable()
            .Where(_ => GameManeger.Instance.GetSceneMoveComplete)
            .First()
            .Subscribe(_ => StartSequence())
            .AddTo(this);
    }

    //スタート演出
    private void StartSequence()
    {
        var startSequence = DOTween.Sequence();

        startSequence.Append(
            DOTween.To(() => startText.characterSpacing,
            x => startText.characterSpacing = x, 30, 1.0f)); //文字の空白

        startSequence.Join(startText.DOFade(1, 1.0f));//表示

        startSequence.Append(startText.DOFade(0, 1.0f).SetDelay(0.2f));//消える

        startSequence.Append(startText.DOFade(1, 1.0f)
            .OnStart(() => startText.text = "  GO!!"));//表示 & 文字変更

        startSequence.Join(startText.transform.DOScale(startText.transform.localScale * 1.5f, 1.0f));//拡大

        startSequence.Append(startText.DOFade(0, 1.0f).SetDelay(0.2f)
            .OnComplete(() => GameManeger.Instance.currentGameStates.Value = GameState.Play)); //消える
    }
}
