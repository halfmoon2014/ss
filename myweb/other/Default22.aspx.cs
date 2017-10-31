using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Default22 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        DataTable table1 = null;
        DataTable table2 = null;

        table1 = new DataTable();
        table1.Columns.Add("symbol", typeof(string));
        table1.Columns.Add("side", typeof(int));
        table1.Columns.Add("qty", typeof(int));
        table1.Rows.Add(new object[] { "HSI", -1, 9000 });
        table1.Rows.Add(new object[] { "TOPIX", 1, 2000 });
        dataGridView1.DataSource = table1.DefaultView;

        table2 = new DataTable();
        table2.Columns.Add("symbol", typeof(string));
        table2.Columns.Add("side", typeof(int));
        table2.Columns.Add("qty", typeof(int));
        table2.Rows.Add(new object[] { "HSI", 1, 6000 });
        table2.Rows.Add(new object[] { "AEX", -1, 5000 });
        dataGridView2.DataSource = table2.DefaultView;

        DataTable t3 = Join(table1, table2,
            new DataColumn[] { table1.Columns[0] },
            new DataColumn[] { table2.Columns[0] },
            true, true);

        dataGridView3.DataSource = t3.DefaultView;

        dataGridView1.DataBind();
        dataGridView2.DataBind();
        dataGridView3.DataBind();

    }
    private DataTable Join(DataTable left, DataTable right,
            DataColumn[] leftCols, DataColumn[] rightCols,
            bool includeLeftJoin, bool includeRightJoin)
    {
        DataTable result = new DataTable("JoinResult");
        using (DataSet ds = new DataSet())
        {
            ds.Tables.AddRange(new DataTable[] { left.Copy(), right.Copy() });
            DataColumn[] leftRelationCols = new DataColumn[leftCols.Length];
            for (int i = 0; i < leftCols.Length; i++)
                leftRelationCols[i] = ds.Tables[0].Columns[leftCols[i].ColumnName];

            DataColumn[] rightRelationCols = new DataColumn[rightCols.Length];
            for (int i = 0; i < rightCols.Length; i++)
                rightRelationCols[i] = ds.Tables[1].Columns[rightCols[i].ColumnName];

            //create result columns
            for (int i = 0; i < left.Columns.Count; i++)
                result.Columns.Add(left.Columns[i].ColumnName, left.Columns[i].DataType);
            for (int i = 0; i < right.Columns.Count; i++)
            {
                string colName = right.Columns[i].ColumnName;
                while (result.Columns.Contains(colName))
                    colName += "_2";
                result.Columns.Add(colName, right.Columns[i].DataType);
            }

            //add left join relations
            DataRelation drLeftJoin = new DataRelation("rLeft", leftRelationCols, rightRelationCols, false);
            ds.Relations.Add(drLeftJoin);

            //join
            result.BeginLoadData();
            foreach (DataRow parentRow in ds.Tables[0].Rows)
            {
                DataRow[] childrenRowList = parentRow.GetChildRows(drLeftJoin);
                if (childrenRowList != null && childrenRowList.Length > 0)
                {
                    object[] parentArray = parentRow.ItemArray;
                    foreach (DataRow childRow in childrenRowList)
                    {
                        object[] childArray = childRow.ItemArray;
                        object[] joinArray = new object[parentArray.Length + childArray.Length];
                        Array.Copy(parentArray, 0, joinArray, 0, parentArray.Length);
                        Array.Copy(childArray, 0, joinArray, parentArray.Length, childArray.Length);
                        result.LoadDataRow(joinArray, true);
                    }
                }
                else //left join
                {
                    if (includeLeftJoin)
                    {
                        object[] parentArray = parentRow.ItemArray;
                        object[] joinArray = new object[parentArray.Length];
                        Array.Copy(parentArray, 0, joinArray, 0, parentArray.Length);
                        result.LoadDataRow(joinArray, true);
                    }
                }
            }

            if (includeRightJoin)
            {
                //add right join relations
                DataRelation drRightJoin = new DataRelation("rRight", rightRelationCols, leftRelationCols, false);
                ds.Relations.Add(drRightJoin);

                foreach (DataRow parentRow in ds.Tables[1].Rows)
                {
                    DataRow[] childrenRowList = parentRow.GetChildRows(drRightJoin);
                    if (childrenRowList == null || childrenRowList.Length == 0)
                    {
                        object[] parentArray = parentRow.ItemArray;
                        object[] joinArray = new object[result.Columns.Count];
                        Array.Copy(parentArray, 0, joinArray,
                            joinArray.Length - parentArray.Length, parentArray.Length);
                        result.LoadDataRow(joinArray, true);
                    }
                }
            }

            result.EndLoadData();
        }

        return result;
    }


}
