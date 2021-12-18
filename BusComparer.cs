using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechProgr
{
	public class BusComparer : IComparer<Vehicle>
	{
		public int Compare(Vehicle x, Vehicle y)
		{
			if (x.Equals(y))
			{
				return 0;
			}
			if ((x.GetType().Name.Equals(y.GetType().Name))) {
				if (!x.mainColor.Name.Equals(y.mainColor.Name))
				{
					int i = 0;
					while (true)
					{
						if (x.mainColor.Name[i] > y.mainColor.Name[i])
						{
							return 1;
						}
						else if (x.mainColor.Name[i] < y.mainColor.Name[i])
						{
							return -1;
						}
						else
						{
							i++;
						}
					}
				}
				else {					
					if (x.GetType().Name.Equals("TwoFloorBus")) {
						if (!((TwoFloorBus)x).dopColor.Name.Equals(((TwoFloorBus)y).dopColor.Name))
						{
							int i = 0;
							while (true)
							{
								if (((TwoFloorBus)x).dopColor.Name[i] > ((TwoFloorBus)y).dopColor.Name[i])
								{
									return -1;
								}
								else if (((TwoFloorBus)x).dopColor.Name[i] < ((TwoFloorBus)y).dopColor.Name[i])
								{
									return 1;
								}
								else
								{
									i++;
								}
							}
						}
						if (((TwoFloorBus)x).polosa && !((TwoFloorBus)y).polosa)
						{
							return -1;
						}
						else if (!((TwoFloorBus)x).polosa && ((TwoFloorBus)y).polosa) {
							return 1;
						}
						if (((TwoFloorBus)x).secondFloor && !((TwoFloorBus)y).secondFloor)
						{
							return -1;
						}
						else if (!((TwoFloorBus)x).secondFloor && ((TwoFloorBus)y).secondFloor)
						{
							return 1;
						}
					}
					if (x.maxSpeed > y.maxSpeed)
					{
						return 1;
					}
					else if (x.maxSpeed < y.maxSpeed)
					{
						return -1;
					}
					if (x.weight > y.weight)
					{
						return 1;
					}
					else if (x.weight < y.weight)
					{
						return -1;
					}
				}
			}
			if ((x.GetType().Name.Equals("TwoFloorBus") && (y.GetType().Name.Equals("Bus")))) {
				return -1;
			}
			else if ((x.GetType().Name.Equals("Bus") && (y.GetType().Name.Equals("TwoFloorBus")))){
				return 1;
			}
			return 0;
		}
		private int ComparerBus(Bus x, Bus y)
		{
			if (x.maxSpeed != y.maxSpeed) {
				return x.maxSpeed.CompareTo(y.maxSpeed);
			}
			if (x.weight != y.weight)
			{
				return x.weight.CompareTo(y.weight);
			}
			if (x.mainColor != y.mainColor)
			{
				return x.mainColor.Name.CompareTo(y.mainColor.Name);
			}
			return 0;
		}

		private int ComparerTwoFloorBus(TwoFloorBus x, TwoFloorBus y) {
			var res = ComparerBus(x, y);
			if (res != 0) {
				return res;
			}
			if (x.dopColor != y.dopColor) { 
				return x.dopColor.Name.CompareTo(y.dopColor.Name);
			}
			if (x.polosa != y.polosa)
			{
				return x.polosa.CompareTo(y.polosa);
			}
			if (x.secondFloor != y.secondFloor)
			{
				return x.secondFloor.CompareTo(y.secondFloor);
			}
			return 0;
		}
	}
}
