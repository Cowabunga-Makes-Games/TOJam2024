using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable {
    
    //========================================
    // Public Methods
    //========================================
        
    #region Public Methods

    public void OnHover();
    
    public void Select();
    public void Unselect();
    
    public void Use();
    public void CancelUse();

    #endregion
}
