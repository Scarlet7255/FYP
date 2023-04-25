using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class InteractablePlotDriver : InteractableObject
{
    public PlayableDirector nextPlot;
    public override void Action(CharacterAgent source)
    {
        gameObject.SetActive(false);
        nextPlot.gameObject.SetActive(true);
        nextPlot.time = 0d;
        nextPlot.Play();
    }
}
