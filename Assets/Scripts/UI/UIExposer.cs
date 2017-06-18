using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIExposer : MonoBehaviour
{
    //This class is just used to easily take stuff from the inspector and load them into a C# container.
    //Other classes can reference this to do fancy things to the UI

    public RectTransform playerPortrait;
    public RectTransform healthBar;
    public RectTransform focusBar;
    public RectTransform readySpell;
    public RectTransform[] inputStringList;
    public RectTransform GemsCounter;
}
