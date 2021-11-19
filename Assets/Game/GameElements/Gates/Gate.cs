using UnityEngine;

public class Gate : MonoBehaviour
{
    private Animator anim;

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    // TODO: rename to just Open
    // Use past tense only on events
    public void GateOpened()
    {
        anim?.SetTrigger("Activate");
    }
}
