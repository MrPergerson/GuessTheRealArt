using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectableChoice : Button
{
    public int choiceIndex;

    protected override void Awake()
    {
        base.Awake();

        choiceIndex = GetComponent<ChoiceIndex>().index;
    }

    protected override void Start()
    {
        base.Start();

        
    }


}
