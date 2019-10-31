//AttackStateの中身が増えたら、ここでゲッターのみ増やせば
//コンストラクタにも追加しないとコンパイルエラーになるし
//コンストラクタ呼び出してる側も全部コンパイルエラーになるから嬉しそうじゃない？

namespace Assets.TKJP.Scripts.Feature.Attack
{
    public struct AttackState
    {
        public int Damage { get; }
        public AttackState(int damage)
        {
            Damage = damage;
        }
    }
}