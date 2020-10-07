using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSCommon
{
	public class GamePlayer
	{
		public int ID;
		public string Name;
		public int Rank;
		public int Health;
		public int Energy;
		public string Tank;
		public string Skin;
		public string AvaHead;
		public string AvaShoulder;
		public string AvaChest;
		public Tuple<float, float> Position;
		public Tuple<float, float> PlayerMid;
		public float Rotate;		
	}
}
