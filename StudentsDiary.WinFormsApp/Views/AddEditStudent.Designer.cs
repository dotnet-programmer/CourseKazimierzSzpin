namespace StudentsDiary.WinFormsApp.Forms;

partial class AddEditStudent
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
		tableLayoutPanel1 = new TableLayoutPanel();
		label10 = new Label();
		label9 = new Label();
		label8 = new Label();
		TbForeignLang = new TextBox();
		label7 = new Label();
		TbPolishLang = new TextBox();
		label6 = new Label();
		TbPhysics = new TextBox();
		label5 = new Label();
		TbTechnology = new TextBox();
		label4 = new Label();
		TbMath = new TextBox();
		label3 = new Label();
		TbLastName = new TextBox();
		label2 = new Label();
		TbFirstName = new TextBox();
		label1 = new Label();
		TbId = new TextBox();
		RtbComments = new RichTextBox();
		CbGroups = new ComboBox();
		CbActivities = new CheckBox();
		flowLayoutPanel1 = new FlowLayoutPanel();
		BtnConfirm = new Button();
		BtnCancel = new Button();
		tableLayoutPanel1.SuspendLayout();
		flowLayoutPanel1.SuspendLayout();
		SuspendLayout();
		// 
		// tableLayoutPanel1
		// 
		tableLayoutPanel1.ColumnCount = 2;
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
		tableLayoutPanel1.Controls.Add(label10, 0, 9);
		tableLayoutPanel1.Controls.Add(label9, 0, 8);
		tableLayoutPanel1.Controls.Add(label8, 0, 7);
		tableLayoutPanel1.Controls.Add(TbForeignLang, 1, 7);
		tableLayoutPanel1.Controls.Add(label7, 0, 6);
		tableLayoutPanel1.Controls.Add(TbPolishLang, 1, 6);
		tableLayoutPanel1.Controls.Add(label6, 0, 5);
		tableLayoutPanel1.Controls.Add(TbPhysics, 1, 5);
		tableLayoutPanel1.Controls.Add(label5, 0, 4);
		tableLayoutPanel1.Controls.Add(TbTechnology, 1, 4);
		tableLayoutPanel1.Controls.Add(label4, 0, 3);
		tableLayoutPanel1.Controls.Add(TbMath, 1, 3);
		tableLayoutPanel1.Controls.Add(label3, 0, 2);
		tableLayoutPanel1.Controls.Add(TbLastName, 1, 2);
		tableLayoutPanel1.Controls.Add(label2, 0, 1);
		tableLayoutPanel1.Controls.Add(TbFirstName, 1, 1);
		tableLayoutPanel1.Controls.Add(label1, 0, 0);
		tableLayoutPanel1.Controls.Add(TbId, 1, 0);
		tableLayoutPanel1.Controls.Add(RtbComments, 1, 9);
		tableLayoutPanel1.Controls.Add(CbGroups, 1, 8);
		tableLayoutPanel1.Controls.Add(CbActivities, 1, 10);
		tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 1, 11);
		tableLayoutPanel1.Dock = DockStyle.Fill;
		tableLayoutPanel1.Location = new Point(0, 0);
		tableLayoutPanel1.Name = "tableLayoutPanel1";
		tableLayoutPanel1.RowCount = 12;
		tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
		tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
		tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
		tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
		tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
		tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
		tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
		tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
		tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
		tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
		tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
		tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
		tableLayoutPanel1.Size = new Size(404, 501);
		tableLayoutPanel1.TabIndex = 0;
		// 
		// label10
		// 
		label10.AutoSize = true;
		label10.Dock = DockStyle.Fill;
		label10.Location = new Point(3, 315);
		label10.Name = "label10";
		label10.Size = new Size(94, 106);
		label10.TabIndex = 18;
		label10.Text = "Uwagi";
		label10.TextAlign = ContentAlignment.MiddleRight;
		// 
		// label9
		// 
		label9.AutoSize = true;
		label9.Dock = DockStyle.Fill;
		label9.Location = new Point(3, 280);
		label9.Name = "label9";
		label9.Size = new Size(94, 35);
		label9.TabIndex = 16;
		label9.Text = "Grupa:";
		label9.TextAlign = ContentAlignment.MiddleRight;
		// 
		// label8
		// 
		label8.AutoSize = true;
		label8.Dock = DockStyle.Fill;
		label8.Location = new Point(3, 245);
		label8.Name = "label8";
		label8.Size = new Size(94, 35);
		label8.TabIndex = 14;
		label8.Text = "J. obcy:";
		label8.TextAlign = ContentAlignment.MiddleRight;
		// 
		// TbForeignLang
		// 
		TbForeignLang.Dock = DockStyle.Top;
		TbForeignLang.Location = new Point(103, 253);
		TbForeignLang.Margin = new Padding(3, 8, 3, 3);
		TbForeignLang.Name = "TbForeignLang";
		TbForeignLang.Size = new Size(298, 23);
		TbForeignLang.TabIndex = 15;
		// 
		// label7
		// 
		label7.AutoSize = true;
		label7.Dock = DockStyle.Fill;
		label7.Location = new Point(3, 210);
		label7.Name = "label7";
		label7.Size = new Size(94, 35);
		label7.TabIndex = 12;
		label7.Text = "J. pol.:";
		label7.TextAlign = ContentAlignment.MiddleRight;
		// 
		// TbPolishLang
		// 
		TbPolishLang.Dock = DockStyle.Top;
		TbPolishLang.Location = new Point(103, 218);
		TbPolishLang.Margin = new Padding(3, 8, 3, 3);
		TbPolishLang.Name = "TbPolishLang";
		TbPolishLang.Size = new Size(298, 23);
		TbPolishLang.TabIndex = 13;
		// 
		// label6
		// 
		label6.AutoSize = true;
		label6.Dock = DockStyle.Fill;
		label6.Location = new Point(3, 175);
		label6.Name = "label6";
		label6.Size = new Size(94, 35);
		label6.TabIndex = 10;
		label6.Text = "Fizyka:";
		label6.TextAlign = ContentAlignment.MiddleRight;
		// 
		// TbPhysics
		// 
		TbPhysics.Dock = DockStyle.Top;
		TbPhysics.Location = new Point(103, 183);
		TbPhysics.Margin = new Padding(3, 8, 3, 3);
		TbPhysics.Name = "TbPhysics";
		TbPhysics.Size = new Size(298, 23);
		TbPhysics.TabIndex = 11;
		// 
		// label5
		// 
		label5.AutoSize = true;
		label5.Dock = DockStyle.Fill;
		label5.Location = new Point(3, 140);
		label5.Name = "label5";
		label5.Size = new Size(94, 35);
		label5.TabIndex = 8;
		label5.Text = "Technologia:";
		label5.TextAlign = ContentAlignment.MiddleRight;
		// 
		// TbTechnology
		// 
		TbTechnology.Dock = DockStyle.Top;
		TbTechnology.Location = new Point(103, 148);
		TbTechnology.Margin = new Padding(3, 8, 3, 3);
		TbTechnology.Name = "TbTechnology";
		TbTechnology.Size = new Size(298, 23);
		TbTechnology.TabIndex = 9;
		// 
		// label4
		// 
		label4.AutoSize = true;
		label4.Dock = DockStyle.Fill;
		label4.Location = new Point(3, 105);
		label4.Name = "label4";
		label4.Size = new Size(94, 35);
		label4.TabIndex = 6;
		label4.Text = "Matematyka:";
		label4.TextAlign = ContentAlignment.MiddleRight;
		// 
		// TbMath
		// 
		TbMath.Dock = DockStyle.Top;
		TbMath.Location = new Point(103, 113);
		TbMath.Margin = new Padding(3, 8, 3, 3);
		TbMath.Name = "TbMath";
		TbMath.Size = new Size(298, 23);
		TbMath.TabIndex = 7;
		// 
		// label3
		// 
		label3.AutoSize = true;
		label3.Dock = DockStyle.Fill;
		label3.Location = new Point(3, 70);
		label3.Name = "label3";
		label3.Size = new Size(94, 35);
		label3.TabIndex = 4;
		label3.Text = "Nazwisko:";
		label3.TextAlign = ContentAlignment.MiddleRight;
		// 
		// TbLastName
		// 
		TbLastName.Dock = DockStyle.Top;
		TbLastName.Location = new Point(103, 78);
		TbLastName.Margin = new Padding(3, 8, 3, 3);
		TbLastName.Name = "TbLastName";
		TbLastName.Size = new Size(298, 23);
		TbLastName.TabIndex = 5;
		// 
		// label2
		// 
		label2.AutoSize = true;
		label2.Dock = DockStyle.Fill;
		label2.Location = new Point(3, 35);
		label2.Name = "label2";
		label2.Size = new Size(94, 35);
		label2.TabIndex = 2;
		label2.Text = "Imię:";
		label2.TextAlign = ContentAlignment.MiddleRight;
		// 
		// TbFirstName
		// 
		TbFirstName.Dock = DockStyle.Top;
		TbFirstName.Location = new Point(103, 43);
		TbFirstName.Margin = new Padding(3, 8, 3, 3);
		TbFirstName.Name = "TbFirstName";
		TbFirstName.Size = new Size(298, 23);
		TbFirstName.TabIndex = 3;
		// 
		// label1
		// 
		label1.AutoSize = true;
		label1.Dock = DockStyle.Fill;
		label1.Location = new Point(3, 0);
		label1.Name = "label1";
		label1.Size = new Size(94, 35);
		label1.TabIndex = 0;
		label1.Text = "Id:";
		label1.TextAlign = ContentAlignment.MiddleRight;
		// 
		// TbId
		// 
		TbId.Dock = DockStyle.Top;
		TbId.Enabled = false;
		TbId.Location = new Point(103, 8);
		TbId.Margin = new Padding(3, 8, 3, 3);
		TbId.Name = "TbId";
		TbId.ReadOnly = true;
		TbId.Size = new Size(298, 23);
		TbId.TabIndex = 1;
		// 
		// RtbComments
		// 
		RtbComments.Dock = DockStyle.Fill;
		RtbComments.Location = new Point(103, 318);
		RtbComments.Name = "RtbComments";
		RtbComments.Size = new Size(298, 100);
		RtbComments.TabIndex = 22;
		RtbComments.Text = "";
		// 
		// CbGroups
		// 
		CbGroups.Dock = DockStyle.Fill;
		CbGroups.FormattingEnabled = true;
		CbGroups.Location = new Point(103, 283);
		CbGroups.Name = "CbGroups";
		CbGroups.Size = new Size(298, 23);
		CbGroups.TabIndex = 23;
		// 
		// CbActivities
		// 
		CbActivities.AutoSize = true;
		CbActivities.Dock = DockStyle.Left;
		CbActivities.Location = new Point(103, 424);
		CbActivities.Name = "CbActivities";
		CbActivities.Size = new Size(125, 29);
		CbActivities.TabIndex = 24;
		CbActivities.Text = "Zajęcia dodatkowe";
		CbActivities.UseVisualStyleBackColor = true;
		// 
		// flowLayoutPanel1
		// 
		flowLayoutPanel1.Controls.Add(BtnConfirm);
		flowLayoutPanel1.Controls.Add(BtnCancel);
		flowLayoutPanel1.Dock = DockStyle.Fill;
		flowLayoutPanel1.Location = new Point(103, 459);
		flowLayoutPanel1.Name = "flowLayoutPanel1";
		flowLayoutPanel1.Size = new Size(298, 39);
		flowLayoutPanel1.TabIndex = 25;
		// 
		// BtnConfirm
		// 
		BtnConfirm.BackColor = Color.LimeGreen;
		BtnConfirm.Location = new Point(3, 3);
		BtnConfirm.Name = "BtnConfirm";
		BtnConfirm.Size = new Size(90, 35);
		BtnConfirm.TabIndex = 0;
		BtnConfirm.Text = "Zatwierdź";
		BtnConfirm.UseVisualStyleBackColor = false;
		BtnConfirm.Click += BtnConfirm_Click;
		// 
		// BtnCancel
		// 
		BtnCancel.BackColor = Color.Tomato;
		BtnCancel.Location = new Point(99, 3);
		BtnCancel.Name = "BtnCancel";
		BtnCancel.Size = new Size(90, 35);
		BtnCancel.TabIndex = 1;
		BtnCancel.Text = "Anuluj";
		BtnCancel.UseVisualStyleBackColor = false;
		BtnCancel.Click += BtnCancel_Click;
		// 
		// AddEditStudent
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(404, 501);
		Controls.Add(tableLayoutPanel1);
		MinimumSize = new Size(320, 540);
		Name = "AddEditStudent";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "Dodawanie ucznia";
		FormClosing += AddEditStudent_FormClosing;
		tableLayoutPanel1.ResumeLayout(false);
		tableLayoutPanel1.PerformLayout();
		flowLayoutPanel1.ResumeLayout(false);
		ResumeLayout(false);
	}

	#endregion

	private TableLayoutPanel tableLayoutPanel1;
	private Label label1;
	private TextBox TbId;
	private Label label10;
	private Label label9;
	private Label label8;
	private TextBox TbForeignLang;
	private Label label7;
	private TextBox TbPolishLang;
	private Label label6;
	private TextBox TbPhysics;
	private Label label5;
	private TextBox TbTechnology;
	private Label label4;
	private TextBox TbMath;
	private Label label3;
	private TextBox TbLastName;
	private Label label2;
	private TextBox TbFirstName;
	private RichTextBox RtbComments;
	private ComboBox CbGroups;
	private CheckBox CbActivities;
	private FlowLayoutPanel flowLayoutPanel1;
	private Button BtnConfirm;
	private Button BtnCancel;
}