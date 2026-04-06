using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsCalculator
{
    public class Form1 : Form
    {
        private TextBox txtDisplay;
        private double operand1 = 0;
        private string currentOperator = "";
        private bool isNewEntry = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Calculator";
            this.ClientSize = new Size(260, 310);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segoe UI", 12F, FontStyle.Regular);

            // Initialize TextBox
            txtDisplay = new TextBox();
            txtDisplay.Location = new Point(15, 15);
            txtDisplay.Size = new Size(230, 35);
            txtDisplay.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            txtDisplay.TextAlign = HorizontalAlignment.Right;
            txtDisplay.ReadOnly = true;
            txtDisplay.Text = "0";
            this.Controls.Add(txtDisplay);
            
            // X coordinates for the 4 columns
            int[] c = { 15, 75, 135, 195 };
            // Y coordinates for the 4 rows
            int[] r = { 65, 125, 185, 245 };

            // Row 1
            CreateBtn("7", c[0], r[0], new EventHandler(Number_Click));
            CreateBtn("8", c[1], r[0], new EventHandler(Number_Click));
            CreateBtn("9", c[2], r[0], new EventHandler(Number_Click));
            CreateBtn("/", c[3], r[0], new EventHandler(Operator_Click));

            // Row 2
            CreateBtn("4", c[0], r[1], new EventHandler(Number_Click));
            CreateBtn("5", c[1], r[1], new EventHandler(Number_Click));
            CreateBtn("6", c[2], r[1], new EventHandler(Number_Click));
            CreateBtn("*", c[3], r[1], new EventHandler(Operator_Click));

            // Row 3
            CreateBtn("1", c[0], r[2], new EventHandler(Number_Click));
            CreateBtn("2", c[1], r[2], new EventHandler(Number_Click));
            CreateBtn("3", c[2], r[2], new EventHandler(Number_Click));
            CreateBtn("-", c[3], r[2], new EventHandler(Operator_Click));

            // Row 4
            CreateBtn("C", c[0], r[3], new EventHandler(Clear_Click));
            CreateBtn("0", c[1], r[3], new EventHandler(Number_Click));
            CreateBtn("=", c[2], r[3], new EventHandler(Equals_Click));
            CreateBtn("+", c[3], r[3], new EventHandler(Operator_Click));
        }

        private Button CreateBtn(string text, int x, int y, EventHandler handler)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Size = new Size(50, 50);
            btn.Location = new Point(x, y);
            btn.Click += handler;
            this.Controls.Add(btn);
            return btn;
        }

        private void Number_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            
            if (txtDisplay.Text == "0" || isNewEntry || txtDisplay.Text == "Error")
            {
                txtDisplay.Text = "";
                isNewEntry = false;
            }

            txtDisplay.Text += btn.Text;
        }

        private void Operator_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            
            // Allow chaining calculations (e.g. 5 + 5 + without hitting equals)
            if (!isNewEntry && currentOperator != "")
            {
                Calculate();
            }
            else
            {
                double val;
                if (double.TryParse(txtDisplay.Text, out val))
                {
                    operand1 = val;
                }
            }

            currentOperator = btn.Text;
            isNewEntry = true;
        }

        private void Calculate()
        {
            if (currentOperator == "") return;

            double operand2;
            if (double.TryParse(txtDisplay.Text, out operand2))
            {
                double result = 0;
                bool error = false;

                switch (currentOperator)
                {
                    case "+": result = operand1 + operand2; break;
                    case "-": result = operand1 - operand2; break;
                    case "*": result = operand1 * operand2; break;
                    case "/":
                        if (operand2 == 0)
                        {
                            txtDisplay.Text = "Error";
                            error = true;
                        }
                        else
                        {
                            result = operand1 / operand2;
                        }
                        break;
                }

                if (!error)
                {
                    txtDisplay.Text = result.ToString();
                    operand1 = result;
                }
                else
                {
                    // Reset operand1 if there's an error like Division by Zero
                    operand1 = 0;
                }
            }
            
            currentOperator = "";
            isNewEntry = true;
        }
        
        private void Equals_Click(object sender, EventArgs e)
        {
            Calculate();
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";
            operand1 = 0;
            currentOperator = "";
            isNewEntry = true;
        }

        // Application Entry Point
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
