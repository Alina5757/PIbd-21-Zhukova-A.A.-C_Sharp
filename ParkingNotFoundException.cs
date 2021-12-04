using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechProgr
{
	public class ParkingNotFoundException : Exception
	{
		public ParkingNotFoundException(int i) : base("Bus in the space " + i + " not found"){ 
		}
	}
}
