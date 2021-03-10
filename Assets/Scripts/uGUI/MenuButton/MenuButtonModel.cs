using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using UniRx;

public class MenuButtonModel
{
    public GameState moveScene;//移動するシーン

    public string menuText;//メニューに表示される文字

    public MenuButtonModel()
    {
        moveScene = GameState.Play;
    }

    //どのボタンを押されたかでどこに飛ぶか決める
    public void ChangeMoveScene(GameState gameState)
    {
        moveScene = gameState;
        ChangeMenuText(gameState);
    }

    //移動するシーンによってテキストを変更
    private void ChangeMenuText(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.VsGame://Vsボタン
                menuText = "その敵で始めますか?";
                break;

            case GameState.RushGame://Vsボタン
                menuText = "始めますか?";
                break;

            case GameState.Retry://リトライボタン
                menuText = "リトライしますか?";
                break;

            case GameState.Title://タイトルに戻るボタン
                menuText = "タイトルに戻りますか?";
                break;

            case GameState.Start://選択画面に戻るボタン
                menuText = "選択画面に戻りますか?";
                break;

            case GameState.GameEnd://ゲーム終了画面表示
                menuText = "ゲーム終了しますか?";
                break;

            default:
                menuText = "シーン移動ではないです";
                Debug.LogError("それは設定されてないシーン移動ボタンです");
                break;
        }
    }


}
