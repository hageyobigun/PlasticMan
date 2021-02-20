using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using Game;

public class NextGameView
{

    //boss rushの次へ行く演出
    public void NextStage(TextMeshProUGUI nextText, Image blackImage)
    {
        var nextSequence = DOTween.Sequence();

        blackImage.gameObject.SetActive(true);//表示
        nextText.gameObject.SetActive(true);

        nextSequence.Append(
            DOTween.To(() => nextText.characterSpacing,
            x => nextText.characterSpacing = x, 25, 1.0f)); //文字の空白

        nextSequence.Join(nextText.DOFade(1, 1.0f));//表示

        nextSequence.Append(nextText.DOFade(0, 1.0f).SetDelay(0.5f));//消える

        nextSequence.Join(blackImage.DOFade(1, 1.0f));//画面を黒くしていく

        nextSequence.OnComplete(() =>
        {
            //次の敵を設定
            GameManeger.Instance.GetEnemyNumber++;
            GameManeger.Instance.currentGameStates.Value = GameState.RushGame;
        });

    }
}
