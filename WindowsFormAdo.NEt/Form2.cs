using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//namespace
using System.Data.SqlClient;
using System.Configuration;


namespace WindowsFormAdo.NEt
{
    public partial class Form2 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommandBuilder scb;
        DataSet ds;
           
        public Form2()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        public DataSet GetAllEmps()
        {
            da = new SqlDataAdapter("select * from Employee", con);
            // assign PK to the col which in the DataSet
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            // emp is a table name given which is in the DataSet
            da.Fill(ds, "emp");// Fill method fire the select qry
            return ds;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmps();
                DataRow row = ds.Tables["emp"].NewRow();
                row["Name"] = txtName.Text;
                row["Salary"] = txtSalary.Text;
                // attach the new row to the ds
                ds.Tables["emp"].Rows.Add(row);
                //update() will reflect the changes to the DB
                int result = da.Update(ds.Tables["emp"]);
                if (result == 1)
                {
                    MessageBox.Show("Success ! Recored inserted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmps();
                DataRow row = ds.Tables["emp"].Rows.Find(txtId.Text);
                if(row!=null)
                {
                    row["Name"] = txtName.Text;
                    row["Salary"] = txtSalary.Text;
                    //update() will reflect the changes to the DB
                    int result = da.Update(ds.Tables["emp"]);
                    if(result==1)
                    {
                        MessageBox.Show("Success ! Record updated");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmps();
                DataRow row = ds.Tables["emp"].Rows.Find(txtId.Text);
                if(row!=null)
                {
                    row.Delete();

                    int result = da.Update(ds.Tables["emp"]);
                    if(result==1)
                    {
                        MessageBox.Show("Success ! record deleted");
                        txtId.Clear();
                        txtName.Clear();
                        txtSalary.Clear();
                    }
                    
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmps();
                DataRow row = ds.Tables["emp"].Rows.Find(txtId.Text);
                if (row != null)
                {
                    txtName.Text = row["Name"].ToString();
                    txtSalary.Text = row["Salary"].ToString();

                }
                else
                {
                    MessageBox.Show("Record not found");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            ds = GetAllEmps();
            EmpGrideViwe.DataSource = ds.Tables["emp"];
        }
    }
}
