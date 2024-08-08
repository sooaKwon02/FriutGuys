using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankItem : MonoBehaviour
{
    public Text nickName;
    public Text score;

    public void ScoreSet(SaveLoad.ScoreEntry entry)
    {
        nickName.text = entry.id;
        score.text = entry.score.ToString();
    }

}
