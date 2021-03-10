using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestTwo : MonoBehaviour
{

    [SerializeField] private GameObject floor = default;


    [field: SerializeField]
    public int Level { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        floor.transform.DOLocalMoveX(floor.transform.localPosition.x - 900, 3f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
