namespace SyncdObjectCreator
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tankRadBut = new System.Windows.Forms.RadioButton();
			this.objTypeGroupBox = new System.Windows.Forms.GroupBox();
			this.avaChestRadBut = new System.Windows.Forms.RadioButton();
			this.avaShoulderRadBut = new System.Windows.Forms.RadioButton();
			this.avaHeadRadBut = new System.Windows.Forms.RadioButton();
			this.skinRadBut = new System.Windows.Forms.RadioButton();
			this.LoadTxtBtn = new System.Windows.Forms.Button();
			this.LoadDBbtn = new System.Windows.Forms.Button();
			this.objectListBox = new System.Windows.Forms.ListBox();
			this.saveDBbtn = new System.Windows.Forms.Button();
			this.saveToTextBtn = new System.Windows.Forms.Button();
			this.editBtn = new System.Windows.Forms.Button();
			this.addBtn = new System.Windows.Forms.Button();
			this.CloseBtn = new System.Windows.Forms.Button();
			this.deleteBtn = new System.Windows.Forms.Button();
			this.objTypeGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// tankRadBut
			// 
			this.tankRadBut.AutoSize = true;
			this.tankRadBut.Location = new System.Drawing.Point(6, 19);
			this.tankRadBut.Name = "tankRadBut";
			this.tankRadBut.Size = new System.Drawing.Size(50, 17);
			this.tankRadBut.TabIndex = 0;
			this.tankRadBut.TabStop = true;
			this.tankRadBut.Text = "Tank";
			this.tankRadBut.UseVisualStyleBackColor = true;
			// 
			// objTypeGroupBox
			// 
			this.objTypeGroupBox.Controls.Add(this.avaChestRadBut);
			this.objTypeGroupBox.Controls.Add(this.avaShoulderRadBut);
			this.objTypeGroupBox.Controls.Add(this.avaHeadRadBut);
			this.objTypeGroupBox.Controls.Add(this.skinRadBut);
			this.objTypeGroupBox.Controls.Add(this.tankRadBut);
			this.objTypeGroupBox.Location = new System.Drawing.Point(12, 12);
			this.objTypeGroupBox.Name = "objTypeGroupBox";
			this.objTypeGroupBox.Size = new System.Drawing.Size(130, 135);
			this.objTypeGroupBox.TabIndex = 1;
			this.objTypeGroupBox.TabStop = false;
			this.objTypeGroupBox.Text = "Object Type";
			// 
			// avaChestRadBut
			// 
			this.avaChestRadBut.AutoSize = true;
			this.avaChestRadBut.Location = new System.Drawing.Point(6, 111);
			this.avaChestRadBut.Name = "avaChestRadBut";
			this.avaChestRadBut.Size = new System.Drawing.Size(86, 17);
			this.avaChestRadBut.TabIndex = 4;
			this.avaChestRadBut.TabStop = true;
			this.avaChestRadBut.Text = "Avatar Chest";
			this.avaChestRadBut.UseVisualStyleBackColor = true;
			// 
			// avaShoulderRadBut
			// 
			this.avaShoulderRadBut.AutoSize = true;
			this.avaShoulderRadBut.Location = new System.Drawing.Point(6, 88);
			this.avaShoulderRadBut.Name = "avaShoulderRadBut";
			this.avaShoulderRadBut.Size = new System.Drawing.Size(101, 17);
			this.avaShoulderRadBut.TabIndex = 3;
			this.avaShoulderRadBut.TabStop = true;
			this.avaShoulderRadBut.Text = "Avatar Shoulder";
			this.avaShoulderRadBut.UseVisualStyleBackColor = true;
			// 
			// avaHeadRadBut
			// 
			this.avaHeadRadBut.AutoSize = true;
			this.avaHeadRadBut.Location = new System.Drawing.Point(6, 65);
			this.avaHeadRadBut.Name = "avaHeadRadBut";
			this.avaHeadRadBut.Size = new System.Drawing.Size(85, 17);
			this.avaHeadRadBut.TabIndex = 2;
			this.avaHeadRadBut.TabStop = true;
			this.avaHeadRadBut.Text = "Avatar Head";
			this.avaHeadRadBut.UseVisualStyleBackColor = true;
			// 
			// skinRadBut
			// 
			this.skinRadBut.AutoSize = true;
			this.skinRadBut.Location = new System.Drawing.Point(6, 42);
			this.skinRadBut.Name = "skinRadBut";
			this.skinRadBut.Size = new System.Drawing.Size(46, 17);
			this.skinRadBut.TabIndex = 1;
			this.skinRadBut.TabStop = true;
			this.skinRadBut.Text = "Skin";
			this.skinRadBut.UseVisualStyleBackColor = true;
			// 
			// LoadTxtBtn
			// 
			this.LoadTxtBtn.Location = new System.Drawing.Point(12, 202);
			this.LoadTxtBtn.Name = "LoadTxtBtn";
			this.LoadTxtBtn.Size = new System.Drawing.Size(123, 41);
			this.LoadTxtBtn.TabIndex = 2;
			this.LoadTxtBtn.Text = "Load From Text File";
			this.LoadTxtBtn.UseVisualStyleBackColor = true;
			this.LoadTxtBtn.Click += new System.EventHandler(this.LoadTxtBtn_Click);
			// 
			// LoadDBbtn
			// 
			this.LoadDBbtn.Location = new System.Drawing.Point(12, 153);
			this.LoadDBbtn.Name = "LoadDBbtn";
			this.LoadDBbtn.Size = new System.Drawing.Size(124, 43);
			this.LoadDBbtn.TabIndex = 3;
			this.LoadDBbtn.Text = "Load From DataBase";
			this.LoadDBbtn.UseVisualStyleBackColor = true;
			this.LoadDBbtn.Click += new System.EventHandler(this.LoadDBbtn_Click);
			// 
			// objectListBox
			// 
			this.objectListBox.FormattingEnabled = true;
			this.objectListBox.Location = new System.Drawing.Point(148, 15);
			this.objectListBox.Name = "objectListBox";
			this.objectListBox.Size = new System.Drawing.Size(233, 407);
			this.objectListBox.TabIndex = 4;
			// 
			// saveDBbtn
			// 
			this.saveDBbtn.Location = new System.Drawing.Point(12, 425);
			this.saveDBbtn.Name = "saveDBbtn";
			this.saveDBbtn.Size = new System.Drawing.Size(124, 36);
			this.saveDBbtn.TabIndex = 5;
			this.saveDBbtn.Text = "Save to Database";
			this.saveDBbtn.UseVisualStyleBackColor = true;
			this.saveDBbtn.Click += new System.EventHandler(this.saveDBbtn_Click);
			// 
			// saveToTextBtn
			// 
			this.saveToTextBtn.Location = new System.Drawing.Point(12, 383);
			this.saveToTextBtn.Name = "saveToTextBtn";
			this.saveToTextBtn.Size = new System.Drawing.Size(123, 36);
			this.saveToTextBtn.TabIndex = 6;
			this.saveToTextBtn.Text = "Save to Text File";
			this.saveToTextBtn.UseVisualStyleBackColor = true;
			this.saveToTextBtn.Click += new System.EventHandler(this.saveToTextBtn_Click);
			// 
			// editBtn
			// 
			this.editBtn.Location = new System.Drawing.Point(18, 282);
			this.editBtn.Name = "editBtn";
			this.editBtn.Size = new System.Drawing.Size(109, 43);
			this.editBtn.TabIndex = 7;
			this.editBtn.Text = "Edit";
			this.editBtn.UseVisualStyleBackColor = true;
			this.editBtn.Click += new System.EventHandler(this.editBtn_Click);
			// 
			// addBtn
			// 
			this.addBtn.Location = new System.Drawing.Point(18, 331);
			this.addBtn.Name = "addBtn";
			this.addBtn.Size = new System.Drawing.Size(109, 39);
			this.addBtn.TabIndex = 8;
			this.addBtn.Text = "Add";
			this.addBtn.UseVisualStyleBackColor = true;
			this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
			// 
			// CloseBtn
			// 
			this.CloseBtn.Location = new System.Drawing.Point(284, 425);
			this.CloseBtn.Name = "CloseBtn";
			this.CloseBtn.Size = new System.Drawing.Size(97, 36);
			this.CloseBtn.TabIndex = 9;
			this.CloseBtn.Text = "Close";
			this.CloseBtn.UseVisualStyleBackColor = true;
			this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
			// 
			// deleteBtn
			// 
			this.deleteBtn.Location = new System.Drawing.Point(18, 250);
			this.deleteBtn.Name = "deleteBtn";
			this.deleteBtn.Size = new System.Drawing.Size(109, 26);
			this.deleteBtn.TabIndex = 10;
			this.deleteBtn.Text = "Delete";
			this.deleteBtn.UseVisualStyleBackColor = true;
			this.deleteBtn.Click += new System.EventHandler(this.deleteBtn_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(387, 468);
			this.Controls.Add(this.deleteBtn);
			this.Controls.Add(this.CloseBtn);
			this.Controls.Add(this.addBtn);
			this.Controls.Add(this.editBtn);
			this.Controls.Add(this.saveToTextBtn);
			this.Controls.Add(this.saveDBbtn);
			this.Controls.Add(this.objectListBox);
			this.Controls.Add(this.LoadDBbtn);
			this.Controls.Add(this.LoadTxtBtn);
			this.Controls.Add(this.objTypeGroupBox);
			this.Name = "MainForm";
			this.Text = "Main Form";
			this.objTypeGroupBox.ResumeLayout(false);
			this.objTypeGroupBox.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.RadioButton tankRadBut;
		private System.Windows.Forms.GroupBox objTypeGroupBox;
		private System.Windows.Forms.RadioButton avaChestRadBut;
		private System.Windows.Forms.RadioButton avaShoulderRadBut;
		private System.Windows.Forms.RadioButton avaHeadRadBut;
		private System.Windows.Forms.RadioButton skinRadBut;
		private System.Windows.Forms.Button LoadTxtBtn;
		private System.Windows.Forms.Button LoadDBbtn;
		private System.Windows.Forms.ListBox objectListBox;
		private System.Windows.Forms.Button saveDBbtn;
		private System.Windows.Forms.Button saveToTextBtn;
		private System.Windows.Forms.Button editBtn;
		private System.Windows.Forms.Button addBtn;
		private System.Windows.Forms.Button CloseBtn;
		private System.Windows.Forms.Button deleteBtn;
	}
}

