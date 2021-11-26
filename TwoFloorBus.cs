using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechProgr
{
	class TwoFloorBus : Bus
	{
		public Color dopColor { private set; get; }
		public bool polosa { private set; get; }
		public bool secondFloor { private set; get; }

		public TwoFloorBus(int Maxspeed, int Weight, Color MainColor, Color DopColor, bool Polosa, bool SecondFloor) :
			base(Maxspeed, Weight, MainColor, 100, 70)
		{
			dopColor = DopColor;
			polosa = Polosa;
			secondFloor = SecondFloor;
		}

		public TwoFloorBus(string info) : base(info) {
			string[] str = info.Split(separator);
			if (str.Length == 6) {
				maxSpeed = Convert.ToInt32(str[0]);
				weight = Convert.ToInt32(str[1]);
				mainColor = Color.FromName(str[2]);
				dopColor = Color.FromName(str[3]);
				polosa = Convert.ToBoolean(str[4]);
				secondFloor = Convert.ToBoolean(str[5]);
			}
		}

		public override void DrawTransport(Graphics g)
		{
			base.DrawTransport(g);

			Pen pen = new Pen(Color.Black);
			Brush brRed = new SolidBrush(mainColor);
			Brush brYel = new SolidBrush(dopColor);
			Brush brBlue = new SolidBrush(Color.LightBlue);
			Brush brGrey = new SolidBrush(Color.DarkGray);

			if (secondFloor)
			{
				g.FillRectangle(brYel, x_koor - 100, y_koor - 80, 200, 50); //корпус второй этаж
				g.DrawRectangle(pen, x_koor - 100, y_koor - 80, 200, 50);

				g.FillRectangle(brBlue, x_koor - 100, y_koor - 65, 10, 25);  //окна второй этаж
				g.DrawRectangle(pen, x_koor - 100, y_koor - 65, 10, 25);
				g.FillRectangle(brBlue, x_koor - 80, y_koor - 65, 25, 25);
				g.DrawRectangle(pen, x_koor - 80, y_koor - 65, 25, 25);
				g.FillRectangle(brBlue, x_koor - 45, y_koor - 65, 25, 25);
				g.DrawRectangle(pen, x_koor - 45, y_koor - 65, 25, 25);
				g.FillRectangle(brBlue, x_koor - 12, y_koor - 65, 25, 25);
				g.DrawRectangle(pen, x_koor - 12, y_koor - 65, 25, 25);
				g.FillRectangle(brBlue, x_koor + 20, y_koor - 65, 25, 25);
				g.DrawRectangle(pen, x_koor + 20, y_koor - 65, 25, 25);
				g.FillRectangle(brBlue, x_koor + 55, y_koor - 65, 25, 25);
				g.DrawRectangle(pen, x_koor + 55, y_koor - 65, 25, 25);
				g.FillRectangle(brBlue, x_koor + 90, y_koor - 65, 10, 25);
				g.DrawRectangle(pen, x_koor + 90, y_koor - 65, 10, 25);
			}

			if (polosa)
			{
				g.FillRectangle(brYel, x_koor - 99, y_koor + 30, 40, 3);
				g.FillRectangle(brYel, x_koor - 25, y_koor + 30, 62, 3);
				g.FillRectangle(brYel, x_koor + 70, y_koor + 30, 29, 3);
			}
		}

		public void SetDopColor(Color color)
		{
			dopColor = color;
		}

        public override string ToString()
        {
            return
				$"{base.ToString()}{separator}{dopColor.Name}{separator}{polosa}{separator}{secondFloor}";
        }
    }
}
