using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(DragRotation))]
public class DraggableInteractable : MonoBehaviour, IInteractable {

    private Rigidbody _rBody;

    private Vector3 _startPosition;
    private Quaternion _startRotation;

    private DragRotation _dragRotation;
    
    [SerializeField] private float _tiltIntensity = 100f;
    [SerializeField] private float _timeToPour = 0.5f;

    private void Awake() {
        this._startPosition = transform.position;
        this._startRotation = transform.rotation;

        this._rBody = GetComponent<Rigidbody>();
        this._dragRotation = GetComponent<DragRotation>();
    }
    
    //========================================
    // Public Methods
    //========================================
        
    #region Public Methods

    public void OnHover() {
        // Trigger some special animation, SFX, etc.
    }

    public IInteractable Select(Vector3 mousePos, out GameObject objToDrag) {
        // Trigger some special animation, SFX, etc.
        // Note that this is the beginning of the dragging state
        objToDrag = gameObject;
        return this;
    }
    
    public void Unselect() {
        // The end of the drag state
        // Warp this object back to its original position and rotation
        transform.DOKill();
        
        this._rBody.velocity = Vector3.zero;
        transform.position = this._startPosition;
        transform.rotation = this._startRotation;
    }

    public void Use() {
        // Trigger some effect when this interactable is interacted with
        
        // TODO: Make this a parent class, using it to populate various types of draggable interactables that can be
        // used in different ways. (bottles to pour liquids, spice shakers, etc.)
        
        this._dragRotation.enabled = false;
        transform.DORotate(Vector3.forward * this._tiltIntensity, this._timeToPour, RotateMode.Fast);
    }

    public void CancelUse() {
        // Cancel the effect when this interactable is interacted with

        this._dragRotation.enabled = true;
        transform.DOKill();
        transform.DORotate(Vector3.zero, this._timeToPour, RotateMode.Fast);
    }

    #endregion
}
