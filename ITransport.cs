using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechProgr
{
	public interface ITransport
	{
		//			установка позиции
		//координата x
		//координата y
		//ширина картинки от центра x_lenght
		//высота картинки от центра y_lenght
		void SetPosition(float x_koor, float y_koor, int x_lenght, int y_lenght);

		//			изменение направления перемещения
		//направление direction
		void MoveTransport(Direction direction);

		//Отрисовка
		void DrawTransport(Graphics g);

		//			изменение основного цвета
		//Цвет color
		void SetMainColor(Color color);
	}
}
