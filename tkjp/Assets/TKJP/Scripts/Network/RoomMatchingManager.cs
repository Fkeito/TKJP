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
        public int PlayerFullCount = 2;
        private bool isFull = false;
        private void Start()
        {
            PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
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
                .AddTo(gameObject);
        }
    }
}