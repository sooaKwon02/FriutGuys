using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    public static EffectController instance;
    public GameObject effectPrefab;

    List<GameObject> effects = new List<GameObject>();

    private void Awake()
    {
        if (EffectController.instance == null)
        {
            EffectController.instance = this;
        }
    }

    private void Start()
    {
        CreateEffects(5);
    }

    private void CreateEffects(int effectCount)
    {
        for (int i = 0; i < effectCount; i++)
        {
            GameObject effect = Instantiate(effectPrefab) as GameObject;
            effect.transform.parent = transform;
            effect.SetActive(false);
            effects.Add(effect);
        }
    }

    public GameObject GetEffect(Vector3 position)
    {
        GameObject reqEffect = null;

        for (int i = 0; i < effects.Count; i++)
        {
            if (effects[i].activeSelf == false)
            {
                reqEffect = effects[i];
                break;
            }
        }

        if (reqEffect == null)
        {
            GameObject newEnemy = Instantiate(effectPrefab) as GameObject;
            newEnemy.transform.parent = transform;
            effects.Add(newEnemy);
            reqEffect = newEnemy;
        }

        reqEffect.SetActive(true);
        reqEffect.transform.position = position;
        return reqEffect;
    }
}