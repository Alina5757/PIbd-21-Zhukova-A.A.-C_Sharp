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
        private readonly ParkingBus<Bus> parking;
        public FormParking()
        {
            InitializeComponent();
            parking = new ParkingBus<Bus>(pictureBoxParking.Height, pictureBoxParking.Width);
            Draw();
        }

        private void Draw()
        {
            Bitmap bmp = new Bitmap(pictureBoxParking.Width, pictureBoxParking.Height);
            Graphics gr = Graphics.FromImage(bmp);
            parking.Draw(gr);
            pictureBoxParking.Image = bmp;
        }

        private void buttonParkingBus_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var bus = new Bus(1000, 100, dialog.Color);
                if (parking + bus != -1)
                {
                    Draw();
                }
                else
                {
                    MessageBox.Show("Parking is full");
                }
            }
        }

        private void buttonParkTwoBus_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ColorDialog dialogDop = new ColorDialog();
                if (dialogDop.ShowDialog() == DialogResult.OK)
                {
                    var Tbus = new TwoFloorBus(50, 100, dialog.Color, dialogDop.Color, true, true);
                    if (parking + Tbus != -1)
                    {
                        Draw();
                    }
                    else {
                        MessageBox.Show("Parking is full");
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text != "") {
                var bus = parking - Convert.ToInt32(maskedTextBox1.Text);
                if (bus != null) {
                    FormBus form = new FormBus();
                    form.SetBus(bus);

                    form.ShowDialog();
                }
                Draw();
            }
        }
    }
}
