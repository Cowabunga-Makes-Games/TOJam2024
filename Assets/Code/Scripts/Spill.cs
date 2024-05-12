using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spill : MonoBehaviour
{
    private ParticleSystem myParticleSystem;
    
    // Start is called before the first frame update
    void Start()
    {
        myParticleSystem = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {   
        Debug.Log("Forward: " + transform.up);
        Debug.Log("Angle: " + Vector3.Angle(Vector3.down, transform.up));
        if (Vector3.Angle(Vector3.down, transform.up) < 90f)
        {   
            Debug.Log("Angle is <= 90.");
            myParticleSystem.Play();
        }
        else
        {
            Debug.Log("Angle is greater than 90.");
            myParticleSystem.Stop();
        }
    }
}
