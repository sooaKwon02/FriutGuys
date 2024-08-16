using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Rank : MonoBehaviour
{
    public GameObject rankItem;
    SaveLoad saveLoad;
    public GameObject rankContents;

    private void Awake()
    {

        saveLoad =FindObjectOfType<SaveLoad>();
    }

    private void OnEnable()
    {
        saveLoad.LoadScore();        
        for(int i=0;i<saveLoad.rankList.entries.Length;i++)
        {
            rankContents.transform.GetChild(i).GetComponent<RankItem>().ScoreSet(saveLoad.rankList.entries[i]);
        }
    }
}
