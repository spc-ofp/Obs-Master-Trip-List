using NHibernate;
using System;
using System.Diagnostics;

public class SqlStatementInterceptor : EmptyInterceptor
{
    public override NHibernate.SqlCommand.SqlString OnPrepareStatement(NHibernate.SqlCommand.SqlString sql)
    {
        //Trace.WriteLine(sql.ToString());
        //Console.WriteLine(sql.ToString());
        //Debug.WriteLine(sql.ToString());
        return sql;
    }
}