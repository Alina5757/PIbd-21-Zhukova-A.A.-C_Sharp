using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechProgr
{
	public class ParkingAlreadyHaveException: Exception
	{
		public ParkingAlreadyHaveException() : base("At this parking already have this Bus") { }
	}
}
