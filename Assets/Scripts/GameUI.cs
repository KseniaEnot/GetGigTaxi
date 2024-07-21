using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;
    public void SetScore(int _score)
    {
        text.text = _score.ToString();
    }

}
