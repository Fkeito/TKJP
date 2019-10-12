namespace Assets.Feature.Attack
{
    public interface IAttackable
    {
        //ここはこれで確定
        //渡すものを増やしたいならStateに追加
        //受け取るものを増やしたいならResultに追加
        AttackResult Attack(AttackState state);
    }
}