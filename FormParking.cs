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

        private void buttonParkingBus_Click(object sender, EventArgs e)
        {
            if (listBoxParking.SelectedIndex > -1)
            {
                ColorDialog dialog = new ColorDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var bus = new Bus(1000, 100, dialog.Color);
                    if (parkingCollection[listBoxParking.SelectedItem.ToString()] + bus != -1)
                    {
                        Draw();
                    }
                    else
                    {
                        MessageBox.Show("Parking is full");
                    }
                }
            }
        }

        private void buttonParkTwoBus_Click(object sender, EventArgs e)
        {
            if (listBoxParking.SelectedIndex > -1)
            {
                ColorDialog dialog = new ColorDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ColorDialog dialogDop = new ColorDialog();
                    if (dialogDop.ShowDialog() == DialogResult.OK)
                    {
                        var Tbus = new TwoFloorBus(50, 100, dialog.Color, dialogDop.Color, true, true);
                        if (parkingCollection[listBoxParking.SelectedItem.ToString()] + Tbus != -1)
                        {
                            Draw();
                        }
                        else
                        {
                            MessageBox.Show("Parking is full");
                        }
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
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
                    parkingCollection.DelParking(textBoxNameParking.Text);
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
	}
}
