using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSCommon
{
	public class UpgradeObject
	{
		public int ID { get; set; }
		public int Type { get; set; }
		public string Name { get; set; }
		public string FilePath { get; set; }
		public int GoldCost { get; set; }
		public int Attack { get; set; }
		public int Defense { get; set; }
		public int Health { get; set; }
		public int Shield { get; set; }
		public int Move { get; set; }
		public int Energy { get; set; }
		public int Popularity { get; set; }
	}
}
