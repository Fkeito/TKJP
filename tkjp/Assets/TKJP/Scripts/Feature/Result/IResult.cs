using System;

namespace Assets.TKJP.Scripts.Feature.Result
{
    //全てのResultに共通する部分である、ResultEnumを持つ、という要素を
    //インターフェースとして抽出してる
    public interface IResult
    {
        ResultEnum Result { get; }
    }

    //上のインターフェースに対する、拡張メソッドを定義する
    public static class ResultExtension
    {
        public static bool IsSuucess(this IResult result)
        {
            return result.Result == ResultEnum.Success;
        }
        public static bool IsFailure(this IResult result)
        {
            return result.Result == ResultEnum.Failure;
        }
        public static string ToString(this IResult result)
        {
            return result.Result.ToString();
        }

        //メソッドチェーンっぽいの作ってみたかったんだよね～
        public static void CallResult(this IResult result, Action<ResultEnum> call)
        {
            if (result == null) return;
            call(result.Result);
        }
    }
}