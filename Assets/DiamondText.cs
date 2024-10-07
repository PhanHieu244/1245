using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiamondText : MonoBehaviour
{
    [SerializeField] private Text _text;

    private void Update()
    {
        _text.text = GameController.instance.Diamond.ToString();
    }
}
