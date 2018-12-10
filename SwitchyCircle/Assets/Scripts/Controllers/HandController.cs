using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandController : MonoBehaviour {

    #region Inspector Vars

    [SerializeField] private int speed;
    [SerializeField] private int direction;

    [SerializeField] private SpriteRenderer sprite;

    #endregion

    #region Memeber Vars

    public int Speed { get { return speed; } set { speed = value; } }
    public int Direction { get { return direction; } }

    #endregion

    #region Properties

    public Hand skin;

    private bool firstTime;

    #endregion

    #region Unity Methods

    void Start()
    {

        speed = 200;
        direction = -1;
        firstTime = true;

        sprite.sprite = skin.handSkin;

    }

    void Update () {

        //Rotate the hand
        transform.Rotate(0f, 0f, (speed*direction)*Time.deltaTime);

	}

    void OnTriggerEnter2D(Collider2D collision)
    {

        List<Color> colors = new List<Color>(GameManager.instance.colors);
        Image colliderSprite = collision.gameObject.GetComponent<Image>();

        //Check which color the hand entered
        for (int i = 0; i < colors.Count; i++)
        {

            if (colliderSprite.color == colors[i])
            {
                
                GameManager.instance.handHoverIndex = i;

                return;

            }

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        Image colliderSprite = collision.gameObject.GetComponent<Image>();

        //Check if hand passed its maching color
        if (colliderSprite.color == sprite.color && !firstTime)
        {

            GameManager.instance.messageState = 4;
            GameManager.instance.GameOver();

            return;

        }

    }

    #endregion

    #region Public Methods

    //Switch hand direction
    public void SwitchDirection() {

        firstTime = false;
        direction = -direction;

    }

    //Changes color and retruns its index
    public int ChangeColor() {

        List<Color> colors = new List<Color>(GameManager.instance.colors);
        List<Color> newColors = new List<Color>(colors);

        //Exlude current color to avoid color repeats
        for (int i = 0; i < newColors.Count; i++) {

            if (sprite.color == newColors[i]) {

                newColors.RemoveAt(i);

            }

        }

        //Set color from the new array
        sprite.color = newColors[Random.Range(0, newColors.Count)];

        //Get current color index
        for (int i = 0; i < colors.Count; i++)
        {

            if (sprite.color == colors[i])
            {

                return i;

            }

        }

        return 0;

    }

    #endregion

}
