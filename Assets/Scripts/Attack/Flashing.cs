using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Flashing
{
    private float flashTime;
    private bool isFlash;

    public Flashing()
    {
        flashTime = 5.0f;
    }

    public void Flash(SpriteRenderer character)
    {
        if (isFlash == false)//点滅中にまた呼び出さないようにするため
        {
            Observable
                .FromCoroutine(() => FlashInterval(character))
                .Subscribe(_ =>
                {
                    character.enabled = true;
                    isFlash = false;
                });
        }
    }

    public IEnumerator FlashInterval(SpriteRenderer character)
    {
        var time = 0;
        isFlash = true;
        while (time < flashTime)
        {
            character.enabled = false;
            yield return new WaitForSeconds(0.1f);
            character.enabled = true;
            yield return new WaitForSeconds(0.1f);
            time += 1;
        }
    }
}
