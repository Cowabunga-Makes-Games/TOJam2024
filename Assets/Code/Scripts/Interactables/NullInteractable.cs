using UnityEngine;

/// <summary>
/// Null Object for an interactable type.
/// </summary>
public class NullInteractable : MonoBehaviour, IInteractable {
    
    //========================================
    // Public Methods
    //========================================
        
    #region Public Methods

    public void OnHover() {}
    
    public void Select() {}
    public void Unselect() {}
    
    public void Use() {}
    public void CancelUse() {}

    #endregion
}
