using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechProgr
{
    class ClassDopDoors
    {
        private EnumDopDoors doors;
        public int SetDoors {
            set {
                if (Enum.IsDefined(typeof(EnumDopDoors), value))
                {
                    doors = (EnumDopDoors)value;
                }
                else {
                    Random rand = new Random();
                    doors = (EnumDopDoors)rand.Next(3, 6);
                }
            }
        }
        public void DrowDoors(Graphics g, Color dopcolor, float x_koor, float y_koor) {
            Pen pen = new Pen(Color.Black);
            Brush brush = new SolidBrush(dopcolor);

            g.FillRectangle(brush, x_koor, y_koor, 16, 45);
            g.DrawRectangle(pen, x_koor, y_koor, 16, 45);
            g.FillRectangle(brush, x_koor + 16, y_koor, 16, 45);
            g.DrawRectangle(pen, x_koor + 16, y_koor, 16, 45);
            g.FillRectangle(brush, x_koor + 68, y_koor, 16, 45);
            g.DrawRectangle(pen, x_koor + 68, y_koor, 16, 45);

            if (doors >= EnumDopDoors.FourDoors) {
                g.FillRectangle(brush, x_koor - 96, y_koor, 16, 45);
                g.DrawRectangle(pen, x_koor - 96, y_koor, 16, 45);
            }
            if (doors == EnumDopDoors.FiveDoors) {
                g.FillRectangle(brush, x_koor - 80, y_koor, 16, 45);
                g.DrawRectangle(pen, x_koor - 80, y_koor, 16, 45);
            }
        }
    }
}
