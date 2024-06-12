using StudentsDiary.WinFormsApp.FileSerialization;
using StudentsDiary.WinFormsApp.Forms;
using StudentsDiary.WinFormsApp.Helpers;
using StudentsDiary.WinFormsApp.Models;

namespace StudentsDiary.WinFormsApp;

public partial class Main : Form
{
	private readonly SerializeToFile<List<Student>> _dataFile = new SerializeToXml<List<Student>>(Program.DataFilePath, Program.DataFileName);

	private ConfigurationManager _configurationManager;
	private List<Student> _students;
	private List<Group> _groups;

	public Main()
	{
		InitializeComponent();
		SetWindowState();
		FillGroups();
		RefreshDiary();
		SetColumnHeaders();
		HideColumns();
	}

	private void BtnAdd_Click(object sender, EventArgs e)
	{
		AddEditStudent addEditStudent = new();
		if (addEditStudent.ShowDialog() == DialogResult.OK)
		{
			RefreshDiary();
			DgvDiary.Rows[^1].Selected = true;
		}
	}

	private void BtnEdit_Click(object sender, EventArgs e) 
		=> EditStudent();

	private void DgvDiary_CellDoubleClick(object sender, DataGridViewCellEventArgs e) 
		=> EditStudent();

	private void CbGroups_SelectedIndexChanged(object sender, EventArgs e) 
		=> RefreshDiary();

	private void BtnDelete_Click(object sender, EventArgs e)
	{
		if (!IsRowSelected())
		{
			MessageBox.Show("Zaznacz ucznia, którego chcesz usun¹æ.");
			return;
		}

		Student selectedStudent = GetSelectedStudent();
		var confirmDelete = MessageBox.Show(
			$"Czy na pewno chcesz usun¹æ ucznia {selectedStudent.FirstName} {selectedStudent.LastName}",
			"Usuwanie ucznia",
			MessageBoxButtons.OKCancel);
		if (confirmDelete == DialogResult.OK)
		{
			DeleteStudent(selectedStudent);
			SaveDiaryToFile();
			RefreshDiary();
		}
	}

	private void BtnRefresh_Click(object sender, EventArgs e) 
		=> RefreshDiary();

	private void Main_FormClosing(object sender, FormClosingEventArgs e) 
		=> _configurationManager.SaveConfiguration();

	private void SetWindowState()
	{
		_configurationManager = new(this);
		_configurationManager.LoadConfiguration();
	}

	private void FillGroups()
	{
		_groups = GroupHelper.GetGroups("Wszyscy");
		CbGroups.SelectedIndexChanged -= CbGroups_SelectedIndexChanged;
		CbGroups.DataSource = _groups;
		CbGroups.SelectedIndexChanged += CbGroups_SelectedIndexChanged;
		CbGroups.DisplayMember = nameof(Group.Name);
		CbGroups.ValueMember = nameof(Group.Id);
	}

	private void RefreshDiary()
	{
		_students = _dataFile.Deserialize();
		int groupId = (CbGroups.SelectedItem as Group).Id;
		if (groupId != 0)
		{
			_students = _students
				.Where(x => x.GroupId == groupId)
				.ToList();
		}
		_students.Sort();
		DgvDiary.DataSource = _students;
	}

	private void SetColumnHeaders()
	{
		DgvDiary.Columns[nameof(Student.Id)].HeaderText = "Id";
		DgvDiary.Columns[nameof(Student.FirstName)].HeaderText = "Imiê";
		DgvDiary.Columns[nameof(Student.LastName)].HeaderText = "Nazwisko";
		DgvDiary.Columns[nameof(Student.Math)].HeaderText = "Matematyka";
		DgvDiary.Columns[nameof(Student.Technology)].HeaderText = "Technologia";
		DgvDiary.Columns[nameof(Student.Physics)].HeaderText = "Fizyka";
		DgvDiary.Columns[nameof(Student.PolishLang)].HeaderText = "Jêzyk polski";
		DgvDiary.Columns[nameof(Student.ForeignLang)].HeaderText = "Jêzyk obcy";
		DgvDiary.Columns[nameof(Student.Comments)].HeaderText = "Uwagi";
		DgvDiary.Columns[nameof(Student.Activities)].HeaderText = "Zaj. dodatkowe";
		DgvDiary.Columns[nameof(Student.GroupId)].HeaderText = "Grupa";
	}

	private void HideColumns()
	{
		DgvDiary.Columns[nameof(Student.Id)].Visible = false;
		DgvDiary.Columns[nameof(Student.GroupId)].Visible = false;
	}

	private void EditStudent()
	{
		if (!IsRowSelected())
		{
			MessageBox.Show("Wybierz ucznia, którego dane chcesz edytowaæ.");
			return;
		}

		int selectedIndex = GetSelectedIndex();
		AddEditStudent addEditStudent = new(GetSelectedStudent());
		if (addEditStudent.ShowDialog() == DialogResult.OK)
		{
			RefreshDiary();
			DgvDiary.Rows[selectedIndex].Selected = true;
		}
	}

	private void SaveDiaryToFile() 
		=> _dataFile.Serialize(_students);

	private bool IsRowSelected() 
		=> DgvDiary.SelectedRows.Count != 0;

	private Student GetSelectedStudent() 
		=> (Student)DgvDiary.SelectedRows[0].DataBoundItem;

	private int GetSelectedIndex() 
		=> DgvDiary.SelectedRows[0].Index;

	private void DeleteStudent(Student student) 
		=> _students.RemoveAll(x => x.Id == student.Id);
}