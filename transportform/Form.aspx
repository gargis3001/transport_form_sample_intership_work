<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form.aspx.cs" Inherits="TransportBill.Form" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Freight Bill Form</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f9f9f9;
            margin: 0;
            padding: 0;
        }

        .container {
            max-width: 900px;
            margin: 30px auto;
            padding: 20px;
            background-color: #fff;
            border: 1px solid #ccc;
            border-radius: 8px;
            box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
        }

        h2 {
            text-align: center;
            margin-bottom: 20px;
        }

        .row {
            display: flex;
            justify-content: space-between;
            margin-bottom: 15px;
        }

        .label {
            flex-basis: 30%;
            font-weight: bold;
        }

        .field {
            flex-basis: 65%;
        }

            .field input, .field select {
                width: 100%;
                padding: 8px;
                border: 1px solid #ccc;
                border-radius: 4px;
            }

        .button-row {
            text-align: center;
            margin-top: 20px;
        }

            .button-row button {
                padding: 10px 20px;
                border: none;
                border-radius: 4px;
                background-color: #007bff;
                color: white;
                cursor: pointer;
            }

                .button-row button:hover {
                    background-color: #0056b3;
                }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Freight Bill Form</h2>

            <!-- User Input Fields -->
            <div class="row">
                <label for="txtTo" class="label">To Location:</label>
                <div class="field">
                    <asp:TextBox ID="txtTo" runat="server" required="true" />
                </div>
            </div>

            <div class="row">
                <label for="txtFrom" class="label">From Location:</label>
                <div class="field">
                    <asp:TextBox ID="txtFrom" runat="server" required="true" />
                </div>
            </div>

            <div class="row">
                <label for="txtVehType" class="label">Veh. Type:</label>
                <div class="field">
                    <asp:TextBox ID="txtVehType" runat="server" required="true" />
                </div>
            </div>

            <div class="row">
                <label for="txtVehNo" class="label">Veh. No.:</label>
                <div class="field">
                    <asp:TextBox ID="txtVehNo" runat="server" required="true" />
                </div>
            </div>

            <div class="row">
                <label for="txtCNNo" class="label">CN No.:</label>
                <div class="field">
                    <asp:TextBox ID="txtCNNo" runat="server" required="true" />
                </div>
            </div>

            <div class="row">
                <label for="txtShortageCharge" class="label">Shortage Charge:</label>
                <div class="field">
                    <asp:TextBox ID="txtShortageCharge" runat="server" />
                </div>
            </div>

            <div class="row">
                <label for="txtExtraCharges" class="label">Extra Charges:</label>
                <div class="field">
                    <asp:TextBox ID="txtExtraCharges" runat="server" />
                </div>
            </div>

            <div class="row">
                <label for="txtLoadingCharges" class="label">Loading Charges:</label>
                <div class="field">
                    <asp:TextBox ID="txtLoadingCharges" runat="server" />
                </div>
            </div>

            <div class="row">
                <label for="txtBillNo" class="label">Bill No.:</label>
                <div class="field">
                    <asp:TextBox ID="txtBillNo" runat="server" />
                </div>
            </div>

            <!-- Retrieved Fields (read-only) -->
            <div class="row">
                <label for="txtRate" class="label">Rate:</label>
                <div class="field">
                    <asp:TextBox ID="txtRate" runat="server" ReadOnly="true" Title="Rate per km" />
                </div>
            </div>

            <div class="row">
                <label for="txtAmountLimit" class="label">Amount Limit:</label>
                <div class="field">
                    <asp:TextBox ID="txtAmountLimit" runat="server" ReadOnly="true" Title="Limit on the bill" />
                </div>
            </div>

            <div class="row">
                <label for="txtDistance" class="label">Distance:</label>
                <div class="field">
                    <asp:TextBox ID="txtDistance" runat="server" ReadOnly="true" Title="Distance for the shipment" />
                </div>
            </div>

            <div class="row">
                <label for="txtBaseFreight" class="label">Base Freight:</label>
                <div class="field">
                    <asp:TextBox ID="txtBaseFreight" runat="server" ReadOnly="true" Title="Base freight value" />
                </div>
            </div>

            <div class="row">
                <label for="txtNetValue" class="label">Net Value:</label>
                <div class="field">
                    <asp:TextBox ID="txtNetValue" runat="server" ReadOnly="true" Title="Net value after all charges" />
                </div>
            </div>

            <!-- Submit Button -->
            <div class="button-row">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
            </div>
        </div>
    </form>
    <script type="text/javascript">
        // JavaScript function to trigger the SaveData method in the code-behind
           function saveData() {
               // Example: Collect form data and send it to server via AJAX or submit the form
               alert("Form data saved!");
           }
    </script>
</body>
</html>

