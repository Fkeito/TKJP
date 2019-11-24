using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TKJP.Battle.State;

public class Timebar : MonoBehaviour
{
    public SpriteMask mask;
    public SpriteRenderer bar;

    //private Color green = new Color(20, 255, 40, 255);
    //private Color yellow = new Color(255, 210, 0, 255);
    //private Color red = new Color(255, 40, 20, 255);

    void Start()
    {
        Manager.AddListenerOnChangeState(state =>
        {
            switch (state) {
                case State.Janken: 
                    this.gameObject.SetActive(true);
                    onTimeChanged(1f);
                    break;
                case State.Result:
                    this.gameObject.SetActive(false);
                    break;
            }
        });

        this.gameObject.SetActive(false);
    }

    public void onTimeChanged(float time)
    {
        float t = 1 - time;

        mask.alphaCutoff = t;
        //if (t > 0.5f) bar.color = green;
        //else if (t > 0.25f) bar.color = yellow;
        //else bar.color = red;
    }
}
