using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchGameController : MonoBehaviour
{
    private RandomRoomMatcher _randomRoomMatcher;
    // Start is called before the first frame update
    void Start()
    {
        _randomRoomMatcher = gameObject.GetComponent<RandomRoomMatcher>();
        _randomRoomMatcher.ConnectStart();
    }

    public void StartMatching()
    {
        _randomRoomMatcher.JoinOrCreat();
    }
}
