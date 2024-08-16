using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeEffect : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke(nameof(EffectDone), 1.0f);
    }

    private void EffectDone()
    {
        gameObject.SetActive(false);
    }
}