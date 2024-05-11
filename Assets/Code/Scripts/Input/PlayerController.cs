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
        this._playerInputActions.Player.Enable();

        this._playerInputActions.Player.Select.performed += this.Select;
        this._playerInputActions.Player.Select.canceled += this.Unselect;
    }

    private void OnDisable() {
        this._playerInputActions.Player.Disable();

        this._playerInputActions.Player.Select.performed -= this.Select;
        this._playerInputActions.Player.Select.canceled -= this.Unselect;
    }
    
    #endregion
    
    private void Select(InputAction.CallbackContext obj) {
        if (!Physics.Raycast(_camera.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hit) ||
            !hit.collider.gameObject.TryGetComponent<IInteractable>(out IInteractable interactable)) return;
        
        this._isDraggingInteractable = true;
        this._currentInteractable = interactable;
        StartCoroutine(this.OnInteractableDrag(hit.collider.gameObject, interactable));
    }
    
    private void Unselect(InputAction.CallbackContext obj) {
        this._isDraggingInteractable = false;
        
        this._currentInteractable.Unselect();
        this._currentInteractable = this._nullInteractable;
    }

    private IEnumerator OnInteractableDrag(GameObject obj, IInteractable interactable) {
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
