using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    [SerializeField] private Button vsButton = default;
    [SerializeField] private Button rushButton = default;
    [SerializeField] private Button explainButton = default;
    [SerializeField] private Slider soundSlider = default;

    [SerializeField] private EventSystemManeger _eventSystemManege = default; 　//eventsystemで管理

    //eventSystemにそれぞれセットするためのもの

    public void SetVsButton() => _eventSystemManege.SetSelectObj(vsButton.gameObject);

    public void SetRushButton() => _eventSystemManege.SetSelectObj(rushButton.gameObject);

    public void SetExplainButton() => _eventSystemManege.SetSelectObj(explainButton.gameObject);

    public void SetSoundButton() => _eventSystemManege.SetSelectObj(soundSlider.gameObject);

    public void SetBackSelectButton() => _eventSystemManege.BackSelectButton();

}
