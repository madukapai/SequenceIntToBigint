using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace SequenceIntToBigint
{
    public partial class frmMain : Form
    {
        SqlConnection SqlConn = null;
        SqlDataAdapter SqlAdp = null;
        List<KeyColumnModel> objColumn, objTable, objKeyColumn;
        int intConvertIndex, intSequenceIndex, intTableIndex, intColumnIndex;
        bool blEnableEdit = false;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            txtDbConn.Text = ConfigurationManager.ConnectionStrings["DbConn"].ToString();
        }

        /// <summary>
        /// 連接資料庫的動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConnDatabase_Click(object sender, EventArgs e)
        {
            blEnableEdit = false;

            // 先連接到資料庫
            SqlConn = new SqlConnection(txtDbConn.Text);

            // 找出Sequence
            string strSql = "Select name As Sequence FROM sys.sequences Where maximum_value <= 2147483647 Order By name";
            SqlAdp = new SqlDataAdapter(strSql, SqlConn);
            DataTable objSeq = new DataTable();
            SqlAdp.Fill(objSeq);

            // 找出所有的Table
            strSql = "Select TABLE_NAME FROM [INFORMATION_SCHEMA].[TABLES]";
            SqlAdp = new SqlDataAdapter(strSql, SqlConn);
            DataTable dtTable = new DataTable();
            SqlAdp.Fill(dtTable);
            objTable = new List<KeyColumnModel>();
            for (int i = 0; i < dtTable.Rows.Count; i++)
            {
                objTable.Add(
                    new KeyColumnModel()
                    {
                        TableName = dtTable.Rows[i]["TABLE_NAME"].ToString(),
                    }
                    );
            }

            // 找出所有的欄位
            strSql = "Select * from [INFORMATION_SCHEMA].[COLUMNS]";
            SqlAdp = new SqlDataAdapter(strSql, SqlConn);
            DataTable dtColumn = new DataTable();
            SqlAdp.Fill(dtColumn);
            objColumn = new List<KeyColumnModel>();
            for (int i = 0; i < dtColumn.Rows.Count; i++)
            {
                objColumn.Add(
                    new KeyColumnModel()
                    {
                        TableName = dtColumn.Rows[i]["TABLE_NAME"].ToString(),
                        ColumnName = dtColumn.Rows[i]["COLUMN_NAME"].ToString()
                    }
                    );
            }

            // 找出有可能對應的資料表以及欄位
            strSql = "Select * from [INFORMATION_SCHEMA].[KEY_COLUMN_USAGE]";
            SqlAdp = new SqlDataAdapter(strSql, SqlConn);
            DataTable dtKeyColumn = new DataTable();
            SqlAdp.Fill(dtKeyColumn);
            objKeyColumn = new List<KeyColumnModel>();
            for (int i = 0; i < dtKeyColumn.Rows.Count; i++)
            {
                objKeyColumn.Add(
                    new KeyColumnModel()
                    {
                        ConstraintName = dtKeyColumn.Rows[i]["CONSTRAINT_NAME"].ToString(),
                        TableName = dtKeyColumn.Rows[i]["TABLE_NAME"].ToString(),
                        ColumnName = dtKeyColumn.Rows[i]["COLUMN_NAME"].ToString()
                    }
                    );
            }

            // 放入到GridView之中
            gvSequence.DataSource = objSeq;
            for (int i = 0; i < gvSequence.Columns.Count; i++)
            {
                if (gvSequence.Columns[i].Name == "Sequence")
                    intSequenceIndex = i;

                if (gvSequence.Columns[i].Name == "TableName")
                    intTableIndex = i;

                if (gvSequence.Columns[i].Name == "ColumnName")
                    intColumnIndex = i;

                if (gvSequence.Columns[i].Name == "Convert")
                    intConvertIndex = i;
            }


            // 依序放入有可能對應的資料表以及其欄位
            for (int i = 0; i < gvSequence.Rows.Count; i++)
            {
                string strSequence = gvSequence.Rows[i].Cells[intSequenceIndex].Value.ToString();
                DataGridViewComboBoxCell ddlTable = (DataGridViewComboBoxCell)gvSequence.Rows[i].Cells[intTableIndex];
                DataGridViewComboBoxCell ddlColumn = (DataGridViewComboBoxCell)gvSequence.Rows[i].Cells[intColumnIndex];
                DataGridViewCheckBoxCell cbxConvert = (DataGridViewCheckBoxCell)gvSequence.Rows[i].Cells[intConvertIndex];

                string strTableName = objKeyColumn.Where(x => x.ColumnName == strSequence).Select(c => c.TableName).FirstOrDefault() ?? "";

                var objTableData = objTable.Select(c => c.TableName).ToList();
                ddlTable.Value = strTableName;
                ddlTable.Items.Add("-");
                for (int d = 0; d < objTableData.Count; d++)
                    ddlTable.Items.Add(objTableData[d]);

                // 放入Column
                cbxConvert.Value = false;
                if (!string.IsNullOrEmpty(strTableName))
                {
                    ddlColumn.DataSource = objColumn.Where(x => x.TableName == strTableName).Select(c => c.ColumnName).ToList();
                    ddlColumn.Value = objKeyColumn.Where(x => x.TableName == strTableName).Select(c => c.ColumnName).FirstOrDefault() ?? "";

                    // 打勾
                    cbxConvert.Value = true;
                }
            }

            blEnableEdit = true;
        }

        /// <summary>
        /// 欄位的值有變更的動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvSequence_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == intTableIndex && blEnableEdit)
            {
                string strTableName = gvSequence.Rows[e.RowIndex].Cells[intTableIndex].Value?.ToString();
                DataGridViewComboBoxCell ddlColumn = (DataGridViewComboBoxCell)gvSequence.Rows[e.RowIndex].Cells[intColumnIndex];
                ddlColumn.DataSource = null;

                // 放入Column
                if (!string.IsNullOrEmpty(strTableName))
                {
                    ddlColumn.DataSource = objColumn.Where(x => x.TableName == strTableName).Select(c => c.ColumnName).ToList();
                    ddlColumn.Value = objKeyColumn.Where(x => x.TableName == strTableName).Select(c => c.ColumnName).FirstOrDefault() ?? "";
                }
                else
                {
                    ddlColumn.Items.Add("-");
                }
            }
        }

        /// <summary>
        /// 進行類型轉換
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConvert_Click(object sender, EventArgs e)
        {
            // 先找出當下的順序代碼
            string strSql = "Select name As Sequence, current_value FROM sys.sequences Where maximum_value <= 2147483647 Order By name";
            SqlAdp = new SqlDataAdapter(strSql, SqlConn);
            DataTable objSeq = new DataTable();
            SqlAdp.Fill(objSeq);
            List<SequenceValueModel> objSeqValue = new List< SequenceValueModel>();
            for (int i=0; i<objSeq.Rows.Count; i++)
            {
                objSeqValue.Add(new SequenceValueModel()
                {
                    CurrentValue = int.Parse(objSeq.Rows[i]["current_value"].ToString()),
                    Sequence = objSeq.Rows[i]["Sequence"].ToString(),
                });
            }

            SqlTransaction objTrans = SqlConn.BeginTransaction();
            SqlCommand SqlCmd = new SqlCommand();
            SqlCmd.Connection = SqlConn;

            try
            {
                SqlCmd.Connection.Open();

                for (int i = 0; i < gvSequence.Rows.Count; i++)
                {
                    string strSequence = gvSequence.Rows[i].Cells[intSequenceIndex].Value.ToString();
                    DataGridViewComboBoxCell ddlTable = (DataGridViewComboBoxCell)gvSequence.Rows[i].Cells[intTableIndex];
                    DataGridViewComboBoxCell ddlColumn = (DataGridViewComboBoxCell)gvSequence.Rows[i].Cells[intColumnIndex];
                    DataGridViewCheckBoxCell cbxConvert = (DataGridViewCheckBoxCell)gvSequence.Rows[i].Cells[intConvertIndex];

                    // 進行更新動作
                    if ((bool)cbxConvert.Value)
                    {
                        // 先刪除Sequence
                        strSql = "DROP SEQUENCE " + strSequence;
                        SqlCmd.CommandText = strSql;
                        SqlCmd.ExecuteNonQuery();

                        // 重新建立Sequence
                        int intCurrentValue = objSeqValue.Where(x => x.Sequence == strSequence).Select(c => c.CurrentValue).FirstOrDefault();
                        strSql = "CREATE SEQUENCE " + strSequence + " As bigint CYCLE MINVALUE 1 MAXVALUE 9223372036854775807 INCREMENT BY 1 START WITH " + intCurrentValue.ToString();
                        SqlCmd.CommandText = strSql;
                        SqlCmd.ExecuteNonQuery();

                        // 更新資料表
                        if (ddlTable.Value.ToString() != "-")
                        {
                            var objKey = objKeyColumn.Where(x => x.ColumnName == strSequence).FirstOrDefault();

                            if (objKey != null)
                            {
                                strSql = $"ALTER TABLE [{objKey.TableName}] DROP CONSTRAINT [{objKey.ConstraintName}];";
                                strSql += $"ALTER TABLE [{objKey.TableName}] ALTER COLUMN [{objKey.ColumnName}] bigint NOT NULL;";

                                strSql += $@"ALTER TABLE [{objKey.TableName}] ADD CONSTRAINT [{objKey.ConstraintName}] PRIMARY KEY CLUSTERED
                                        ([{objKey.ColumnName}] ASC) WITH 
                                        (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];";
                                SqlCmd.CommandText = strSql;
                                SqlCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }

                objTrans.Commit();
                SqlCmd.Connection.Close();

                MessageBox.Show("完成所有轉換，請重新進行資料庫連線並重整順序清單畫面", "順序轉移", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                objTrans.Rollback();
                MessageBox.Show("轉換失敗:" + ex.Message, "順序轉移", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public class KeyColumnModel
        {
            public string ConstraintName { get; set; }
            public string TableName { get; set; }
            public string ColumnName { get; set; }
        }

        public class SequenceValueModel
        {
            public string Sequence { get; set; }
            public int CurrentValue { get; set; }
        }
    }
}
