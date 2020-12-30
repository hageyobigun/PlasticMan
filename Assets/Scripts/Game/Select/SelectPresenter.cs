using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Game;

public class SelectPresenter : MonoBehaviour
{
    [SerializeField] private Button vsButton = null;
    [SerializeField] private Button rushButton = null;
    [SerializeField] private Button explainButton = null;
    [SerializeField] private Button closeButton = null;
    [SerializeField] private Button TitleButton = null;
    [SerializeField] private SelectView _selectView = null;

    // Start is called before the first frame update
    void Awake()
    {
        vsButton.OnClickAsObservable()
           .Subscribe(_ =>
           {
               _selectView.OpneVsImage();
               SoundManager.Instance.PlaySe("NormalButton");
           });    

        rushButton.OnClickAsObservable()
           .Subscribe(_ =>
           {
               _selectView.OpneRushImage();
               SoundManager.Instance.PlaySe("NormalButton");
           });

        explainButton.OnClickAsObservable()
           .Subscribe(_ =>
           {
               _selectView.OpneExplainImage();
               SoundManager.Instance.PlaySe("NormalButton");
           });

        closeButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                _selectView.CloseImage();
                SoundManager.Instance.PlaySe("Cancel");
            });

        TitleButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                GameManeger.Instance.currentGameStates.Value = GameState.Title;
                SoundManager.Instance.PlaySe("NormalButton");
            });
    }
}
