using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScriptBlue : MonoBehaviour
{
    Text _scoreBlue;
    public static int _scoreBlueValue = 0;

    // Start is called before the first frame update
    void Start()
    {
      _scoreBlue = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
       _scoreBlue.text = _scoreBlueValue.ToString();
    }
}
