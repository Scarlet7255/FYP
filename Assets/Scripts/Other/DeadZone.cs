
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

public class DeadZone : MonoBehaviour
{
    public PlayableDirector deadAni;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            DetectionTarget t = other.gameObject.GetComponent<DetectionTarget>();
            var ani = t.owner.GetComponent<Animator>();
            ani.applyRootMotion = true;
            ani.Play("Dying");
            deadAni.gameObject.SetActive(true);
            deadAni.Play();
            t.owner.GetComponent<NavMeshAgent>().speed = 0f;
        }
    }
}
