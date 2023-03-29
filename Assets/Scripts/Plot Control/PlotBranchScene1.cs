using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotBranchScene1 : MonoBehaviour
{
   public DoorLock plotLock;
   public GameObject branch1;
   public GameObject branch2;
   
   public void StartNextPlot()
   {
      if (plotLock.locked)
      {
         branch1.SetActive(true);
      }
      else
         branch2.SetActive(true);
   }
}
