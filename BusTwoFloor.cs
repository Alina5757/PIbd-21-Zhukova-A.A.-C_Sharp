using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechProgr
{
	class BusTwoFloor
	{
		int maxspeed;  //Макс скорость
		int weight;   //Вес
		bool polosa;  //полоса на корпусе
		bool secondFloor;  // второй этаж
		float x_koor;   //x центра отрисовки
		float y_koor;   //y центра отрисовки
		int x_lenght = 100;   //длина рисунка от центра
		int y_lenght = 75;   //высота рисунка от центра
		public Color mainColor;   //основной цвет
		public Color dopColor;   //дополнительный цвет

		ClassDopDoors dopdoors;  //класс отрисовки дверей

		public void Init(int Maxspeed, int Weight, bool SecondFloor, bool Polosa, Color MainColor, Color DopColor) {
			maxspeed = Maxspeed;
			weight = Weight;
			polosa = Polosa;
			secondFloor = SecondFloor;
			mainColor = MainColor;
			dopColor = DopColor;
			dopdoors = new ClassDopDoors();
			Random random = new Random();
			dopdoors.SetDoors = random.Next(3, 6);
		}
		public void SetPosition(int X_koor, int Y_koor) {
			x_koor = X_koor;
			y_koor = Y_koor;
		}

		public void MoveTransport(Direction direction)
		{
			float step = maxspeed * 100 / weight;

			switch (direction)
			{
				case Direction.Up:
					if (y_koor - y_lenght - step + 20 >= 0)
					{
						y_koor -= step;
					}
					break;
				case Direction.Down:
					if (y_koor + y_lenght + step <= 500)
					{
						y_koor += step;
					}
					break;
				case Direction.Left:
					if (x_koor - x_lenght - step >= 0)
					{
						x_koor -= step;
					}
					break;
				case Direction.Right:
					if (x_koor + x_lenght + step <= 900)
					{
						x_koor += step;
					}
					break;
			}
		}

		public void DrowBus(Graphics g) {
			Pen pen = new Pen(Color.Black);
			Brush brRed = new SolidBrush(mainColor);
			Brush brYel = new SolidBrush(dopColor);
			Brush brBlue = new SolidBrush(Color.LightBlue);
			Brush brGrey = new SolidBrush(Color.DarkGray);

			g.FillRectangle(brRed, x_koor - 100, y_koor - 10, 200, 70);  //корпус первый этаж
			if (polosa) {
				g.FillRectangle(brYel, x_koor - 100, y_koor + 50, 200, 3);
			}
			g.DrawRectangle(pen, x_koor - 100, y_koor - 10, 200, 70);
			if (secondFloor)
			{
				g.FillRectangle(brYel, x_koor - 100, y_koor - 60, 200, 50); //корпус второй этаж
				g.DrawRectangle(pen, x_koor - 100, y_koor - 60, 200, 50);

				g.FillRectangle(brBlue, x_koor - 100, y_koor - 50, 10, 25);  //окна второй этаж
				g.DrawRectangle(pen, x_koor - 100, y_koor - 50, 10, 25);
				g.FillRectangle(brBlue, x_koor - 80, y_koor - 50, 25, 25);
				g.DrawRectangle(pen, x_koor - 80, y_koor - 50, 25, 25);
				g.FillRectangle(brBlue, x_koor - 45, y_koor - 50, 25, 25);
				g.DrawRectangle(pen, x_koor - 45, y_koor - 50, 25, 25);
				g.FillRectangle(brBlue, x_koor - 12, y_koor - 50, 25, 25);
				g.DrawRectangle(pen, x_koor - 12, y_koor - 50, 25, 25);
				g.FillRectangle(brBlue, x_koor + 20, y_koor - 50, 25, 25);
				g.DrawRectangle(pen, x_koor + 20, y_koor - 50, 25, 25);
				g.FillRectangle(brBlue, x_koor + 55, y_koor - 50, 25, 25);
				g.DrawRectangle(pen, x_koor + 55, y_koor - 50, 25, 25);
				g.FillRectangle(brBlue, x_koor + 90, y_koor - 50, 10, 25);
				g.DrawRectangle(pen, x_koor + 90, y_koor - 50, 10, 25);
			}

			dopdoors.DrowDoors(g, dopColor, x_koor, y_koor);

			g.FillRectangle(brBlue, x_koor - 60, y_koor, 25, 25);  //окна первый этаж
			g.DrawRectangle(pen, x_koor - 60, y_koor, 25, 25);  
			g.FillRectangle(brBlue, x_koor - 30, y_koor, 25, 25);
			g.DrawRectangle(pen, x_koor - 30, y_koor, 25, 25);
			g.FillRectangle(brBlue, x_koor + 38, y_koor, 25, 25);
			g.DrawRectangle(pen, x_koor + 38, y_koor, 25, 25);
			g.FillRectangle(brBlue, x_koor + 90, y_koor, 10, 25);
			g.DrawRectangle(pen, x_koor + 90, y_koor, 10, 25);

			g.FillEllipse(brGrey, x_koor - 60, y_koor + 40, 35, 35);   //колеса
			g.DrawEllipse(pen, x_koor - 60, y_koor + 40, 35, 35); 
			g.FillEllipse(brGrey, x_koor + 35, y_koor + 40, 35, 35);
			g.DrawEllipse(pen, x_koor + 35, y_koor + 40, 35, 35);

			
		}
	}
}
