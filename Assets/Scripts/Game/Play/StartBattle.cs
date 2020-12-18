using System.Collections;
using UnityEngine;
using TMPro;
using Game;

public class StartBattle : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI startText = null;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartBattleText());
    }

    private IEnumerator StartBattleText()
    {
        startText.text = "READY";
        yield return new WaitForSeconds(1f);
        startText.text = "   GO!!";
        yield return new WaitForSeconds(0.5f);
        startText.enabled = false;
        GameManeger.Instance.currentGameStates.Value = GameState.Play;
    }
}
