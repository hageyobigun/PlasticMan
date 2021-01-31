using System.Collections;
using UnityEngine;
using TMPro;
using Game;
using DG.Tweening;
using UnityEngine.UI;

public class StartBattle : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI startText = default;

    // Start is called before the first frame update
    void Start()
    {

        startText.DOFade(1, 1.0f)
            .OnComplete(() =>
            {
                startText.DOFade(0, 1.0f).SetDelay(0.2f).OnComplete(() =>
                {
                    startText.text = "  GO!!";
                    startText.DOFade(1, 1.0f)
                    .OnComplete(() =>
                    {
                        startText.DOFade(0, 1.0f).SetDelay(0.2f);
                        GameManeger.Instance.currentGameStates.Value = GameState.Play;
                    });
                    startText.transform.DOScale(startText.transform.localScale * 1.5f, 1.0f);
                });
            });
    }
}
