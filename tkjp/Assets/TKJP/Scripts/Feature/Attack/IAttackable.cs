using UniRx.Async;
using System.Threading;
namespace Assets.Feature.Attack
{
    public interface IAttackable
    {
        //ここはこれで確定
        //渡すものを増やしたいならStateに追加
        //受け取るものを増やしたいならResultに追加
        UniTask<AttackResult> Attack(AttackState state);
    }
}