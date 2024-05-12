using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    
    private PlayerInputActions _playerInputActions;

    [SerializeField] private Camera _camera;

    private IInteractable _currentInteractable, _nullInteractable;

    public float interactableDragSpeed;
    private WaitForFixedUpdate _dragPhysicsUpdate;
    private bool _isDraggingInteractable;
    private int _maxTilt = 180;
    private int _defaultTilt = 0;
    private GameObject objToDrag;
    
    //========================================
    // Unity Methods
    //========================================
        
    #region Unity Methods

    private void Awake() {
        this._currentInteractable = this._nullInteractable = new NullInteractable();
        
        this._playerInputActions = new PlayerInputActions();
        this._playerInputActions.Enable();
    }

    private void OnEnable() {
        this._playerInputActions.Player.Select.performed += this.SelectInteractable;
        this._playerInputActions.Player.Select.canceled += this.UnselectInteractable;
        
        this._playerInputActions.Player.Use.performed += this.UseInteractable;
        this._playerInputActions.Player.Use.canceled += this.CancelUseInteractable;
        
        this._playerInputActions.Player.Enable();
    }

    private void OnDisable() {
        this._playerInputActions.Player.Select.performed -= this.SelectInteractable;
        this._playerInputActions.Player.Select.canceled -= this.UnselectInteractable;
        
        this._playerInputActions.Player.Use.performed -= this.UseInteractable;
        this._playerInputActions.Player.Use.canceled -= this.CancelUseInteractable;
        
        this._playerInputActions.Player.Disable();
    }
    
    #endregion
    
    private void SelectInteractable(InputAction.CallbackContext obj) {
        if (!Physics.Raycast(_camera.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hit) ||
            !hit.collider.gameObject.TryGetComponent<IInteractable>(out IInteractable interactable)) return;
        
        this._isDraggingInteractable = true;
        
        // Delegate the decision of which gameObject to manipulate with the click-and-drag mechanic to the clicked interactable.
        // This is crucial when considering cases where the object you select may spawn an entity to be dragged
        this._currentInteractable = interactable.Select(hit.point, out objToDrag);

        if (objToDrag is null) {
            Debug.LogWarning("The selected interactable does not have an associated GameObject! " +
                             "Please reconsider its interactable component type.");
            return;
        }
        
        StartCoroutine(this.OnInteractableDrag(objToDrag));
    }
    
    private void UnselectInteractable(InputAction.CallbackContext obj) {
        this._isDraggingInteractable = false;
        
        this._currentInteractable.Unselect();
        this._currentInteractable = this._nullInteractable;
    }

    private void UseInteractable(InputAction.CallbackContext obj) {
        this._currentInteractable.Use();
    }
    
    private void CancelUseInteractable(InputAction.CallbackContext obj) {
        this._currentInteractable.CancelUse();
    }

    private IEnumerator OnInteractableDrag(GameObject obj) {
        if (!obj.TryGetComponent<Rigidbody>(out var rb)) {
            Debug.LogError("Attempting to move an interactable without a rigidbody. Please add a rigidbody component: " + obj);
        }
        
        // Maintain the distance between the camera and the selected interactable.
        // This could be overridden later with a hand grabbing animation and fixing the held interactable at a certain
        // distance from the camera.
        var vCameraToObject = Vector3.Distance(obj.transform.position, this._camera.transform.position);
        
        while (this._isDraggingInteractable) {
            var mouseRay = this._camera.ScreenPointToRay(Mouse.current.position.ReadValue());

            rb.velocity = (mouseRay.GetPoint(vCameraToObject) - obj.transform.position) * interactableDragSpeed; 
            
            yield return this._dragPhysicsUpdate;
        }
    }
}
