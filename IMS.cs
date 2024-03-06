using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Runtime.Remoting.Messaging;
using System.Xml.Schema;



namespace InventoryManagementSystem2
{
    public partial class IMS : Form
    {
        
        List<Inventory> inventory = new List<Inventory>();
        public IMS()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

           inventory.ForEach(inventory => Console.WriteLine($"Stock Number\n\n{inventory}"));           
                                                
            File.WriteAllLines(@"I:\1. Documents\Microsoft Software & Systems Academy_MSSA\1. Technical Development\Assignments\Mini Project\IMS.txt", inventory.Select(inventory => $"Stock Number:  {inventory.StockNumber},   Name:  {inventory.Name},  Category:  {inventory.Cat},   Price:  {inventory.Price},   Quantity:  {inventory.Quantity},  Description:  {inventory.Description}" ));            
            MessageBox.Show("File Saved");
        }

        private void IMS_Load(object sender, EventArgs e)
        {   
         // 

            inventory.Add(new Inventory() { StockNumber = 1064, Cat = Category.Miscellanous, Name = "Machine Bolts", Price = 2, Quantity = 20, Description = "1.5 x 6 Inches"});
            inventory.Add(new Inventory() { StockNumber = 4241, Cat = Category.Plumbing, Name = "Plunger", Price = 4, Quantity = 8, Description = "Accordion-Style" });
            inventory.Add(new Inventory() { StockNumber = 1148, Cat = Category.Electrical, Name = "Indoor Light Switch", Price = 1, Quantity = 10, Description = "Beige Single Pole" });
            inventory.Add(new Inventory() { StockNumber = 1647, Cat = Category.Outdoor, Name = "Shovel", Price = 2, Quantity = 5, Description = "Plastic Snow" });
            IMSGrid.DataSource = inventory;
            cmbCategory.DataSource = Enum.GetValues(typeof(Category));
            foreach (DataGridViewRow IMSGrid in IMSGrid.Rows)
            {
                IMSGrid.Cells[1].Value = Convert.ToDecimal(IMSGrid.Cells[4].Value) * Convert.ToDecimal(IMSGrid.Cells[5].Value);
            }
                     

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            {
                if (txtName.Text != String.Empty && txtQuantity.Text != String.Empty && txtStockNumber.Text != String.Empty)

                {
                    Inventory newInventory = new Inventory();
                    newInventory.Name = txtName.Text;
                    newInventory.StockNumber = Int32.Parse(txtStockNumber.Text);
                    newInventory.Cat = (Category)(cmbCategory.SelectedIndex + 1);
                    newInventory.Price = decimal.Parse(txtPrice.Text);
                    newInventory.Quantity = Int32.Parse(txtQuantity.Text);
                    newInventory.Description = txtDescription.Text;                        
                    inventory.Add(newInventory);
                  
                    MessageBox.Show("Item added!");
                    RefreshData();
                }
                else
                {
                    var result = MessageBox.Show("Please enter a Stock Number, Item Name, and Quantity", "Error", MessageBoxButtons.OK);
                }
               
            }
            foreach (DataGridViewRow IMSGrid in IMSGrid.Rows)
            {
                IMSGrid.Cells[1].Value = Convert.ToDecimal(IMSGrid.Cells[4].Value) * Convert.ToDecimal(IMSGrid.Cells[5].Value);
            }
        }
        private void RefreshData()
        {
            txtStockNumber.Clear();
            txtName.Clear();
            cmbCategory.SelectedIndex = 0;
            txtQuantity.Clear();
            txtDescription.Clear();
            txtPrice.Clear();
            IMSGrid.DataSource = null;
            IMSGrid.DataSource = inventory;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            {
                var result = MessageBox.Show("Are you sure you want to delete?", "Warning", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                    inventory.RemoveAt(IMSGrid.CurrentRow.Index);
                RefreshData();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Do you want to save before exiting?", "Warning", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                inventory.ForEach(inventory => Console.WriteLine(inventory));
                File.WriteAllLines(@"I:\1. Documents\Microsoft Software & Systems Academy_MSSA\1. Technical Development\Assignments\Mini Project\IMS.txt", inventory.Select(inventory => $"Stock Number:{inventory.StockNumber},  Name:  {inventory.Name},  Category: {inventory.Cat},Price: {inventory.Price},Quantity: {inventory.Quantity}, Description: {inventory.Description}"));
                MessageBox.Show("File Saved");
                Application.Exit();
            }
            if (result == DialogResult.No)

            {
                Application.Exit();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            {
                int search = Int32.Parse(txtSearch.Text);

                if (search == 0 || search <= 0)
                {
                    MessageBox.Show("Please Enter A Valid Stock Number");
                }
                else
                {
                    foreach (DataGridViewRow row in IMSGrid.Rows)
                    {
                        if (row.Cells["StockNumber"].Value.Equals(search))
                        {
                            IMSGrid.CurrentRow.Selected = false;
                            IMSGrid.Rows[row.Index].Selected = true;
                            int index = row.Index;
                            IMSGrid.FirstDisplayedScrollingRowIndex = index;
                            // IMSGrid.DefaultCellStyle.BackColor = Color.Red;
                            break;
                        }
                    }

                }

            }

        }

        private void IMSGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
       
        }

        private void lblPrice_Click(object sender, EventArgs e)
        {

        }

        private void txtStockNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < IMSGrid.Rows.Count; i++)
            {
                IMSGrid.Rows[i].Cells[0].Value = true;
            }
            SelectedRowTotal();

        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < IMSGrid.Rows.Count; i++)
            {
                IMSGrid.Rows[i].Cells[0].Value = false;
            }
            SelectedRowTotal();

        }

        public void SelectedRowTotal()
        {
            decimal sum = 0;
            for (int i = 0; i < IMSGrid.Rows.Count; i++)
            {
                if (Convert.ToBoolean(IMSGrid.Rows[i].Cells[0].Value))
                {
                    sum += decimal.Parse(IMSGrid.Rows[i].Cells[1].Value.ToString());                  
                }
                              
                else
                {
                    sum = 0;                    
                }
            }
            txtTotal.Text = sum.ToString();
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSum_Click(object sender, EventArgs e)

        {
            SelectedRowTotal();
        }

        private void IMSGrid_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void lnlName_Click(object sender, EventArgs e)
        {

        }
    }
 
}   
     
  

            

