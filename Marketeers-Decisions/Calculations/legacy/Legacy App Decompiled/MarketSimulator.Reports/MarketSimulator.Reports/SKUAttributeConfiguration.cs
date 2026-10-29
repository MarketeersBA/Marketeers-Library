using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Xml;
using MarketSimulator.Model;
using MarketSimulator.Reports.Properties;
using UndoRedoFramework;

namespace MarketSimulator.Reports;

public class SKUAttributeConfiguration : Form
{
	private ProjectCore project;

	private Dictionary<string, string> studyscenariosLibrary = new Dictionary<string, string>();

	private Dictionary<string, string> validationScenarios = new Dictionary<string, string>();

	private string scenariosPath = "";

	private bool cancelDisplayResultEdit = false;

	private string projectName;

	private Dictionary<string, int> columnsSizeDictionary = new Dictionary<string, int>();

	private char[] forbiddenCharacters = new char[9] { '\\', '/', ':', '?', '*', '|', '"', '<', '>' };

	private bool validation = false;

	private IContainer components = null;

	private Label label1;

	private PictureBox pictureBox1;

	private ToolStrip toolStrip1;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripLabel toolStripLabel1;

	private ToolStripComboBox scenarioDropdownbox;

	private ToolStripButton saveToolStripButton;

	private ToolStripSeparator toolStripSeparator2;

	private Button button2;

	private Label label2;

	private ToolStripDropDownButton toolStripDropDownButton1;

	private ToolStripSeparator toolStripSeparator3;

	private ToolStripDropDownButton segmentationToolstripSplitButton;

	internal DataGridView skuAttributeConfigurationDatagridview;

	private CheckBox checkBox1;

	private CheckBox checkBox2;

	private ToolStripSeparator toolStripSeparator4;

	private ToolStripButton restoreDefaultButton;

	private Button uncheckAllProductsButton;

	private ToolTip toolTip1;

	private Button checkAllProductsButton;

	private MenuStrip menuStrip1;

	private ToolStripMenuItem saveScenarioToolStripMenuItem;

	public SKUAttributeConfiguration()
	{
		InitializeComponent();
	}

	private void SKUAttributeConfiguration_Load(object sender, EventArgs e)
	{
		if (ProjectCore.EnlargeAll)
		{
			checkBox2.Checked = true;
		}
		if (project.getGroupResearchStart() != "")
		{
			checkBox1.Checked = true;
		}
		reloadAttributeConfiguration();
		autoSizeDatagridviewColumns();
		storeDisplayResultDatagridviewColumnSizes();
	}

	private void SKUAttributeConfiguration_Paint(object sender, PaintEventArgs e)
	{
		Rectangle rect = new Rectangle(0, 0, base.ClientSize.Width, base.ClientSize.Height);
		LinearGradientBrush brush = new LinearGradientBrush(rect, Color.White, Color.FromArgb(45, Color.SteelBlue), LinearGradientMode.ForwardDiagonal);
		e.Graphics.FillRectangle(brush, rect);
		Pen pen = new Pen(Color.Gray, 1f);
		e.Graphics.DrawLine(pen, 415, 36, base.ClientSize.Width - 5, 36);
		e.Graphics.DrawLine(pen, 5, base.ClientSize.Height - 40, base.ClientSize.Width - 5, base.ClientSize.Height - 40);
	}

	private void SKUAttributeConfiguration_FormClosing(object sender, FormClosingEventArgs e)
	{
		base.DialogResult = DialogResult.OK;
	}

	public void setScenariosProjectDictionary(Dictionary<string, string> ScenariosProject, Dictionary<string, string> validationScenarios, string scenariosPath)
	{
		studyscenariosLibrary = ScenariosProject;
		this.validationScenarios = validationScenarios;
		this.scenariosPath = scenariosPath;
		loadScenarios();
	}

	private void loadScenarios()
	{
		scenarioDropdownbox.Items.Clear();
		foreach (KeyValuePair<string, string> item in studyscenariosLibrary)
		{
			scenarioDropdownbox.Items.Add(item.Key);
		}
	}

	public void setSkuAttributeConfigurationForm(ProjectCore projectObject, string projectObjectName)
	{
		project = projectObject;
		projectName = projectObjectName;
		loadSegments(project.loadSegmentsDictionary());
	}

	private void loadingOtherDatagridviewColumns()
	{
		foreach (string discreteAttributeName in project.DiscreteAttributeNameList)
		{
			if (project.isAttributeIncluded(discreteAttributeName) && !skuAttributeConfigurationDatagridview.Columns.Contains(discreteAttributeName))
			{
				DataGridViewComboBoxColumn dataGridViewComboBoxColumn = new DataGridViewComboBoxColumn();
				string[] discreteAttributeLevelsNameArray = project.getDiscreteAttributeLevelsNameArray(discreteAttributeName);
				dataGridViewComboBoxColumn.Name = discreteAttributeName;
				string[] array = discreteAttributeLevelsNameArray;
				foreach (string item in array)
				{
					dataGridViewComboBoxColumn.Items.Add(item);
				}
				skuAttributeConfigurationDatagridview.Columns.Insert(skuAttributeConfigurationDatagridview.Columns["Base " + discreteAttributeName].Index + 1, dataGridViewComboBoxColumn);
				skuAttributeConfigurationDatagridview.Columns["Base " + discreteAttributeName].Visible = false;
			}
		}
		if (!skuAttributeConfigurationDatagridview.Columns.Contains("E/C"))
		{
			DataGridViewImageColumn dataGridViewImageColumn = new DataGridViewImageColumn();
			dataGridViewImageColumn.HeaderText = "E/C";
			dataGridViewImageColumn.Name = "E/C";
			dataGridViewImageColumn.DefaultCellStyle.NullValue = null;
			skuAttributeConfigurationDatagridview.Columns.Insert(1, dataGridViewImageColumn);
		}
		foreach (DataGridViewColumn column in skuAttributeConfigurationDatagridview.Columns)
		{
			column.SortMode = DataGridViewColumnSortMode.NotSortable;
		}
	}

	private void bindDisplayResultDatagridviewToolTip()
	{
		foreach (string conditionalAttributeName in project.ConditionalAttributeNameList)
		{
			if (!project.isAttributeIncluded(conditionalAttributeName))
			{
				continue;
			}
			foreach (DataGridViewRow item in (IEnumerable)skuAttributeConfigurationDatagridview.Rows)
			{
				if (int.Parse(item.Cells["Product Index"].Value.ToString()) == -1)
				{
					double[] attributeLevelsBoundries = project.getAttributeLevelsBoundries(conditionalAttributeName.Replace("New ", ""));
					item.Cells["New " + conditionalAttributeName].ToolTipText = "Specify a percentage value for all the following group items\nMin is " + attributeLevelsBoundries[0].ToString("P") + " and the Max is " + attributeLevelsBoundries[1].ToString("P");
				}
				else
				{
					double[] productMinMaxConditionalValidator = project.getProductMinMaxConditionalValidator(int.Parse(item.Cells["Product Index"].Value.ToString()) - 1, conditionalAttributeName.Replace("New ", ""));
					item.Cells["New " + conditionalAttributeName].ToolTipText = "Minimum value is " + productMinMaxConditionalValidator[0] + " and Maximum value is " + productMinMaxConditionalValidator[1];
				}
			}
		}
		foreach (DataGridViewRow item2 in (IEnumerable)skuAttributeConfigurationDatagridview.Rows)
		{
			if (int.Parse(item2.Cells["Product Index"].Value.ToString()) != -1)
			{
				item2.Cells["N.Share"].ToolTipText = project.getProductShare("NewShare", item2.Cells["Product Name"].Value.ToString()) + "%";
				item2.Cells["Preference Share"].ToolTipText = project.getProductShare("PreferenceShare", item2.Cells["Product Name"].Value.ToString()) + "%";
			}
			else
			{
				item2.Cells["N.Share"].ToolTipText = project.getGroupShare("NewShare", project.getGroupResearchStart(), project.getGroupMemberIndex(project.getGroupResearchStart(), item2.Cells["Product Name"].Value.ToString())) + "%";
				item2.Cells["Preference Share"].ToolTipText = project.getGroupNewPreferenceShare(project.getGroupResearchStart(), project.getGroupMemberIndex(project.getGroupResearchStart(), item2.Cells["Product Name"].Value.ToString())) + "%";
			}
		}
	}

	private void customizeDisplayResultDatagridview()
	{
		foreach (string discreteAttributeName in project.DiscreteAttributeNameList)
		{
			if (!project.isAttributeIncluded(discreteAttributeName))
			{
				continue;
			}
			foreach (DataGridViewRow item in (IEnumerable)skuAttributeConfigurationDatagridview.Rows)
			{
				if (int.Parse(item.Cells["Product Index"].Value.ToString()) == -1)
				{
					item.Cells[discreteAttributeName] = new DataGridViewTextBoxCell();
					item.Cells[discreteAttributeName].ReadOnly = true;
				}
				else
				{
					item.Cells[discreteAttributeName].Value = project.getDiscreteAttributeLevel(discreteAttributeName, (int)project.getProductAttribute(item.Cells["Product Name"].Value.ToString(), discreteAttributeName));
				}
			}
		}
		foreach (DataGridViewRow item2 in (IEnumerable)skuAttributeConfigurationDatagridview.Rows)
		{
			if (int.Parse(item2.Cells["Product Index"].Value.ToString()) == -1)
			{
				item2.Cells["E/C"].Value = Resources.Collapse_large;
				item2.Cells["E/C"].Tag = "Collapse_large";
			}
		}
	}

	private void formatDisplayResultDatagridview()
	{
		skuAttributeConfigurationDatagridview.Columns["E/C"].Frozen = true;
		skuAttributeConfigurationDatagridview.Columns["Selected"].Frozen = true;
		skuAttributeConfigurationDatagridview.Columns["Product Name"].Frozen = true;
		foreach (DataGridViewRow item in (IEnumerable)skuAttributeConfigurationDatagridview.Rows)
		{
			if (int.Parse(item.Cells["Product Index"].Value.ToString()) == -1)
			{
				item.DefaultCellStyle.ForeColor = Color.White;
				item.DefaultCellStyle.BackColor = Color.SlateGray;
				item.Cells["Product Name"].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
				item.Cells["Product Name"].Style.Padding = new Padding(10, 1, 1, 1);
				item.Cells["New WD"].ReadOnly = true;
			}
			else if (!bool.Parse(item.Cells["Selected"].Value.ToString()))
			{
				item.ReadOnly = true;
				item.DefaultCellStyle.ForeColor = Color.Gray;
				item.Cells["Product Name"].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
				item.Cells["Product Name"].Style.Padding = new Padding(10, 1, 1, 1);
			}
			else if ((object)item.Cells["Product Name"].GetType() == typeof(DataGridViewTextBoxCell))
			{
				item.Cells["Product Name"].ReadOnly = true;
				item.Cells["Product Name"].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
				item.Cells["Product Name"].Style.Padding = new Padding(10, 1, 1, 1);
			}
		}
		foreach (DataGridViewColumn column in skuAttributeConfigurationDatagridview.Columns)
		{
			if (column.Index > 10 && column.Visible && !column.ReadOnly)
			{
				column.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192);
			}
		}
		foreach (DataGridViewRow item2 in (IEnumerable)skuAttributeConfigurationDatagridview.Rows)
		{
			foreach (DataGridViewColumn column2 in skuAttributeConfigurationDatagridview.Columns)
			{
				if (int.Parse(item2.Cells["Product Index"].Value.ToString()) == -1 || column2.Index <= 10 || !column2.Visible || column2.ReadOnly || !(column2.Name != "Audit Share"))
				{
					continue;
				}
				if (item2.Cells[column2.Name].Value.ToString() != item2.Cells[column2.Index - 1].Value.ToString())
				{
					item2.Cells[column2.Name].Style.BackColor = Color.DarkGoldenrod;
					item2.Cells[column2.Name].Style.ForeColor = Color.White;
					continue;
				}
				item2.Cells[column2.Name].Style.BackColor = Color.FromArgb(255, 255, 192);
				if (bool.Parse(item2.Cells["Selected"].Value.ToString()))
				{
					item2.Cells[column2.Name].Style.ForeColor = Color.Black;
				}
			}
		}
		if (projectName.Equals("ValidationChartProject"))
		{
			skuAttributeConfigurationDatagridview.Columns["Audit Share"].Visible = true;
		}
		else
		{
			skuAttributeConfigurationDatagridview.Columns["Audit Share"].Visible = false;
		}
		skuAttributeConfigurationDatagridview.Columns["Product Index"].Visible = false;
		skuAttributeConfigurationDatagridview.Columns["B.Share"].Visible = false;
		skuAttributeConfigurationDatagridview.Columns["N.Share"].Visible = false;
		skuAttributeConfigurationDatagridview.Columns["S.Index"].Visible = false;
		skuAttributeConfigurationDatagridview.Columns["Preference Share"].Visible = false;
		skuAttributeConfigurationDatagridview.Columns["Base Product"].Visible = false;
		skuAttributeConfigurationDatagridview.Columns["Type"].Visible = false;
		skuAttributeConfigurationDatagridview.Columns["NOS"].Visible = false;
		skuAttributeConfigurationDatagridview.Columns["TDC"].Visible = false;
		skuAttributeConfigurationDatagridview.Columns["Revenue"].Visible = false;
		skuAttributeConfigurationDatagridview.Columns["R.Index"].Visible = false;
	}

	private void autoSizeDatagridviewColumns()
	{
		skuAttributeConfigurationDatagridview.AutoResizeColumns();
		int num = skuAttributeConfigurationDatagridview.ClientSize.Width - skuAttributeConfigurationDatagridview.Columns.GetColumnsWidth(DataGridViewElementStates.Visible) - 12;
		if (num > 1)
		{
			int num2 = (int)((double)num * 0.75 / (double)(skuAttributeConfigurationDatagridview.Columns.GetColumnCount(DataGridViewElementStates.Visible) - 2));
			skuAttributeConfigurationDatagridview.Columns["Product Name"].Width = skuAttributeConfigurationDatagridview.Columns["Product Name"].Width + (int)((double)num * 0.25);
			for (int i = 4; i < skuAttributeConfigurationDatagridview.ColumnCount; i++)
			{
				if (skuAttributeConfigurationDatagridview.Columns[i].Visible)
				{
					skuAttributeConfigurationDatagridview.Columns[i].Width = skuAttributeConfigurationDatagridview.Columns[i].Width + num2;
				}
			}
		}
		num = skuAttributeConfigurationDatagridview.ClientSize.Width - skuAttributeConfigurationDatagridview.Columns.GetColumnsWidth(DataGridViewElementStates.Visible) - 38;
		skuAttributeConfigurationDatagridview.Columns[skuAttributeConfigurationDatagridview.Columns.GetLastColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None).Name].Width += num;
	}

	private void loadBaseProduct()
	{
		foreach (DataGridViewRow item in (IEnumerable)skuAttributeConfigurationDatagridview.Rows)
		{
			int num = 0;
			if (item.Cells["Base Product"].Value.ToString() != "")
			{
				num = int.Parse(item.Cells["Base Product"].Value.ToString());
			}
			if (num <= 0)
			{
				continue;
			}
			foreach (DataGridViewRow item2 in (IEnumerable)skuAttributeConfigurationDatagridview.Rows)
			{
				if (int.Parse(item2.Cells["Product Index"].Value.ToString()) == num)
				{
					DataGridViewComboBoxCell dataGridViewComboBoxCell;
					if ((object)item2.Cells["Product Name"].GetType() == typeof(DataGridViewTextBoxCell))
					{
						dataGridViewComboBoxCell = new DataGridViewComboBoxCell();
						dataGridViewComboBoxCell.Items.Add(item2.Cells["Product Name"].Value);
					}
					else
					{
						dataGridViewComboBoxCell = item2.Cells["Product Name"] as DataGridViewComboBoxCell;
					}
					dataGridViewComboBoxCell.Items.Add(item.Cells["Product Name"].Value);
					dataGridViewComboBoxCell.ReadOnly = false;
					dataGridViewComboBoxCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
					dataGridViewComboBoxCell.Style.Padding = new Padding(10, 1, 2, 1);
					item2.Cells["Product Name"] = dataGridViewComboBoxCell.Clone() as DataGridViewComboBoxCell;
				}
			}
			item.Visible = false;
			if (!bool.Parse(item.Cells["Selected"].Value.ToString()))
			{
				continue;
			}
			foreach (DataGridViewRow item3 in (IEnumerable)skuAttributeConfigurationDatagridview.Rows)
			{
				if (int.Parse(item3.Cells["Product Index"].Value.ToString()) != num)
				{
					continue;
				}
				for (int i = 3; i < skuAttributeConfigurationDatagridview.ColumnCount; i++)
				{
					if (((object)item3.Cells[skuAttributeConfigurationDatagridview.Columns[i].Name].GetType() == typeof(DataGridViewTextBoxCell) || (object)item3.Cells[skuAttributeConfigurationDatagridview.Columns[i].Name].GetType() == typeof(DataGridViewComboBoxCell)) && project.DtDisplayResult.Columns.Contains(skuAttributeConfigurationDatagridview.Columns[i].Name) && skuAttributeConfigurationDatagridview.Columns[i].Name != "Base Product")
					{
						project.DtDisplayResult.Columns[skuAttributeConfigurationDatagridview.Columns[i].Name].ReadOnly = false;
						item3.Cells[skuAttributeConfigurationDatagridview.Columns[i].Name].Value = item.Cells[skuAttributeConfigurationDatagridview.Columns[i].Name].Value;
					}
				}
				project.DtDisplayResult.Columns["Selected"].ReadOnly = false;
				item3.Cells["Selected"].Value = true;
				project.DtDisplayResult.Columns["Selected"].ReadOnly = true;
				break;
			}
		}
	}

	private void reloadAttributeConfiguration()
	{
		skuAttributeConfigurationDatagridview.SelectionMode = DataGridViewSelectionMode.CellSelect;
		skuAttributeConfigurationDatagridview.ClearSelection();
		int firstDisplayedScrollingRowIndex = skuAttributeConfigurationDatagridview.FirstDisplayedScrollingRowIndex;
		project.fillDisplayResultTable();
		skuAttributeConfigurationDatagridview.DataSource = project.DtDisplayResult;
		loadingOtherDatagridviewColumns();
		bindDisplayResultDatagridviewToolTip();
		customizeDisplayResultDatagridview();
		if (!ProjectCore.EnlargeAll)
		{
			loadBaseProduct();
		}
		loadNewProducts();
		formatDisplayResultDatagridview();
		setDatagridviewColumnsWidth();
		setSegmentationToolStripButton();
		if (firstDisplayedScrollingRowIndex != -1 && skuAttributeConfigurationDatagridview.Rows[firstDisplayedScrollingRowIndex].Visible)
		{
			skuAttributeConfigurationDatagridview.FirstDisplayedScrollingRowIndex = firstDisplayedScrollingRowIndex;
		}
	}

	private void setDatagridviewColumnsWidth()
	{
		foreach (DataGridViewColumn column in skuAttributeConfigurationDatagridview.Columns)
		{
			if (columnsSizeDictionary.ContainsKey(column.Name))
			{
				column.Width = columnsSizeDictionary[column.Name];
			}
		}
	}

	private void storeDisplayResultDatagridviewColumnSizes()
	{
		columnsSizeDictionary.Clear();
		foreach (DataGridViewColumn column in skuAttributeConfigurationDatagridview.Columns)
		{
			if (column.Visible)
			{
				columnsSizeDictionary.Add(column.Name, column.Width);
			}
		}
	}

	private void loadNewProducts()
	{
		string text = "";
		ToolStripMenuItem toolStripMenuItem = null;
		toolStripDropDownButton1.DropDownItems.Clear();
		foreach (DataGridViewRow item in (IEnumerable)skuAttributeConfigurationDatagridview.Rows)
		{
			if (int.Parse(item.Cells["Product Index"].Value.ToString()) == -1)
			{
				toolStripMenuItem = new ToolStripMenuItem();
				toolStripMenuItem.Text = item.Cells["Product Name"].Value.ToString();
				toolStripDropDownButton1.DropDownItems.Add(toolStripMenuItem);
				text = item.Cells["Product Name"].Value.ToString();
			}
			if (!(item.Cells["Type"].Value.ToString() == "NewProduct"))
			{
				continue;
			}
			if (text != "")
			{
				ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem(item.Cells["Product Name"].Value.ToString());
				if (bool.Parse(item.Cells["Selected"].Value.ToString()))
				{
					toolStripMenuItem2.Checked = true;
				}
				else if (!ProjectCore.EnlargeAll)
				{
					item.Visible = false;
				}
				toolStripMenuItem2.Click += newProductItem_Click;
				toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
			}
			else if (text == "")
			{
				toolStripMenuItem = new ToolStripMenuItem(item.Cells["Product Name"].Value.ToString());
				toolStripMenuItem.Click += newProductItem_Click;
				toolStripDropDownButton1.DropDownItems.Add(toolStripMenuItem);
				if (bool.Parse(item.Cells["Selected"].Value.ToString()))
				{
					toolStripMenuItem.Checked = true;
				}
				else if (!ProjectCore.EnlargeAll)
				{
					item.Visible = false;
				}
			}
		}
	}

	private void newProductItem_Click(object sender, EventArgs e)
	{
		ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)sender;
		toolStripMenuItem.Checked = !toolStripMenuItem.Checked;
		if (toolStripMenuItem.OwnerItem.Text != "New Products")
		{
			ToolStripMenuItem toolStripMenuItem2 = toolStripMenuItem.OwnerItem as ToolStripMenuItem;
			foreach (ToolStripItem dropDownItem in toolStripMenuItem2.DropDownItems)
			{
				ToolStripMenuItem toolStripMenuItem3 = (ToolStripMenuItem)dropDownItem;
				if (toolStripMenuItem3.Checked)
				{
					toolStripMenuItem2.Checked = true;
					break;
				}
				toolStripMenuItem2.Checked = false;
			}
		}
		foreach (DataGridViewRow item in (IEnumerable)skuAttributeConfigurationDatagridview.Rows)
		{
			if (item.Cells["Product Name"].Value.ToString() == toolStripMenuItem.Text)
			{
				using (UndoRedoManager.Start("IncludeExcludeProducts"))
				{
					project.includeExcludeProductScenarios(item.Cells["Product Name"].Value.ToString(), !bool.Parse(item.Cells["Selected"].Value.ToString()));
					UndoRedoManager.Commit();
				}
				reloadAttributeConfiguration();
				item.Visible = true;
				break;
			}
		}
	}

	private void toolStripDropDownButton1_DropDownOpening(object sender, EventArgs e)
	{
		foreach (ToolStripItem dropDownItem in toolStripDropDownButton1.DropDownItems)
		{
			ToolStripMenuItem toolStripMenuItem = dropDownItem as ToolStripMenuItem;
			if (!toolStripMenuItem.HasDropDownItems && checkBox1.Checked)
			{
				dropDownItem.Enabled = false;
			}
			else
			{
				dropDownItem.Enabled = true;
			}
		}
	}

	public void loadSegments(Dictionary<string, ArrayList> segments)
	{
		segmentationToolstripSplitButton.DropDownItems.Clear();
		string[] segmentation = project.getSegmentation();
		ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem("No Segmentation");
		toolStripMenuItem.Name = "No Segmentation";
		if (string.IsNullOrEmpty(segmentation[0]))
		{
			toolStripMenuItem.Checked = true;
		}
		toolStripMenuItem.Click += segmentItem_Click;
		segmentationToolstripSplitButton.DropDownItems.Add(toolStripMenuItem);
		foreach (KeyValuePair<string, ArrayList> segment in segments)
		{
			ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem(segment.Key);
			toolStripMenuItem2.Name = segment.Key;
			if (string.Equals(segmentation[0], segment.Key))
			{
				toolStripMenuItem2.Checked = true;
			}
			foreach (object item in segment.Value)
			{
				ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem(item.ToString());
				toolStripMenuItem3.Name = item.ToString();
				toolStripMenuItem3.Click += segmentItem_Click;
				if (object.Equals(segmentation[1], item))
				{
					toolStripMenuItem3.Checked = true;
				}
				toolStripMenuItem2.DropDownItems.Add(toolStripMenuItem3);
			}
			segmentationToolstripSplitButton.DropDownItems.Add(toolStripMenuItem2);
		}
	}

	private void segmentItem_Click(object sender, EventArgs e)
	{
		skuAttributeConfigurationDatagridview.SelectionMode = DataGridViewSelectionMode.CellSelect;
		skuAttributeConfigurationDatagridview.ClearSelection();
		ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)sender;
		if (toolStripMenuItem.Text == "No Segmentation")
		{
			using (UndoRedoManager.Start("ChangeSegment"))
			{
				project.resetSegmentation();
				UndoRedoManager.Commit();
				return;
			}
		}
		ToolStripMenuItem toolStripMenuItem2 = toolStripMenuItem.OwnerItem as ToolStripMenuItem;
		using (UndoRedoManager.Start("ChangeSegment"))
		{
			project.setSegmentation(toolStripMenuItem2.Text, toolStripMenuItem.Text);
			UndoRedoManager.Commit();
		}
	}

	private void uncheckSegmentToolstrip()
	{
		foreach (ToolStripItem dropDownItem in segmentationToolstripSplitButton.DropDownItems)
		{
			ToolStripMenuItem toolStripMenuItem = dropDownItem as ToolStripMenuItem;
			if (!toolStripMenuItem.Checked)
			{
				continue;
			}
			toolStripMenuItem.Checked = false;
			foreach (ToolStripItem dropDownItem2 in toolStripMenuItem.DropDownItems)
			{
				ToolStripMenuItem toolStripMenuItem2 = dropDownItem2 as ToolStripMenuItem;
				if (toolStripMenuItem2.Checked)
				{
					toolStripMenuItem2.Checked = false;
				}
			}
		}
	}

	private void skuAttributeConfigurationDatagridview_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		int firstDisplayedScrollingRowIndex = skuAttributeConfigurationDatagridview.FirstDisplayedScrollingRowIndex;
		if (!cancelDisplayResultEdit)
		{
			if (e.ColumnIndex > 8)
			{
				string attributeName = (((object)skuAttributeConfigurationDatagridview.Columns[e.ColumnIndex].GetType() != typeof(DataGridViewComboBoxColumn)) ? skuAttributeConfigurationDatagridview.Columns[e.ColumnIndex].HeaderText.Replace("New ", "") : skuAttributeConfigurationDatagridview.Columns[e.ColumnIndex].HeaderText);
				if (skuAttributeConfigurationDatagridview.Columns[e.ColumnIndex].HeaderText.Equals("Audit Share"))
				{
					using (UndoRedoManager.Start("ChangeProductAttribute"))
					{
						project.changeProductAttributeCombination(skuAttributeConfigurationDatagridview["Product Name", e.RowIndex].Value.ToString(), "Audit Share", skuAttributeConfigurationDatagridview[e.ColumnIndex, e.RowIndex].Value.ToString());
						UndoRedoManager.Commit();
					}
				}
				else if (int.Parse(skuAttributeConfigurationDatagridview["Product Index", e.RowIndex].Value.ToString()) != -1)
				{
					using (UndoRedoManager.Start("ChangeProductAttribute"))
					{
						project.changeProductAttributeCombination(skuAttributeConfigurationDatagridview["Product Name", e.RowIndex].Value.ToString(), attributeName, skuAttributeConfigurationDatagridview[e.ColumnIndex, e.RowIndex].Value.ToString());
						UndoRedoManager.Commit();
					}
				}
				else if (skuAttributeConfigurationDatagridview[e.ColumnIndex, e.RowIndex].Value.ToString() != null && skuAttributeConfigurationDatagridview[e.ColumnIndex, e.RowIndex].Value.ToString() != "")
				{
					if (!skuAttributeConfigurationDatagridview[e.ColumnIndex, e.RowIndex].Value.ToString().Contains("%"))
					{
						MessageBox.Show(this, "Operation couldn't accept absolute values because this value might be out of the SKU's boundry", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					else
					{
						using (UndoRedoManager.Start("ChangeGroupAttribute"))
						{
							for (int i = e.RowIndex + 1; i < skuAttributeConfigurationDatagridview.RowCount && int.Parse(skuAttributeConfigurationDatagridview["Product Index", i].Value.ToString()) != -1; i++)
							{
								if (skuAttributeConfigurationDatagridview.Rows[i].Visible)
								{
									project.changeProductAttributeCombination(skuAttributeConfigurationDatagridview["Product Name", i].Value.ToString(), attributeName, skuAttributeConfigurationDatagridview[e.ColumnIndex, e.RowIndex].Value.ToString());
								}
							}
							UndoRedoManager.Commit();
						}
					}
				}
			}
			else if (skuAttributeConfigurationDatagridview.Columns[e.ColumnIndex].Name == "Product Name" && (object)skuAttributeConfigurationDatagridview[e.ColumnIndex, e.RowIndex].GetType() == typeof(DataGridViewComboBoxCell))
			{
				if (skuAttributeConfigurationDatagridview["Type", e.RowIndex].Value.ToString() == "ExistingProduct")
				{
					using (UndoRedoManager.Start("IncludeExcludeProducts"))
					{
						project.includeExcludeProductScenarios(skuAttributeConfigurationDatagridview[e.ColumnIndex, e.RowIndex].Value.ToString(), include: true);
						UndoRedoManager.Commit();
					}
				}
				else if (skuAttributeConfigurationDatagridview["Type", e.RowIndex].Value.ToString() == "ReplacedProduct")
				{
					using (UndoRedoManager.Start("IncludeExcludeProducts"))
					{
						project.includeExcludeProductScenarios(skuAttributeConfigurationDatagridview[e.ColumnIndex, e.RowIndex].Value.ToString(), include: true);
						UndoRedoManager.Commit();
					}
				}
			}
			reloadAttributeConfiguration();
		}
		if (e.RowIndex != -1 && e.ColumnIndex != -1 && skuAttributeConfigurationDatagridview[e.ColumnIndex, e.RowIndex].Visible)
		{
			skuAttributeConfigurationDatagridview.FirstDisplayedScrollingRowIndex = firstDisplayedScrollingRowIndex;
			skuAttributeConfigurationDatagridview.SelectionMode = DataGridViewSelectionMode.CellSelect;
			skuAttributeConfigurationDatagridview.CurrentCell = skuAttributeConfigurationDatagridview[e.ColumnIndex, e.RowIndex];
			skuAttributeConfigurationDatagridview.CurrentCell.Selected = true;
		}
	}

	private void skuAttributeConfigurationDatagridview_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		int firstDisplayedScrollingRowIndex = skuAttributeConfigurationDatagridview.FirstDisplayedScrollingRowIndex;
		if (e.ColumnIndex > -1 && e.RowIndex > -1 && skuAttributeConfigurationDatagridview.Columns[e.ColumnIndex].HeaderText == "Selected")
		{
			if (int.Parse(skuAttributeConfigurationDatagridview["Product Index", e.RowIndex].Value.ToString()) == -1)
			{
				using (UndoRedoManager.Start("IncludeExcludeProducts"))
				{
					for (int i = e.RowIndex + 1; i < skuAttributeConfigurationDatagridview.RowCount && int.Parse(skuAttributeConfigurationDatagridview["Product Index", i].Value.ToString()) != -1; i++)
					{
						if (skuAttributeConfigurationDatagridview.Rows[i].Visible)
						{
							project.includeExcludeProductScenarios(skuAttributeConfigurationDatagridview["Product Name", i].Value.ToString(), !bool.Parse(project.DtDisplayResult.Rows[e.RowIndex]["Selected"].ToString()));
						}
					}
					UndoRedoManager.Commit();
				}
			}
			else
			{
				using (UndoRedoManager.Start("IncludeExcludeProducts"))
				{
					project.includeExcludeProductScenarios(skuAttributeConfigurationDatagridview["Product Name", e.RowIndex].Value.ToString(), !bool.Parse(project.DtDisplayResult.Rows[e.RowIndex]["Selected"].ToString()));
					UndoRedoManager.Commit();
				}
			}
			reloadAttributeConfiguration();
		}
		else if (e.RowIndex > -1 && e.ColumnIndex > -1 && skuAttributeConfigurationDatagridview.Columns[e.ColumnIndex].HeaderText == "E/C" && int.Parse(skuAttributeConfigurationDatagridview["Product Index", e.RowIndex].Value.ToString()) == -1)
		{
			if (skuAttributeConfigurationDatagridview["E/C", e.RowIndex].Tag.ToString() == "Collapse_large")
			{
				skuAttributeConfigurationDatagridview["E/C", e.RowIndex].Value = Resources.Expand_large;
				skuAttributeConfigurationDatagridview["E/C", e.RowIndex].Tag = "Expand_large";
				for (int i = e.RowIndex + 1; i < skuAttributeConfigurationDatagridview.RowCount && int.Parse(skuAttributeConfigurationDatagridview["Product Index", i].Value.ToString()) != -1; i++)
				{
					skuAttributeConfigurationDatagridview.Rows[i].Visible = false;
				}
			}
			else if (skuAttributeConfigurationDatagridview["E/C", e.RowIndex].Tag.ToString() == "Expand_large")
			{
				skuAttributeConfigurationDatagridview["E/C", e.RowIndex].Value = Resources.Collapse_large;
				skuAttributeConfigurationDatagridview["E/C", e.RowIndex].Tag = "Collapse_large";
				reloadAttributeConfiguration();
			}
		}
		if (e.RowIndex != -1 && e.ColumnIndex != -1 && skuAttributeConfigurationDatagridview[e.ColumnIndex, e.RowIndex].Visible)
		{
			skuAttributeConfigurationDatagridview.FirstDisplayedScrollingRowIndex = firstDisplayedScrollingRowIndex;
			skuAttributeConfigurationDatagridview.SelectionMode = DataGridViewSelectionMode.CellSelect;
			skuAttributeConfigurationDatagridview.CurrentCell = skuAttributeConfigurationDatagridview[e.ColumnIndex, e.RowIndex];
			skuAttributeConfigurationDatagridview.CurrentCell.Selected = true;
		}
	}

	private void skuAttributeConfigurationDatagridview_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
	{
		cancelDisplayResultEdit = false;
		if (e.FormattedValue != null && skuAttributeConfigurationDatagridview[e.ColumnIndex, e.RowIndex].Value != null && e.FormattedValue.ToString() != skuAttributeConfigurationDatagridview[e.ColumnIndex, e.RowIndex].Value.ToString() && skuAttributeConfigurationDatagridview.Columns[e.ColumnIndex].Name != "Audit Share")
		{
			if (skuAttributeConfigurationDatagridview[e.ColumnIndex, e.RowIndex].ReadOnly)
			{
				return;
			}
			if (!string.IsNullOrEmpty(e.FormattedValue.ToString()))
			{
				if (e.FormattedValue.ToString() == skuAttributeConfigurationDatagridview[e.ColumnIndex, e.RowIndex].Value.ToString())
				{
					skuAttributeConfigurationDatagridview.CancelEdit();
					cancelDisplayResultEdit = true;
				}
				else if (int.Parse(skuAttributeConfigurationDatagridview["Product Index", e.RowIndex].Value.ToString()) == -1)
				{
					double[] attributeLevelsBoundries = project.getAttributeLevelsBoundries(skuAttributeConfigurationDatagridview.Columns[e.ColumnIndex].Name.Replace("New ", ""));
					if (double.Parse(e.FormattedValue.ToString().Replace("%", "")) / 100.0 < attributeLevelsBoundries[0] && double.Parse(e.FormattedValue.ToString().Replace("%", "")) / 100.0 > attributeLevelsBoundries[1])
					{
						MessageBox.Show(this, skuAttributeConfigurationDatagridview[e.ColumnIndex, e.RowIndex].ToolTipText, "Process Failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
				else
				{
					if ((object)skuAttributeConfigurationDatagridview[e.ColumnIndex, e.RowIndex].GetType() != typeof(DataGridViewTextBoxCell))
					{
						return;
					}
					if (skuAttributeConfigurationDatagridview.Columns[e.ColumnIndex].Name != "New WD")
					{
						if (!e.FormattedValue.ToString().Contains("%") && !string.Equals(e.FormattedValue.ToString(), "0"))
						{
							string[] array = skuAttributeConfigurationDatagridview[e.ColumnIndex, e.RowIndex].ToolTipText.Split(new string[1] { "and" }, StringSplitOptions.RemoveEmptyEntries);
							string s = array[1].Replace(" Maximum value is ", "");
							string s2 = array[0].Replace("Minimum value is ", "");
							if (double.Parse(e.FormattedValue.ToString()) > double.Parse(s))
							{
								MessageBox.Show(this, "Entered Value is outside the MAX Boundry\nCouldn't process this operation", "Process Failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								skuAttributeConfigurationDatagridview.CancelEdit();
							}
							else if (double.Parse(e.FormattedValue.ToString()) < double.Parse(s2))
							{
								MessageBox.Show(this, "Entered Value is outside the MIN Boundry\nCouldn't process this operation", "Process Failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								skuAttributeConfigurationDatagridview.CancelEdit();
								cancelDisplayResultEdit = true;
							}
						}
						else
						{
							double[] attributeLevelsBoundries = project.getAttributeLevelsBoundries(skuAttributeConfigurationDatagridview.Columns[e.ColumnIndex].Name.Replace("New ", ""));
							if (double.Parse(e.FormattedValue.ToString().Replace("%", "")) < attributeLevelsBoundries[0] * 100.0)
							{
								MessageBox.Show(this, "Entered Value is outside the MAX Boundry\nCouldn't process this operation", "Process Failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								skuAttributeConfigurationDatagridview.CancelEdit();
							}
							else if (double.Parse(e.FormattedValue.ToString().Replace("%", "")) > attributeLevelsBoundries[1] * 100.0)
							{
								MessageBox.Show(this, "Entered Value is outside the MIN Boundry\nCouldn't process this operation", "Process Failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								skuAttributeConfigurationDatagridview.CancelEdit();
								cancelDisplayResultEdit = true;
							}
						}
					}
					else if (skuAttributeConfigurationDatagridview.Columns[e.ColumnIndex].Name == "New WD" && (double.Parse(e.FormattedValue.ToString()) > 100.0 || double.Parse(e.FormattedValue.ToString()) < 0.0))
					{
						MessageBox.Show(this, "WD Value must be less than or equal 100 and greater than or equal 0\nCouldn't process this operation", "Process Failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						skuAttributeConfigurationDatagridview.CancelEdit();
						cancelDisplayResultEdit = true;
					}
				}
			}
			else
			{
				skuAttributeConfigurationDatagridview.CancelEdit();
				cancelDisplayResultEdit = true;
			}
		}
		else
		{
			cancelDisplayResultEdit = true;
		}
	}

	private void skuAttributeConfigurationDatagridview_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
	{
		if (e.Control && e.KeyCode == Keys.C)
		{
			Clipboard.SetDataObject(skuAttributeConfigurationDatagridview.GetClipboardContent());
		}
		else if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
		{
			if (Clipboard.GetDataObject().GetDataPresent(DataFormats.CommaSeparatedValue))
			{
				pasteDataGridViewFromClipboard();
			}
		}
		else if (e.Control && e.KeyCode == Keys.Z)
		{
			undoOperation();
		}
		else if (e.Control && e.KeyCode == Keys.Y)
		{
			redoOperation();
		}
	}

	private void skuAttributeConfigurationDatagridview_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		skuAttributeConfigurationDatagridview.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;
		skuAttributeConfigurationDatagridview.Columns[e.ColumnIndex].Selected = true;
		if (e.ColumnIndex != -1)
		{
			skuAttributeConfigurationDatagridview.CurrentCell = skuAttributeConfigurationDatagridview[e.ColumnIndex, 0];
		}
	}

	private void skuAttributeConfigurationDatagridview_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		skuAttributeConfigurationDatagridview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		skuAttributeConfigurationDatagridview.Rows[e.RowIndex].Selected = true;
		if (e.RowIndex != -1)
		{
			skuAttributeConfigurationDatagridview.CurrentCell = skuAttributeConfigurationDatagridview[0, e.RowIndex];
		}
	}

	private void pasteDataGridViewFromClipboard()
	{
		string text = "";
		try
		{
			bool flag = true;
			int rowIndex = skuAttributeConfigurationDatagridview.FirstDisplayedCell.RowIndex;
			int rowIndex2 = skuAttributeConfigurationDatagridview.SelectedCells[skuAttributeConfigurationDatagridview.SelectedCells.Count - 1].RowIndex;
			int i = skuAttributeConfigurationDatagridview.SelectedCells[skuAttributeConfigurationDatagridview.SelectedCells.Count - 1].RowIndex;
			int columnIndex = skuAttributeConfigurationDatagridview.SelectedCells[skuAttributeConfigurationDatagridview.SelectedCells.Count - 1].ColumnIndex;
			if (i > skuAttributeConfigurationDatagridview.SelectedCells[0].RowIndex)
			{
				i = skuAttributeConfigurationDatagridview.SelectedCells[0].RowIndex;
				columnIndex = skuAttributeConfigurationDatagridview.SelectedCells[0].ColumnIndex;
			}
			object data = Clipboard.GetDataObject().GetData(DataFormats.CommaSeparatedValue);
			string text2 = data.ToString();
			if (text2 == "System.IO.MemoryStream")
			{
				StreamReader streamReader = new StreamReader(Clipboard.GetDataObject().GetData(DataFormats.CommaSeparatedValue) as MemoryStream);
				text2 = streamReader.ReadToEnd();
			}
			if (text2.EndsWith("\r\n\0"))
			{
				text2 = text2.Substring(0, text2.Length - 3);
			}
			string[] array = text2.Split(new string[3] { ",", "\r\n", "\0" }, StringSplitOptions.None);
			int num = skuAttributeConfigurationDatagridview.SelectedCells.Count / array.Length;
			if (num == 0)
			{
				num = 1;
			}
			using (UndoRedoManager.Start("PasteCells"))
			{
				for (int j = 0; j < num; j++)
				{
					for (int k = 0; k < array.Length; k++)
					{
						text = array[k];
						if (skuAttributeConfigurationDatagridview.SelectedCells.Count != 1 && rowIndex2 + skuAttributeConfigurationDatagridview.SelectedCells.Count < i)
						{
							break;
						}
						if (string.IsNullOrEmpty(array[k]))
						{
							i++;
							continue;
						}
						if (int.Parse(skuAttributeConfigurationDatagridview.Rows[i].Cells["Product Index"].Value.ToString()) == -1)
						{
							i++;
						}
						for (; skuAttributeConfigurationDatagridview.RowCount > i && (!skuAttributeConfigurationDatagridview.Rows[i].Visible || int.Parse(skuAttributeConfigurationDatagridview.Rows[i].Cells["Product Index"].Value.ToString()) == -1); i++)
						{
						}
						if (skuAttributeConfigurationDatagridview.RowCount > i && skuAttributeConfigurationDatagridview.Rows[i].Visible && bool.Parse(skuAttributeConfigurationDatagridview["Selected", i].Value.ToString()))
						{
							if ((object)skuAttributeConfigurationDatagridview[columnIndex, i].GetType() == typeof(DataGridViewTextBoxCell))
							{
								if (skuAttributeConfigurationDatagridview.Columns[columnIndex].Name != "Audit Share")
								{
									bool flag2 = true;
									if (!skuAttributeConfigurationDatagridview.Columns[columnIndex].Name.Equals("New WD"))
									{
										if (double.Parse(skuAttributeConfigurationDatagridview[columnIndex, i].Value.ToString()) != 0.0)
										{
											string[] array2 = skuAttributeConfigurationDatagridview[columnIndex, i].ToolTipText.Split(new string[1] { "and" }, StringSplitOptions.RemoveEmptyEntries);
											string s = array2[1].Replace(" Maximum value is ", "");
											string s2 = array2[0].Replace("Minimum value is ", "");
											if (double.Parse(array[k].Replace("%", "")) > double.Parse(s))
											{
												flag2 = false;
												flag = false;
											}
											else if (double.Parse(array[k].Replace("%", "")) < double.Parse(s2))
											{
												flag2 = false;
												flag = false;
											}
										}
									}
									else if (double.Parse(array[k].Replace("%", "")) > 100.0 || double.Parse(array[k].Replace("%", "")) < 0.0)
									{
										flag2 = false;
										flag = false;
									}
									if (flag2)
									{
										project.changeProductAttributeCombination(skuAttributeConfigurationDatagridview.Rows[i].Cells["Product Name"].Value.ToString(), skuAttributeConfigurationDatagridview.Columns[columnIndex].HeaderText.Replace("New ", ""), array[k]);
									}
								}
								else
								{
									project.changeProductAttributeCombination(skuAttributeConfigurationDatagridview.Rows[i].Cells["Product Name"].Value.ToString(), "Audit Share", array[k]);
								}
							}
							i++;
						}
						else if (skuAttributeConfigurationDatagridview.Rows[i].Visible && !bool.Parse(skuAttributeConfigurationDatagridview["Selected", i].Value.ToString()))
						{
							i++;
						}
					}
				}
				UndoRedoManager.Commit();
			}
			if (!flag)
			{
				MessageBox.Show(this, "Some entered Value is outside the Boundaries, Can't Change these values", "Process Failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			reloadAttributeConfiguration();
			skuAttributeConfigurationDatagridview.CurrentCell = skuAttributeConfigurationDatagridview[columnIndex, rowIndex2];
			skuAttributeConfigurationDatagridview.CurrentCell.Selected = true;
			skuAttributeConfigurationDatagridview.FirstDisplayedScrollingRowIndex = rowIndex;
		}
		catch (FormatException)
		{
			MessageBox.Show(this, "Some entries has wrong variables = [" + text + "]\nCouldn't continue operation", "FormatException", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		catch (Exception ex2)
		{
			MessageBox.Show(this, ex2.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{
			if (scenarioDropdownbox.SelectedItem != null)
			{
				using (UndoRedoManager.Start("Load Scenario"))
				{
					project.bindScenarioDatatableObjectToProjectProducts(getScenario(scenarioDropdownbox.Text));
					UndoRedoManager.Commit();
				}
			}
			reloadAttributeConfiguration();
		}
		catch (Exception ex)
		{
			MessageBox.Show(this, ex.Message, "Failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void setSegmentationToolStripButton()
	{
		string[] segmentation = project.getSegmentation();
		if (segmentation[0] != null && !segmentation[0].Equals(""))
		{
			ToolStripMenuItem toolStripMenuItem = segmentationToolstripSplitButton.DropDownItems[segmentation[0]] as ToolStripMenuItem;
			toolStripMenuItem.Checked = true;
			ToolStripMenuItem toolStripMenuItem2 = toolStripMenuItem.DropDownItems[segmentation[1]] as ToolStripMenuItem;
			toolStripMenuItem2.Checked = true;
			segmentationToolstripSplitButton.Text = string.Concat(toolStripMenuItem, "/", toolStripMenuItem2);
		}
		else
		{
			ToolStripMenuItem toolStripMenuItem = segmentationToolstripSplitButton.DropDownItems["No Segmentation"] as ToolStripMenuItem;
			toolStripMenuItem.Checked = true;
			segmentationToolstripSplitButton.Text = "No Segmentation";
		}
	}

	private DataTable getScenario(string scenarioName)
	{
		Stream stream = null;
		try
		{
			stream = File.Open(scenariosPath + scenarioName + ".osl", FileMode.Open);
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			return (DataTable)binaryFormatter.Deserialize(stream);
		}
		catch (Exception ex)
		{
			MessageBox.Show(this, ex.Message, "Failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return null;
		}
		finally
		{
			stream.Close();
		}
	}

	private void saveToolStripButton_Click(object sender, EventArgs e)
	{
		try
		{
			if (skuAttributeConfigurationDatagridview.Columns["Audit Share"].Visible)
			{
				foreach (DataGridViewRow item in (IEnumerable)skuAttributeConfigurationDatagridview.Rows)
				{
					if (double.Parse(item.Cells["Product Index"].Value.ToString()) != -1.0)
					{
						double.Parse(item.Cells["Audit Share"].Value.ToString().Replace("%", ""));
					}
				}
			}
			StudyScenariosForm studyScenariosForm = new StudyScenariosForm();
			base.DialogResult = studyScenariosForm.ShowDialog(this);
			if (base.DialogResult == DialogResult.OK)
			{
				try
				{
					string text = checkStringCharacters(studyScenariosForm.textBox1.Text);
					if (!searchScenarioXMLFileElementExist(text))
					{
						string text2 = ProjectCore.StudyPath.Remove(ProjectCore.StudyPath.LastIndexOf("\\Database")) + "\\Scenarios\\";
						Stream stream = File.Open(text2 + text + ".osl", FileMode.Create);
						BinaryFormatter binaryFormatter = new BinaryFormatter();
						binaryFormatter.Serialize(stream, project.getScenarioDatatableObject(text));
						stream.Close();
						studyscenariosLibrary.Add(text, studyScenariosForm.textBox2.Text);
						if (skuAttributeConfigurationDatagridview.Columns["Audit Share"].Visible)
						{
							validationScenarios.Add(text, studyScenariosForm.textBox2.Text);
							validation = true;
						}
						saveScenarioToXmlFile(text, studyScenariosForm.textBox2.Text);
						loadScenarios();
						scenarioDropdownbox.SelectedItem = scenarioDropdownbox.Items[scenarioDropdownbox.Items.IndexOf(studyScenariosForm.textBox1.Text)];
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(this, ex.Message, "Process Failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			base.DialogResult = DialogResult.None;
		}
		catch (Exception)
		{
			MessageBox.Show(this, "There is error with the audit shares", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private bool searchScenarioXMLFileElementExist(string nameElement)
	{
		XmlTextReader xmlTextReader = new XmlTextReader(ProjectCore.StudyPath.Remove(ProjectCore.StudyPath.LastIndexOf("\\Database")) + "\\Scenarios\\Scenarios.xml");
		try
		{
			xmlTextReader.WhitespaceHandling = WhitespaceHandling.None;
			while (xmlTextReader.ReadToFollowing("Scenario"))
			{
				XmlReader xmlReader = xmlTextReader.ReadSubtree();
				string text = "";
				xmlReader.ReadToFollowing("Name", "");
				text = xmlReader.ReadString();
				if (text == nameElement)
				{
					throw new Exception("The Scenario Name is already exist in the library\nPlease rename this Sceanrio");
				}
			}
			return false;
		}
		catch (Exception ex)
		{
			MessageBox.Show(this, ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return true;
		}
		finally
		{
			xmlTextReader.Close();
		}
	}

	private void saveScenarioToXmlFile(string scenarioName, string scenarioDescription)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.Load(ProjectCore.StudyPath.Remove(ProjectCore.StudyPath.LastIndexOf("\\Database")) + "\\Scenarios\\Scenarios.xml");
		XmlElement xmlElement = xmlDocument.CreateElement("Scenario");
		XmlElement xmlElement2 = xmlDocument.CreateElement("Name");
		xmlElement2.InnerText = scenarioName;
		xmlElement.AppendChild(xmlElement2);
		XmlElement xmlElement3 = xmlDocument.CreateElement("Description");
		xmlElement3.InnerText = scenarioDescription;
		xmlElement.AppendChild(xmlElement3);
		xmlElement.SetAttribute("Validation", validation.ToString());
		xmlDocument.DocumentElement.InsertAfter(xmlElement, xmlDocument.DocumentElement.LastChild);
		xmlDocument.Save(ProjectCore.StudyPath.Remove(ProjectCore.StudyPath.LastIndexOf("\\Database")) + "\\Scenarios\\Scenarios.xml");
	}

	private void button2_Click(object sender, EventArgs e)
	{
		try
		{
			if (skuAttributeConfigurationDatagridview.Columns.Contains("Audit Share"))
			{
				foreach (DataGridViewRow item in (IEnumerable)skuAttributeConfigurationDatagridview.Rows)
				{
					if (double.Parse(item.Cells["Product Index"].Value.ToString()) != -1.0)
					{
						double.Parse(item.Cells["Audit Share"].Value.ToString().Replace("%", ""));
					}
				}
			}
			base.DialogResult = DialogResult.OK;
		}
		catch (Exception)
		{
			MessageBox.Show(this, "There is error with the audit shares", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void checkBox1_CheckedChanged(object sender, EventArgs e)
	{
		if (checkBox1.Checked)
		{
			project.setGroupResearchStart(project.GroupTypeList[0], checkBox1.Checked);
		}
		else
		{
			project.UseGroupng = checkBox1.Checked;
		}
		reloadAttributeConfiguration();
	}

	private void undoOperation()
	{
		if (!project.CanUndo || string.IsNullOrEmpty(project.UndoCommandType))
		{
			return;
		}
		if (project.UndoCommandType.Equals("ChangeSegment") || project.UndoCommandType.Equals("ResetStudy"))
		{
			project.undoOperation();
			string[] segmentation = project.getSegmentation();
			if (!string.IsNullOrEmpty(segmentation[0]))
			{
				using (UndoRedoManager.Start("Undo"))
				{
					project.setSegmentation(segmentation[0], segmentation[1]);
				}
			}
			else
			{
				using (UndoRedoManager.Start("Undo"))
				{
					project.resetSegmentation();
				}
			}
		}
		else
		{
			project.undoOperation();
		}
		reloadAttributeConfiguration();
	}

	private void redoOperation()
	{
		if (!project.CanRedo || string.IsNullOrEmpty(project.RedoCommandType))
		{
			return;
		}
		if (project.RedoCommandType.Equals("ChangeSegment") || project.RedoCommandType.Equals("ResetStudy"))
		{
			project.redoOperation();
			string[] segmentation = project.getSegmentation();
			if (!string.IsNullOrEmpty(segmentation[0]))
			{
				using (UndoRedoManager.Start("Redo"))
				{
					project.setSegmentation(segmentation[0], segmentation[1]);
				}
			}
			else
			{
				using (UndoRedoManager.Start("Redo"))
				{
					project.resetSegmentation();
				}
			}
		}
		else
		{
			project.redoOperation();
		}
		reloadAttributeConfiguration();
	}

	private void checkBox2_CheckedChanged(object sender, EventArgs e)
	{
		if (checkBox2.Checked)
		{
			ProjectCore.EnlargeAll = true;
			checkBox2.FlatAppearance.BorderSize = 1;
		}
		else
		{
			ProjectCore.EnlargeAll = false;
			checkBox2.FlatAppearance.BorderSize = 0;
		}
		reloadAttributeConfiguration();
	}

	private void restoreDefaultButton_Click(object sender, EventArgs e)
	{
		using (UndoRedoManager.Start("ResetStudy"))
		{
			project.resetProductValues();
			UndoRedoManager.Commit();
		}
		scenarioDropdownbox.SelectedItem = null;
		reloadAttributeConfiguration();
		skuAttributeConfigurationDatagridview.Focus();
	}

	private void checkAllProductsButton_Click(object sender, EventArgs e)
	{
		using (UndoRedoManager.Start("CheckAllProducts"))
		{
			foreach (DataGridViewRow item in (IEnumerable)skuAttributeConfigurationDatagridview.Rows)
			{
				if (item.Visible && int.Parse(item.Cells["Product Index"].Value.ToString()) != -1 && !string.Equals(item.Cells["Type"].Value.ToString(), "ReplacedProduct"))
				{
					project.includeExcludeProductScenarios(item.Cells["Product Name"].Value.ToString(), include: true);
				}
			}
			reloadAttributeConfiguration();
			UndoRedoManager.Commit();
		}
	}

	private void uncheckAllProductsButton_Click(object sender, EventArgs e)
	{
		using (UndoRedoManager.Start("UncheckAllProducts"))
		{
			foreach (DataGridViewRow item in (IEnumerable)skuAttributeConfigurationDatagridview.Rows)
			{
				if (item.Visible && int.Parse(item.Cells["Product Index"].Value.ToString()) != -1)
				{
					project.includeExcludeProductScenarios(item.Cells["Product Name"].Value.ToString(), include: false);
				}
			}
			reloadAttributeConfiguration();
			UndoRedoManager.Commit();
		}
	}

	private void skuAttributeConfigurationDatagridview_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
	{
		columnsSizeDictionary[e.Column.Name] = e.Column.Width;
	}

	private string checkStringCharacters(string name)
	{
		char[] array = forbiddenCharacters;
		foreach (char oldChar in array)
		{
			name = name.Replace(oldChar, '+');
		}
		return name;
	}

	private void saveScenarioToolStripMenuItem_Click(object sender, EventArgs e)
	{
		saveToolStripButton_Click(sender, e);
	}

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
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MarketSimulator.Reports.SKUAttributeConfiguration));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		this.label1 = new System.Windows.Forms.Label();
		this.toolStrip1 = new System.Windows.Forms.ToolStrip();
		this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
		this.scenarioDropdownbox = new System.Windows.Forms.ToolStripComboBox();
		this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
		this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.segmentationToolstripSplitButton = new System.Windows.Forms.ToolStripDropDownButton();
		this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
		this.restoreDefaultButton = new System.Windows.Forms.ToolStripButton();
		this.button2 = new System.Windows.Forms.Button();
		this.label2 = new System.Windows.Forms.Label();
		this.skuAttributeConfigurationDatagridview = new System.Windows.Forms.DataGridView();
		this.checkBox2 = new System.Windows.Forms.CheckBox();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.checkBox1 = new System.Windows.Forms.CheckBox();
		this.uncheckAllProductsButton = new System.Windows.Forms.Button();
		this.checkAllProductsButton = new System.Windows.Forms.Button();
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.menuStrip1 = new System.Windows.Forms.MenuStrip();
		this.saveScenarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStrip1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.skuAttributeConfigurationDatagridview).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		this.menuStrip1.SuspendLayout();
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Georgia", 21.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.SteelBlue;
		this.label1.Location = new System.Drawing.Point(3, 9);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(417, 34);
		this.label1.TabIndex = 0;
		this.label1.Text = "SKU's Attributes Configuration";
		this.toolStrip1.AutoSize = false;
		this.toolStrip1.BackColor = System.Drawing.Color.Gray;
		this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
		this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
		this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[10] { this.saveToolStripButton, this.scenarioDropdownbox, this.toolStripLabel1, this.toolStripSeparator1, this.toolStripSeparator2, this.toolStripDropDownButton1, this.toolStripSeparator3, this.segmentationToolstripSplitButton, this.toolStripSeparator4, this.restoreDefaultButton });
		this.toolStrip1.Location = new System.Drawing.Point(9, 82);
		this.toolStrip1.Name = "toolStrip1";
		this.toolStrip1.Size = new System.Drawing.Size(792, 25);
		this.toolStrip1.TabIndex = 2;
		this.toolStrip1.Text = "toolStrip1";
		this.saveToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
		this.saveToolStripButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
		this.saveToolStripButton.Image = (System.Drawing.Image)resources.GetObject("saveToolStripButton.Image");
		this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.saveToolStripButton.Name = "saveToolStripButton";
		this.saveToolStripButton.Size = new System.Drawing.Size(99, 22);
		this.saveToolStripButton.Text = "&Save Scenario";
		this.saveToolStripButton.Click += new System.EventHandler(saveToolStripButton_Click);
		this.scenarioDropdownbox.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
		this.scenarioDropdownbox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.scenarioDropdownbox.Name = "scenarioDropdownbox";
		this.scenarioDropdownbox.Size = new System.Drawing.Size(121, 25);
		this.scenarioDropdownbox.SelectedIndexChanged += new System.EventHandler(toolStripComboBox1_SelectedIndexChanged);
		this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
		this.toolStripLabel1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
		this.toolStripLabel1.Name = "toolStripLabel1";
		this.toolStripLabel1.Size = new System.Drawing.Size(52, 22);
		this.toolStripLabel1.Text = "Scenario";
		this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
		this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.toolStripDropDownButton1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
		this.toolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton1.Image");
		this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
		this.toolStripDropDownButton1.Size = new System.Drawing.Size(94, 22);
		this.toolStripDropDownButton1.Text = "New Products";
		this.toolStripDropDownButton1.DropDownOpening += new System.EventHandler(toolStripDropDownButton1_DropDownOpening);
		this.toolStripSeparator3.Name = "toolStripSeparator3";
		this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
		this.segmentationToolstripSplitButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.segmentationToolstripSplitButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
		this.segmentationToolstripSplitButton.Image = (System.Drawing.Image)resources.GetObject("segmentationToolstripSplitButton.Image");
		this.segmentationToolstripSplitButton.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.segmentationToolstripSplitButton.Name = "segmentationToolstripSplitButton";
		this.segmentationToolstripSplitButton.Size = new System.Drawing.Size(113, 22);
		this.segmentationToolstripSplitButton.Text = "No Segmentation";
		this.toolStripSeparator4.Name = "toolStripSeparator4";
		this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
		this.restoreDefaultButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.restoreDefaultButton.ForeColor = System.Drawing.Color.White;
		this.restoreDefaultButton.Image = (System.Drawing.Image)resources.GetObject("restoreDefaultButton.Image");
		this.restoreDefaultButton.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.restoreDefaultButton.Name = "restoreDefaultButton";
		this.restoreDefaultButton.Size = new System.Drawing.Size(96, 22);
		this.restoreDefaultButton.Text = "Restore Defaults";
		this.restoreDefaultButton.Click += new System.EventHandler(restoreDefaultButton_Click);
		this.button2.Location = new System.Drawing.Point(726, 520);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(75, 23);
		this.button2.TabIndex = 5;
		this.button2.Text = "Finish";
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Font = new System.Drawing.Font("Tahoma", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.MidnightBlue;
		this.label2.Location = new System.Drawing.Point(12, 55);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(562, 16);
		this.label2.TabIndex = 7;
		this.label2.Text = "Change SKU's attribute manually or choose from the saved scenarios then press the finish button";
		this.skuAttributeConfigurationDatagridview.AllowUserToAddRows = false;
		this.skuAttributeConfigurationDatagridview.AllowUserToDeleteRows = false;
		this.skuAttributeConfigurationDatagridview.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.skuAttributeConfigurationDatagridview.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
		this.skuAttributeConfigurationDatagridview.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle.Font = new System.Drawing.Font("Tahoma", 8f);
		dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.skuAttributeConfigurationDatagridview.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8f);
		dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.skuAttributeConfigurationDatagridview.DefaultCellStyle = dataGridViewCellStyle2;
		this.skuAttributeConfigurationDatagridview.Location = new System.Drawing.Point(9, 105);
		this.skuAttributeConfigurationDatagridview.Name = "skuAttributeConfigurationDatagridview";
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8f);
		dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.skuAttributeConfigurationDatagridview.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
		this.skuAttributeConfigurationDatagridview.RowHeadersWidth = 20;
		this.skuAttributeConfigurationDatagridview.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.skuAttributeConfigurationDatagridview.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		this.skuAttributeConfigurationDatagridview.RowTemplate.Height = 20;
		this.skuAttributeConfigurationDatagridview.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.skuAttributeConfigurationDatagridview.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
		this.skuAttributeConfigurationDatagridview.Size = new System.Drawing.Size(792, 400);
		this.skuAttributeConfigurationDatagridview.TabIndex = 8;
		this.skuAttributeConfigurationDatagridview.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(skuAttributeConfigurationDatagridview_ColumnHeaderMouseClick);
		this.skuAttributeConfigurationDatagridview.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(skuAttributeConfigurationDatagridview_PreviewKeyDown);
		this.skuAttributeConfigurationDatagridview.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(skuAttributeConfigurationDatagridview_CellValidating);
		this.skuAttributeConfigurationDatagridview.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(skuAttributeConfigurationDatagridview_CellEndEdit);
		this.skuAttributeConfigurationDatagridview.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(skuAttributeConfigurationDatagridview_CellClick);
		this.skuAttributeConfigurationDatagridview.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(skuAttributeConfigurationDatagridview_RowHeaderMouseClick);
		this.skuAttributeConfigurationDatagridview.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(skuAttributeConfigurationDatagridview_ColumnWidthChanged);
		this.checkBox2.Appearance = System.Windows.Forms.Appearance.Button;
		this.checkBox2.BackColor = System.Drawing.Color.Transparent;
		this.checkBox2.FlatAppearance.BorderSize = 0;
		this.checkBox2.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
		this.checkBox2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
		this.checkBox2.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientActiveCaption;
		this.checkBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.checkBox2.Font = new System.Drawing.Font("Wingdings 3", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 2);
		this.checkBox2.ForeColor = System.Drawing.Color.SteelBlue;
		this.checkBox2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.checkBox2.Location = new System.Drawing.Point(747, 60);
		this.checkBox2.Margin = new System.Windows.Forms.Padding(0);
		this.checkBox2.Name = "checkBox2";
		this.checkBox2.Size = new System.Drawing.Size(18, 25);
		this.checkBox2.TabIndex = 10;
		this.checkBox2.Text = "G";
		this.checkBox2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.checkBox2.UseVisualStyleBackColor = false;
		this.checkBox2.CheckedChanged += new System.EventHandler(checkBox2_CheckedChanged);
		this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
		this.pictureBox1.Image = MarketSimulator.Reports.Properties.Resources.Marketeers_Logo_Blue;
		this.pictureBox1.Location = new System.Drawing.Point(647, 6);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(164, 34);
		this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
		this.pictureBox1.TabIndex = 1;
		this.pictureBox1.TabStop = false;
		this.checkBox1.Appearance = System.Windows.Forms.Appearance.Button;
		this.checkBox1.BackColor = System.Drawing.Color.Transparent;
		this.checkBox1.Checked = true;
		this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox1.FlatAppearance.BorderSize = 0;
		this.checkBox1.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
		this.checkBox1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
		this.checkBox1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientActiveCaption;
		this.checkBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.checkBox1.Image = MarketSimulator.Reports.Properties.Resources.showHideTree;
		this.checkBox1.Location = new System.Drawing.Point(723, 61);
		this.checkBox1.Margin = new System.Windows.Forms.Padding(0);
		this.checkBox1.Name = "checkBox1";
		this.checkBox1.Size = new System.Drawing.Size(21, 20);
		this.checkBox1.TabIndex = 9;
		this.checkBox1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.checkBox1.UseVisualStyleBackColor = false;
		this.checkBox1.CheckedChanged += new System.EventHandler(checkBox1_CheckedChanged);
		this.uncheckAllProductsButton.BackColor = System.Drawing.Color.Transparent;
		this.uncheckAllProductsButton.FlatAppearance.BorderSize = 0;
		this.uncheckAllProductsButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
		this.uncheckAllProductsButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientActiveCaption;
		this.uncheckAllProductsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.uncheckAllProductsButton.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.uncheckAllProductsButton.ForeColor = System.Drawing.Color.SteelBlue;
		this.uncheckAllProductsButton.Location = new System.Drawing.Point(783, 60);
		this.uncheckAllProductsButton.Name = "uncheckAllProductsButton";
		this.uncheckAllProductsButton.Size = new System.Drawing.Size(18, 20);
		this.uncheckAllProductsButton.TabIndex = 12;
		this.uncheckAllProductsButton.Text = "X";
		this.toolTip1.SetToolTip(this.uncheckAllProductsButton, "Uncheck All Products");
		this.uncheckAllProductsButton.UseVisualStyleBackColor = false;
		this.uncheckAllProductsButton.Click += new System.EventHandler(uncheckAllProductsButton_Click);
		this.checkAllProductsButton.BackColor = System.Drawing.Color.Transparent;
		this.checkAllProductsButton.FlatAppearance.BorderSize = 0;
		this.checkAllProductsButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
		this.checkAllProductsButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientActiveCaption;
		this.checkAllProductsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.checkAllProductsButton.Font = new System.Drawing.Font("Wingdings 2", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 2);
		this.checkAllProductsButton.ForeColor = System.Drawing.Color.SteelBlue;
		this.checkAllProductsButton.Location = new System.Drawing.Point(765, 60);
		this.checkAllProductsButton.Name = "checkAllProductsButton";
		this.checkAllProductsButton.Size = new System.Drawing.Size(18, 20);
		this.checkAllProductsButton.TabIndex = 11;
		this.checkAllProductsButton.Text = "P";
		this.toolTip1.SetToolTip(this.checkAllProductsButton, "Check All Products");
		this.checkAllProductsButton.UseVisualStyleBackColor = false;
		this.checkAllProductsButton.Click += new System.EventHandler(checkAllProductsButton_Click);
		this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.saveScenarioToolStripMenuItem });
		this.menuStrip1.Location = new System.Drawing.Point(0, 0);
		this.menuStrip1.Name = "menuStrip1";
		this.menuStrip1.Size = new System.Drawing.Size(810, 24);
		this.menuStrip1.TabIndex = 13;
		this.menuStrip1.Text = "menuStrip1";
		this.menuStrip1.Visible = false;
		this.saveScenarioToolStripMenuItem.Name = "saveScenarioToolStripMenuItem";
		this.saveScenarioToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.S | System.Windows.Forms.Keys.Control;
		this.saveScenarioToolStripMenuItem.Size = new System.Drawing.Size(91, 20);
		this.saveScenarioToolStripMenuItem.Text = "Save Scenario";
		this.saveScenarioToolStripMenuItem.Click += new System.EventHandler(saveScenarioToolStripMenuItem_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.ControlLightLight;
		base.ClientSize = new System.Drawing.Size(810, 551);
		base.Controls.Add(this.checkAllProductsButton);
		base.Controls.Add(this.uncheckAllProductsButton);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.button2);
		base.Controls.Add(this.toolStrip1);
		base.Controls.Add(this.menuStrip1);
		base.Controls.Add(this.pictureBox1);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.skuAttributeConfigurationDatagridview);
		base.Controls.Add(this.checkBox1);
		base.Controls.Add(this.checkBox2);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.MainMenuStrip = this.menuStrip1;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "SKUAttributeConfiguration";
		this.Text = "Marketeers Charts Automation";
		base.Load += new System.EventHandler(SKUAttributeConfiguration_Load);
		base.Paint += new System.Windows.Forms.PaintEventHandler(SKUAttributeConfiguration_Paint);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(SKUAttributeConfiguration_FormClosing);
		this.toolStrip1.ResumeLayout(false);
		this.toolStrip1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.skuAttributeConfigurationDatagridview).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		this.menuStrip1.ResumeLayout(false);
		this.menuStrip1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
