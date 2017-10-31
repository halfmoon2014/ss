using System.Data;
using Service.Util;

namespace FM.Business
{
    public class WebLs
    {
        public DataSet GetLSXx(string lx)
        {
            SqlCommandString sqlstring = new SqlCommandString();
            ConnetString connstr = new ConnetString();
            Service.DAL.DALInterface execObj = new Service.DAL.DALInterface(null, connstr.GetDb(MySession.SessionHandle.Get("tzid"), MySession.SessionHandle.Get("userid")));
            return execObj.SubmitTextDataSet(sqlstring.GetLSDlxx(lx)+";"+sqlstring.GetLSDl(lx));
        }
    }
}
