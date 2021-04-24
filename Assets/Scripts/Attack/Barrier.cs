using UnityEngine;

public class Barrier : MonoBehaviour, IAttacknotable
{
    //バリアの処理(追加するかも）
    public void barriered()
    {
        SoundManager.Instance.PlaySe("Barrier");
    }

}
