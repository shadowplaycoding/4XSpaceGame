using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CloseUI : MonoBehaviour {

    
    public void ClosePanel()
    {
        if (this.transform.parent != null)
        {
            this.transform.parent.gameObject.SetActive(false);
        }
    }

    /*
   Copyright Shadowplay Coding 2017 - see www.shadowplaycoding.com for licensing details
   Removing this comment forfits any rights given to the user under licensing.
   */

}
