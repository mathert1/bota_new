using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SyncdObjectCreator
{
	public partial class MainForm : Form
	{
		DataBaseConnection DBconn;
		List<GameObject> ObjectList = new List<GameObject>();

		public MainForm()
		{
			InitializeComponent();
			DBconn = new DataBaseConnection();
		}		

		private void LoadDBbtn_Click(object sender, EventArgs e)
		{
			if (tankRadBut.Checked)
			{
				ObjectList = DBconn.LoadObjectList("Tanks");
			}
			else if (skinRadBut.Checked)
			{
				ObjectList = DBconn.LoadObjectList("Skins");
			}
			else if (avaHeadRadBut.Checked)
			{
				ObjectList = DBconn.LoadObjectList("AvatarHead");
			}
			else if (avaChestRadBut.Checked)
			{
				ObjectList = DBconn.LoadObjectList("AvatarChest");
			}
			else if (avaShoulderRadBut.Checked)
			{
				ObjectList = DBconn.LoadObjectList("AvatarShoulder");
			}
			DataBindList();
		}

		public void ChangeObject(GameObject gO)
		{
			ObjectList[objectListBox.SelectedIndex] = gO;
			DataBindList();
		}

		public void AddObject(GameObject gO)
		{
			ObjectList.Add(gO);
			DataBindList();
		}

		private void addBtn_Click(object sender, EventArgs e)
		{
			EditForm ef = new EditForm(this, new GameObject(), 2);
			this.Hide();
			ef.Show();
		}

		private void editBtn_Click(object sender, EventArgs e)
		{
			EditForm ef = new EditForm(this, (GameObject)objectListBox.SelectedItem, 1);
			this.Hide();
			ef.Show();
		}

		private void CloseBtn_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void saveToTextBtn_Click(object sender, EventArgs e)
		{
			if (ObjectList.Count > 0)
			{
				string fileName = "";
				string text = "";
				if (tankRadBut.Checked)
				{
					fileName = "Tanks";
				}
				else if (skinRadBut.Checked)
				{
					fileName = "Skins";
				}
				else if (avaHeadRadBut.Checked)
				{
					fileName = "AvatarHead";
				}
				else if (avaChestRadBut.Checked)
				{
					fileName = "AvatarChest";
				}
				else if (avaShoulderRadBut.Checked)
				{
					fileName = "AvatarShoulder";
				}				

				foreach (GameObject gO in ObjectList)
				{
					text += gO.ID + "|" + gO.Type + "|" + gO.Name + "|" +
						gO.FilePath + "|" + gO.GoldCost + "|" +
						gO.Attack + "|" + gO.Defense + "|" +
						gO.Health + "|" + gO.Shield + "|" +
						gO.Move + "|" + gO.Energy + "|" +
						gO.Popularity + Environment.NewLine;
				}
				
				System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + fileName + ".txt", text);
			}
		}

		private void LoadTxtBtn_Click(object sender, EventArgs e)
		{
			string fileName = "";
			
			if (tankRadBut.Checked)
			{
				fileName = "Tanks";
			}
			else if (skinRadBut.Checked)
			{
				fileName = "Skins";
			}
			else if (avaHeadRadBut.Checked)
			{
				fileName = "AvatarHead";
			}
			else if (avaChestRadBut.Checked)
			{
				fileName = "AvatarChest";
			}
			else if (avaShoulderRadBut.Checked)
			{
				fileName = "AvatarShoulder";
			}
			TextReader tr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + fileName + ".txt");
			string s = "";
			ObjectList = new List<GameObject>();
			while ((s = tr.ReadLine()) != null)
			{
				string[] words = s.Split('|');
				GameObject gO = new GameObject();
				gO.ID = Convert.ToInt32(words[0]);
				gO.Type = Convert.ToInt32(words[1]);
				gO.Name = words[2];
				gO.FilePath = words[3];
				gO.GoldCost = Convert.ToInt32(words[4]);
				gO.Attack = Convert.ToInt32(words[5]);
				gO.Defense = Convert.ToInt32(words[6]);
				gO.Health = Convert.ToInt32(words[7]);
				gO.Shield = Convert.ToInt32(words[8]);
				gO.Move = Convert.ToInt32(words[9]);
				gO.Energy = Convert.ToInt32(words[10]);
				gO.Popularity = Convert.ToInt32(words[11]);
				ObjectList.Add(gO);
			}
			tr.Close();
			DataBindList();
		}

		private void saveDBbtn_Click(object sender, EventArgs e)
		{
			string fileName = "";

			if (tankRadBut.Checked)
			{
				fileName = "Tanks";
			}
			else if (skinRadBut.Checked)
			{
				fileName = "Skins";
			}
			else if (avaHeadRadBut.Checked)
			{
				fileName = "AvatarHead";
			}
			else if (avaChestRadBut.Checked)
			{
				fileName = "AvatarChest";
			}
			else if (avaShoulderRadBut.Checked)
			{
				fileName = "AvatarShoulder";
			}

			DBconn.SaveObjectList(ObjectList,fileName);

		}

		private void deleteBtn_Click(object sender, EventArgs e)
		{			
			ObjectList.Remove((GameObject)objectListBox.SelectedItem);
			objectListBox.SelectedIndex = 0;
			DataBindList();			
		}

		private void DataBindList()
		{
			objectListBox.DataSource = null;
			objectListBox.DisplayMember = "Name";
			objectListBox.DataSource = ObjectList;
		}
	}
}
