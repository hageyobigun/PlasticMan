using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Flashing
{
    private float flashTime = 5.0f;//点滅回数
    private float intervalTime = 0.1f;//点滅の感覚
    private bool isFlash;//点滅中か

    public void Flash(SpriteRenderer character)
    {
        if (isFlash == false)//点滅中にまた呼び出さないようにするため
        {
            Observable
                .FromCoroutine(() => FlashInterval(character))
                .Subscribe(_ =>
                {
                    character.enabled = true;//最後は表示に
                    isFlash = false;
                }).AddTo(character);
        }
    }

    public IEnumerator FlashInterval(SpriteRenderer character)
    {
        var time = 0;
        isFlash = true;
        while (time < flashTime)
        {
            character.enabled = false;
            yield return new WaitForSeconds(intervalTime);
            character.enabled = true;
            yield return new WaitForSeconds(intervalTime);
            time += 1;
        }
    }
}
