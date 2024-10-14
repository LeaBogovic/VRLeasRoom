using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirrormatchange : MonoBehaviour
{
    public Material Clear;
    public Material Mirror;
    

    void Start()
    {

    }
    public void SetMatClear()
    {
        GetComponent<Renderer>().material = Clear;
    }
    public void SetMatMirror()
    {
        GetComponent<Renderer>().material = Mirror; 
    }
}
