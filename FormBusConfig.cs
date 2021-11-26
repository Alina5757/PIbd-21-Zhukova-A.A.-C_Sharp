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
    public partial class FormBusConfig : Form
    {
        Vehicle bus = null;
        private event Action<Vehicle> eventAddBus;
        
        Vehicle Temp(Func<Vehicle> funk) {
            return funk();
        }
        public FormBusConfig()
        {
            InitializeComponent();
            panelColor1.MouseDown += new MouseEventHandler(panelColor_MouseDown);
            panelColor2.MouseDown += new MouseEventHandler(panelColor_MouseDown);
            panelColor3.MouseDown += new MouseEventHandler(panelColor_MouseDown);
            panelColor4.MouseDown += new MouseEventHandler(panelColor_MouseDown);
            panelColor5.MouseDown += new MouseEventHandler(panelColor_MouseDown);
            panelColor6.MouseDown += new MouseEventHandler(panelColor_MouseDown);
            panelColor7.MouseDown += new MouseEventHandler(panelColor_MouseDown);
            panelColor8.MouseDown += new MouseEventHandler(panelColor_MouseDown);
            labelDopColor.DragEnter += new DragEventHandler(labelMainColor_DragEnter);

            buttonCansel.Click += (object sender, EventArgs e) => { Close(); };
        }

        private void DrawBus()
        {
            if (bus != null)
            {
                Bitmap bmp = new Bitmap(pictureBoxBus.Width, pictureBoxBus.Height);
                Graphics g = Graphics.FromImage(bmp);
                bus.SetPosition(105, 110, pictureBoxBus.Width, pictureBoxBus.Height);
                bus.DrawTransport(g);
                pictureBoxBus.Image = bmp;
            }
        }

        public void AddEvent(Action<Vehicle> ev)
        {
            if (eventAddBus == null)
            {
                eventAddBus = new Action<Vehicle>(ev);
            }
            else
            {
                eventAddBus += ev;
            }
        }

        private void labelBus_MouseDown(object sender, MouseEventArgs e)
        {
            labelBus.DoDragDrop(labelBus.Text, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void labelTwoFloorBus_MouseDown(object sender, MouseEventArgs e)
        {
            labelTwoFloorBus.DoDragDrop(labelTwoFloorBus.Text, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void panelPicture_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void panelPicture_DragDrop(object sender, DragEventArgs e)
        {
            switch (e.Data.GetData(DataFormats.Text).ToString())
            {
                case "Bus":
                    bus = new Bus(100, 500, Color.White);
                    break;
                case "Two-Floor Bus":
                    bus = new TwoFloorBus((int)numericUpDownSpeed.Value, (int)numericUpDownWeight.Value, Color.White, Color.Black, checkBoxPolosa.Checked, checkBoxSecondFloor.Checked);
                    break;
            }
            DrawBus();
        }

        private void panelColor_MouseDown(object sender, MouseEventArgs e)
        {
            (sender as Panel).DoDragDrop((sender as Panel).BackColor, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void labelMainColor_DragEnter(object sender, DragEventArgs e)
        {
            Color color = (Color)e.Data.GetData(typeof(Color));
            if (color != null)
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void labelMainColor_DragDrop(object sender, DragEventArgs e)
        {
            if (bus != null)
            {
                Color color = (Color)e.Data.GetData(typeof(Color));
                bus.mainColor = color;
                DrawBus();
            }
        }

        private void labelDopColor_DragDrop(object sender, DragEventArgs e)
        {
            if (bus != null)
            {
                Color color = (Color)e.Data.GetData(typeof(Color));
                if (bus is TwoFloorBus)
                {
                    ((TwoFloorBus)bus).SetDopColor(color);
                    DrawBus();
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            eventAddBus?.Invoke(bus);
            Close();
        }       
    }
}
