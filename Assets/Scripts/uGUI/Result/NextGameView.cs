using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using Game;

public class NextGameView
{

    private float stagingTime = 1.0f;  //演出時間
    private float characterSpacing = 25f; //文字間隔

    //boss rushの次へ行く演出
    //引数にテキストと画面を覆う黒いimage
    public void NextStage(TextMeshProUGUI nextText, Image blackImage)
    {
        var nextSequence = DOTween.Sequence();

        blackImage.gameObject.SetActive(true);//表示
        nextText.gameObject.SetActive(true);

        nextSequence.Append(
            DOTween.To(() => nextText.characterSpacing,
            x => nextText.characterSpacing = x, characterSpacing, stagingTime)); //文字の空白

        nextSequence.Join(nextText.DOFade(1, stagingTime));//表示

        nextSequence.Append(nextText.DOFade(0, stagingTime));//消える

        nextSequence.Join(blackImage.DOFade(1, stagingTime));//画面を黒くしていく

        nextSequence.OnComplete(() =>
        {
            //次の敵を設定
            GameManeger.Instance.EnemyNumber++;
            GameManeger.Instance.currentGameStates.Value = GameState.RushGame;
        });

    }
}
