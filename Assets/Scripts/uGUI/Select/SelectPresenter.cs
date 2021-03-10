using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Game;
using UniRx.Triggers;

public class SelectPresenter : MonoBehaviour
{
    [SerializeField] private Button vsButton = default;
    [SerializeField] private Button rushButton = default;
    [SerializeField] private Button explainButton = default;
    [SerializeField] private Button soundButton = default;
    [SerializeField] private Button closeButton = default;
    [SerializeField] private SelectView _selectView = default;

    // Start is called before the first frame update
    void Awake()
    {
        //vsボタン
        vsButton.OnClickAsObservable()
           .Subscribe(_ => _selectView.OpneVsImage());

        //rushボタン
        rushButton.OnClickAsObservable()
           .Subscribe(_ => _selectView.OpneRushImage());

        //操作説明ボタン
        explainButton.OnClickAsObservable()
           .Subscribe(_ => _selectView.OpneExplainImage());

        //音量調整画面表示
        soundButton.OnClickAsObservable()
            .Subscribe(_ => _selectView.OpenSoundImage());

        //閉じるボタン
        closeButton.OnClickAsObservable()
            .Subscribe(_ => _selectView.CloseImage());
    }
}
