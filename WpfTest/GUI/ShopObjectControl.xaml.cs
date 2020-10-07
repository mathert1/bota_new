﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MSCommon;

namespace WpfTest.GUI
{
	/// <summary>
	/// Interaction logic for ShopObjectControl.xaml
	/// </summary>
	public partial class ShopObjectControl : UserControl
	{
		public ShopObjectControl()
		{
			InitializeComponent();
		}

		private void UserControl_MouseEnter(object sender, MouseEventArgs e)
		{
			stackPanel1.Visibility = System.Windows.Visibility.Visible;
		}

		private void UserControl_MouseLeave(object sender, MouseEventArgs e)
		{
			stackPanel1.Visibility = System.Windows.Visibility.Hidden;
		}		

		private void tryBtn_Click(object sender, RoutedEventArgs e)
		{
            var owner1 = FindVisualParent<Window>(this);
            ShopWindow test1 = owner1 as ShopWindow;

			UpgradeObject tempObj = this.DataContext as UpgradeObject;
			switch (tempObj.Type)
			{
                case 1:
                    test1.tempTank = tempObj.ID;
                    //Variables.TempTank = tempObj.ID;
                    break;
                case 2:
                    test1.tempAvaHead = tempObj.ID;
                    //Variables.TempAvaHead = tempObj.ID;
                    break;
                case 3:
                    test1.tempAvaShoulder = tempObj.ID;
                    //Variables.TempAvaShoulder = tempObj.ID;
                    break;
                case 4:
                    test1.tempAvaChest = tempObj.ID;
                    //Variables.TempAvaChest = tempObj.ID;
                    break;
                case 5:
                    test1.tempSkin = tempObj.ID;
                    //Variables.TempSkin = tempObj.ID;
                    break;
			}			
			test1.UpdateChar();
		}

		private void buyBtn_Click(object sender, RoutedEventArgs e)
		{
			UpgradeObject tempObj = this.DataContext as UpgradeObject;
            var owner1 = FindVisualParent<Window>(this);
            ShopWindow test1 = owner1 as ShopWindow;
            test1.PurchaseItem(tempObj.Type, tempObj.ID, tempObj.GoldCost);
            //if (tempObj.GoldCost < Variables.player.Gold)
            //{
            //    var owner1 = FindVisualParent<Window>(this);
            //    ShopWindow test1 = owner1 as ShopWindow;
            //    test1.PurchaseItem(tempObj);
            //}
            //else
            //    MessageBox.Show("You don't have enough gold.", "Uh oh!");
		}

		public static T FindVisualParent<T>(DependencyObject child)
			where T : DependencyObject
		{
			// get parent item
			DependencyObject parentObject = VisualTreeHelper.GetParent(child);

			// we’ve reached the end of the tree
			if (parentObject == null) return null;

			// check if the parent matches the type we’re looking for
			T parent = parentObject as T;
			if (parent != null)
			{
				return parent;
			}
			else
			{
				// use recursion to proceed with next level
				return FindVisualParent<T>(parentObject);
			}
		}
	}
}