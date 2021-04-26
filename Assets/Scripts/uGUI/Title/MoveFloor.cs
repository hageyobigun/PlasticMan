using UnityEngine;
using DG.Tweening;


public class MoveFloor : MonoBehaviour
{
    [SerializeField] private GameObject floor = default;
    [SerializeField] private float loopPoint = 500f;   //ループするポイント
    [SerializeField] private float backMoveSpeed = 5.0f;//背景が動く速度
    [SerializeField] private float runningSpeed = 0.1f;//キャラが走る速度
    private Tween moveTween;


    // Start is called before the first frame update
    void Start()
    {
        //背景を動かす
        moveTween = floor.transform
            .DOLocalMoveX(floor.transform.localPosition.x - loopPoint, backMoveSpeed)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);//無限ループ
    }

    //停止
    public void  StopMoving()
    {
        moveTween.Kill();
    }

    //走る男
    public void RunningMan(GameObject runningMan)
    {
        var startPos = runningMan.transform.position;
        var runPoint = 5.0f; //画面外に走り去る場所
        var endPos = startPos + new Vector3(runPoint, 0, 0);
        runningMan.transform.position = Vector3.MoveTowards(startPos, endPos, runningSpeed);
    }

}
