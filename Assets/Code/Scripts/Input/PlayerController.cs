using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    
    private PlayerInputActions _playerInputActions;

    [SerializeField] private Camera _camera;

    private Coroutine _dragCoroutine;
    
    //========================================
    // Unity Methods
    //========================================
        
    #region Unity Methods

    private void Awake() {
        this._playerInputActions = new PlayerInputActions();
        this._playerInputActions.Enable();
    }

    // Update is called once per frame
    void Update() {
        
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
        
        print(hit.collider.gameObject);
        StartCoroutine(this.OnInteractableDrag(hit.collider.gameObject, interactable));
    }
    
    private void Unselect(InputAction.CallbackContext obj) {
        // this._currentInteractable.Unselect();
        // this._currentInteractable = this._nullInteractable;
    }

    private IEnumerator OnInteractableDrag(GameObject obj, IInteractable interactable) {
        yield return null;
    }
}
