using UnityEngine;



public interface IEventSystem
{
    //押したボタンは順にリストに入れていく

    //押したボタン（オブジェクト）をセット
    void SetSelectObj(GameObject selectObj);

    //前のボタンに戻ってセット
    void BackSelectButton();

    //押したボタンではなく指定したボタンをセットする
    void FirstSetObj(GameObject setObj);

    //リストも登録も解除で初期状態に
    void Clear();
}