using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;


public class ExplainButton : MonoBehaviour
{
    [SerializeField] private Button leftButton = null;
    [SerializeField] private Button rightButton = null;

    [SerializeField] private GameObject attackImage = null;
    [SerializeField] private GameObject controllerImage = null;

    // Start is called before the first frame update
    void Awake()
    {
        leftButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                controllerImage.SetActive(true);
                attackImage.SetActive(false);
                SoundManager.Instance.PlaySe("NormalButton");
            });

        rightButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                controllerImage.SetActive(false);
                attackImage.SetActive(true);
                SoundManager.Instance.PlaySe("NormalButton");
            });
    }
}
