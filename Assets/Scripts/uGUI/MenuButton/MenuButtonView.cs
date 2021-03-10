using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Game;

public class MenuButtonView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI menuText = default; //確認画面のテキスト
    [SerializeField] private GameObject confirmImage = default; //確認画面

    private MenuButtonImageOpen _menuButtonImageOpen;//開く演出

    void Awake()
    {
        _menuButtonImageOpen = new MenuButtonImageOpen();
        confirmImage.SetActive(false);
    }


    //シーン移動するボタン(リトライ、タイトル、選択画面など)
    public void MenuButton(string text)
    {
        menuText.text = text;//表示文字を変える
        confirmImage.SetActive(true);
        _menuButtonImageOpen.ImageOpen(confirmImage);//開く演出
        SoundManager.Instance.PlaySe("NormalButton");
    }

    //シーン移動
    public void YesButton(GameState gameState)
    {
        GameManeger.Instance.currentGameStates.Value = gameState;
        SoundManager.Instance.PlaySe("NormalButton");
    }

    //閉じるボタン(No)
    public void NoButton()
    {
        if (confirmImage.activeInHierarchy == true) ////開いてないときは押せないようにしておく
        {
            confirmImage.SetActive(false);
            SoundManager.Instance.PlaySe("Cancel");
        }
    }
}
