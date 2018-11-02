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

namespace SQLDataDictionaryDisplay
{
    public partial class frmMain : Form
    {

        private string selectedDB = "";

        private SqlConnection sqlConn;

        private string[] ignoredDBs = { "master", "tempdb", "model", "msdb" };

        public frmMain()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedDB != cbDatabases.SelectedItem.ToString())
            {
                lbDBItems.Items.Clear();
                selectedDB = cbDatabases.SelectedItem.ToString();
            }
            using (sqlConn = new SqlConnection("Data Source=PL1\\MTCDB;Initial Catalog=" + selectedDB + "; TRUSTED_CONNECTION=True;"))
            {
                SqlCommand getTables = new SqlCommand("SELECT * FROM INFORMATION_SCHEMA.TABLES UNION SELECT ROUTINE_CATALOG, " +
                    "ROUTINE_SCHEMA, ROUTINE_NAME, ROUTINE_TYPE FROM INFORMATION_SCHEMA.ROUTINES " +
                    "WHERE ROUTINE_TYPE = 'PROCEDURE' ", sqlConn);
                
                SqlDataAdapter dbAdapt = new SqlDataAdapter(getTables);
                DataTable tables = new DataTable();
                dbAdapt.Fill(tables);

                foreach (DataRow row in tables.Rows)
                {
                    string itemName = row[2].ToString();
                    string itemSchema = row[1].ToString();
                    if(itemSchema == "dbo")
                    {
                        lbDBItems.Items.Add(itemName);
                    }
                    else
                    {
                        lbDBItems.Items.Add(itemSchema + "." + itemName);
                    }
                        
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                using (sqlConn = new SqlConnection("Data Source=PL1\\MTCDB;Initial Catalog=master; TRUSTED_CONNECTION=True;"))
                {

                    SqlCommand getDB = new SqlCommand("SELECT name FROM sys.databases", sqlConn);
                    SqlDataAdapter dbAdapt = new SqlDataAdapter(getDB);
                    DataTable databases = new DataTable();
                    dbAdapt.Fill(databases);
                    
                    foreach (DataRow row in databases.Rows)
                    {
                        string dbName = row[0].ToString();
                        if (!ignoredDBs.Contains(dbName) && !dbName.Contains("ReportServer"))
                        {
                            cbDatabases.Items.Add(dbName);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lbDBItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblDependent.Text = "";
            lblDepending.Text = "";
            lbDepending.Items.Clear();
            lbDependents.Items.Clear();
            DataSet dependencies = new DataSet();
            string item = lbDBItems.SelectedItem.ToString();
            try
            {
                using (sqlConn = new SqlConnection("Data Source=PL1\\MTCDB;Initial Catalog=" + selectedDB + "; TRUSTED_CONNECTION=True;"))
                {
                    SqlCommand dependProc = new SqlCommand("sp_depends", sqlConn);
                    dependProc.CommandType = CommandType.StoredProcedure;
                    SqlParameter itemName = new SqlParameter("@objname", item);
                    dependProc.Parameters.Add(itemName);

                    SqlDataAdapter dbAdapt = new SqlDataAdapter(dependProc);

                    dbAdapt.Fill(dependencies);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if(dependencies.Tables.Count == 0)
            {
                lbDependents.Items.Add(item + " has no items depending upon it.");
                lbDepending.Items.Add(item + " has no dependencies upon other items.");
            }
            else if(dependencies.Tables.Count == 1)
            {
                if(dependencies.Tables[0].Columns.Count == 2)
                {
                    lbDepending.Items.Add(item + " has no dependencies upon other items.");
                    lblDependent.Text = item + " has the following dependents:";

                    foreach(DataRow row in dependencies.Tables[0].Rows)
                    {
                        if(row[1].ToString() == "check cns")
                        {
                            lbDependents.Items.Add("The \"" + row[0].ToString() + "\" check constraint.");
                        }
                        else
                        {
                            lbDependents.Items.Add("The \"" + row[0].ToString() + "\" " + row[1].ToString() + ".");
                        }
                        
                    }
                    

                }
                else if(dependencies.Tables[0].Columns.Count == 5)
                {
                    
                    lbDependents.Items.Add(item + " has no items depending upon it.");
                    lblDepending.Text = item + " is dependent upon the...";
                    foreach (DataRow row in dependencies.Tables[0].Rows)
                    {
                        if(row[1].ToString() == "user table" || row[1].ToString() == "view")
                        {
                            if(row[4].ToString() != "" && row[4].ToString() != null)
                            {
                                
                                lbDepending.Items.Add("\"" + row[4].ToString() + "\" column of the " + row[0].ToString() + " " + row[1].ToString() + ".");
                            }
                            else
                            {
                                lbDepending.Items.Add(row[0].ToString() + " " + row[1].ToString() + ".");
                            }
                        }
                        else
                        {
                            lbDepending.Items.Add(row[1].ToString() + " called \"" + row[0].ToString() + "\".");
                        }

                    }
                }
            }
            else
            {
                lblDepending.Text = item + " is dependent upon the...";
                lblDependent.Text = item + " has the following dependents:";
                foreach (DataRow row in dependencies.Tables[0].Rows)
                {
                    if (row[1].ToString() == "user table" || row[1].ToString() == "view")
                    {
                        if (row[4].ToString() != "" && row[4].ToString() != null)
                        {
                            
                            lbDepending.Items.Add("\"" + row[4].ToString() + "\" column of the " + row[0].ToString() + " " + row[1].ToString() + ".");
                        }
                        else
                        {
                            lbDepending.Items.Add(item + " is dependent upon the " + row[0].ToString() + " " + row[1].ToString() + ".");
                        }
                    }
                    else
                    {
                        lbDepending.Items.Add(item + " is dependent upon the " + row[1].ToString() + " called \"" + row[0].ToString() + "\".");
                    }
                }
                foreach (DataRow row in dependencies.Tables[1].Rows)
                {
                    if (row[1].ToString() == "check cns")
                    {
                        lbDependents.Items.Add("The \"" + row[0].ToString() + "\" check constraint.");
                    }
                    else
                    {
                        lbDependents.Items.Add("The \"" + row[0].ToString() + "\" " + row[1].ToString() + ".");
                    }
                }
            }
        }
    }
}
