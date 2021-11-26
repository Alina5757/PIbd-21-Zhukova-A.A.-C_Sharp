using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechProgr
{
    public partial class FormParking : Form
    {
        private readonly ParkingCollection parkingCollection;
        public FormParking()
        {
            InitializeComponent();
            parkingCollection = new ParkingCollection(pictureBoxParking.Height, pictureBoxParking.Width);
        }

        private void ReloadLevels()
        {
            int index = listBoxParking.SelectedIndex;
            listBoxParking.Items.Clear();
            for (int i = 0; i < parkingCollection.Keys.Count; i++)
            {
                listBoxParking.Items.Add(parkingCollection.Keys[i]);
            }

            if (listBoxParking.Items.Count > 0 && (index == -1 || index >= listBoxParking.Items.Count))
            {
                listBoxParking.SelectedIndex = 0;
            }

            else if (listBoxParking.Items.Count > 0 && index > -1 && index < listBoxParking.Items.Count)
            {
                listBoxParking.SelectedIndex = index;
            }
        }
        private void Draw()
        {
            if (listBoxParking.SelectedIndex > -1)
            {
                Bitmap bmp = new Bitmap(pictureBoxParking.Width, pictureBoxParking.Height);
                Graphics gr = Graphics.FromImage(bmp);
                parkingCollection[listBoxParking.SelectedItem.ToString()].Draw(gr);
                pictureBoxParking.Image = bmp;
            }
        }             

        private void buttonCreateBus_Click(object sender, EventArgs e)
        {
            if (listBoxParking.SelectedIndex > -1)
            {               
                var formconfig = new FormBusConfig();
                formconfig.AddEvent(AddBus);
                formconfig.Show();
            }
        }

        private void AddBus(Vehicle bus) {
            if (bus != null && listBoxParking.SelectedIndex > -1) {
                if ((parkingCollection[listBoxParking.SelectedItem.ToString()]) + bus == 1)
                {
                    Draw();
                }
                else {
                    MessageBox.Show("Error, the bus could not be place");
                }
            }
        }

        private void buttonPickUp_Click(object sender, EventArgs e)
        {
            if (listBoxParking.SelectedIndex > -1 && maskedTextBox1.Text != "") {
                var bus = parkingCollection[listBoxParking.SelectedItem.ToString()] - Convert.ToInt32(maskedTextBox1.Text);
                if (bus != null) {
                    FormBus form = new FormBus();
                    form.SetBus(bus);

                    form.ShowDialog();
                }
                Draw();
            }
        }

		private void ButtonAddParking_Click(object sender, EventArgs e)
		{
            if (string.IsNullOrEmpty(textBoxNameParking.Text))
            {
                MessageBox.Show("Enter name of Parking", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            parkingCollection.AddParking(textBoxNameParking.Text);
            ReloadLevels();
        }

		private void ButtonDeleteParking_Click(object sender, EventArgs e)
		{
            if (listBoxParking.SelectedIndex > -1) {
                if (MessageBox.Show($"Delete Parking {listBoxParking.SelectedItem.ToString()}?",
                    "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                    parkingCollection.DelParking(listBoxParking.SelectedItem.ToString());
                    ReloadLevels();
                }
            }
		}

		private void listBoxParking_SelectedIndexChanged(object sender, EventArgs e)
		{
            if (listBoxParking.SelectedIndex > -1) {
                Draw();
            }
		}

        private void safeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                if (parkingCollection.SafeData(saveFileDialog.FileName))
                {
                    MessageBox.Show("Safe complete successfully", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else {
                    MessageBox.Show("File not safe", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                if (parkingCollection.LoadData(openFileDialog.FileName))
                {
                    MessageBox.Show("File load successfully", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else {
                    MessageBox.Show("File not load", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            ReloadLevels();
        }       
    }
}
