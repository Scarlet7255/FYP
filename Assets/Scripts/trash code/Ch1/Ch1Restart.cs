using UnityEngine;
using UnityEngine.Playables;

public class Ch1Restart : MonoBehaviour
{
   public PlayableDirector d;
   public Animator CGMask;
   public PlayableDirector ending;
   public void RestartGame()
   {
      d.time = 0f;
      CGMask.gameObject.SetActive(true);
      CGMask.Play("CG Mask Fade");
      d.Play();
      ending.gameObject.SetActive(false);
      ending.time = 0d;
      DialogControl.Instance.continueTime = 0f;
   }
}
