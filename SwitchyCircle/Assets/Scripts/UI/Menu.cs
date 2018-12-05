using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour {

    public TextMeshProUGUI gemsUI;

    void Update () {
		
        gemsUI.text = GameManager.instance.gems.ToString();

    }

}
