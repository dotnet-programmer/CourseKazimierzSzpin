namespace PictureViewer.Homework.WinFormsApp;

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
		BottomPanel = new FlowLayoutPanel();
		BtnOpenPicture = new Button();
		BtnDeletePicture = new Button();
		BtnSaveOrginal = new Button();
		BtnSaveCopy = new Button();
		BtnRotateLeft = new Button();
		BtnRotateRight = new Button();
		BtnFlipHorizontal = new Button();
		BtnFlipVertical = new Button();
		PbPicture = new PictureBox();
		OpenFile = new OpenFileDialog();
		BottomPanel.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)PbPicture).BeginInit();
		SuspendLayout();
		// 
		// BottomPanel
		// 
		BottomPanel.Controls.Add(BtnOpenPicture);
		BottomPanel.Controls.Add(BtnDeletePicture);
		BottomPanel.Controls.Add(BtnSaveOrginal);
		BottomPanel.Controls.Add(BtnSaveCopy);
		BottomPanel.Controls.Add(BtnRotateLeft);
		BottomPanel.Controls.Add(BtnRotateRight);
		BottomPanel.Controls.Add(BtnFlipHorizontal);
		BottomPanel.Controls.Add(BtnFlipVertical);
		BottomPanel.Dock = DockStyle.Bottom;
		BottomPanel.Location = new Point(0, 398);
		BottomPanel.Margin = new Padding(5);
		BottomPanel.Name = "BottomPanel";
		BottomPanel.Size = new Size(800, 52);
		BottomPanel.TabIndex = 0;
		// 
		// BtnOpenPicture
		// 
		BtnOpenPicture.Location = new Point(6, 6);
		BtnOpenPicture.Margin = new Padding(6);
		BtnOpenPicture.Name = "BtnOpenPicture";
		BtnOpenPicture.Size = new Size(110, 40);
		BtnOpenPicture.TabIndex = 0;
		BtnOpenPicture.Text = "Otwórz zdjęcie";
		BtnOpenPicture.UseVisualStyleBackColor = true;
		BtnOpenPicture.Click += BtnOpenPicture_Click;
		// 
		// BtnDeletePicture
		// 
		BtnDeletePicture.Location = new Point(128, 6);
		BtnDeletePicture.Margin = new Padding(6);
		BtnDeletePicture.Name = "BtnDeletePicture";
		BtnDeletePicture.Size = new Size(110, 40);
		BtnDeletePicture.TabIndex = 1;
		BtnDeletePicture.Text = "Usuń zdjęcie";
		BtnDeletePicture.UseVisualStyleBackColor = true;
		BtnDeletePicture.Click += BtnDeletePicture_Click;
		// 
		// BtnSaveOrginal
		// 
		BtnSaveOrginal.Location = new Point(250, 6);
		BtnSaveOrginal.Margin = new Padding(6);
		BtnSaveOrginal.Name = "BtnSaveOrginal";
		BtnSaveOrginal.Size = new Size(110, 40);
		BtnSaveOrginal.TabIndex = 2;
		BtnSaveOrginal.Text = "Zapisz zmiany";
		BtnSaveOrginal.UseVisualStyleBackColor = true;
		BtnSaveOrginal.Click += OnSave_Click;
		// 
		// BtnSaveCopy
		// 
		BtnSaveCopy.Location = new Point(372, 6);
		BtnSaveCopy.Margin = new Padding(6);
		BtnSaveCopy.Name = "BtnSaveCopy";
		BtnSaveCopy.Size = new Size(110, 40);
		BtnSaveCopy.TabIndex = 3;
		BtnSaveCopy.Text = "Zapisz kopię";
		BtnSaveCopy.UseVisualStyleBackColor = true;
		BtnSaveCopy.Click += OnSave_Click;
		// 
		// BtnRotateLeft
		// 
		BtnRotateLeft.Image = Properties.Resources.RotateLeft;
		BtnRotateLeft.Location = new Point(494, 6);
		BtnRotateLeft.Margin = new Padding(6);
		BtnRotateLeft.Name = "BtnRotateLeft";
		BtnRotateLeft.Size = new Size(40, 40);
		BtnRotateLeft.TabIndex = 4;
		BtnRotateLeft.UseVisualStyleBackColor = true;
		BtnRotateLeft.Click += OnRotation_Click;
		// 
		// BtnRotateRight
		// 
		BtnRotateRight.Image = Properties.Resources.RotateRight;
		BtnRotateRight.Location = new Point(546, 6);
		BtnRotateRight.Margin = new Padding(6);
		BtnRotateRight.Name = "BtnRotateRight";
		BtnRotateRight.Size = new Size(40, 40);
		BtnRotateRight.TabIndex = 5;
		BtnRotateRight.UseVisualStyleBackColor = true;
		BtnRotateRight.Click += OnRotation_Click;
		// 
		// BtnFlipHorizontal
		// 
		BtnFlipHorizontal.Image = Properties.Resources.Horizontal;
		BtnFlipHorizontal.Location = new Point(598, 6);
		BtnFlipHorizontal.Margin = new Padding(6);
		BtnFlipHorizontal.Name = "BtnFlipHorizontal";
		BtnFlipHorizontal.Size = new Size(40, 40);
		BtnFlipHorizontal.TabIndex = 6;
		BtnFlipHorizontal.UseVisualStyleBackColor = true;
		BtnFlipHorizontal.Click += OnRotation_Click;
		// 
		// BtnFlipVertical
		// 
		BtnFlipVertical.Image = Properties.Resources.Vertical;
		BtnFlipVertical.Location = new Point(650, 6);
		BtnFlipVertical.Margin = new Padding(6);
		BtnFlipVertical.Name = "BtnFlipVertical";
		BtnFlipVertical.Size = new Size(40, 40);
		BtnFlipVertical.TabIndex = 7;
		BtnFlipVertical.UseVisualStyleBackColor = true;
		BtnFlipVertical.Click += OnRotation_Click;
		// 
		// PbPicture
		// 
		PbPicture.Dock = DockStyle.Fill;
		PbPicture.Location = new Point(0, 0);
		PbPicture.Name = "PbPicture";
		PbPicture.Size = new Size(800, 398);
		PbPicture.TabIndex = 1;
		PbPicture.TabStop = false;
		// 
		// Main
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(800, 450);
		Controls.Add(PbPicture);
		Controls.Add(BottomPanel);
		Name = "Main";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "Przegladarka zdjęć";
		FormClosing += Main_FormClosing;
		FormClosed += Main_FormClosed;
		Shown += Main_Shown;
		BottomPanel.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)PbPicture).EndInit();
		ResumeLayout(false);
	}

	#endregion

	private FlowLayoutPanel BottomPanel;
	private Button BtnOpenPicture;
	private Button BtnDeletePicture;
	private Button BtnSaveOrginal;
	private Button BtnSaveCopy;
	private Button BtnRotateLeft;
	private PictureBox PbPicture;
	private Button BtnRotateRight;
	private Button BtnFlipHorizontal;
	private Button BtnFlipVertical;
	private OpenFileDialog OpenFile;
}
