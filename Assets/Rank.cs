using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Rank : MonoBehaviour
{
    public GameObject rankItem;
    SaveLoad saveLoad;
    public GameObject rankContents;
    int count;

    private void Awake()
    {

        saveLoad =FindObjectOfType<SaveLoad>();
    }

    private void OnEnable()
    {
        count = 0;
        saveLoad.LoadScore();
        
        for(int i=0;i<saveLoad.rankList.entries.Length;i++)
        {
            rankContents.transform.GetChild(i).GetComponent<RankItem>().ScoreSet(saveLoad.rankList.entries[i]);
        }
    }
}
