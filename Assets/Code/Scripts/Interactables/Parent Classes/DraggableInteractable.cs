using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DraggableInteractable : MonoBehaviour, IInteractable {

    private Rigidbody _rBody;

    private Vector3 _startPosition;
    private Quaternion _startRotation;

    private void Awake() {
        this._startPosition = transform.position;
        this._startRotation = transform.rotation;

        this._rBody = GetComponent<Rigidbody>();
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
        this._rBody.velocity = Vector3.zero;
        transform.position = this._startPosition;
        transform.rotation = this._startRotation;
    }

    public void Use() {
        // Trigger some effect when this interactable is interacted with
    }

    public void CancelUse() {
        // Cancel the effect when this interactable is interacted with
    }

    #endregion
}
