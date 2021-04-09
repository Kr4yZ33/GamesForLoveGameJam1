using System.Collections;
using UnityEngine;

public class ExtendAbility : PowerupAbility
{
    public float duration;

    private Coroutine _powerupTimerCoroutine;

    public override void Activate()
    {
        isActivated = true;
        _animator.Play("Extend");

        if (_powerupTimerCoroutine != null)
        {
            StopCoroutine(_powerupTimerCoroutine);
        }

        _powerupTimerCoroutine = StartCoroutine(PowerupTimer());
    }

    public override void Deactivate()
    {
        isActivated = false;
        _animator.Play("Shrink");
    }

    IEnumerator PowerupTimer()
    {
        yield return new WaitForSeconds(duration);
        Deactivate();
    }
}
