using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class PauseView : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI backText = default;
    [SerializeField] private GameObject pauseMenu = default;
    [SerializeField] private Image backBlackImage = default;    //薄暗い背景
    [SerializeField] private GameObject confirmImage = default; //確認画面

    private float menuPos;
    private bool isPause;

    void Awake()
    {
        menuPos = pauseMenu.transform.localPosition.x;
        isPause = false;
        pauseMenu.transform.DOLocalMoveX(menuPos - 300, 0.0f);
        backBlackImage.gameObject.SetActive(false);
    }

    public void PauseButton()
    {
        if (!isPause)
        {
            pauseMenu.transform.DOLocalMoveX(menuPos, 0.5f).SetEase(Ease.OutBounce);
            backBlackImage.gameObject.SetActive(true);
            isPause = true;
        }
        else
        {
            pauseMenu.transform.DOLocalMoveX(menuPos - 300, 0.5f).SetEase(Ease.InOutBack);
            backBlackImage.gameObject.SetActive(false);
            isPause = false;
        }
    }

    public void RetryButton()
    {
        backText.text = "やり直しますか?";
        confirmImage.SetActive(true);
    }

    public void TitleButton()
    {
        backText.text = "タイトルに戻りますか?";
        confirmImage.SetActive(true);
    }

    public void SelectButton()
    {
        backText.text = "選択画面に戻りますか?";
        confirmImage.SetActive(true);
    }



}
