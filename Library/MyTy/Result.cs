using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyTy
{
    public class Result<T>
    {
        private int errcode;
        private string errmsg;
        private T data;

        public int Errcode
        {
            get
            {
                return errcode;
            }

            set
            {
                errcode = value;
            }
        }

        public string Errmsg
        {
            get
            {
                return errmsg;
            }

            set
            {
                errmsg = value;
            }
        }

        public T Data
        {
            get
            {
                return data;
            }

            set
            {
                data = value;
            }
        }
    }
}
