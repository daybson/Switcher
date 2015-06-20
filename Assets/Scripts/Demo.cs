using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Demo : MonoBehaviour
{
    public Switcher switcher;
    public Text txt;

    public void Status()
    {
        txt.text = switcher.IsOn.ToString();
    }

    public void Alternate()
    {
        switcher.IsOn = !switcher.IsOn;
    }
}
