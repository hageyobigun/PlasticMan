using UnityEngine;
using DG.Tweening;


public class MoveFloor : MonoBehaviour
{
    [SerializeField] private GameObject floor = default;
    [SerializeField] private float loopPoint = 500f;
    [SerializeField] private float moveSpeed = 3.0f;

    [SerializeField] private float runningSpeed = 0.1f;
    private Tween moveTween;


    // Start is called before the first frame update
    void Start()
    {
        //背景を動かす
        moveTween = floor.transform
            .DOLocalMoveX(floor.transform.localPosition.x - loopPoint, moveSpeed)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);

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
        var endPos = startPos + new Vector3(5, 0, 0);
        runningMan.transform.position = Vector3.MoveTowards(startPos, endPos, runningSpeed);
    }

}
