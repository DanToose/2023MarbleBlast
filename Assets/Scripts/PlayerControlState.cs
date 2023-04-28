using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlState : MonoBehaviour
{
    public Player playerScript;


    // Start is called before the first frame update
    void Start()
    {
        playerScript = this.GetComponent<Player>();
    }

    public void TurnPlayerOff()
    {
        playerScript.enabled = false;
    }

    public void TurnPlayerOn()
    {
        playerScript.enabled = true;
    }
}
