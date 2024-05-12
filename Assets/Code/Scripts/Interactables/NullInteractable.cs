using UnityEngine;

/// <summary>
/// Null Object for an interactable type.
/// </summary>
public class NullInteractable : IInteractable {
    
    //========================================
    // Public Methods
    //========================================
        
    #region Public Methods

    public void OnHover() {}

    public IInteractable Select(Vector3 mousePos, out GameObject obj) {
        obj = null;
        return this;
    }
    public void Unselect() {}
    
    public void Use() {}
    public void CancelUse() {}

    #endregion
}
