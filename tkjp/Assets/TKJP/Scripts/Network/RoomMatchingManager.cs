using UnityEngine;
using UniRx;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
namespace TKJP.Network
{
    public class RoomMatchingManager : MonoBehaviourPunCallbacks
    {
        //public int PlayerFullCount = 2;
        private bool isFull = false;
        private void Awake()
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        public override void OnConnectedToMaster()
        {
            base.OnJoinedLobby();
            PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
        }
        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            SceneManager.LoadScene("OnlineBattle", LoadSceneMode.Single);
            /*
            Observable
                .EveryUpdate()
                .Where(_ => !isFull)
                .Subscribe(_ =>
                {
                    if (PhotonNetwork.CurrentRoom.PlayerCount == PlayerFullCount)
                    {
                        SceneManager.LoadScene("OnlineBattle", LoadSceneMode.Single);
                        isFull = true;
                    }
                })
                .AddTo(gameObject);*/
        }
    }
}