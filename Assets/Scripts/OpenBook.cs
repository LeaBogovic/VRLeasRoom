using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBook : MonoBehaviour
{
    public GameObject Cover;
    public HingeJoint myHinge;

    void Start()
    {
        var myHinge = Cover.GetComponent<HingeJoint>();

        myHinge.useMotor = false;
    }
    public void OpenSesame()
    {
        myHinge.useMotor = true;
        Debug.Log("motor should be true");
    }
}
