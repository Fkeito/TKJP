namespace TKJP.Feature.Attack
{
    //AttackStateの中身が増えたら、ここでゲッターのみ増やせば
    //コンストラクタにも追加しないとコンパイルエラーになるし
    //コンストラクタ呼び出してる側も全部コンパイルエラーになるから嬉しそうじゃない？
    public struct AttackState
    {
        public int Damage { get; }
        public AttackState(int damage)
        {
            Damage = damage;
        }
    }
}