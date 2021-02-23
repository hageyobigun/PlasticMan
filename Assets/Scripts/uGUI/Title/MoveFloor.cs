using UnityEngine;
using DG.Tweening;


public class MoveFloor : MonoBehaviour
{
    [SerializeField] private GameObject floor = default;
    [SerializeField] private float loopPoint = 500f;
    [SerializeField] private float moveSpeed = 3.0f;

    private Tween moveTween;


    // Start is called before the first frame update
    void Start()
    {
        moveTween = floor.transform
            .DOLocalMoveX(floor.transform.localPosition.x - loopPoint, moveSpeed)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);

    }



    public void  StopMoving()
    {
        moveTween.Kill();
    }


}
