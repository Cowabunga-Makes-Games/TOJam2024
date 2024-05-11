using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DragRotation : MonoBehaviour {

    [Range(0, 2)]
    public float _rotationIntensity = 1f;
    [Range(0, 1)]
    public float _timeToRotate = 0.5f;
    private Rigidbody _rBody;
    
    private void Awake() {
        this._rBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        // Capture the velocity of the draggable object and push the rotation of this object in the dampened opposite direction
        var velocity = this._rBody.velocity.x * this._rotationIntensity;

        // Rotate the draggable object with more intensity along the z-axis than the x-axis
        var rotation = transform.rotation;
        var targetRotation = new Vector3(rotation.x - velocity * 0.5f, 0f, rotation.z - velocity);
        transform.DORotate(targetRotation, this._timeToRotate, RotateMode.Fast);
    }
}
