using UnityEngine;
using UnityEngine.UI;
public class Ch1DoorCheck : MonoBehaviour
{
    public Door door;
    public Button button;

    private void Start()
    {
        if (!door.Lock) button.interactable = false;
    }
}
