using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timebar : MonoBehaviour
{
    public SpriteMask mask;
    public SpriteRenderer bar;

    public void onTimeChanged(float time)
    {
        mask.alphaCutoff = time;
    }
}
