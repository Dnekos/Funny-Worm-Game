using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelectManager : MonoBehaviour
{
    [SerializeField]
    Text Textbox;
    [SerializeField]
    string Levelname;
    public void OnSelect()
    {
        Textbox.text = Levelname;
    }


}
