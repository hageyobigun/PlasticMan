using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        startText.text = "GO!!";
        yield return new WaitForSeconds(0.5f);
        startText.enabled = false;
    }
}
