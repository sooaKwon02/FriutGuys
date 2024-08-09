using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsHitAlarm : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke(nameof(AlarmDone), 0.5f);
    }

    private void AlarmDone()
    {
        gameObject.SetActive(false);
    }
}