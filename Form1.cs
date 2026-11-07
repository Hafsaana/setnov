using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace LuckyCafeOrder
{
    public partial class Form1 : Form
    {
        // Variables
        string customerName;
        string drinkName;
        decimal drinkPrice;
        decimal totalPrice;
        decimal discount;

        int quantity;
        int luckyNumber;

        bool gotPrize;

        Random random = new Random();

        // Form Controls
        Label lblTitle = new Label();
        Label lblName = new Label();
        Label lblQuantity = new Label();
        Label lblTotal = new Label();
        Label lblLucky = new Label();

        TextBox txtName = new TextBox();
        TextBox txtQuantity = new TextBox();

        RadioButton rdoTea = new RadioButton();
        RadioButton rdoCoffee = new RadioButton();
        RadioButton rdoJuice = new RadioButton();

        CheckBox chkStudent = new CheckBox();

        ListBox lstOrder = new ListBox();

        Button btnCalculate = new Button();
        Button btnSave = new Button();
        Button btnLoad = new Button();
        Button btnClear = new Button();

        public Form1()
        {
            InitializeComponent();

            CreateForm();
        }

        // Create Form
        private void CreateForm()
        {
            Text = "Lucky Cafe Order";

            Size = new Size(1000, 650);

            StartPosition = FormStartPosition.CenterScreen;

            // Title
            lblTitle.Text = "Lucky Cafe Order";

            lblTitle.Location = new Point(350, 20);

            lblTitle.Size = new Size(300, 40);

            lblTitle.Font = new Font(
                "Arial", 18, FontStyle.Bold);

            // Customer Information
            GroupBox customerGroup = new GroupBox();

            customerGroup.Text = "Customer Information";

            customerGroup.Location = new Point(30, 80);

            customerGroup.Size = new Size(380, 320);

            // Customer Name
            lblName.Text = "Customer Name:";

            lblName.Location = new Point(20, 35);

            lblName.Size = new Size(120, 25);

            txtName.Location = new Point(20, 65);

            txtName.Size = new Size(330, 25);

            // Drink Type
            GroupBox drinkGroup = new GroupBox();

            drinkGroup.Text = "Drink Type";

            drinkGroup.Location = new Point(20, 105);

            drinkGroup.Size = new Size(330, 120);

            rdoTea.Text = "Tea ($1)";

            rdoTea.Location = new Point(15, 25);

            rdoTea.Size = new Size(120, 25);

            rdoCoffee.Text = "Coffee ($2)";

            rdoCoffee.Location = new Point(15, 55);

            rdoCoffee.Size = new Size(120, 25);

            rdoJuice.Text = "Juice ($3)";

            rdoJuice.Location = new Point(15, 85);

            rdoJuice.Size = new Size(120, 25);

            drinkGroup.Controls.Add(rdoTea);

            drinkGroup.Controls.Add(rdoCoffee);

            drinkGroup.Controls.Add(rdoJuice);

            // Quantity
            lblQuantity.Text = "Quantity:";

            lblQuantity.Location = new Point(20, 245);

            lblQuantity.Size = new Size(80, 25);

            txtQuantity.Location = new Point(100, 245);

            txtQuantity.Size = new Size(70, 25);

            // Student Discount
            chkStudent.Text = "Student Member (10% Off)";

            chkStudent.Location = new Point(20, 280);

            chkStudent.Size = new Size(220, 25);

            // Add Customer Controls
            customerGroup.Controls.Add(lblName);

            customerGroup.Controls.Add(txtName);

            customerGroup.Controls.Add(drinkGroup);

            customerGroup.Controls.Add(lblQuantity);

            customerGroup.Controls.Add(txtQuantity);

            customerGroup.Controls.Add(chkStudent);

            // Order List
            GroupBox orderGroup = new GroupBox();

            orderGroup.Text = "Order List";

            orderGroup.Location = new Point(440, 80);

            orderGroup.Size = new Size(510, 320);

            lstOrder.Name = "Order List";

            lstOrder.Location = new Point(15, 25);

            lstOrder.Size = new Size(480, 275);

            orderGroup.Controls.Add(lstOrder);

            // Calculate Button
            btnCalculate.Text = "Calculate Order";

            btnCalculate.Location = new Point(30, 420);

            btnCalculate.Size = new Size(210, 40);

            // Save Button
            btnSave.Text = "Save to File";

            btnSave.Location = new Point(265, 420);

            btnSave.Size = new Size(210, 40);

            // Load Button
            btnLoad.Text = "Load from File";

            btnLoad.Location = new Point(500, 420);

            btnLoad.Size = new Size(210, 40);

            // Clear Button
            btnClear.Text = "Clear";

            btnClear.Location = new Point(735, 420);

            btnClear.Size = new Size(210, 40);

            // Total Price
            lblTotal.Text = "Total Price: $0.00";

            lblTotal.Location = new Point(30, 500);

            lblTotal.Size = new Size(400, 30);

            // Lucky Message
            lblLucky.Text = "Lucky Message: No prize this time.";

            lblLucky.Location = new Point(30, 540);

            lblLucky.Size = new Size(600, 30);

            // Add Controls to Form
            Controls.Add(lblTitle);

            Controls.Add(customerGroup);

            Controls.Add(orderGroup);

            Controls.Add(btnCalculate);

            Controls.Add(btnSave);

            Controls.Add(btnLoad);

            Controls.Add(btnClear);

            Controls.Add(lblTotal);

            Controls.Add(lblLucky);

            // Button Events
            btnCalculate.Click += CalculateOrder;

            btnSave.Click += SaveToFile;

            btnLoad.Click += LoadFromFile;

            btnClear.Click += ClearForm;
        }

        // Calculate Order
        private void CalculateOrder(object sender, EventArgs e)
        {
            // Read Customer Name
            customerName = txtName.Text;

            // Check Customer Name
            if (customerName == "")
            {
                MessageBox.Show("Enter Customer Name");

                return;
            }

            // Read Quantity
            if (!int.TryParse(txtQuantity.Text, out quantity)
                || quantity < 1)
            {
                MessageBox.Show("Enter a valid Quantity");

                return;
            }

            // Select Drink and Price
            if (rdoTea.Checked)
            {
                drinkName = "Tea";

                drinkPrice = 1;
            }
            else if (rdoCoffee.Checked)
            {
                drinkName = "Coffee";

                drinkPrice = 2;
            }
            else if (rdoJuice.Checked)
            {
                drinkName = "Juice";

                drinkPrice = 3;
            }
            else
            {
                MessageBox.Show("Select a Drink");

                return;
            }

            // Clear Previous Order
            lstOrder.Items.Clear();

            gotPrize = false;

            // Generate Lucky Numbers
            for (int i = 1; i <= quantity; i++)
            {
                luckyNumber = random.Next(1, 51);

                lstOrder.Items.Add(
                    i + ") " + customerName +
                    " - " + drinkName +
                    " - Lucky: " + luckyNumber +
                    " - $" + drinkPrice.ToString("F2"));

                if (luckyNumber == 7 || luckyNumber == 17)
                {
                    gotPrize = true;
                }
            }

            // Calculate Total Price
            totalPrice = drinkPrice * quantity;

            // Student Discount
            if (chkStudent.Checked)
            {
                discount = totalPrice * 0.10m;

                totalPrice = totalPrice - discount;
            }

            // Display Total Price
            lblTotal.Text = "Total Price: $" +
                            totalPrice.ToString("F2");

            // Display Lucky Message
            if (gotPrize)
            {
                lblLucky.Text =
                    "Lucky Message: You got a Free Cookie!";
            }
            else
            {
                lblLucky.Text =
                    "Lucky Message: No prize this time.";
            }
        }

        // Save Order to File
        private void SaveToFile(object sender, EventArgs e)
        {
            if (lstOrder.Items.Count == 0)
            {
                MessageBox.Show("No order to save.");

                return;
            }

            StreamWriter writer = File.AppendText("orders.txt");

            for (int i = 0; i < lstOrder.Items.Count; i++)
            {
                writer.WriteLine(lstOrder.Items[i].ToString());
            }

            writer.WriteLine(lblTotal.Text);

            writer.WriteLine(lblLucky.Text);

            writer.WriteLine("-------------------------");

            writer.Close();

            MessageBox.Show("Order saved.");
        }

        // Load Order from File
        private void LoadFromFile(object sender, EventArgs e)
        {
            if (!File.Exists("orders.txt"))
            {
                MessageBox.Show("No file found.");

                return;
            }

            lstOrder.Items.Clear();

            StreamReader reader = File.OpenText("orders.txt");

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();

                lstOrder.Items.Add(line);
            }

            reader.Close();
        }

        // Clear Form
        private void ClearForm(object sender, EventArgs e)
        {
            txtName.Clear();

            txtQuantity.Clear();

            lstOrder.Items.Clear();

            lblTotal.Text = "Total Price: $0.00";

            lblLucky.Text =
                "Lucky Message: No prize this time.";

            chkStudent.Checked = false;

            rdoTea.Checked = false;

            rdoCoffee.Checked = false;

            rdoJuice.Checked = false;

            totalPrice = 0;

            discount = 0;

            gotPrize = false;
        }
    }
}