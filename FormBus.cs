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
	public partial class FormBus : Form
	{
		private ITransport Bus;
		private bool bus_exist = false;
		public FormBus()
		{
			InitializeComponent();
		}

		public void SetBus(ITransport bus) {
			this.Bus = bus;
			bus_exist = true;
			Draw();
		}
		private void Draw() {
			Bitmap bmp = new Bitmap(pictureBoxBus.Width, pictureBoxBus.Height);
			Graphics gr = Graphics.FromImage(bmp);
			Bus?.DrawTransport(gr);
			pictureBoxBus.Image = bmp;
		}

		private void buttonCreateBus_Click(object sender, EventArgs e)
		{
			Random random = new Random();
			Bus = new Bus(random.Next(100, 200), random.Next(1000, 3000), Color.Red);
			Bus.SetPosition((float)random.Next(200, 700), (float)random.Next(200, 400), pictureBoxBus.Height, pictureBoxBus.Width);
			bus_exist = true;
			Draw();
		}

		private void buttomMove_Click(object sender, EventArgs e) {
			string name = (sender as Button).Name;
			if (bus_exist)
			{
				switch (name)
				{
					case "buttonUp":
						Bus?.MoveTransport(Direction.Up);
						break;
					case "buttonDown":
						Bus?.MoveTransport(Direction.Down);
						break;
					case "buttonLeft":
						Bus?.MoveTransport(Direction.Left);
						break;
					case "buttonRight":
						Bus?.MoveTransport(Direction.Right);
						break;
				}
				Draw();
			}
		}

		private void buttonCreateTwoFloorBus_Click(object sender, EventArgs e)
		{
			Random random = new Random();
			Bus = new TwoFloorBus(random.Next(100, 200), random.Next(1000, 3000), Color.Red, Color.Yellow, true, true);
			Bus.SetPosition(random.Next(200, 700), random.Next(200, 400), pictureBoxBus.Height, pictureBoxBus.Width);
			bus_exist = true;
			Draw();
		}
	}
}
