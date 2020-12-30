using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Game;

public class RushButton : MonoBehaviour
{
    [SerializeField] private Button startButton = null;

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

    public void StartRush()
    {
        GameManeger.Instance.GetEnemyNumber = 0;
        GameManeger.Instance.currentGameStates.Value = GameState.RushGame;
    }
}
