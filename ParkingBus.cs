using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechProgr
{
    public class ParkingBus<T> where T : class, ITransport
    {
        private readonly List <T> places;
        private readonly int maxCount;
        private readonly int pictureHeight;
        private readonly int pictureWidth;
        private readonly int placeHeight = 170;
        private readonly int placeWidth = 300;
        private readonly int height;
        private readonly int width;

        public ParkingBus(int parkingHeight, int parkingWidth)
        {
            height = parkingHeight / placeHeight;
            width = parkingWidth / placeWidth;
            maxCount = width * height;
            places = new List <T>();
            pictureHeight = parkingHeight;
            pictureWidth = parkingWidth;
        }

        public static int operator +(ParkingBus<T> p, T bus)
        {
            if (p.places.Count < p.maxCount)
            {
                for (int i = 0; i < p.maxCount; i++)
                {
                    p.places.Add(bus);
                    bus.SetPosition(110 + (i % p.height) * p.placeWidth, 100 + ((i) / (p.width)) * p.placeHeight, p.pictureHeight, p.pictureWidth);
                    return 1;
                }
            }
            return -1;
        }

        public static T operator -(ParkingBus<T> p, int index)
        {
            if ((index < p.places.Count) && (index >= 0))
            {
                if (p.places[index] != null)
                {
                    T bus = p.places[index];
                    p.places.RemoveAt(index);
                    return bus;
                }
            }
            return null;
        }

        public void Draw(Graphics g) {
            DrawMapking(g);
            for (int i = 0; i < places.Count; i++) {
                places[i].SetPosition(110 + (i % height) * placeWidth, 100 + ((i) / (width)) * placeHeight, pictureHeight, pictureWidth);
                places[i].DrawTransport(g);
            }
        }

        public void DrawMapking(Graphics g)
        {
            Pen pen = new Pen(Color.Black, 3);
            for (int i = 0; i < pictureWidth / placeWidth; i++)
            {
                for (int j = 0; j < pictureHeight / placeHeight + 1; j++) {
                    g.DrawLine(pen, i * placeWidth, j * placeHeight, i * placeWidth + placeWidth/10*7, j*placeHeight);
                    g.DrawLine(pen, i * placeWidth, 0, i * placeWidth, (pictureHeight / placeHeight) * placeHeight);
                }
            }
        }
    }
}