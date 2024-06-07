using StudentsDiary.WinFormsApp.FileSerialization;
using StudentsDiary.WinFormsApp.Helpers;
using StudentsDiary.WinFormsApp.Models;

namespace StudentsDiary.WinFormsApp.Forms;

public partial class AddEditStudent : Form
{
	private readonly SerializeToFile<List<Student>> _dataFile = new SerializeToXml<List<Student>>(Program.DataFilePath, Program.DataFileName);
	private readonly Student _student;

	private ConfigurationManager _configurationManager;
	private List<Group> _groups;

	public AddEditStudent()
	{
		InitializeComponent();
		SetWindowState();
		FillGroups();
	}

	public AddEditStudent(Student student) : this()
	{
		//if (student is null)
		//{
		//	throw new ArgumentNullException(nameof(student));
		//}
		ArgumentNullException.ThrowIfNull(student);
		_student = student;
		Text = "Edytowanie danych ucznia";
		FillTextBoxes();
	}

	private void BtnConfirm_Click(object sender, EventArgs e)
	{
		if (!IsFieldsProperlyFill())
		{
			return;
		}

		var students = _dataFile.Deserialize();
		DeleteEditedStudentFromList(students);
		AddNewUserToList(students);
		SaveDiaryToFile(students);
		DialogResult = DialogResult.OK;
		Close();
	}

	private void BtnCancel_Click(object sender, EventArgs e)
	{
		DialogResult = DialogResult.Cancel;
		Close();
	}

	private void AddEditStudent_FormClosing(object sender, FormClosingEventArgs e)
		=> _configurationManager.SaveConfiguration();

	private void SetWindowState()
	{
		_configurationManager = new(this);
		_configurationManager.LoadConfiguration();
	}

	private void FillGroups()
	{
		_groups = GroupHelper.GetGroups("Brak");
		CbGroups.DataSource = _groups;
		CbGroups.DisplayMember = nameof(Group.Name);
		CbGroups.ValueMember = nameof(Group.Id);
	}

	private void FillTextBoxes()
	{
		TbId.Text = _student.Id.ToString();
		TbFirstName.Text = _student.FirstName;
		TbLastName.Text = _student.LastName;
		TbMath.Text = _student.Math;
		TbTechnology.Text = _student.Technology;
		TbPhysics.Text = _student.Physics;
		TbPolishLang.Text = _student.PolishLang;
		TbForeignLang.Text = _student.ForeignLang;
		CbGroups.SelectedItem = _groups.FirstOrDefault(x => x.Id == _student.GroupId);
		RtbComments.Text = _student.Comments;
		CbActivities.Checked = _student.Activities;
	}

	private bool IsFieldsProperlyFill()
	{
		if (string.IsNullOrWhiteSpace(TbFirstName.Text) || string.IsNullOrWhiteSpace(TbLastName.Text))
		{
			MessageBox.Show("Pola imię i nazwisko muszą być wypełnione!");
			return false;
		}

		if (CbGroups.SelectedIndex < 0)
		{
			MessageBox.Show("Wybierz poprawną grupę z listy!");
			return false;
		}

		return true;
	}

	private void DeleteEditedStudentFromList(List<Student> students)
	{
		if (_student != null)
		{
			students.RemoveAll(x => x.Id == _student.Id);
		}
	}

	private void AddNewUserToList(List<Student> students)
	{
		students.Add(new Student()
		{
			Id = _student == null ? GetStudentId(students) : _student.Id,
			FirstName = TbFirstName.Text,
			LastName = TbLastName.Text,
			Math = TbMath.Text,
			Technology = TbTechnology.Text,
			Physics = TbPhysics.Text,
			PolishLang = TbPolishLang.Text,
			ForeignLang = TbForeignLang.Text,
			GroupId = (CbGroups.SelectedItem as Group).Id,
			Comments = RtbComments.Text,
			Activities = CbActivities.Checked,
		});

		static int GetStudentId(List<Student> students)
			=> students.Any() 
				? students.Max(x => x.Id) + 1 
				: 1;
	}

	private void SaveDiaryToFile(List<Student> students)
		=> _dataFile.Serialize(students);
}