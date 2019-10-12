using Assets.Feature.Result;
namespace Assets.Feature.Attack
{
    public struct AttackResult : IResult
    {
        public ResultEnum ResultEnum { get; }
        public AttackResult(ResultEnum result)
        {
            ResultEnum = result;
        }


        //いちいちコンストラクタ呼ぶのめんどいので
        //よく使うコンストラクタはstaticで定義してあげてもよさそうだよね
        public static AttackResult Success()
        {
            return new AttackResult(ResultEnum.Success);
        }
        public static AttackResult Miss()
        {
            return new AttackResult(ResultEnum.Failure);
        }
    }
}