using System;

namespace MyTy
{
    public class ResultUtil<T>
    {

        public static Result<T> Success()
        {
            Result<T> result = new Result<T>
            {
                Errcode = 0,
                Errmsg = "成功"
            };
            return result;
        }
        public static Result<T> Success(T obj)
        {
            Result<T> result = new Result<T>
            {
                Errcode = 0,
                Data = obj,
                Errmsg = "成功"
            };
            return result;
        }

        public static Result<T> Success(T obj, string errorMsg)
        {
            Result<T> result = new Result<T>
            {
                Errcode = 0,
                Data = obj,
                Errmsg = errorMsg
            };
            return result;
        }
               

        public static Result<T> Error(int intCode, string strMsg)
        {
            Result<T> result = new Result<T>
            {
                Errcode = intCode,
                Errmsg = strMsg
            };
            return result;
        }
    }

    public class ResultUtil
    {

        public static Result<string> Success()
        {
            Result<string> result = new Result<string>
            {
                Errcode = 0,
                Errmsg = "成功",
                Data = ""
            };
            return result;
        }
        public static Result<string> Error(int intCode, string strMsg)
        {
            Result<string> result = new Result<string>
            {
                Errcode = intCode,
                Errmsg = strMsg
            };
            return result;
        }
    }

}


