using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechProgr
{
	public class ParkingOverflowException : Exception
	{
		public ParkingOverflowException() : base("No free spaces in this parking"){

		}
	}
}
