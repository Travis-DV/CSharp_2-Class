namespace CustomerMaintenance
{
    public partial class frmCustomer : Form
    {
        public frmCustomer()
        {
            InitializeComponent();
        }

        private CustomerList customers = new();

        private void frmCustomer_Load(object sender, EventArgs e)
        {
            customers.Changed += customers =>
            {
                customers.Save();
                FillCustomerListBox();
            };

            customers.Fill();
            FillCustomerListBox();
        }

        private void FillCustomerListBox()
        {
            lstCustomers.Items.Clear();
            foreach(Customer customer in customers)
            {
                lstCustomers.Items.Add(customer.GetDisplayText());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddCustomer addCustomerForm = new();
            Customer customer = addCustomerForm.GetNewCustomer();
            if (customer != null)
            {
                customers += customer;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int i = lstCustomers.SelectedIndex;
            if (i != -1)
            {
                Customer customer = customers[i];
                string message = "Are you sure you want to delete "
                    + customer.FirstName + " " + customer.LastName + "?";
                DialogResult button = MessageBox.Show(message, "Confirm Delete",
                    MessageBoxButtons.YesNo);
                if (button == DialogResult.Yes)
                {
                    customers -= customer;
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}