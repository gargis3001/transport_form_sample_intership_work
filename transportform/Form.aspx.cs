using System;
using System.Web.UI;
using Oracle.ManagedDataAccess.Client;

namespace TransportBill
{
    public partial class Form : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connectionString = "User Id=C##GARGI;Password=gargi125;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)))";

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    Response.Write("Connection Successful!");
                }
                catch (OracleException ex)
                {
                    Response.Write($"Oracle Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Response.Write($"General Error: {ex.Message}");
                }
            }
        }

        private void RetrieveBillData()
        {
            string billNo = txtBillNo.Text.Trim();
            if (string.IsNullOrEmpty(billNo))
            {
                Response.Write("Bill number cannot be empty.");
                return;
            }

            string connectionString = "User Id=C##GARGI;Password=gargi125;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCL)))";

            try
            {
                using (OracleConnection conn = new OracleConnection(connectionString))
                {
                    conn.Open();

                    // Query to fetch data based on Bill Number
                    string query = @"SELECT tm.FREIGHT_RATE AS RATE, tb.AMOUNT_LIMIT, tm.DISTANCE, tm.BASE_FREIGHT, 
                                     (tm.BASE_FREIGHT + vd.LOADING_CHARGES + tb.EXTRA_CHARGES) AS NET_VALUE
                                     FROM TRANSPORT_MASTER tm
                                     JOIN TRANSPORT_BILL tb ON tm.TRANSPORT_ID = tb.VEHICLE_ID
                                     JOIN VEHICLE_DETAILS vd ON vd.VEHICLE_ID = tb.VEHICLE_ID
                                     WHERE tb.BILL_NO = :billNo";

                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(":billNo", OracleDbType.Int32).Value = billNo;

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtRate.Text = reader["RATE"]?.ToString() ?? "N/A";
                                txtAmountLimit.Text = reader["AMOUNT_LIMIT"]?.ToString() ?? "N/A";
                                txtDistance.Text = reader["DISTANCE"]?.ToString() ?? "N/A";
                                txtBaseFreight.Text = reader["BASE_FREIGHT"]?.ToString() ?? "N/A";
                                txtNetValue.Text = reader["NET_VALUE"]?.ToString() ?? "N/A";
                                 txtExtraCharges.Text = reader["EXTRA_CHARGES"]?.ToString() ?? "N/A"; 
                            }
                            else
                            {
                                Response.Write("No data found for the given bill number.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write($"Database connection error: {ex.Message}");
            }
        }

        // Method to save data to TRANSPORT_BILL table
        protected void SaveData(object sender, EventArgs e)
        {
            string connectionString = "User Id=C##GARGI;Password=gargi125;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCL)))";

            try
            {
                using (OracleConnection conn = new OracleConnection(connectionString))
                {
                    conn.Open();

                    // Query to insert data into TRANSPORT_BILL table
                    string insertQuery = @"
                INSERT INTO TRANSPORT_BILL 
                (BILL_NO, VEHICLE_ID, BILL_DATE, CN_NO, CN_DATE, BASE_FREIGHT, SHORTAGE_CHARGE, EXTRA_CHARGES, 
                LOADING_CHARGES, NET_VALUE, AMOUNT_LIMIT, FORM) 
                VALUES 
                (:billNo, :vehicleId, :billDate, :cnNo, :cnDate, :baseFreight, :shortageCharge, :extraCharges, 
                :loadingCharges, :netValue, :amountLimit, :form)";

                    using (OracleCommand cmd = new OracleCommand(insertQuery, conn))
                    {
                        // Parameters from your form controls
                        cmd.Parameters.Add(":billNo", OracleDbType.Int32).Value = txtBillNo.Text.Trim();
                        cmd.Parameters.Add(":vehicleId", OracleDbType.Int32).Value = txtVehNo.Text.Trim();
                        cmd.Parameters.Add(":billDate", OracleDbType.Date).Value = DateTime.Now;
                        cmd.Parameters.Add(":cnNo", OracleDbType.Varchar2).Value = txtCNNo.Text.Trim();
                        cmd.Parameters.Add(":cnDate", OracleDbType.Date).Value = DateTime.Now;
                        cmd.Parameters.Add(":baseFreight", OracleDbType.Decimal).Value = decimal.TryParse(txtBaseFreight.Text.Trim(), out var baseFreight) ? baseFreight : 0;
                        cmd.Parameters.Add(":shortageCharge", OracleDbType.Decimal).Value = decimal.TryParse(txtShortageCharge.Text.Trim(), out var shortageCharge) ? shortageCharge : 0;
                        cmd.Parameters.Add(":extraCharges", OracleDbType.Decimal).Value = decimal.TryParse(txtExtraCharges.Text.Trim(), out var extraCharges) ? extraCharges : 0;
                        cmd.Parameters.Add(":loadingCharges", OracleDbType.Decimal).Value = decimal.TryParse(txtLoadingCharges.Text.Trim(), out var loadingCharges) ? loadingCharges : 0;
                        cmd.Parameters.Add(":netValue", OracleDbType.Decimal).Value = decimal.TryParse(txtNetValue.Text.Trim(), out var netValue) ? netValue : 0;
                        cmd.Parameters.Add(":amountLimit", OracleDbType.Decimal).Value = decimal.TryParse(txtAmountLimit.Text.Trim(), out var amountLimit) ? amountLimit : 0;
                        cmd.Parameters.Add(":form", OracleDbType.Varchar2).Value = txtVehType.Text.Trim();

                        cmd.ExecuteNonQuery();
                        Response.Write("Data saved successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write($"Error: {ex.Message}");
            }
        }

        // Method to handle button click for form submission
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            RetrieveBillData();
            SaveData(sender, e);
        }
    }
}

