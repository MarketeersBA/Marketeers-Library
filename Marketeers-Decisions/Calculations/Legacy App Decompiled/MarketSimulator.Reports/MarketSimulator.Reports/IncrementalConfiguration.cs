using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using MarketSimulator.Model;

namespace MarketSimulator.Reports;

public class IncrementalConfiguration : Form
{
	private IContainer components = null;

	private Label label1;

	private Button button1;

	private Button button2;

	internal DataGridView dataGridView1;

	private Label label46;

	private Label label47;

	internal ComboBox incrementalConfigurationGroupMemberCombobox;

	internal ComboBox incrementalConfigurationGroupTypeCombobox;

	public List<string> groupTypeMemberProducts;

	private ProjectCore project;

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		this.label1 = new System.Windows.Forms.Label();
		this.button1 = new System.Windows.Forms.Button();
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.button2 = new System.Windows.Forms.Button();
		this.incrementalConfigurationGroupMemberCombobox = new System.Windows.Forms.ComboBox();
		this.label46 = new System.Windows.Forms.Label();
		this.incrementalConfigurationGroupTypeCombobox = new System.Windows.Forms.ComboBox();
		this.label47 = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Georgia", 21.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.SteelBlue;
		this.label1.Location = new System.Drawing.Point(0, 7);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(514, 34);
		this.label1.TabIndex = 3;
		this.label1.Text = "Marketeers Incremental Configuration";
		this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.button1.Location = new System.Drawing.Point(422, 501);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(75, 23);
		this.button1.TabIndex = 5;
		this.button1.Text = "Finish";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.dataGridView1.AllowUserToAddRows = false;
		this.dataGridView1.AllowUserToDeleteRows = false;
		this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
		this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
		this.dataGridView1.Location = new System.Drawing.Point(18, 88);
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.RowHeadersWidth = 25;
		this.dataGridView1.Size = new System.Drawing.Size(478, 398);
		this.dataGridView1.TabIndex = 15;
		this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dataGridView1_CellMouseClick);
		this.dataGridView1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dataGridView1_ColumnHeaderMouseClick);
		this.dataGridView1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(dataGridView1_PreviewKeyDown);
		this.dataGridView1.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dataGridView1_RowHeaderMouseClick);
		this.dataGridView1.KeyUp += new System.Windows.Forms.KeyEventHandler(dataGridView1_KeyUp);
		this.button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.button2.Location = new System.Drawing.Point(341, 501);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(75, 23);
		this.button2.TabIndex = 16;
		this.button2.Text = "Cancel";
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.incrementalConfigurationGroupMemberCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.incrementalConfigurationGroupMemberCombobox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.incrementalConfigurationGroupMemberCombobox.FormattingEnabled = true;
		this.incrementalConfigurationGroupMemberCombobox.Location = new System.Drawing.Point(351, 61);
		this.incrementalConfigurationGroupMemberCombobox.Name = "incrementalConfigurationGroupMemberCombobox";
		this.incrementalConfigurationGroupMemberCombobox.Size = new System.Drawing.Size(145, 21);
		this.incrementalConfigurationGroupMemberCombobox.TabIndex = 56;
		this.incrementalConfigurationGroupMemberCombobox.SelectedIndexChanged += new System.EventHandler(incrementalConfigurationGroupMemberCombobox_SelectedIndexChanged);
		this.label46.AutoSize = true;
		this.label46.BackColor = System.Drawing.Color.Transparent;
		this.label46.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label46.Location = new System.Drawing.Point(271, 64);
		this.label46.Name = "label46";
		this.label46.Size = new System.Drawing.Size(80, 13);
		this.label46.TabIndex = 55;
		this.label46.Text = "Group Member:";
		this.incrementalConfigurationGroupTypeCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.incrementalConfigurationGroupTypeCombobox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.incrementalConfigurationGroupTypeCombobox.FormattingEnabled = true;
		this.incrementalConfigurationGroupTypeCombobox.Location = new System.Drawing.Point(85, 61);
		this.incrementalConfigurationGroupTypeCombobox.Name = "incrementalConfigurationGroupTypeCombobox";
		this.incrementalConfigurationGroupTypeCombobox.Size = new System.Drawing.Size(143, 21);
		this.incrementalConfigurationGroupTypeCombobox.TabIndex = 54;
		this.incrementalConfigurationGroupTypeCombobox.SelectedIndexChanged += new System.EventHandler(incrementalConfigurationGroupTypeCombobox_SelectedIndexChanged);
		this.label47.AutoSize = true;
		this.label47.BackColor = System.Drawing.Color.Transparent;
		this.label47.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label47.Location = new System.Drawing.Point(18, 64);
		this.label47.Name = "label47";
		this.label47.Size = new System.Drawing.Size(66, 13);
		this.label47.TabIndex = 53;
		this.label47.Text = "Group Type:";
		base.AcceptButton = this.button1;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.ControlLightLight;
		base.ClientSize = new System.Drawing.Size(514, 531);
		base.Controls.Add(this.incrementalConfigurationGroupMemberCombobox);
		base.Controls.Add(this.label46);
		base.Controls.Add(this.incrementalConfigurationGroupTypeCombobox);
		base.Controls.Add(this.label47);
		base.Controls.Add(this.button2);
		base.Controls.Add(this.dataGridView1);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.label1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "IncrementalConfiguration";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Incremental";
		base.Load += new System.EventHandler(IncrementalConfiguration_Load);
		base.Paint += new System.Windows.Forms.PaintEventHandler(IncrementalConfiguration_Paint);
		base.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(IncrementalConfiguration_PreviewKeyDown);
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public IncrementalConfiguration()
	{
		InitializeComponent();
		groupTypeMemberProducts = new List<string>();
	}

	public IncrementalConfiguration(ProjectCore incrementalProject)
	{
		InitializeComponent();
		project = incrementalProject;
		groupTypeMemberProducts = new List<string>();
		incrementalConfigurationGroupTypeCombobox.Items.AddRange(project.GroupTypeList.ToArray());
		incrementalConfigurationGroupTypeCombobox.Items.Add("Company Products");
		incrementalConfigurationGroupTypeCombobox.SelectedIndex = 0;
	}

	private void IncrementalConfiguration_Load(object sender, EventArgs e)
	{
		dataGridView1.Columns[0].Width = 222;
		dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
		dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
		dataGridView1.ClearSelection();
	}

	private void IncrementalConfiguration_Paint(object sender, PaintEventArgs e)
	{
		Rectangle rect = new Rectangle(0, 0, base.ClientSize.Width, base.ClientSize.Height);
		LinearGradientBrush brush = new LinearGradientBrush(rect, Color.White, Color.FromArgb(45, Color.SteelBlue), LinearGradientMode.ForwardDiagonal);
		e.Graphics.FillRectangle(brush, rect);
	}

	private void pasteDataGridViewFromClipboard()
	{
		int rowIndex = dataGridView1.FirstDisplayedCell.RowIndex;
		int num = dataGridView1.SelectedCells[dataGridView1.SelectedCells.Count - 1].RowIndex;
		int columnIndex = dataGridView1.SelectedCells[dataGridView1.SelectedCells.Count - 1].ColumnIndex;
		if (num > dataGridView1.SelectedCells[0].RowIndex)
		{
			num = dataGridView1.SelectedCells[0].RowIndex;
			columnIndex = dataGridView1.SelectedCells[0].ColumnIndex;
		}
		object data = Clipboard.GetDataObject().GetData(DataFormats.CommaSeparatedValue);
		if (data == null)
		{
			return;
		}
		string text = data.ToString();
		if (text == "System.IO.MemoryStream")
		{
			StreamReader streamReader = new StreamReader(Clipboard.GetDataObject().GetData(DataFormats.CommaSeparatedValue) as MemoryStream);
			text = streamReader.ReadToEnd();
		}
		string[] array = text.Split(new string[2] { "\r\n", "\0" }, StringSplitOptions.RemoveEmptyEntries);
		DataTable dataTable = dataGridView1.DataSource as DataTable;
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(new string[1] { "," }, StringSplitOptions.RemoveEmptyEntries);
			int num2 = columnIndex;
			for (int j = 0; j < array2.Length; j++)
			{
				if (num2 < dataTable.Columns.Count && num < dataTable.Rows.Count)
				{
					dataTable.Rows[num][num2] = array2[j];
					num2++;
				}
			}
			num++;
		}
	}

	private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.Delete)
		{
			return;
		}
		DataTable dataTable = dataGridView1.DataSource as DataTable;
		foreach (DataGridViewCell selectedCell in dataGridView1.SelectedCells)
		{
			dataTable.Rows[selectedCell.RowIndex][selectedCell.ColumnIndex] = "";
		}
	}

	private void dataGridView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
	{
		if (e.Control && e.KeyCode == Keys.C)
		{
			Clipboard.SetDataObject(dataGridView1.GetClipboardContent());
		}
		else if (e.Control && e.KeyCode == Keys.V)
		{
			pasteDataGridViewFromClipboard();
		}
	}

	private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
		if (e.RowIndex != -1 && e.ColumnIndex != -1)
		{
			dataGridView1[e.ColumnIndex, e.RowIndex].Selected = true;
		}
	}

	private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		if (e.ColumnIndex != -1)
		{
			dataGridView1.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;
			dataGridView1.Columns[e.ColumnIndex].Selected = true;
		}
	}

	private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		if (e.RowIndex != -1)
		{
			dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dataGridView1.Rows[e.RowIndex].Selected = true;
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
		if (checkEnteredProductsName())
		{
			if (string.Equals(Text, "Incremental Loss"))
			{
				if (checkIncrementalLossProductsExistInLineUp())
				{
					base.DialogResult = DialogResult.OK;
				}
				else
				{
					MessageBox.Show(this, "The selected cells has products doesn't exist in the current line up products", "Unvalid Products", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			else
			{
				base.DialogResult = DialogResult.OK;
			}
		}
		else
		{
			MessageBox.Show(this, "The selected cells has products doesn't match with the stored products for the selected group", "Unvalid Products", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void button2_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.Cancel;
	}

	private bool checkEnteredProductsName()
	{
		bool flag = false;
		dataGridView1.ClearSelection();
		foreach (DataGridViewRow item in (IEnumerable)dataGridView1.Rows)
		{
			foreach (DataGridViewColumn column in dataGridView1.Columns)
			{
				if (!string.IsNullOrEmpty(dataGridView1[column.Index, item.Index].Value.ToString()) && !groupTypeMemberProducts.Contains(dataGridView1[column.Index, item.Index].Value.ToString()))
				{
					dataGridView1[column.Index, item.Index].Selected = true;
					flag = true;
				}
			}
		}
		if (flag)
		{
			return false;
		}
		return true;
	}

	private bool checkIncrementalLossProductsExistInLineUp()
	{
		foreach (DataGridViewRow item in (IEnumerable)dataGridView1.Rows)
		{
			if (string.IsNullOrEmpty(item.Cells[1].Value.ToString()))
			{
				continue;
			}
			dataGridView1.ClearSelection();
			item.Cells[1].Selected = true;
			bool flag = false;
			foreach (DataGridViewRow item2 in (IEnumerable)dataGridView1.Rows)
			{
				if (!string.IsNullOrEmpty(item2.Cells[0].Value.ToString()) && string.Equals(item.Cells[1].Value.ToString(), item2.Cells[0].Value.ToString()))
				{
					flag = true;
				}
			}
			if (!flag)
			{
				return false;
			}
		}
		return true;
	}

	private void IncrementalConfiguration_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
	{
		dataGridView1_PreviewKeyDown(sender, e);
	}

	private void incrementalConfigurationGroupTypeCombobox_SelectedIndexChanged(object sender, EventArgs e)
	{
		incrementalConfigurationGroupMemberCombobox.Items.Clear();
		if (!string.Equals(incrementalConfigurationGroupTypeCombobox.SelectedItem.ToString(), "Company Products"))
		{
			incrementalConfigurationGroupMemberCombobox.Items.AddRange(project.GroupMember[incrementalConfigurationGroupTypeCombobox.SelectedItem.ToString()].ToArray());
			incrementalConfigurationGroupMemberCombobox.SelectedIndex = 0;
		}
		else
		{
			groupTypeMemberProducts.AddRange(project.CompanyProducts.ToArray());
		}
	}

	private void incrementalConfigurationGroupMemberCombobox_SelectedIndexChanged(object sender, EventArgs e)
	{
		groupTypeMemberProducts.Clear();
		if (string.Equals(incrementalConfigurationGroupTypeCombobox.SelectedItem.ToString(), "Company Products"))
		{
			groupTypeMemberProducts.AddRange(project.CompanyProducts.ToArray());
		}
		else
		{
			groupTypeMemberProducts.AddRange(project.GroupProducts[incrementalConfigurationGroupTypeCombobox.SelectedItem.ToString()][incrementalConfigurationGroupMemberCombobox.SelectedItem.ToString()].ToArray());
		}
	}
}
