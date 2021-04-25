using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class RushButton : MonoBehaviour
{
    [SerializeField] private Button startButton = default;

    // Start is called before the first frame update
    void Awake()
    {
        startButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                StartRush();
                SoundManager.Instance.PlaySe("NormalButton");
            });
    }

    //最初の敵をセット
    public void StartRush()
    {
        GameManeger.Instance.EnemyNumber = 0;//最初の敵セット
    }
}
