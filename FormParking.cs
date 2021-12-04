using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;

namespace TechProgr
{
    public partial class FormParking : Form
    {
        private readonly ParkingCollection parkingCollection;
        private readonly Logger logger;
        public FormParking()
        {
            InitializeComponent();
            parkingCollection = new ParkingCollection(pictureBoxParking.Height, pictureBoxParking.Width);
            logger = LogManager.GetCurrentClassLogger();
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
            var formconfig = new FormBusConfig();
            formconfig.AddEvent(AddBus);
            formconfig.Show();
        }

        private void AddBus(Vehicle bus)
        {
            try
            {
                if ((parkingCollection[listBoxParking.SelectedItem.ToString()]) + bus == 1)
                {
                    Draw();
                    logger.Info($"Add bus {bus}");
                }
                else
                {
                    logger.Warn($"Add bus error: add {bus} to parking " + (parkingCollection[listBoxParking.SelectedItem.ToString()]) + " failed");
                    MessageBox.Show("Error, the bus could not be place");
                }
                Draw();
            }
            catch (ParkingOverflowException ex)
            {
                logger.Warn($"Add bus error: parking " + (parkingCollection[listBoxParking.SelectedItem.ToString()]) + " overflow");
                MessageBox.Show(ex.Message, "Overflow", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                logger.Warn($"Add bus error: unknow error of parking " + parkingCollection[listBoxParking.SelectedItem.ToString()]);
                MessageBox.Show(ex.Message, "Unknow error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }// Логи -> уровень - дата - текст лога

        private void buttonPickUp_Click(object sender, EventArgs e)
        {
            try
            {
                var bus = parkingCollection[listBoxParking.SelectedItem.ToString()] - Convert.ToInt32(maskedTextBox1.Text);
                if (bus != null)
                {
                    FormBus form = new FormBus();
                    form.SetBus(bus);
                    form.ShowDialog();

                    logger.Info($"Bus {bus} was pick up from place {maskedTextBox1.Text}");
                    Draw();
                }
                else { throw new ParkingNotFoundException(Convert.ToInt32(maskedTextBox1.Text)); }
            }
            catch (ParkingNotFoundException ex)
            {
                logger.Warn($"Pick up bus error: in parking " + (parkingCollection[listBoxParking.SelectedItem.ToString()]) +
                    $" place {maskedTextBox1.Text} is empty");
                MessageBox.Show(ex.Message, "Not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                logger.Warn($"Pick up bus error: unknown error in parking " + parkingCollection[listBoxParking.SelectedItem.ToString()]);
                MessageBox.Show(ex.Message, "Unknown error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonAddParking_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxNameParking.Text))
            {
                logger.Warn($"Add parking error: text field 'name' is empty");
                MessageBox.Show("Enter name of Parking", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            logger.Info($"Parking {textBoxNameParking.Text} was add");
            parkingCollection.AddParking(textBoxNameParking.Text);
            ReloadLevels();
        }

        private void ButtonDeleteParking_Click(object sender, EventArgs e)
        {
            if (listBoxParking.SelectedIndex > -1) {
                if (MessageBox.Show($"Delete Parking {listBoxParking.SelectedItem.ToString()}?",
                    "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                    logger.Info($"Parking {listBoxParking.SelectedItem.ToString()} was delete");
                    parkingCollection.DelParking(listBoxParking.SelectedItem.ToString());
                    ReloadLevels();
                }
            }
        }

        private void listBoxParking_SelectedIndexChanged(object sender, EventArgs e)
        {
            logger.Info($"Go to {listBoxParking.SelectedItem.ToString()}");
            Draw();
        }

        private void safeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try 
                {
                    parkingCollection.SafeData(saveFileDialog.FileName);
                    MessageBox.Show("Safe was succesfully", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    logger.Info("Safe in file " + saveFileDialog.FileName);
                }
                catch (Exception ex) {
                    logger.Warn($"Safe parking error: unknown error");
                    MessageBox.Show(ex.Message, "Unknown error saving", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                try
                {
                    parkingCollection.LoadData(openFileDialog.FileName);
                    MessageBox.Show("File load successfully", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    logger.Info("Load from file " + openFileDialog.FileName);
                    ReloadLevels();
                    Draw();
                }
                catch (Exception ex) {
                    logger.Warn($"Load parking error: unknown error");
                    MessageBox.Show(ex.Message, "Unknown error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }                
            }
        }       
    }
}
