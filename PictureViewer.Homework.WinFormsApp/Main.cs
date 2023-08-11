using PictureViewer.Homework.WinFormsApp.Properties;

namespace PictureViewer.Homework.WinFormsApp;

public partial class Main : Form
{
	private string _filePath;

	public Main()
	{
		InitializeComponent();
		SetApplicationSettings();
		SetOpenPictureBoxSettings();
	}

	private void BtnOpenPicture_Click(object sender, EventArgs e)
	{
		if (OpenFile.ShowDialog() == DialogResult.OK)
		{
			_filePath = OpenFile.FileName;
			SetPicture(_filePath);
		}
	}

	private void BtnDeletePicture_Click(object sender, EventArgs e)
	{
		PbPicture.Image = null;
		_filePath = string.Empty;
		SetButtonsEnabled(false);
		SetSaveButtonsEnabled(false);
	}

	private void OnSave_Click(object sender, EventArgs e)
	{
		string btnName = (sender as Button).Name;
		string path = btnName == BtnSaveCopy.Name ?
			Path.Combine(
				Path.GetDirectoryName(_filePath),
				$"{Path.GetFileNameWithoutExtension(_filePath)}_edited_{DateTime.Now:dd-MM-yyyy}_{DateTime.Now:HH_mm}{Path.GetExtension(_filePath)}") :
			_filePath;

		PbPicture.Image.Save(path);
		MessageBox.Show("Zmiany zosta³y zapisane", "Zapis pliku", MessageBoxButtons.OK, MessageBoxIcon.Information);
	}

	private void OnRotation_Click(object sender, EventArgs e)
	{
		string btnName = (sender as Button).Name;
		RotateDirection direction = btnName switch
		{
			nameof(Main.BtnRotateLeft) => RotateDirection.Left,
			nameof(Main.BtnRotateRight) => RotateDirection.Right,
			nameof(Main.BtnFlipHorizontal) => RotateDirection.Horizontal,
			nameof(Main.BtnFlipVertical) => RotateDirection.Vertical,
			_ => RotateDirection.None
		};
		RotateImage(direction);
	}

	private void Main_Shown(object sender, EventArgs e) => LoadLastOpenedPicture();

	private void Main_FormClosed(object sender, FormClosedEventArgs e)
	{
		Settings.Default.FilePath = _filePath;
		Settings.Default.IsMaximize = WindowState == FormWindowState.Maximized;
		if (WindowState != FormWindowState.Maximized)
		{
			Settings.Default.MainWindowWidth = Width;
			Settings.Default.MainWindowHeight = Height;
		}
		Settings.Default.Save();
	}

	private void SetApplicationSettings()
	{
		_filePath = Settings.Default.FilePath;
		WindowState = Settings.Default.IsMaximize ? FormWindowState.Maximized : FormWindowState.Normal;
		if (WindowState != FormWindowState.Maximized)
		{
			Width = Settings.Default.MainWindowWidth;
			Height = Settings.Default.MainWindowHeight;
		}
	}

	private void SetOpenPictureBoxSettings() => OpenFile.Filter = "Pliki jpg|*.jpg|" + "Pliki gif|*.gif|" + "Pliki png|*.png|" + "Wszystkie pliki|*.*";

	private void LoadLastOpenedPicture() => SetPicture(_filePath);

	private void SetPictureBoxSizeMode(Image image) => PbPicture.SizeMode = image.Height < PbPicture.Height ? PictureBoxSizeMode.CenterImage : PictureBoxSizeMode.Zoom;

	private void SetButtonsEnabled(bool enabled) => BtnDeletePicture.Enabled = BtnRotateLeft.Enabled = BtnRotateRight.Enabled = BtnFlipHorizontal.Enabled = BtnFlipVertical.Enabled = enabled;

	private void SetSaveButtonsEnabled(bool enabled) => BtnSaveOrginal.Enabled = BtnSaveCopy.Enabled = enabled;

	private void SetPicture(string filePath)
	{
		if (!string.IsNullOrWhiteSpace(filePath) && File.Exists(filePath))
		{
			Image image = Image.FromFile(filePath);
			SetPictureBoxSizeMode(image);
			PbPicture.Image = image;
			SetButtonsEnabled(true);
		}
	}

	private void RotateImage(RotateDirection direction)
	{
		SetSaveButtonsEnabled(true);
		Image image = PbPicture.Image;
		switch (direction)
		{
			case RotateDirection.Left:
				image.RotateFlip(RotateFlipType.Rotate270FlipNone);
				break;
			case RotateDirection.Right:
				image.RotateFlip(RotateFlipType.Rotate90FlipNone);
				break;
			case RotateDirection.Horizontal:
				image.RotateFlip(RotateFlipType.RotateNoneFlipY);
				break;
			case RotateDirection.Vertical:
				image.RotateFlip(RotateFlipType.RotateNoneFlipX);
				break;
		}
		SetPictureBoxSizeMode(image);
		PbPicture.Image = image;
	}
}