using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using DG.Tweening;

public class PauseButton : MonoBehaviour
{
    private bool isPause = false;
    [SerializeField] private GameObject pauseImage = default;

    public void Pause()
    {
        SoundManager.Instance.PlaySe("NormalButton");
        if (!isPause)//停止
        {
            GameManeger.Instance.currentGameStates.Value = GameState.Pause;
        }
        else//再開
        {
            GameManeger.Instance.currentGameStates.Value = GameState.Play;
        }
        isPause = !isPause;
        pauseImage.SetActive(isPause);
    }

}
