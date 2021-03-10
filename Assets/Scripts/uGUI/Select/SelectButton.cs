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

    public void SetVsButton() => _eventSystemManege.SetSelectButton(vsButton);

    public void SetRushButton() => _eventSystemManege.SetSelectButton(rushButton);

    public void SetExplainButton() => _eventSystemManege.SetSelectButton(explainButton);

    public void SetSoundButton() => _eventSystemManege.SetSelectSlider(soundSlider);

    public void SetBackSelectButton() => _eventSystemManege.BackSelectButton();

}
