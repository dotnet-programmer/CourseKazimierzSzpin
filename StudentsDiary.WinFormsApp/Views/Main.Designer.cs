namespace StudentsDiary.WinFormsApp;

partial class Main
{
	/// <summary>
	///  Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary>
	///  Clean up any resources being used.
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
	///  Required method for Designer support - do not modify
	///  the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
		flowLayoutPanel1 = new FlowLayoutPanel();
		BtnAdd = new Button();
		BtnEdit = new Button();
		BtnDelete = new Button();
		BtnRefresh = new Button();
		CbGroups = new ComboBox();
		DgvDiary = new DataGridView();
		flowLayoutPanel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)DgvDiary).BeginInit();
		SuspendLayout();
		// 
		// flowLayoutPanel1
		// 
		flowLayoutPanel1.Controls.Add(BtnAdd);
		flowLayoutPanel1.Controls.Add(BtnEdit);
		flowLayoutPanel1.Controls.Add(BtnDelete);
		flowLayoutPanel1.Controls.Add(BtnRefresh);
		flowLayoutPanel1.Controls.Add(CbGroups);
		flowLayoutPanel1.Dock = DockStyle.Top;
		flowLayoutPanel1.Location = new Point(0, 0);
		flowLayoutPanel1.Name = "flowLayoutPanel1";
		flowLayoutPanel1.Size = new Size(884, 48);
		flowLayoutPanel1.TabIndex = 0;
		// 
		// BtnAdd
		// 
		BtnAdd.BackColor = Color.LimeGreen;
		BtnAdd.Location = new Point(5, 8);
		BtnAdd.Margin = new Padding(5, 8, 5, 8);
		BtnAdd.Name = "BtnAdd";
		BtnAdd.Size = new Size(75, 30);
		BtnAdd.TabIndex = 0;
		BtnAdd.Text = "Dodaj";
		BtnAdd.UseVisualStyleBackColor = false;
		BtnAdd.Click += BtnAdd_Click;
		// 
		// BtnEdit
		// 
		BtnEdit.BackColor = Color.Goldenrod;
		BtnEdit.Location = new Point(90, 8);
		BtnEdit.Margin = new Padding(5, 8, 5, 8);
		BtnEdit.Name = "BtnEdit";
		BtnEdit.Size = new Size(75, 30);
		BtnEdit.TabIndex = 1;
		BtnEdit.Text = "Edytuj";
		BtnEdit.UseVisualStyleBackColor = false;
		BtnEdit.Click += BtnEdit_Click;
		// 
		// BtnDelete
		// 
		BtnDelete.BackColor = Color.Tomato;
		BtnDelete.Location = new Point(175, 8);
		BtnDelete.Margin = new Padding(5, 8, 5, 8);
		BtnDelete.Name = "BtnDelete";
		BtnDelete.Size = new Size(75, 30);
		BtnDelete.TabIndex = 2;
		BtnDelete.Text = "Usuń";
		BtnDelete.UseVisualStyleBackColor = false;
		BtnDelete.Click += BtnDelete_Click;
		// 
		// BtnRefresh
		// 
		BtnRefresh.BackColor = Color.CornflowerBlue;
		BtnRefresh.Location = new Point(260, 8);
		BtnRefresh.Margin = new Padding(5, 8, 5, 8);
		BtnRefresh.Name = "BtnRefresh";
		BtnRefresh.Size = new Size(75, 30);
		BtnRefresh.TabIndex = 3;
		BtnRefresh.Text = "Odśwież";
		BtnRefresh.UseVisualStyleBackColor = false;
		BtnRefresh.Click += BtnRefresh_Click;
		// 
		// CbGroups
		// 
		CbGroups.FormattingEnabled = true;
		CbGroups.Location = new Point(345, 12);
		CbGroups.Margin = new Padding(5, 12, 5, 5);
		CbGroups.Name = "CbGroups";
		CbGroups.Size = new Size(121, 23);
		CbGroups.TabIndex = 4;
		// 
		// DgvDiary
		// 
		DgvDiary.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
		DgvDiary.BackgroundColor = Color.White;
		DgvDiary.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		DgvDiary.Dock = DockStyle.Fill;
		DgvDiary.Location = new Point(0, 48);
		DgvDiary.MultiSelect = false;
		DgvDiary.Name = "DgvDiary";
		DgvDiary.ReadOnly = true;
		DgvDiary.RowHeadersVisible = false;
		DgvDiary.RowTemplate.Height = 25;
		DgvDiary.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		DgvDiary.Size = new Size(884, 413);
		DgvDiary.TabIndex = 1;
		DgvDiary.CellDoubleClick += DgvDiary_CellDoubleClick;
		// 
		// Main
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		BackColor = Color.WhiteSmoke;
		ClientSize = new Size(884, 461);
		Controls.Add(DgvDiary);
		Controls.Add(flowLayoutPanel1);
		MinimumSize = new Size(450, 150);
		Name = "Main";
		StartPosition = FormStartPosition.Manual;
		Text = "Dziennik ucznia";
		FormClosing += Main_FormClosing;
		flowLayoutPanel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)DgvDiary).EndInit();
		ResumeLayout(false);
	}

	#endregion

	private FlowLayoutPanel flowLayoutPanel1;
	private Button BtnAdd;
	private Button BtnEdit;
	private Button BtnDelete;
	private Button BtnRefresh;
	private ComboBox CbGroups;
	private DataGridView DgvDiary;
}
