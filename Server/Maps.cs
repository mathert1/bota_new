using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
	class Maps
	{
		public List<bool[,]> mapList;
		//public bool[,] radiatin2dArray;
		//public bool[,] donkey2dArray;

		public string GetMapName(int map)
		{
			string mapName = "";
			switch (map)
			{
				case 0:
					mapName = "radiatin";
					break;
				case 1:
					mapName = "donkey";
					break;
			}
			return mapName;
		}

		public string GetBackgroundName(int map)
		{
			string backName = "";
			switch (map)
			{
				case 0:
					backName = "dragon_b";
					break;
				case 1:
					backName = "dragon_b";
					break;
			}
			return backName;
		}

		public void LoadMap2dArrays()
		{
			try
			{
				Console.Write("Parsing map files..." + Environment.NewLine);
				mapList = new List<bool[,]>();
				Bitmap mapimage;
				String path = AppDomain.CurrentDomain.BaseDirectory;
				mapimage = new Bitmap(@"../../Maps/radiatin.bmp");
				bool[,] temp2dArray = new bool[mapimage.Width, mapimage.Height];
				int x, y;
				for (x = 0; x < mapimage.Width; x++)
				{
					for (y = 0; y < mapimage.Height; y++)
					{
						System.Drawing.Color pixelColor = mapimage.GetPixel(x, y);
						if (pixelColor.Name == "ffffffff")
						{
							temp2dArray[x, y] = false;
						}
						else
						{
							temp2dArray[x, y] = true;
						}
					}
				}
				mapList.Add(temp2dArray);
				//temp2dArray = new bool[mapimage.Width, mapimage.Height];
				//mapimage = new Bitmap(@"../../Maps/donkey.bmp");
				//for (x = 0; x < mapimage.Width; x++)
				//{
				//    for (y = 0; y < mapimage.Height; y++)
				//    {
				//        System.Drawing.Color pixelColor = mapimage.GetPixel(x, y);
				//        if (pixelColor.Name == "ffffffff")
				//        {
				//            temp2dArray[x, y] = false;
				//        }
				//        else
				//        {
				//            temp2dArray[x, y] = true;
				//        }
				//    }
				//}
				//mapList.Add(temp2dArray);
			}
			catch (ArgumentException)
			{
				Console.Write("Error loading image" + Environment.NewLine);
			}
		}
	}
}
