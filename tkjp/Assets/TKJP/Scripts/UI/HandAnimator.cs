using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimator : MonoBehaviour
{
    public Animator rock;
    public Animator sissors;
    public Animator paper;

    private const string reset = "reset";
    private const string selected = "selected";
    private const string unselected = "unselected";

    private void OnEnable()
    {
        rock.SetTrigger(reset);
        sissors.SetTrigger(reset);
        paper.SetTrigger(reset);
    }

    public void SelectRock()
    {
        rock.SetTrigger(selected);
        sissors.SetTrigger(unselected);
        paper.SetTrigger(unselected);
    }
    public void SelectSissors()
    {
        rock.SetTrigger(unselected);
        sissors.SetTrigger(selected);
        paper.SetTrigger(unselected);
    }
    public void SelectPaper()
    {
        rock.SetTrigger(unselected);
        sissors.SetTrigger(unselected);
        paper.SetTrigger(selected);
    }
}
