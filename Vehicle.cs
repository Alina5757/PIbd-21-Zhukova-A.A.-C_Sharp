using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechProgr
{
	abstract class Vehicle : ITransport
	{
		protected float x_koor;
		protected float y_koor;
		protected int widthScreen;
		protected int heightScreen;
		public int maxSpeed { protected set; get; }
		public int weight { protected set; get; }
		public Color mainColor;

		public void SetPosition(float x, float y, int HeightScreen, int WidhtScreen)
		{
			if (((x > 0) && (x < WidhtScreen)) &&
				((y > 0) && (y < HeightScreen)))
			{
				heightScreen = HeightScreen;
				widthScreen = WidhtScreen;
				x_koor = x;
				y_koor = y;
			}
		}
		public abstract void DrawTransport(Graphics g);
		public abstract void MoveTransport(Direction direction);
	}
}