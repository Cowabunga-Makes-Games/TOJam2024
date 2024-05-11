using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerInteractable : MonoBehaviour, IInteractable {
    
    [SerializeField] private GameObject _itemToSpawn;
    
    //========================================
    // Public Methods
    //========================================
        
    #region Public Methods

    public void OnHover() {
        // Trigger some special animation, SFX, etc.
    }

    public IInteractable Select(Vector3 mousePos, out GameObject objToDrag) {
        // Trigger some special animation, SFX, etc.
        
        // Gross, gross, gross without object pooling, but with the lack of time it'll have to do
        var newDraggable = Instantiate(this._itemToSpawn, mousePos, Quaternion.identity, transform);
        
        // The new object will be dragged, but won't be an interactable. Thus, this interactable type will do nothing
        // when unselected.
        objToDrag = newDraggable.gameObject;
        return this;
    }
    
    public void Unselect() {}

    public void Use() {
        // Trigger some effect when this interactable is interacted with
    }

    public void CancelUse() {
        // Cancel the effect when this interactable is interacted with
    }

    #endregion
    
}
