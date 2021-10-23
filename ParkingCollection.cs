using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechProgr
{
	public class ParkingCollection
	{
		public readonly Dictionary<string, ParkingBus<Vehicle>> parkingStages;

		public List<string> Keys => parkingStages.Keys.ToList();
		private readonly int pictureHeight;
		private readonly int pictureWidth;

		public ParkingCollection(int PictureHeight, int PictureWidth) {
			parkingStages = new Dictionary<string, ParkingBus<Vehicle>>();
			pictureHeight = PictureHeight;
			pictureWidth = PictureWidth;
		}

		public void AddParking(string name) {
			if (!parkingStages.ContainsKey(name))
			{
				parkingStages.Add(name, new ParkingBus<Vehicle>(pictureHeight, pictureWidth));
			}
		}

		public void DelParking(string name) {
			if (parkingStages.ContainsKey(name))
			{
				parkingStages.Remove(name);
			}
		}

		public ParkingBus<Vehicle> this[string ind] {
			get {
				if (parkingStages.ContainsKey(ind)) {
					return parkingStages[ind];							
				}
				return null;
			}
		}
	}
}
