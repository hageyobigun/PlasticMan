using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class NextView : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI nextText = default;

    // Start is called before the first frame update
    void Start()
    {
        NextSequence();
    }

    public void NextSequence()
    {
        var nextSequence = DOTween.Sequence();

        //nextSequence.AppendInterval(3f);

        //nextSequence.Append(nextText.transform.DOLocalMoveX(nextText.transform.localPosition.x + 100, 2.0f)
        //    .SetEase(Ease.InOutQuart));

        //nextSequence.Join(nextText.DOFade(1, 1.0f));//表示

        //nextSequence.AppendInterval(1f);

        //nextSequence.Append(nextText.transform.DOLocalMoveX(nextText.transform.localPosition.x + 200, 2)
        //    .SetEase(Ease.InOutQuart));

        //nextSequence.Join(nextText.DOFade(0, 2.0f));//消える



        nextSequence.Append(
            DOTween.To(() => nextText.characterSpacing,
            x => nextText.characterSpacing = x, 30, 1.0f)); //文字の空白

        nextSequence.Join(nextText.DOFade(1, 1.0f));//表示

        nextSequence.Append(nextText.DOFade(0, 1.0f).SetDelay(0.5f));//消える

    }
}
