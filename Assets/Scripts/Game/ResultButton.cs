using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TMPro;

public class ResultButton : MonoBehaviour
{
    [SerializeField] private GameObject resultObj = null;
    [SerializeField] private TextMeshProUGUI winText = null;
    [SerializeField] private TextMeshProUGUI loseText = null;


    public void Start()
    {
        resultObj.SetActive(false);
        winText.enabled = false;
        loseText.enabled = false;

        GameManeger.Instance.gameStateChanged
            .Where(state => state == GameManeger.GameState.Win)
            .Subscribe(_ =>
            {
                resultObj.SetActive(true);
                winText.enabled = true;
            });

        GameManeger.Instance.gameStateChanged
            .Where(state => state == GameManeger.GameState.Lose)
            .Subscribe(_ =>
            {
                resultObj.SetActive(true);
                loseText.enabled = true;
            });
    }


    public void RetryButton()
    {
        GameManeger.Instance.SetCurrentState(GameManeger.GameState.Playing_Vs);
    }

    public void BackOptionButton()
    {
        GameManeger.Instance.SetCurrentState(GameManeger.GameState.Start);
    }

    public void TitleButton()
    {
        GameManeger.Instance.SetCurrentState(GameManeger.GameState.Opening);
    }

}
