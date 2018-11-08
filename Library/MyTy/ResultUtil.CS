using System;

namespace MyTy
{
    public class ResultUtil<T>
    {
        public static Result<T> success(T obj)
        {
            Result<T> result = new Result<T>();
            result.Errcode = 0;
            result.Data=obj;
            result.Errmsg="成功";
            return result;
        }

        public static Result<T> success(T obj, String errorMsg)
        {
            Result<T> result = new Result<T>();
            result.Errcode=0;
            result.Data = obj;
            result.Errmsg=errorMsg;
            return result;
        }
               

        public static Result<T> error(int intCode, String strMsg)
        {
            Result<T> result = new Result<T>();
            result.Errcode=intCode;
            result.Errmsg=strMsg;
            return result;
        }
    }
}
