using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;
using Game;
using UnityEngine.EventSystems;

public class PausePresenter : MonoBehaviour
{
    [SerializeField] private Button FirstButton = default;//最初にeventSystemに登録するよう
    [SerializeField] private PauseView _pauseView = default;
    [SerializeField] private EventSystemManeger _eventSystemManeger = default;
    private PauseModel _pauseModel;

    // Start is called before the first frame update
    void Start()
    {
        _pauseModel = new PauseModel();

        //pause
        this.UpdateAsObservable()
            .Where(_ => Input.GetButtonDown("pause"))
            .Where(_ => GameManeger.Instance.currentGameStates.Value == GameState.Play
                    || GameManeger.Instance.currentGameStates.Value == GameState.Pause)
            .Subscribe(_ => _pauseModel.ChangePauseState());

        //pause
        _pauseModel.pauseState
            .Where(state => state == GameState.Pause)
            .Subscribe(_ =>
            {
                _eventSystemManeger.FirstSetObj(FirstButton.gameObject);
                _pauseView.PauseButton();
            });

        //restart
        _pauseModel.pauseState
            .Where(state => state == GameState.Play)
            .Subscribe(_ =>
            {
                _eventSystemManeger.Clear();
                _pauseView.RestartButton();
            });
    }
}
