﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIExposer : MonoBehaviour
{
    //This class is just used to easily take stuff from the inspector and load them into a C# container.
    //Other classes can reference this to do fancy things to the UI

    public RectTransform PlayerPortrait;
    public RectTransform PlayerHeartBackgroundsPanel;
    public RectTransform PlayerHeartsPanel;
    public RectTransform PlayerFocusBar;
    public RectTransform PlayerReadySpell;
    public RectTransform[] PlayerInputStringList;
    public RectTransform PlayerGemsCounter;
}
