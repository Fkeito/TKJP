using System;
using UnityEngine;
using UniRx;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
namespace TKJP.Network
{
    public class RoomMatchingManager : MonoBehaviour
    {
        public TKJP.Common.Scene.Transition transition;
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
                    transition.Load();
                    room = true;

                })
                .AddTo(gameObject);
        }
    }
}