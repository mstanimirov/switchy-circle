using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleController : MonoBehaviour {

    #region Inspector Vars

    [SerializeField] private int speed;
    [SerializeField] private int direction;

    #endregion

    #region Memeber Vars

    public int Speed { get { return speed; } set { speed = value; } }
    public int Direction { get { return direction; } set { direction = value; } }

    #endregion

    void Start()
    {

        speed = 20;
        direction = 0;

    }

    void Update () {

        if (direction == 0) {

            return;

        }

        //Rotate circle
        transform.Rotate(0f, 0f, (speed * direction) * Time.deltaTime);

    }

    public void SwitchDirection()
    {

        if (direction == 0)
        {

            direction = -1;
            return;

        }

        direction = -direction;

    }

}
