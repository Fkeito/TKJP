using System;
using UnityEngine;
using UniRx;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
namespace TKJP.Network
{
    public class RoomMatchingManager : MonoBehaviour
    {
        private bool connect;
        private bool room;
        private void Start()
        {
            Debug.Log("awake");
            PhotonNetwork.ConnectUsingSettings();
            Observable
                .Interval(TimeSpan.FromSeconds(5))
                .Where(_ => PhotonNetwork.IsConnectedAndReady)
                .Where(_ => !connect)
                .Subscribe(_ => {
                    PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
                    Debug.Log("joinOrCreate");
                })
                .AddTo(gameObject);
            Observable
                .EveryUpdate()
                .Where(_ => PhotonNetwork.InRoom)
                .Where(_ => !room)
                .Subscribe(_ =>
                {
                    connect = true;
                    Debug.Log("InRoom");
                    SceneManager.LoadScene("OnlineBattle", LoadSceneMode.Single);
                    room = true;

                })
                .AddTo(gameObject);
        }
    }
}