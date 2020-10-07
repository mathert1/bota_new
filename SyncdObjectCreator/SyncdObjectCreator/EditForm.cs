using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SyncdObjectCreator
{
	public partial class EditForm : Form
	{
		MainForm MF;
		int action = 0;
		GameObject gObject = new GameObject();

		public EditForm(MainForm m, GameObject gO, int act)
		{
			InitializeComponent();
			MF = m;
			action = act;
			gObject = gO;
			SetUpTextBoxes();	
		}

		private void SetUpTextBoxes()
		{
			if (action == 1)
			{
				IDtxtBox.Text = gObject.ID.ToString();
				typeTxtBox.Text = gObject.Type.ToString();
				nameTxtBox.Text = gObject.Name;
				filePathTxtBox.Text = gObject.FilePath;
				goldCostTxtBox.Text = gObject.GoldCost.ToString();
				attackTxtBox.Text = gObject.Attack.ToString(); ;
				defenseTxtBox.Text = gObject.Defense.ToString();
				shieldTxtBox.Text = gObject.Shield.ToString();
				healthTxtBox.Text = gObject.Health.ToString();
				shieldTxtBox.Text = gObject.Shield.ToString();
				moveTxtBox.Text = gObject.Move.ToString();
				energyTxtBox.Text = gObject.Energy.ToString();
				popularityTxtBox.Text = gObject.Popularity.ToString();
			}
			else if (action == 2)
			{
			}

		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			GameObject gO = new GameObject();
			gO.ID = Convert.ToInt32(IDtxtBox.Text);
			gO.Type = Convert.ToInt32(typeTxtBox.Text);
			gO.Name = nameTxtBox.Text;
			gO.FilePath = filePathTxtBox.Text;
			gO.GoldCost = Convert.ToInt32(goldCostTxtBox.Text);
			gO.Attack = Convert.ToInt32(attackTxtBox.Text);
			gO.Defense = Convert.ToInt32(defenseTxtBox.Text);
			gO.Health = Convert.ToInt32(healthTxtBox.Text);
			gO.Shield = Convert.ToInt32(shieldTxtBox.Text);
			gO.Move = Convert.ToInt32(moveTxtBox.Text);
			gO.Energy = Convert.ToInt32(energyTxtBox.Text);
			gO.Popularity = Convert.ToInt32(popularityTxtBox.Text);

			if (action == 1)
				MF.ChangeObject(gO);
			else if (action == 2)
				MF.AddObject(gO);

			MF.Show();
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			MF.Show();
			this.Close();
		}
	}
}
