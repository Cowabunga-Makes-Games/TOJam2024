using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable {
    
    //========================================
    // Public Methods
    //========================================
        
    #region Public Methods

    public void OnHover();
    
    public IInteractable Select(Vector3 mousePos, out GameObject objToDrag);
    public void Unselect();
    
    public void Use();
    public void CancelUse();

    #endregion
}
