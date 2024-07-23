using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    private void Start()
    {
        Invoke(nameof(EffectDone), 1.0f);
    }

    private void EffectDone()
    {
        Destroy(gameObject);
    }
}