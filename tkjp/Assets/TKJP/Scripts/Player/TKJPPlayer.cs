using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TKJP.Common.Container;
using TKJP.Feature.Hp;
using TKJP.UI;
using UniRx;
using Photon;
using Photon.Pun;
using Photon.Realtime;
namespace TKJP.Player
{
    public class TKJPPlayer : MonoBehaviour
    {
        private readonly HpHolder Hp = new HpHolder(100);
        [SerializeField] private HPBar HPBar;
        private HpAgent MyHp;
        private PhotonView view;
        public PhotonView head, right, left;
        public GameObject EnemyPrefab;

        TKJPEnemy enemy;

        private void Awake()
        {
            MyHp = new HpAgent(Hp);
            SceneContainer.BindConextObj(MyHp);
            SceneContainer.BindConextObj(this);

            view = GetComponent<PhotonView>();
            PhotonNetwork.AllocateViewID(view);
            PhotonNetwork.AllocateViewID(head);
            PhotonNetwork.AllocateViewID(right);
            PhotonNetwork.AllocateViewID(left);

            GetHp()
                .OnChangeHp
                .Subscribe(value =>
                {
                    HPBar.SetValue(value);
                    view.RPC("OnChangePlayerHp", RpcTarget.Others, value);
                })
                .AddTo(gameObject);
        }
        public IReadOnlyHpHolder GetHp()
        {
            return Hp;
        }
        public void ViewSyncro()
        {
            var headId = head.ViewID;
            var rightId = right.ViewID;
            var leftId = left.ViewID;
            GetComponent<PhotonView>().RPC("CreatreMeAsEnemy", RpcTarget.OthersBuffered, headId, rightId, leftId);
        }

        [PunRPC]
        private void CreatreMeAsEnemy(int headid, int rightid, int leftid)
        {
            enemy = Instantiate(EnemyPrefab).GetComponent<TKJPEnemy>();
            enemy.Constructor(headid, rightid, leftid);
        }
        [PunRPC]
        private void OnChangePlayerHp(int hp)
        {
            enemy.SetHP(hp);
        }
    }
}