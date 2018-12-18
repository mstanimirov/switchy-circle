using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hand", menuName = "Hand Skin")]
public class Hand : ScriptableObject {

    public Sprite handSkin;

    public GameObject explosion_red;
    public GameObject explosion_blue;

    public int  price;
    public bool isLocked;

}
