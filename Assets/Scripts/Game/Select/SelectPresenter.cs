using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class SelectPresenter : MonoBehaviour
{
    [SerializeField] private Button vsButton = null;
    [SerializeField] private Button rushButton = null;
    [SerializeField] private Button explainButton = null;
    [SerializeField] private Button closeButton = null;

    [SerializeField] private SelectView _selectView = null;

    // Start is called before the first frame update
    void Awake()
    {
        vsButton.OnClickAsObservable()
           .Subscribe(_ => _selectView.OpneVsImage());

        rushButton.OnClickAsObservable()
           .Subscribe(_ => _selectView.OpneRushImage());

        explainButton.OnClickAsObservable()
           .Subscribe(_ => _selectView.OpneExplainImage());

        closeButton.OnClickAsObservable()
            .Subscribe(_ => _selectView.CloseImage());
    }
}
