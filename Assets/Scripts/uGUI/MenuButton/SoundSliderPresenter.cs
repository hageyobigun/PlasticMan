using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using Game;

public class SoundSliderPresenter : MonoBehaviour
{

    [SerializeField] private Button soundButton = default;
    [SerializeField] private Button closeButton = default;
    [SerializeField] private SoundSliderView _soundSliderView = default;
    [SerializeField] private EventSystemManeger _eventSystemManeger = default;

    // Start is called before the first frame update
    void Start()
    {

        //音量設定画面表示
        soundButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                _soundSliderView.SoundButton();
                _eventSystemManeger.SetSelectObj(closeButton.gameObject);//登録
            });


        //サウンドの閉じるボタン
        closeButton.OnClickAsObservable()
            .Subscribe(_ => BackPauseMenu());

        //閉じるボタン
        this.UpdateAsObservable()
            .Where(_ => Input.GetButtonDown("Cancel"))
            .Subscribe(_ => BackPauseMenu());

    }

    //閉じるボタンの設定
    private void BackPauseMenu()
    {
        _soundSliderView.CloseButton();
        _eventSystemManeger.BackSelectButton();
    }


}
