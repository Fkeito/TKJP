using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTest : MonoBehaviour
{
    private MeshRenderer mr;

    private void Start()
    {
        mr = this.GetComponent<MeshRenderer>();
    }
    public void OnClick()
    {
        mr.material.color = Color.red;
        delayMethod(() =>
        {
            mr.material.color = Color.white;
        });
    }

    private IEnumerator delayMethod(System.Action action)
    {
        yield return 5f;
        action();
    }
}
