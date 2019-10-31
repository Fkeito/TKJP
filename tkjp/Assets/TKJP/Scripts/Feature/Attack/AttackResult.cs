using TKJP.Feature.Result;
namespace TKJP.Feature.Attack
{
    public struct AttackResult : IResult
    {
        public ResultEnum Result { get; }
        public AttackResult(ResultEnum result)
        {
            Result = result;
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