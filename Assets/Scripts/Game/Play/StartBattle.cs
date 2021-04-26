using UnityEngine;
using TMPro;
using Game;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;

public class StartBattle : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI startText = default;
    [SerializeField] private float stagingTime = 1.0f;  //演出時間
    [SerializeField] private float characterSpacing = 30f; //文字間隔
    [SerializeField] private float scaleSize = 1.5f; //文字の拡大サイズ

    // Start is called before the first frame update
    void Start()
    {
        //シーンの移動が完了したら動かす
        this.UpdateAsObservable()
            .Where(_ => GameManeger.Instance.SceneMoveComplete)
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
            x => startText.characterSpacing = x, characterSpacing, stagingTime)); //文字の空白

        startSequence.Join(startText.DOFade(1, stagingTime));//表示

        startSequence.Append(startText.DOFade(0, stagingTime));//消える

        startSequence.Append(startText.DOFade(1, stagingTime)
            .OnStart(() => startText.text = "  GO!!"));//表示 & 文字変更

        startSequence.Join(startText.transform.DOScale(startText.transform.localScale * scaleSize, stagingTime));//拡大

        startSequence.Append(startText.DOFade(0, stagingTime)
            .OnComplete(() => GameManeger.Instance.currentGameStates.Value = GameState.Play)); //消える
    }
}
