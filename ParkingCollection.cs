﻿using System;
using System.Collections.Generic;
using System.IO;
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
		private readonly char separator = ':';

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

		public bool SafeData(string filename) {
			if (File.Exists(filename)) {
				File.Delete(filename);
			}
			using (StreamWriter sw = new StreamWriter(filename))
			{
				sw.WriteLine($"ParkingCollection");
				foreach (var level in parkingStages)
				{
					sw.WriteLine($"Parking{separator}{level.Key}");
					ITransport bus = null;
					for (int i = 0; (bus = level.Value.GetNext(i)) != null; i++)
					{
						if (bus != null)
						{
							if (bus.GetType().Name == "Bus")
							{
								sw.WriteLine($"Bus{separator}");
							}
							if (bus.GetType().Name == "TwoFloorBus")
							{
								sw.WriteLine($"TwoFloorBus{separator}");
							}
							sw.WriteLine(bus);
						}
					}
				}
			}
			return true;
		}		

		public bool LoadData(string filename) {
			if (!File.Exists(filename)) {
				return false;
			}
			string bufferTextFromFile = "";
			Vehicle bus;
			string key;
			using (StreamReader sr = new StreamReader(filename))
			{
				string line = sr.ReadLine();
				if (line.Contains("ParkingCollection"))
				{
					parkingStages.Clear();
				}
				else
				{
					return false;
				}
				bus = null;
				key = string.Empty;
				while (line != null)
				{
					line = sr.ReadLine();
					if (line != null)
					{
						if (line.Contains("Parking"))
						{
							key = line.Split(separator)[1];
							parkingStages.Add(key, new ParkingBus<Vehicle>(pictureHeight, pictureWidth));
							continue;
						}

						if (string.IsNullOrEmpty(line))
						{
							continue;
						}
						if (line.Split(separator)[0] == "Bus")
						{
							line = sr.ReadLine();
							bus = new Bus(line);
						}
						else if (line.Split(separator)[0] == "TwoFloorBus")
						{
							line = sr.ReadLine();
							bus = new TwoFloorBus(line);
						}
						if (line != "")
						{
							var result = parkingStages[key] + bus;
							if (result != 1)
							{
								return false;
							}
						}
					}
				}
			}
			return true;
		}			
	}
}
