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
    //[SerializeField] private Image whiteImage = null;
    //[SerializeField] private Image blackBack = default;

    void Start()
    {

        //点滅
        var tween = _textMeshPro.DOFade(0, time).SetLoops(-1, LoopType.Yoyo);

        //スタート
        this.UpdateAsObservable()
            .Where(_ => Input.GetKeyDown(KeyCode.Return))
            .Take(1)
            .Subscribe(_ =>
            {
                tween.Kill(); //停止
                SoundManager.Instance.PlaySe("TitleButton"); //サウンド
                GameManeger.Instance.currentGameStates.Value = GameState.Start;
                //画面遷移
                //whiteImage.DOFade(1, 1.0f)
                //.OnComplete(() => GameManeger.Instance.currentGameStates.Value = GameState.Start);
            });
    }

    //16:9
    //-375 375 200 -200  50が基準
    //private void InstanceBlock()
    //{
    //    var oneBlockScale = blackBack.rectTransform.sizeDelta;
    //    for (int x = -375; x <= 375; x += (int)oneBlockScale.x)
    //    {
    //        for (int y = -200; y <= 200; y += (int)oneBlockScale.x)
    //        {
    //            var copy = Instantiate(blackBack, new Vector3(x, y, 0), Quaternion.identity);
    //            copy.transform.SetParent(canvas.transform, false);
    //            copy.gameObject.SetActive(false);
    //            backList.Add(copy);
    //        }
    //    }
    //}


}
