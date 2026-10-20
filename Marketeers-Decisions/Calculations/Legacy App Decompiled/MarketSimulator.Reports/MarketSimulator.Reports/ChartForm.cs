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
using System.Windows.Forms.DataVisualization.Charting;
using MarketSimulator.Model;
using MarketSimulator.Reports.Properties;
using PresentationControls;
using UndoRedoFramework;

namespace MarketSimulator.Reports;

public class ChartForm : Form
{
	private IContainer components = null;

	private Label label1;

	private TabControl tabControl1;

	private TabPage tabPage1;

	private TabPage tabPage2;

	private PictureBox pictureBox1;

	private Label label2;

	private Button button2;

	private Button button3;

	private TabPage tabPage3;

	private TabPage tabPage4;

	private TabPage tabPage6;

	private TabPage tabPage7;

	private GroupBox attributeElasticityPerGroupGroupbox;

	private GroupBox attributeElasticityPerCompanyGroupbox;

	private GroupBox attributeElasticityPerSKUGroupbox;

	private GroupBox groupBox4;

	private CheckBox elasticityPerCompanyGroupCheckbox;

	private CheckBox elasticityPerGroupCheckbox;

	private CheckBox elasticityPerSKUCheckbox;

	private Label label3;

	private ComboBox elasticityAttributeNamePerCompanyCombobox;

	private Label label7;

	private ComboBox elasticityAttributeNamePerSKUCombobox;

	private Label label5;

	private ComboBox elasticityAttributeNamePerGroupCombobox;

	private Label label6;

	private Label label10;

	private Label label8;

	private Label label11;

	private ComboBox elasticityGroupTypeCombobox;

	private Label label9;

	private Label label12;

	private Chart chart1;

	private Chart chart3;

	private Chart chart2;

	private Chart chart4;

	private GroupBox groupBox5;

	private CheckBox priceIndexTotalCompanyRevenue;

	private CheckBox priceIndexTotalCompanyShare;

	private ComboBox priceIndexGroupTypeCombobox;

	private Label label13;

	private GroupBox groupBox6;

	private Label label14;

	private ComboBox priceIndexGroupNameCombobox;

	private Label label15;

	private GroupBox groupBox8;

	private Button priceIndexRandomizeButton;

	private TextBox priceIndexScaleTextbox;

	private Label label18;

	private Label priceIndexScaleLabel;

	private GroupBox groupBox7;

	private Label label16;

	private ComboBox priceIndexReferenceGroupNameCombobox;

	private Label label17;

	private Chart chart5;

	private Label label21;

	private GroupBox groupBox9;

	private Button brandInteractionConfigSKUButton;

	private ComboBox brandInteractionGroupTypeCombobox;

	private Label label22;

	private Label label23;

	private RadioButton brandInteractionSKURadiobox;

	private RadioButton brandInteractionGroupRadiobox;

	private GroupBox groupBox10;

	private Label label35;

	private Button elasticityDonePerCompanyButton;

	private Button elasticityDonePerSKUButton;

	private Button elasticityDonePerGroupButton;

	private Button priceIndexDoneButton;

	private Button brandInteractionDoneButton;

	private GroupBox groupBox11;

	private Button somsopDoneButton;

	private Label label36;

	private Button fairShareDoneButton;

	private Chart chart8;

	private Chart chart9;

	private GroupBox groupBox19;

	private Label label28;

	private RadioButton somsopSKURadiobox;

	private RadioButton somsopGroupRadiobox;

	private Button somsopConfigSKUButton;

	private ComboBox somsopGroupTypeCombobox;

	private Label label29;

	private GroupBox groupBox14;

	private Label label32;

	private RadioButton fairShareSKURadiobox;

	private RadioButton fairShareGroupRadiobox;

	private Button fairShareConfigSKUButton;

	private ComboBox fairShareGroupTypeCombobox;

	private Label label33;

	private GroupBox groupBox16;

	private Label label34;

	private RadioButton validationSKURadiobox;

	private RadioButton validationGroupRadiobox;

	private ComboBox validationGroupTypeCombobox;

	private Label label37;

	private Label label25;

	private RadioButton priceIndexSKUsRadiobox;

	private RadioButton priceIndexGroupsRadiobox;

	private Button validationConfigScenarioButton;

	private CheckBoxComboBox elasticityCompanySeriesCombobox;

	private CheckBoxComboBox elasticitySKUsCombobox;

	private CheckBoxComboBox elasticityGroupsCombobox;

	private CheckBoxComboBox priceIndexSeriesCombobox;

	private ComboBox priceIndexReferenceSKUNameCombobox;

	private ComboBox priceIndexSKUNameCombobox;

	private CheckBoxComboBox brandInteractionSeriesCombobox;

	private CheckBoxComboBox somsopSeriesCombobox;

	private CheckBoxComboBox fairShareSeriesCombobox;

	private GroupBox groupBox1;

	private CheckBoxComboBox validationSeriesCombobox;

	private Button validationDoneButton;

	private Label label26;

	private Chart chart6;

	private ComboBox fairSharAttributeCombobox;

	private Label label19;

	private TextBox textBox1;

	private TextBox textBox2;

	private TextBox textBox3;

	private TextBox textBox4;

	private TextBox textBox6;

	private TextBox textBox7;

	private TextBox textBox8;

	private TabPage tabPage8;

	private GroupBox groupBox2;

	private CheckBoxComboBox sharesSenariosCombobox;

	private Button sharesDoneButton;

	private Label label24;

	private GroupBox groupBox3;

	private Button sharesConfigSKUButton;

	private Label label27;

	private RadioButton shareSKURadiobox;

	private RadioButton sharesGroupRadiobox;

	private ComboBox sharesGroupTypeCombobox;

	private Label label38;

	private Chart chart10;

	private TextBox textBox10;

	private CheckBox sharesRevenueCheckbox;

	private CheckBox sharesVolumeShareCheckbox;

	private CheckBoxComboBox sharesSeriesCombobox;

	private Label label40;

	private TextBox priceIndexNameTextbox;

	private Label label4;

	private TextBox sharesNameTextBox;

	private Label label30;

	private TextBox brandIneractionNameTextbox;

	private Label label31;

	private TextBox somsopNameTextbox;

	private Label label39;

	private TextBox fairShareTextbox;

	private Label label41;

	private CheckBoxComboBox validationScenarioCheckboxCombobox;

	private Label label44;

	private TabPage tabPage5;

	private TabPage tabPage9;

	private TextBox textBox5;

	private GroupBox groupBox12;

	private TextBox incrementalGainNameTextbox;

	private Label label45;

	private Button incrementalGainConfigureSKUButton;

	private Chart chart7;

	private Button incrementalGainIncrementalConfigurationButton;

	private GroupBox groupBox13;

	private Button incrementalGainRunButton;

	private CheckBox incrementalGainRevenuesCheckbox;

	private CheckBox incrementalGainSharesCheckbox;

	private Button incrementalLossRunButton;

	private TextBox textBox12;

	private GroupBox groupBox18;

	private Button incrementalLossIncrementalConfigurationButton;

	private TextBox incrementalLossNameTextbox;

	private Label label53;

	private Button incrementalLossConfigureSKUButton;

	private Chart chart11;

	private Button button1;

	private BackgroundWorker initializeChartsProjectBackgoundWorker;

	private GroupBox groupBox20;

	private RadioButton exclusiveIncrementalGainRadiobutton;

	private RadioButton cumulativeIncrementalGainRadiobutton;

	private GroupBox groupBox17;

	private RadioButton exclusiveIncrementalLossRadiobutton;

	private RadioButton cumulativeIncrementalLossRadiobutton;

	private GroupBox groupBox21;

	private CheckBox incrementalLossRevenuesCheckbox;

	private CheckBox incrementalLossSharesCheckbox;

	private GroupBox groupBox22;

	private RadioButton validationByMonthRadioButton;

	private RadioButton validationByBrandRadiobutton;

	private ComboBox brandInteractionBasicScenarioCombobox;

	private Label label43;

	private CheckBox elasticitySegmentationCheckbox;

	private ComboBox elasticitySegmentationTypeCombobox;

	private CheckBoxComboBox brandInteractionScenariosCombobox;

	private Label label46;

	private ContextMenuStrip scenariosMenuStrip;

	private Button somsopDropdownButton;

	private Label somsopScenariosLabel;

	private Button incrementalGainDropdownButton;

	private Label incrementalGainScenarioLabel;

	private Button IncrementalLossDropdownButton;

	private Label incrementalLossScenarioLabel;

	private CheckBox elasticityScenarioCheckbox;

	private CheckBox sharesRevenueIndexCheckbox;

	private CheckBox sharesVolumeShareIndexCheckbox;

	private CheckBoxComboBox elasticityScenarioCombobox;

	private CheckBoxComboBox elasticitySegmentMembersCombobox;

	private TextBox textBox9;

	private Button priceIndexScenariosDropdownmenu;

	private Label priceIndexScenarioLabel;

	private Button priceIndexConfigButton;

	private ComboBox fairshareChangeCombobox;

	private Label label20;

	private ComboBox elasticityGroupTypePerCompanyCombobox;

	private Label label47;

	private CheckBox elasticityPerCompanySKUCheckbox;

	private GroupBox groupBox23;

	private Label label48;

	private Label label49;

	private ComboBox fairShareBasicScenarioCombobox;

	private Label label50;

	private ComboBox fairShareScenarioCombobox;

	private Label wdCautionMessageLabel;

	private CheckBox sharesWeightedSOPIndexCheckbox;

	private CheckBox sharesWeightedSOPCheckbox;

	private CheckBox sharesValueShareIndexCheckbox;

	private CheckBox sharesValueShareCheckbox;

	private ProjectCore project;

	private DataSet dsCharts = new DataSet();

	private Dictionary<string, string> studyScenariosLibrary = new Dictionary<string, string>();

	private Dictionary<string, string> validationScenarios = new Dictionary<string, string>();

	private string scenariosPath = "";

	private bool checkAllItemsCombobox = true;

	private List<char> specialCharacter = new List<char>();

	private DataTable dtIncrementalGain = new DataTable();

	private DataTable dtIncrementalLoss = new DataTable();

	private string incrementalGainGroupType = "";

	private string incrementalGainGroupMember = "";

	private string incrementalLossGroupType = "";

	private string incrementalLossGroupMember = "";

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
		System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
		System.Windows.Forms.DataVisualization.Charting.Series series = new System.Windows.Forms.DataVisualization.Charting.Series();
		System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0, 0.0);
		System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint2 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0, 0.0);
		System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint3 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0, 0.0);
		System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint4 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0, 0.0);
		System.Windows.Forms.DataVisualization.Charting.Title title = new System.Windows.Forms.DataVisualization.Charting.Title();
		System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
		System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
		System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint5 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0, 0.0);
		System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint6 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0, 0.0);
		System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint7 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0, 0.0);
		System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint8 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0, 0.0);
		System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
		System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
		System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
		System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint9 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0, 0.0);
		System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint10 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0, 0.0);
		System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint11 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0, 0.0);
		System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint12 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0, 0.0);
		System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
		PresentationControls.CheckBoxProperties checkBoxProperties = new PresentationControls.CheckBoxProperties();
		PresentationControls.CheckBoxProperties checkBoxProperties2 = new PresentationControls.CheckBoxProperties();
		PresentationControls.CheckBoxProperties checkBoxProperties3 = new PresentationControls.CheckBoxProperties();
		PresentationControls.CheckBoxProperties checkBoxProperties4 = new PresentationControls.CheckBoxProperties();
		PresentationControls.CheckBoxProperties checkBoxProperties5 = new PresentationControls.CheckBoxProperties();
		PresentationControls.CheckBoxProperties checkBoxProperties6 = new PresentationControls.CheckBoxProperties();
		System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
		System.Windows.Forms.DataVisualization.Charting.Legend legend = new System.Windows.Forms.DataVisualization.Charting.Legend();
		System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
		System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint13 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0, 0.0);
		System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint14 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0, 0.0);
		System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint15 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0, 0.0);
		System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint16 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0, 0.0);
		System.Windows.Forms.DataVisualization.Charting.Title title4 = new System.Windows.Forms.DataVisualization.Charting.Title();
		PresentationControls.CheckBoxProperties checkBoxProperties7 = new PresentationControls.CheckBoxProperties();
		PresentationControls.CheckBoxProperties checkBoxProperties8 = new PresentationControls.CheckBoxProperties();
		System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
		System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
		System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
		System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint17 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0, "0,0,0");
		System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint18 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0, "0,0,0");
		System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint19 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0, "0,0,0");
		System.Windows.Forms.DataVisualization.Charting.Title title5 = new System.Windows.Forms.DataVisualization.Charting.Title();
		PresentationControls.CheckBoxProperties checkBoxProperties9 = new PresentationControls.CheckBoxProperties();
		PresentationControls.CheckBoxProperties checkBoxProperties10 = new PresentationControls.CheckBoxProperties();
		System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
		System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
		System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
		System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint20 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0, 0.0);
		System.Windows.Forms.DataVisualization.Charting.Title title6 = new System.Windows.Forms.DataVisualization.Charting.Title();
		System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
		System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
		System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
		System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint21 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0, 0.0);
		System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint22 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0, 0.0);
		System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint23 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0, 0.0);
		System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint24 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0, 0.0);
		System.Windows.Forms.DataVisualization.Charting.Title title7 = new System.Windows.Forms.DataVisualization.Charting.Title();
		PresentationControls.CheckBoxProperties checkBoxProperties11 = new PresentationControls.CheckBoxProperties();
		PresentationControls.CheckBoxProperties checkBoxProperties12 = new PresentationControls.CheckBoxProperties();
		System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea8 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
		System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
		System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
		System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint25 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0, 0.0);
		System.Windows.Forms.DataVisualization.Charting.Title title8 = new System.Windows.Forms.DataVisualization.Charting.Title();
		PresentationControls.CheckBoxProperties checkBoxProperties13 = new PresentationControls.CheckBoxProperties();
		PresentationControls.CheckBoxProperties checkBoxProperties14 = new PresentationControls.CheckBoxProperties();
		System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea9 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
		System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
		System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
		System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint26 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0, "0,0,0");
		System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint27 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0, "0,0,0");
		System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint28 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0, "0,0,0");
		System.Windows.Forms.DataVisualization.Charting.Title title9 = new System.Windows.Forms.DataVisualization.Charting.Title();
		System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea10 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
		System.Windows.Forms.DataVisualization.Charting.Legend legend7 = new System.Windows.Forms.DataVisualization.Charting.Legend();
		System.Windows.Forms.DataVisualization.Charting.Series series10 = new System.Windows.Forms.DataVisualization.Charting.Series();
		System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint29 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0, "0,0,0");
		System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint30 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0, "0,0,0");
		System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint31 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0, "0,0,0");
		System.Windows.Forms.DataVisualization.Charting.Title title10 = new System.Windows.Forms.DataVisualization.Charting.Title();
		System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea11 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
		System.Windows.Forms.DataVisualization.Charting.Legend legend8 = new System.Windows.Forms.DataVisualization.Charting.Legend();
		System.Windows.Forms.DataVisualization.Charting.Series series11 = new System.Windows.Forms.DataVisualization.Charting.Series();
		System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint32 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0, "0,0,0");
		System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint33 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0, "0,0,0");
		System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint34 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0, "0,0,0");
		System.Windows.Forms.DataVisualization.Charting.Title title11 = new System.Windows.Forms.DataVisualization.Charting.Title();
		this.label1 = new System.Windows.Forms.Label();
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.chart3 = new System.Windows.Forms.DataVisualization.Charting.Chart();
		this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
		this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
		this.label12 = new System.Windows.Forms.Label();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.elasticityPerCompanySKUCheckbox = new System.Windows.Forms.CheckBox();
		this.elasticityScenarioCombobox = new PresentationControls.CheckBoxComboBox();
		this.elasticityScenarioCheckbox = new System.Windows.Forms.CheckBox();
		this.elasticitySegmentationTypeCombobox = new System.Windows.Forms.ComboBox();
		this.elasticitySegmentationCheckbox = new System.Windows.Forms.CheckBox();
		this.elasticityPerCompanyGroupCheckbox = new System.Windows.Forms.CheckBox();
		this.elasticityPerGroupCheckbox = new System.Windows.Forms.CheckBox();
		this.elasticityPerSKUCheckbox = new System.Windows.Forms.CheckBox();
		this.label3 = new System.Windows.Forms.Label();
		this.elasticitySegmentMembersCombobox = new PresentationControls.CheckBoxComboBox();
		this.attributeElasticityPerCompanyGroupbox = new System.Windows.Forms.GroupBox();
		this.elasticityGroupTypePerCompanyCombobox = new System.Windows.Forms.ComboBox();
		this.label47 = new System.Windows.Forms.Label();
		this.elasticityCompanySeriesCombobox = new PresentationControls.CheckBoxComboBox();
		this.elasticityDonePerCompanyButton = new System.Windows.Forms.Button();
		this.label10 = new System.Windows.Forms.Label();
		this.elasticityAttributeNamePerCompanyCombobox = new System.Windows.Forms.ComboBox();
		this.label7 = new System.Windows.Forms.Label();
		this.attributeElasticityPerSKUGroupbox = new System.Windows.Forms.GroupBox();
		this.elasticityDonePerSKUButton = new System.Windows.Forms.Button();
		this.elasticitySKUsCombobox = new PresentationControls.CheckBoxComboBox();
		this.label8 = new System.Windows.Forms.Label();
		this.elasticityAttributeNamePerSKUCombobox = new System.Windows.Forms.ComboBox();
		this.label5 = new System.Windows.Forms.Label();
		this.attributeElasticityPerGroupGroupbox = new System.Windows.Forms.GroupBox();
		this.elasticityGroupsCombobox = new PresentationControls.CheckBoxComboBox();
		this.elasticityDonePerGroupButton = new System.Windows.Forms.Button();
		this.label11 = new System.Windows.Forms.Label();
		this.elasticityGroupTypeCombobox = new System.Windows.Forms.ComboBox();
		this.label9 = new System.Windows.Forms.Label();
		this.elasticityAttributeNamePerGroupCombobox = new System.Windows.Forms.ComboBox();
		this.label6 = new System.Windows.Forms.Label();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.textBox2 = new System.Windows.Forms.TextBox();
		this.groupBox8 = new System.Windows.Forms.GroupBox();
		this.priceIndexSeriesCombobox = new PresentationControls.CheckBoxComboBox();
		this.priceIndexDoneButton = new System.Windows.Forms.Button();
		this.label21 = new System.Windows.Forms.Label();
		this.priceIndexRandomizeButton = new System.Windows.Forms.Button();
		this.priceIndexScaleTextbox = new System.Windows.Forms.TextBox();
		this.label18 = new System.Windows.Forms.Label();
		this.priceIndexScaleLabel = new System.Windows.Forms.Label();
		this.groupBox7 = new System.Windows.Forms.GroupBox();
		this.priceIndexReferenceSKUNameCombobox = new System.Windows.Forms.ComboBox();
		this.label16 = new System.Windows.Forms.Label();
		this.priceIndexReferenceGroupNameCombobox = new System.Windows.Forms.ComboBox();
		this.label17 = new System.Windows.Forms.Label();
		this.groupBox6 = new System.Windows.Forms.GroupBox();
		this.priceIndexSKUNameCombobox = new System.Windows.Forms.ComboBox();
		this.label14 = new System.Windows.Forms.Label();
		this.priceIndexGroupNameCombobox = new System.Windows.Forms.ComboBox();
		this.label15 = new System.Windows.Forms.Label();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.priceIndexScenariosDropdownmenu = new System.Windows.Forms.Button();
		this.priceIndexScenarioLabel = new System.Windows.Forms.Label();
		this.priceIndexConfigButton = new System.Windows.Forms.Button();
		this.priceIndexNameTextbox = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label25 = new System.Windows.Forms.Label();
		this.priceIndexSKUsRadiobox = new System.Windows.Forms.RadioButton();
		this.priceIndexGroupsRadiobox = new System.Windows.Forms.RadioButton();
		this.priceIndexTotalCompanyRevenue = new System.Windows.Forms.CheckBox();
		this.priceIndexTotalCompanyShare = new System.Windows.Forms.CheckBox();
		this.priceIndexGroupTypeCombobox = new System.Windows.Forms.ComboBox();
		this.label13 = new System.Windows.Forms.Label();
		this.chart4 = new System.Windows.Forms.DataVisualization.Charting.Chart();
		this.tabPage8 = new System.Windows.Forms.TabPage();
		this.textBox10 = new System.Windows.Forms.TextBox();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.sharesRevenueIndexCheckbox = new System.Windows.Forms.CheckBox();
		this.sharesVolumeShareIndexCheckbox = new System.Windows.Forms.CheckBox();
		this.sharesSeriesCombobox = new PresentationControls.CheckBoxComboBox();
		this.label40 = new System.Windows.Forms.Label();
		this.sharesRevenueCheckbox = new System.Windows.Forms.CheckBox();
		this.sharesVolumeShareCheckbox = new System.Windows.Forms.CheckBox();
		this.sharesSenariosCombobox = new PresentationControls.CheckBoxComboBox();
		this.sharesDoneButton = new System.Windows.Forms.Button();
		this.label24 = new System.Windows.Forms.Label();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.sharesNameTextBox = new System.Windows.Forms.TextBox();
		this.label30 = new System.Windows.Forms.Label();
		this.sharesConfigSKUButton = new System.Windows.Forms.Button();
		this.label27 = new System.Windows.Forms.Label();
		this.shareSKURadiobox = new System.Windows.Forms.RadioButton();
		this.sharesGroupRadiobox = new System.Windows.Forms.RadioButton();
		this.sharesGroupTypeCombobox = new System.Windows.Forms.ComboBox();
		this.label38 = new System.Windows.Forms.Label();
		this.chart10 = new System.Windows.Forms.DataVisualization.Charting.Chart();
		this.tabPage3 = new System.Windows.Forms.TabPage();
		this.textBox3 = new System.Windows.Forms.TextBox();
		this.groupBox10 = new System.Windows.Forms.GroupBox();
		this.brandInteractionScenariosCombobox = new PresentationControls.CheckBoxComboBox();
		this.label46 = new System.Windows.Forms.Label();
		this.label43 = new System.Windows.Forms.Label();
		this.brandInteractionBasicScenarioCombobox = new System.Windows.Forms.ComboBox();
		this.brandInteractionSeriesCombobox = new PresentationControls.CheckBoxComboBox();
		this.brandInteractionDoneButton = new System.Windows.Forms.Button();
		this.label35 = new System.Windows.Forms.Label();
		this.groupBox9 = new System.Windows.Forms.GroupBox();
		this.brandIneractionNameTextbox = new System.Windows.Forms.TextBox();
		this.label31 = new System.Windows.Forms.Label();
		this.label23 = new System.Windows.Forms.Label();
		this.brandInteractionSKURadiobox = new System.Windows.Forms.RadioButton();
		this.brandInteractionGroupRadiobox = new System.Windows.Forms.RadioButton();
		this.brandInteractionConfigSKUButton = new System.Windows.Forms.Button();
		this.brandInteractionGroupTypeCombobox = new System.Windows.Forms.ComboBox();
		this.label22 = new System.Windows.Forms.Label();
		this.chart5 = new System.Windows.Forms.DataVisualization.Charting.Chart();
		this.tabPage4 = new System.Windows.Forms.TabPage();
		this.textBox4 = new System.Windows.Forms.TextBox();
		this.chart6 = new System.Windows.Forms.DataVisualization.Charting.Chart();
		this.groupBox19 = new System.Windows.Forms.GroupBox();
		this.wdCautionMessageLabel = new System.Windows.Forms.Label();
		this.somsopDropdownButton = new System.Windows.Forms.Button();
		this.somsopNameTextbox = new System.Windows.Forms.TextBox();
		this.label39 = new System.Windows.Forms.Label();
		this.somsopScenariosLabel = new System.Windows.Forms.Label();
		this.label28 = new System.Windows.Forms.Label();
		this.somsopSKURadiobox = new System.Windows.Forms.RadioButton();
		this.somsopGroupRadiobox = new System.Windows.Forms.RadioButton();
		this.somsopConfigSKUButton = new System.Windows.Forms.Button();
		this.somsopGroupTypeCombobox = new System.Windows.Forms.ComboBox();
		this.label29 = new System.Windows.Forms.Label();
		this.groupBox11 = new System.Windows.Forms.GroupBox();
		this.somsopSeriesCombobox = new PresentationControls.CheckBoxComboBox();
		this.somsopDoneButton = new System.Windows.Forms.Button();
		this.label36 = new System.Windows.Forms.Label();
		this.tabPage6 = new System.Windows.Forms.TabPage();
		this.groupBox23 = new System.Windows.Forms.GroupBox();
		this.fairShareScenarioCombobox = new System.Windows.Forms.ComboBox();
		this.fairShareDoneButton = new System.Windows.Forms.Button();
		this.fairShareSeriesCombobox = new PresentationControls.CheckBoxComboBox();
		this.label48 = new System.Windows.Forms.Label();
		this.label49 = new System.Windows.Forms.Label();
		this.fairShareBasicScenarioCombobox = new System.Windows.Forms.ComboBox();
		this.label50 = new System.Windows.Forms.Label();
		this.textBox6 = new System.Windows.Forms.TextBox();
		this.groupBox14 = new System.Windows.Forms.GroupBox();
		this.fairShareTextbox = new System.Windows.Forms.TextBox();
		this.label41 = new System.Windows.Forms.Label();
		this.fairshareChangeCombobox = new System.Windows.Forms.ComboBox();
		this.label20 = new System.Windows.Forms.Label();
		this.fairSharAttributeCombobox = new System.Windows.Forms.ComboBox();
		this.label19 = new System.Windows.Forms.Label();
		this.label32 = new System.Windows.Forms.Label();
		this.fairShareSKURadiobox = new System.Windows.Forms.RadioButton();
		this.fairShareGroupRadiobox = new System.Windows.Forms.RadioButton();
		this.fairShareConfigSKUButton = new System.Windows.Forms.Button();
		this.fairShareGroupTypeCombobox = new System.Windows.Forms.ComboBox();
		this.label33 = new System.Windows.Forms.Label();
		this.chart8 = new System.Windows.Forms.DataVisualization.Charting.Chart();
		this.tabPage7 = new System.Windows.Forms.TabPage();
		this.groupBox22 = new System.Windows.Forms.GroupBox();
		this.validationByMonthRadioButton = new System.Windows.Forms.RadioButton();
		this.validationByBrandRadiobutton = new System.Windows.Forms.RadioButton();
		this.textBox7 = new System.Windows.Forms.TextBox();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.validationScenarioCheckboxCombobox = new PresentationControls.CheckBoxComboBox();
		this.label44 = new System.Windows.Forms.Label();
		this.validationSeriesCombobox = new PresentationControls.CheckBoxComboBox();
		this.validationDoneButton = new System.Windows.Forms.Button();
		this.label26 = new System.Windows.Forms.Label();
		this.groupBox16 = new System.Windows.Forms.GroupBox();
		this.validationConfigScenarioButton = new System.Windows.Forms.Button();
		this.label34 = new System.Windows.Forms.Label();
		this.validationSKURadiobox = new System.Windows.Forms.RadioButton();
		this.validationGroupRadiobox = new System.Windows.Forms.RadioButton();
		this.validationGroupTypeCombobox = new System.Windows.Forms.ComboBox();
		this.label37 = new System.Windows.Forms.Label();
		this.chart9 = new System.Windows.Forms.DataVisualization.Charting.Chart();
		this.tabPage5 = new System.Windows.Forms.TabPage();
		this.groupBox20 = new System.Windows.Forms.GroupBox();
		this.exclusiveIncrementalGainRadiobutton = new System.Windows.Forms.RadioButton();
		this.cumulativeIncrementalGainRadiobutton = new System.Windows.Forms.RadioButton();
		this.groupBox13 = new System.Windows.Forms.GroupBox();
		this.incrementalGainRevenuesCheckbox = new System.Windows.Forms.CheckBox();
		this.incrementalGainSharesCheckbox = new System.Windows.Forms.CheckBox();
		this.textBox5 = new System.Windows.Forms.TextBox();
		this.groupBox12 = new System.Windows.Forms.GroupBox();
		this.incrementalGainDropdownButton = new System.Windows.Forms.Button();
		this.incrementalGainIncrementalConfigurationButton = new System.Windows.Forms.Button();
		this.incrementalGainScenarioLabel = new System.Windows.Forms.Label();
		this.incrementalGainNameTextbox = new System.Windows.Forms.TextBox();
		this.label45 = new System.Windows.Forms.Label();
		this.incrementalGainConfigureSKUButton = new System.Windows.Forms.Button();
		this.incrementalGainRunButton = new System.Windows.Forms.Button();
		this.chart7 = new System.Windows.Forms.DataVisualization.Charting.Chart();
		this.tabPage9 = new System.Windows.Forms.TabPage();
		this.groupBox17 = new System.Windows.Forms.GroupBox();
		this.exclusiveIncrementalLossRadiobutton = new System.Windows.Forms.RadioButton();
		this.cumulativeIncrementalLossRadiobutton = new System.Windows.Forms.RadioButton();
		this.groupBox21 = new System.Windows.Forms.GroupBox();
		this.incrementalLossRevenuesCheckbox = new System.Windows.Forms.CheckBox();
		this.incrementalLossSharesCheckbox = new System.Windows.Forms.CheckBox();
		this.incrementalLossRunButton = new System.Windows.Forms.Button();
		this.textBox12 = new System.Windows.Forms.TextBox();
		this.groupBox18 = new System.Windows.Forms.GroupBox();
		this.IncrementalLossDropdownButton = new System.Windows.Forms.Button();
		this.incrementalLossIncrementalConfigurationButton = new System.Windows.Forms.Button();
		this.incrementalLossScenarioLabel = new System.Windows.Forms.Label();
		this.incrementalLossNameTextbox = new System.Windows.Forms.TextBox();
		this.label53 = new System.Windows.Forms.Label();
		this.incrementalLossConfigureSKUButton = new System.Windows.Forms.Button();
		this.chart11 = new System.Windows.Forms.DataVisualization.Charting.Chart();
		this.label2 = new System.Windows.Forms.Label();
		this.button2 = new System.Windows.Forms.Button();
		this.button3 = new System.Windows.Forms.Button();
		this.textBox8 = new System.Windows.Forms.TextBox();
		this.button1 = new System.Windows.Forms.Button();
		this.initializeChartsProjectBackgoundWorker = new System.ComponentModel.BackgroundWorker();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.scenariosMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.textBox9 = new System.Windows.Forms.TextBox();
		this.sharesValueShareIndexCheckbox = new System.Windows.Forms.CheckBox();
		this.sharesValueShareCheckbox = new System.Windows.Forms.CheckBox();
		this.sharesWeightedSOPIndexCheckbox = new System.Windows.Forms.CheckBox();
		this.sharesWeightedSOPCheckbox = new System.Windows.Forms.CheckBox();
		this.tabControl1.SuspendLayout();
		this.tabPage1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.chart3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chart2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chart1).BeginInit();
		this.groupBox4.SuspendLayout();
		this.attributeElasticityPerCompanyGroupbox.SuspendLayout();
		this.attributeElasticityPerSKUGroupbox.SuspendLayout();
		this.attributeElasticityPerGroupGroupbox.SuspendLayout();
		this.tabPage2.SuspendLayout();
		this.groupBox8.SuspendLayout();
		this.groupBox7.SuspendLayout();
		this.groupBox6.SuspendLayout();
		this.groupBox5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.chart4).BeginInit();
		this.tabPage8.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.chart10).BeginInit();
		this.tabPage3.SuspendLayout();
		this.groupBox10.SuspendLayout();
		this.groupBox9.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.chart5).BeginInit();
		this.tabPage4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.chart6).BeginInit();
		this.groupBox19.SuspendLayout();
		this.groupBox11.SuspendLayout();
		this.tabPage6.SuspendLayout();
		this.groupBox23.SuspendLayout();
		this.groupBox14.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.chart8).BeginInit();
		this.tabPage7.SuspendLayout();
		this.groupBox22.SuspendLayout();
		this.groupBox1.SuspendLayout();
		this.groupBox16.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.chart9).BeginInit();
		this.tabPage5.SuspendLayout();
		this.groupBox20.SuspendLayout();
		this.groupBox13.SuspendLayout();
		this.groupBox12.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.chart7).BeginInit();
		this.tabPage9.SuspendLayout();
		this.groupBox17.SuspendLayout();
		this.groupBox21.SuspendLayout();
		this.groupBox18.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.chart11).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Georgia", 21.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.SteelBlue;
		this.label1.Location = new System.Drawing.Point(5, 9);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(417, 34);
		this.label1.TabIndex = 1;
		this.label1.Text = "Marketeers Charts Automation";
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Controls.Add(this.tabPage8);
		this.tabControl1.Controls.Add(this.tabPage3);
		this.tabControl1.Controls.Add(this.tabPage4);
		this.tabControl1.Controls.Add(this.tabPage6);
		this.tabControl1.Controls.Add(this.tabPage7);
		this.tabControl1.Controls.Add(this.tabPage5);
		this.tabControl1.Controls.Add(this.tabPage9);
		this.tabControl1.Location = new System.Drawing.Point(11, 90);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(792, 448);
		this.tabControl1.TabIndex = 2;
		this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(tabControl1_Selecting);
		this.tabPage1.BackColor = System.Drawing.Color.White;
		this.tabPage1.Controls.Add(this.textBox1);
		this.tabPage1.Controls.Add(this.chart3);
		this.tabPage1.Controls.Add(this.chart2);
		this.tabPage1.Controls.Add(this.chart1);
		this.tabPage1.Controls.Add(this.label12);
		this.tabPage1.Controls.Add(this.groupBox4);
		this.tabPage1.Controls.Add(this.attributeElasticityPerCompanyGroupbox);
		this.tabPage1.Controls.Add(this.attributeElasticityPerSKUGroupbox);
		this.tabPage1.Controls.Add(this.attributeElasticityPerGroupGroupbox);
		this.tabPage1.Location = new System.Drawing.Point(4, 22);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(784, 422);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "Elasticity";
		this.textBox1.BackColor = System.Drawing.Color.Gray;
		this.textBox1.Enabled = false;
		this.textBox1.Location = new System.Drawing.Point(58, 221);
		this.textBox1.Multiline = true;
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(720, 1);
		this.textBox1.TabIndex = 7;
		chartArea.AxisX.LabelStyle.ForeColor = System.Drawing.Color.DimGray;
		chartArea.AxisX.LineColor = System.Drawing.Color.DimGray;
		chartArea.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
		chartArea.AxisY.LabelStyle.ForeColor = System.Drawing.Color.DimGray;
		chartArea.AxisY.LineColor = System.Drawing.Color.DimGray;
		chartArea.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
		chartArea.Name = "ChartArea1";
		this.chart3.ChartAreas.Add(chartArea);
		this.chart3.Enabled = false;
		this.chart3.Location = new System.Drawing.Point(524, 225);
		this.chart3.Name = "chart3";
		series.ChartArea = "ChartArea1";
		series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
		series.LabelForeColor = System.Drawing.Color.DarkGray;
		series.Legend = "Legend1";
		series.Name = "Series1";
		dataPoint.IsEmpty = true;
		dataPoint2.IsEmpty = true;
		dataPoint3.IsEmpty = true;
		dataPoint4.IsEmpty = true;
		series.Points.Add(dataPoint);
		series.Points.Add(dataPoint2);
		series.Points.Add(dataPoint3);
		series.Points.Add(dataPoint4);
		series.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
		series.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
		this.chart3.Series.Add(series);
		this.chart3.Size = new System.Drawing.Size(248, 191);
		this.chart3.TabIndex = 6;
		this.chart3.Text = "chart3";
		title.DockedToChartArea = "ChartArea1";
		title.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Right;
		title.ForeColor = System.Drawing.Color.SteelBlue;
		title.Name = "Title1";
		title.Text = "Elasticity Per Company";
		this.chart3.Titles.Add(title);
		chartArea2.AxisX.LabelStyle.ForeColor = System.Drawing.Color.DimGray;
		chartArea2.AxisX.LineColor = System.Drawing.Color.DimGray;
		chartArea2.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
		chartArea2.AxisY.LabelStyle.ForeColor = System.Drawing.Color.DimGray;
		chartArea2.AxisY.LineColor = System.Drawing.Color.DimGray;
		chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
		chartArea2.Name = "ChartArea1";
		this.chart2.ChartAreas.Add(chartArea2);
		this.chart2.Enabled = false;
		this.chart2.Location = new System.Drawing.Point(268, 225);
		this.chart2.Name = "chart2";
		series2.ChartArea = "ChartArea1";
		series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
		series2.LabelForeColor = System.Drawing.Color.DarkGray;
		series2.Legend = "Legend1";
		series2.Name = "Series1";
		dataPoint5.IsEmpty = true;
		dataPoint6.IsEmpty = true;
		dataPoint7.IsEmpty = true;
		dataPoint8.IsEmpty = true;
		series2.Points.Add(dataPoint5);
		series2.Points.Add(dataPoint6);
		series2.Points.Add(dataPoint7);
		series2.Points.Add(dataPoint8);
		this.chart2.Series.Add(series2);
		this.chart2.Size = new System.Drawing.Size(248, 191);
		this.chart2.TabIndex = 5;
		this.chart2.Text = "chart2";
		title2.DockedToChartArea = "ChartArea1";
		title2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Right;
		title2.ForeColor = System.Drawing.Color.SteelBlue;
		title2.Name = "Title1";
		title2.Text = "Elasticity Per Group";
		this.chart2.Titles.Add(title2);
		chartArea3.AxisX.LabelStyle.ForeColor = System.Drawing.Color.DimGray;
		chartArea3.AxisX.LineColor = System.Drawing.Color.DimGray;
		chartArea3.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
		chartArea3.AxisY.LabelStyle.ForeColor = System.Drawing.Color.DimGray;
		chartArea3.AxisY.LineColor = System.Drawing.Color.DimGray;
		chartArea3.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
		chartArea3.Name = "ChartArea1";
		this.chart1.ChartAreas.Add(chartArea3);
		this.chart1.Enabled = false;
		this.chart1.Location = new System.Drawing.Point(14, 225);
		this.chart1.Name = "chart1";
		series3.ChartArea = "ChartArea1";
		series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
		series3.LabelForeColor = System.Drawing.Color.DarkGray;
		series3.Legend = "Legend1";
		series3.Name = "Series1";
		dataPoint9.IsEmpty = true;
		dataPoint10.IsEmpty = true;
		dataPoint11.IsEmpty = true;
		dataPoint12.IsEmpty = true;
		series3.Points.Add(dataPoint9);
		series3.Points.Add(dataPoint10);
		series3.Points.Add(dataPoint11);
		series3.Points.Add(dataPoint12);
		this.chart1.Series.Add(series3);
		this.chart1.Size = new System.Drawing.Size(248, 191);
		this.chart1.TabIndex = 4;
		title3.DockedToChartArea = "ChartArea1";
		title3.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Right;
		title3.ForeColor = System.Drawing.Color.SteelBlue;
		title3.Name = "Title1";
		title3.Text = "Elasticity Per SKU";
		this.chart1.Titles.Add(title3);
		this.label12.AutoSize = true;
		this.label12.ForeColor = System.Drawing.Color.Blue;
		this.label12.Location = new System.Drawing.Point(14, 214);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(45, 13);
		this.label12.TabIndex = 3;
		this.label12.Text = "Preview";
		this.groupBox4.Controls.Add(this.elasticityPerCompanySKUCheckbox);
		this.groupBox4.Controls.Add(this.elasticityScenarioCombobox);
		this.groupBox4.Controls.Add(this.elasticityScenarioCheckbox);
		this.groupBox4.Controls.Add(this.elasticitySegmentationTypeCombobox);
		this.groupBox4.Controls.Add(this.elasticitySegmentationCheckbox);
		this.groupBox4.Controls.Add(this.elasticityPerCompanyGroupCheckbox);
		this.groupBox4.Controls.Add(this.elasticityPerGroupCheckbox);
		this.groupBox4.Controls.Add(this.elasticityPerSKUCheckbox);
		this.groupBox4.Controls.Add(this.label3);
		this.groupBox4.Controls.Add(this.elasticitySegmentMembersCombobox);
		this.groupBox4.Location = new System.Drawing.Point(14, 10);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(758, 93);
		this.groupBox4.TabIndex = 2;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "Elasticity Global Configration";
		this.elasticityPerCompanySKUCheckbox.AutoSize = true;
		this.elasticityPerCompanySKUCheckbox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.elasticityPerCompanySKUCheckbox.Location = new System.Drawing.Point(621, 16);
		this.elasticityPerCompanySKUCheckbox.Name = "elasticityPerCompanySKUCheckbox";
		this.elasticityPerCompanySKUCheckbox.Size = new System.Drawing.Size(95, 17);
		this.elasticityPerCompanySKUCheckbox.TabIndex = 61;
		this.elasticityPerCompanySKUCheckbox.Text = "Company SKU";
		this.elasticityPerCompanySKUCheckbox.UseVisualStyleBackColor = true;
		this.elasticityPerCompanySKUCheckbox.CheckedChanged += new System.EventHandler(elasticityPerCompanySKUCheckbox_CheckedChanged);
		checkBoxProperties.ForeColor = System.Drawing.SystemColors.ControlText;
		this.elasticityScenarioCombobox.CheckBoxProperties = checkBoxProperties;
		this.elasticityScenarioCombobox.DisplayMemberSingleItem = "";
		this.elasticityScenarioCombobox.Enabled = false;
		this.elasticityScenarioCombobox.FormattingEnabled = true;
		this.elasticityScenarioCombobox.Items.AddRange(new object[1] { "* All" });
		this.elasticityScenarioCombobox.Location = new System.Drawing.Point(279, 63);
		this.elasticityScenarioCombobox.Name = "elasticityScenarioCombobox";
		this.elasticityScenarioCombobox.Size = new System.Drawing.Size(441, 21);
		this.elasticityScenarioCombobox.TabIndex = 60;
		this.elasticityScenarioCombobox.Enter += new System.EventHandler(elasticityScenarioCombobox_Enter);
		this.elasticityScenarioCombobox.CheckBoxCheckedChanged += new System.EventHandler(elasticityScenarioCombobox_CheckBoxCheckedChanged);
		this.elasticityScenarioCheckbox.AutoSize = true;
		this.elasticityScenarioCheckbox.Location = new System.Drawing.Point(19, 67);
		this.elasticityScenarioCheckbox.Name = "elasticityScenarioCheckbox";
		this.elasticityScenarioCheckbox.Size = new System.Drawing.Size(167, 17);
		this.elasticityScenarioCheckbox.TabIndex = 7;
		this.elasticityScenarioCheckbox.Text = "calculate elasticity at scenario";
		this.elasticityScenarioCheckbox.UseVisualStyleBackColor = true;
		this.elasticityScenarioCheckbox.CheckedChanged += new System.EventHandler(elasticityScenarioCheckbox_CheckedChanged);
		this.elasticitySegmentationTypeCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.elasticitySegmentationTypeCombobox.Enabled = false;
		this.elasticitySegmentationTypeCombobox.FormattingEnabled = true;
		this.elasticitySegmentationTypeCombobox.Location = new System.Drawing.Point(279, 39);
		this.elasticitySegmentationTypeCombobox.Name = "elasticitySegmentationTypeCombobox";
		this.elasticitySegmentationTypeCombobox.Size = new System.Drawing.Size(210, 21);
		this.elasticitySegmentationTypeCombobox.TabIndex = 5;
		this.elasticitySegmentationCheckbox.AutoSize = true;
		this.elasticitySegmentationCheckbox.Location = new System.Drawing.Point(19, 41);
		this.elasticitySegmentationCheckbox.Name = "elasticitySegmentationCheckbox";
		this.elasticitySegmentationCheckbox.Size = new System.Drawing.Size(228, 17);
		this.elasticitySegmentationCheckbox.TabIndex = 4;
		this.elasticitySegmentationCheckbox.Text = "Use segmentation on the attribute elasticity";
		this.elasticitySegmentationCheckbox.UseVisualStyleBackColor = true;
		this.elasticitySegmentationCheckbox.CheckedChanged += new System.EventHandler(elasticitySegmentationCheckbox_CheckedChanged);
		this.elasticityPerCompanyGroupCheckbox.AutoSize = true;
		this.elasticityPerCompanyGroupCheckbox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.elasticityPerCompanyGroupCheckbox.Location = new System.Drawing.Point(475, 16);
		this.elasticityPerCompanyGroupCheckbox.Name = "elasticityPerCompanyGroupCheckbox";
		this.elasticityPerCompanyGroupCheckbox.Size = new System.Drawing.Size(102, 17);
		this.elasticityPerCompanyGroupCheckbox.TabIndex = 3;
		this.elasticityPerCompanyGroupCheckbox.Text = "Company Group";
		this.elasticityPerCompanyGroupCheckbox.UseVisualStyleBackColor = true;
		this.elasticityPerCompanyGroupCheckbox.CheckedChanged += new System.EventHandler(checkBox3_CheckedChanged);
		this.elasticityPerGroupCheckbox.AutoSize = true;
		this.elasticityPerGroupCheckbox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.elasticityPerGroupCheckbox.Location = new System.Drawing.Point(376, 16);
		this.elasticityPerGroupCheckbox.Name = "elasticityPerGroupCheckbox";
		this.elasticityPerGroupCheckbox.Size = new System.Drawing.Size(55, 17);
		this.elasticityPerGroupCheckbox.TabIndex = 2;
		this.elasticityPerGroupCheckbox.Text = "Group";
		this.elasticityPerGroupCheckbox.UseVisualStyleBackColor = true;
		this.elasticityPerGroupCheckbox.CheckedChanged += new System.EventHandler(checkBox2_CheckedChanged);
		this.elasticityPerSKUCheckbox.AutoSize = true;
		this.elasticityPerSKUCheckbox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.elasticityPerSKUCheckbox.Location = new System.Drawing.Point(284, 16);
		this.elasticityPerSKUCheckbox.Name = "elasticityPerSKUCheckbox";
		this.elasticityPerSKUCheckbox.Size = new System.Drawing.Size(48, 17);
		this.elasticityPerSKUCheckbox.TabIndex = 1;
		this.elasticityPerSKUCheckbox.Text = "SKU";
		this.elasticityPerSKUCheckbox.UseVisualStyleBackColor = true;
		this.elasticityPerSKUCheckbox.CheckedChanged += new System.EventHandler(checkBox1_CheckedChanged);
		this.label3.AutoSize = true;
		this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label3.Location = new System.Drawing.Point(16, 19);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(206, 13);
		this.label3.TabIndex = 0;
		this.label3.Text = "Generate charts for attribute elasticity per: ";
		checkBoxProperties2.ForeColor = System.Drawing.SystemColors.ControlText;
		this.elasticitySegmentMembersCombobox.CheckBoxProperties = checkBoxProperties2;
		this.elasticitySegmentMembersCombobox.DisplayMemberSingleItem = "";
		this.elasticitySegmentMembersCombobox.Enabled = false;
		this.elasticitySegmentMembersCombobox.FormattingEnabled = true;
		this.elasticitySegmentMembersCombobox.Items.AddRange(new object[1] { "* All" });
		this.elasticitySegmentMembersCombobox.Location = new System.Drawing.Point(510, 38);
		this.elasticitySegmentMembersCombobox.Name = "elasticitySegmentMembersCombobox";
		this.elasticitySegmentMembersCombobox.Size = new System.Drawing.Size(210, 21);
		this.elasticitySegmentMembersCombobox.TabIndex = 59;
		this.elasticitySegmentMembersCombobox.Enter += new System.EventHandler(elasticitySegmentMembersCombobox_Enter);
		this.elasticitySegmentMembersCombobox.CheckBoxCheckedChanged += new System.EventHandler(elasticitySegmentMembersCombobox_CheckBoxCheckedChanged);
		this.attributeElasticityPerCompanyGroupbox.Controls.Add(this.elasticityGroupTypePerCompanyCombobox);
		this.attributeElasticityPerCompanyGroupbox.Controls.Add(this.label47);
		this.attributeElasticityPerCompanyGroupbox.Controls.Add(this.elasticityCompanySeriesCombobox);
		this.attributeElasticityPerCompanyGroupbox.Controls.Add(this.elasticityDonePerCompanyButton);
		this.attributeElasticityPerCompanyGroupbox.Controls.Add(this.label10);
		this.attributeElasticityPerCompanyGroupbox.Controls.Add(this.elasticityAttributeNamePerCompanyCombobox);
		this.attributeElasticityPerCompanyGroupbox.Controls.Add(this.label7);
		this.attributeElasticityPerCompanyGroupbox.Enabled = false;
		this.attributeElasticityPerCompanyGroupbox.Location = new System.Drawing.Point(524, 109);
		this.attributeElasticityPerCompanyGroupbox.Name = "attributeElasticityPerCompanyGroupbox";
		this.attributeElasticityPerCompanyGroupbox.Size = new System.Drawing.Size(248, 103);
		this.attributeElasticityPerCompanyGroupbox.TabIndex = 1;
		this.attributeElasticityPerCompanyGroupbox.TabStop = false;
		this.attributeElasticityPerCompanyGroupbox.Text = "Attribute Elasticity Per Company";
		this.elasticityGroupTypePerCompanyCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.elasticityGroupTypePerCompanyCombobox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.elasticityGroupTypePerCompanyCombobox.FormattingEnabled = true;
		this.elasticityGroupTypePerCompanyCombobox.Location = new System.Drawing.Point(91, 49);
		this.elasticityGroupTypePerCompanyCombobox.Name = "elasticityGroupTypePerCompanyCombobox";
		this.elasticityGroupTypePerCompanyCombobox.Size = new System.Drawing.Size(151, 21);
		this.elasticityGroupTypePerCompanyCombobox.TabIndex = 60;
		this.elasticityGroupTypePerCompanyCombobox.SelectedIndexChanged += new System.EventHandler(elasticityGroupTypePerCompanyCombobox_SelectedIndexChanged);
		this.label47.AutoSize = true;
		this.label47.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label47.Location = new System.Drawing.Point(22, 55);
		this.label47.Name = "label47";
		this.label47.Size = new System.Drawing.Size(63, 13);
		this.label47.TabIndex = 59;
		this.label47.Text = "Group Type";
		checkBoxProperties3.ForeColor = System.Drawing.SystemColors.ControlText;
		this.elasticityCompanySeriesCombobox.CheckBoxProperties = checkBoxProperties3;
		this.elasticityCompanySeriesCombobox.DisplayMemberSingleItem = "";
		this.elasticityCompanySeriesCombobox.FormattingEnabled = true;
		this.elasticityCompanySeriesCombobox.Items.AddRange(new object[1] { "* All" });
		this.elasticityCompanySeriesCombobox.Location = new System.Drawing.Point(91, 76);
		this.elasticityCompanySeriesCombobox.Name = "elasticityCompanySeriesCombobox";
		this.elasticityCompanySeriesCombobox.Size = new System.Drawing.Size(93, 21);
		this.elasticityCompanySeriesCombobox.TabIndex = 58;
		this.elasticityCompanySeriesCombobox.CheckBoxCheckedChanged += new System.EventHandler(elasticityCompanySKUsCombobox_CheckBoxCheckedChanged);
		this.elasticityDonePerCompanyButton.ForeColor = System.Drawing.SystemColors.ControlText;
		this.elasticityDonePerCompanyButton.Location = new System.Drawing.Point(190, 74);
		this.elasticityDonePerCompanyButton.Name = "elasticityDonePerCompanyButton";
		this.elasticityDonePerCompanyButton.Size = new System.Drawing.Size(52, 23);
		this.elasticityDonePerCompanyButton.TabIndex = 57;
		this.elasticityDonePerCompanyButton.Text = "Done";
		this.elasticityDonePerCompanyButton.UseVisualStyleBackColor = true;
		this.elasticityDonePerCompanyButton.Click += new System.EventHandler(elasticityDonePerCompanyButton_Click);
		this.label10.AutoSize = true;
		this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label10.Location = new System.Drawing.Point(51, 79);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(36, 13);
		this.label10.TabIndex = 12;
		this.label10.Text = "Series";
		this.elasticityAttributeNamePerCompanyCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.elasticityAttributeNamePerCompanyCombobox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.elasticityAttributeNamePerCompanyCombobox.FormattingEnabled = true;
		this.elasticityAttributeNamePerCompanyCombobox.Location = new System.Drawing.Point(91, 22);
		this.elasticityAttributeNamePerCompanyCombobox.Name = "elasticityAttributeNamePerCompanyCombobox";
		this.elasticityAttributeNamePerCompanyCombobox.Size = new System.Drawing.Size(151, 21);
		this.elasticityAttributeNamePerCompanyCombobox.TabIndex = 11;
		this.label7.AutoSize = true;
		this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label7.Location = new System.Drawing.Point(5, 25);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(77, 13);
		this.label7.TabIndex = 10;
		this.label7.Text = "Attribute Name";
		this.attributeElasticityPerSKUGroupbox.Controls.Add(this.elasticityDonePerSKUButton);
		this.attributeElasticityPerSKUGroupbox.Controls.Add(this.elasticitySKUsCombobox);
		this.attributeElasticityPerSKUGroupbox.Controls.Add(this.label8);
		this.attributeElasticityPerSKUGroupbox.Controls.Add(this.elasticityAttributeNamePerSKUCombobox);
		this.attributeElasticityPerSKUGroupbox.Controls.Add(this.label5);
		this.attributeElasticityPerSKUGroupbox.Enabled = false;
		this.attributeElasticityPerSKUGroupbox.Location = new System.Drawing.Point(14, 109);
		this.attributeElasticityPerSKUGroupbox.Name = "attributeElasticityPerSKUGroupbox";
		this.attributeElasticityPerSKUGroupbox.Size = new System.Drawing.Size(248, 103);
		this.attributeElasticityPerSKUGroupbox.TabIndex = 0;
		this.attributeElasticityPerSKUGroupbox.TabStop = false;
		this.attributeElasticityPerSKUGroupbox.Text = "Attribute Elasticity Per SKU";
		this.elasticityDonePerSKUButton.ForeColor = System.Drawing.SystemColors.ControlText;
		this.elasticityDonePerSKUButton.Location = new System.Drawing.Point(91, 76);
		this.elasticityDonePerSKUButton.Name = "elasticityDonePerSKUButton";
		this.elasticityDonePerSKUButton.Size = new System.Drawing.Size(69, 23);
		this.elasticityDonePerSKUButton.TabIndex = 57;
		this.elasticityDonePerSKUButton.Text = "Done";
		this.elasticityDonePerSKUButton.UseVisualStyleBackColor = true;
		this.elasticityDonePerSKUButton.Click += new System.EventHandler(elasticityDonePerSKUButton_Click);
		checkBoxProperties4.ForeColor = System.Drawing.SystemColors.ControlText;
		this.elasticitySKUsCombobox.CheckBoxProperties = checkBoxProperties4;
		this.elasticitySKUsCombobox.DisplayMemberSingleItem = "";
		this.elasticitySKUsCombobox.FormattingEnabled = true;
		this.elasticitySKUsCombobox.Items.AddRange(new object[1] { "* All" });
		this.elasticitySKUsCombobox.Location = new System.Drawing.Point(91, 49);
		this.elasticitySKUsCombobox.Name = "elasticitySKUsCombobox";
		this.elasticitySKUsCombobox.Size = new System.Drawing.Size(151, 21);
		this.elasticitySKUsCombobox.TabIndex = 58;
		this.elasticitySKUsCombobox.CheckBoxCheckedChanged += new System.EventHandler(elasticitySKUsCombobox_CheckBoxCheckedChanged);
		this.label8.AutoSize = true;
		this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label8.Location = new System.Drawing.Point(54, 52);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(34, 13);
		this.label8.TabIndex = 10;
		this.label8.Text = "SKUs";
		this.elasticityAttributeNamePerSKUCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.elasticityAttributeNamePerSKUCombobox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.elasticityAttributeNamePerSKUCombobox.FormattingEnabled = true;
		this.elasticityAttributeNamePerSKUCombobox.Location = new System.Drawing.Point(91, 22);
		this.elasticityAttributeNamePerSKUCombobox.Name = "elasticityAttributeNamePerSKUCombobox";
		this.elasticityAttributeNamePerSKUCombobox.Size = new System.Drawing.Size(151, 21);
		this.elasticityAttributeNamePerSKUCombobox.TabIndex = 9;
		this.label5.AutoSize = true;
		this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label5.Location = new System.Drawing.Point(5, 25);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(77, 13);
		this.label5.TabIndex = 8;
		this.label5.Text = "Attribute Name";
		this.attributeElasticityPerGroupGroupbox.Controls.Add(this.elasticityGroupsCombobox);
		this.attributeElasticityPerGroupGroupbox.Controls.Add(this.elasticityDonePerGroupButton);
		this.attributeElasticityPerGroupGroupbox.Controls.Add(this.label11);
		this.attributeElasticityPerGroupGroupbox.Controls.Add(this.elasticityGroupTypeCombobox);
		this.attributeElasticityPerGroupGroupbox.Controls.Add(this.label9);
		this.attributeElasticityPerGroupGroupbox.Controls.Add(this.elasticityAttributeNamePerGroupCombobox);
		this.attributeElasticityPerGroupGroupbox.Controls.Add(this.label6);
		this.attributeElasticityPerGroupGroupbox.Enabled = false;
		this.attributeElasticityPerGroupGroupbox.Location = new System.Drawing.Point(268, 109);
		this.attributeElasticityPerGroupGroupbox.Name = "attributeElasticityPerGroupGroupbox";
		this.attributeElasticityPerGroupGroupbox.Size = new System.Drawing.Size(248, 103);
		this.attributeElasticityPerGroupGroupbox.TabIndex = 1;
		this.attributeElasticityPerGroupGroupbox.TabStop = false;
		this.attributeElasticityPerGroupGroupbox.Text = "Attribute Elasticity Per Group";
		checkBoxProperties5.ForeColor = System.Drawing.SystemColors.ControlText;
		this.elasticityGroupsCombobox.CheckBoxProperties = checkBoxProperties5;
		this.elasticityGroupsCombobox.DisplayMemberSingleItem = "";
		this.elasticityGroupsCombobox.FormattingEnabled = true;
		this.elasticityGroupsCombobox.Location = new System.Drawing.Point(91, 76);
		this.elasticityGroupsCombobox.Name = "elasticityGroupsCombobox";
		this.elasticityGroupsCombobox.Size = new System.Drawing.Size(94, 21);
		this.elasticityGroupsCombobox.TabIndex = 59;
		this.elasticityGroupsCombobox.CheckBoxCheckedChanged += new System.EventHandler(elasticityGroupsCombobox_CheckBoxCheckedChanged);
		this.elasticityDonePerGroupButton.ForeColor = System.Drawing.SystemColors.ControlText;
		this.elasticityDonePerGroupButton.Location = new System.Drawing.Point(190, 76);
		this.elasticityDonePerGroupButton.Name = "elasticityDonePerGroupButton";
		this.elasticityDonePerGroupButton.Size = new System.Drawing.Size(52, 23);
		this.elasticityDonePerGroupButton.TabIndex = 57;
		this.elasticityDonePerGroupButton.Text = "Done";
		this.elasticityDonePerGroupButton.UseVisualStyleBackColor = true;
		this.elasticityDonePerGroupButton.Click += new System.EventHandler(elasticityDonePerGroupButton_Click);
		this.label11.AutoSize = true;
		this.label11.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label11.Location = new System.Drawing.Point(44, 79);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(41, 13);
		this.label11.TabIndex = 14;
		this.label11.Text = "Groups";
		this.elasticityGroupTypeCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.elasticityGroupTypeCombobox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.elasticityGroupTypeCombobox.FormattingEnabled = true;
		this.elasticityGroupTypeCombobox.Location = new System.Drawing.Point(91, 49);
		this.elasticityGroupTypeCombobox.Name = "elasticityGroupTypeCombobox";
		this.elasticityGroupTypeCombobox.Size = new System.Drawing.Size(151, 21);
		this.elasticityGroupTypeCombobox.TabIndex = 13;
		this.elasticityGroupTypeCombobox.SelectedIndexChanged += new System.EventHandler(elasticityGroupTypeCombobox_SelectedIndexChanged);
		this.label9.AutoSize = true;
		this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label9.Location = new System.Drawing.Point(22, 52);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(63, 13);
		this.label9.TabIndex = 12;
		this.label9.Text = "Group Type";
		this.elasticityAttributeNamePerGroupCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.elasticityAttributeNamePerGroupCombobox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.elasticityAttributeNamePerGroupCombobox.FormattingEnabled = true;
		this.elasticityAttributeNamePerGroupCombobox.Location = new System.Drawing.Point(91, 22);
		this.elasticityAttributeNamePerGroupCombobox.Name = "elasticityAttributeNamePerGroupCombobox";
		this.elasticityAttributeNamePerGroupCombobox.Size = new System.Drawing.Size(151, 21);
		this.elasticityAttributeNamePerGroupCombobox.TabIndex = 11;
		this.label6.AutoSize = true;
		this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label6.Location = new System.Drawing.Point(5, 25);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(77, 13);
		this.label6.TabIndex = 10;
		this.label6.Text = "Attribute Name";
		this.tabPage2.BackColor = System.Drawing.Color.White;
		this.tabPage2.Controls.Add(this.textBox2);
		this.tabPage2.Controls.Add(this.groupBox8);
		this.tabPage2.Controls.Add(this.groupBox7);
		this.tabPage2.Controls.Add(this.groupBox6);
		this.tabPage2.Controls.Add(this.groupBox5);
		this.tabPage2.Controls.Add(this.chart4);
		this.tabPage2.Location = new System.Drawing.Point(4, 22);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(784, 422);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "Price Index";
		this.textBox2.BackColor = System.Drawing.Color.Gray;
		this.textBox2.Enabled = false;
		this.textBox2.Location = new System.Drawing.Point(344, 8);
		this.textBox2.Multiline = true;
		this.textBox2.Name = "textBox2";
		this.textBox2.Size = new System.Drawing.Size(1, 410);
		this.textBox2.TabIndex = 12;
		this.groupBox8.Controls.Add(this.priceIndexSeriesCombobox);
		this.groupBox8.Controls.Add(this.priceIndexDoneButton);
		this.groupBox8.Controls.Add(this.label21);
		this.groupBox8.Controls.Add(this.priceIndexRandomizeButton);
		this.groupBox8.Controls.Add(this.priceIndexScaleTextbox);
		this.groupBox8.Controls.Add(this.label18);
		this.groupBox8.Controls.Add(this.priceIndexScaleLabel);
		this.groupBox8.Location = new System.Drawing.Point(6, 323);
		this.groupBox8.Name = "groupBox8";
		this.groupBox8.Size = new System.Drawing.Size(332, 93);
		this.groupBox8.TabIndex = 11;
		this.groupBox8.TabStop = false;
		this.groupBox8.Text = "Scale && Chart Output Series";
		checkBoxProperties6.ForeColor = System.Drawing.SystemColors.ControlText;
		this.priceIndexSeriesCombobox.CheckBoxProperties = checkBoxProperties6;
		this.priceIndexSeriesCombobox.DisplayMemberSingleItem = "";
		this.priceIndexSeriesCombobox.FormattingEnabled = true;
		this.priceIndexSeriesCombobox.Location = new System.Drawing.Point(51, 66);
		this.priceIndexSeriesCombobox.Name = "priceIndexSeriesCombobox";
		this.priceIndexSeriesCombobox.Size = new System.Drawing.Size(200, 21);
		this.priceIndexSeriesCombobox.TabIndex = 57;
		this.priceIndexSeriesCombobox.CheckBoxCheckedChanged += new System.EventHandler(priceIndexSeriesCombobox_CheckBoxCheckedChanged);
		this.priceIndexDoneButton.ForeColor = System.Drawing.SystemColors.ControlText;
		this.priceIndexDoneButton.Location = new System.Drawing.Point(257, 63);
		this.priceIndexDoneButton.Name = "priceIndexDoneButton";
		this.priceIndexDoneButton.Size = new System.Drawing.Size(69, 23);
		this.priceIndexDoneButton.TabIndex = 56;
		this.priceIndexDoneButton.Text = "Done";
		this.priceIndexDoneButton.UseVisualStyleBackColor = true;
		this.priceIndexDoneButton.Click += new System.EventHandler(priceIndexDoneButton_Click);
		this.label21.AutoSize = true;
		this.label21.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label21.Location = new System.Drawing.Point(9, 69);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(36, 13);
		this.label21.TabIndex = 54;
		this.label21.Text = "Series";
		this.priceIndexRandomizeButton.ForeColor = System.Drawing.SystemColors.ControlText;
		this.priceIndexRandomizeButton.Location = new System.Drawing.Point(257, 36);
		this.priceIndexRandomizeButton.Name = "priceIndexRandomizeButton";
		this.priceIndexRandomizeButton.Size = new System.Drawing.Size(69, 23);
		this.priceIndexRandomizeButton.TabIndex = 53;
		this.priceIndexRandomizeButton.Text = "Randomize ";
		this.priceIndexRandomizeButton.UseVisualStyleBackColor = true;
		this.priceIndexRandomizeButton.Click += new System.EventHandler(priceIndexRandomizeButton_Click);
		this.priceIndexScaleTextbox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.priceIndexScaleTextbox.Location = new System.Drawing.Point(51, 39);
		this.priceIndexScaleTextbox.Name = "priceIndexScaleTextbox";
		this.priceIndexScaleTextbox.Size = new System.Drawing.Size(200, 20);
		this.priceIndexScaleTextbox.TabIndex = 52;
		this.label18.AutoSize = true;
		this.label18.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label18.Location = new System.Drawing.Point(13, 42);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(34, 13);
		this.label18.TabIndex = 51;
		this.label18.Text = "Scale";
		this.priceIndexScaleLabel.AutoSize = true;
		this.priceIndexScaleLabel.ForeColor = System.Drawing.SystemColors.ControlText;
		this.priceIndexScaleLabel.Location = new System.Drawing.Point(13, 20);
		this.priceIndexScaleLabel.Name = "priceIndexScaleLabel";
		this.priceIndexScaleLabel.Size = new System.Drawing.Size(63, 13);
		this.priceIndexScaleLabel.TabIndex = 50;
		this.priceIndexScaleLabel.Text = "Boundaries:";
		this.groupBox7.Controls.Add(this.priceIndexReferenceSKUNameCombobox);
		this.groupBox7.Controls.Add(this.label16);
		this.groupBox7.Controls.Add(this.priceIndexReferenceGroupNameCombobox);
		this.groupBox7.Controls.Add(this.label17);
		this.groupBox7.Location = new System.Drawing.Point(7, 242);
		this.groupBox7.Name = "groupBox7";
		this.groupBox7.Size = new System.Drawing.Size(331, 76);
		this.groupBox7.TabIndex = 10;
		this.groupBox7.TabStop = false;
		this.groupBox7.Text = "Versus Reference At";
		this.priceIndexReferenceSKUNameCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.priceIndexReferenceSKUNameCombobox.FormattingEnabled = true;
		this.priceIndexReferenceSKUNameCombobox.Location = new System.Drawing.Point(55, 49);
		this.priceIndexReferenceSKUNameCombobox.Name = "priceIndexReferenceSKUNameCombobox";
		this.priceIndexReferenceSKUNameCombobox.Size = new System.Drawing.Size(270, 21);
		this.priceIndexReferenceSKUNameCombobox.TabIndex = 44;
		this.priceIndexReferenceSKUNameCombobox.SelectedIndexChanged += new System.EventHandler(priceIndexReferenceSKUNameCombobox_SelectedIndexChanged);
		this.label16.AutoSize = true;
		this.label16.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label16.Location = new System.Drawing.Point(13, 52);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(42, 13);
		this.label16.TabIndex = 42;
		this.label16.Text = "At SKU";
		this.priceIndexReferenceGroupNameCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.priceIndexReferenceGroupNameCombobox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.priceIndexReferenceGroupNameCombobox.FormattingEnabled = true;
		this.priceIndexReferenceGroupNameCombobox.Location = new System.Drawing.Point(55, 23);
		this.priceIndexReferenceGroupNameCombobox.Name = "priceIndexReferenceGroupNameCombobox";
		this.priceIndexReferenceGroupNameCombobox.Size = new System.Drawing.Size(270, 21);
		this.priceIndexReferenceGroupNameCombobox.TabIndex = 41;
		this.priceIndexReferenceGroupNameCombobox.SelectedIndexChanged += new System.EventHandler(priceIndexReferenceGroupNameCombobox_SelectedIndexChanged);
		this.label17.AutoSize = true;
		this.label17.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label17.Location = new System.Drawing.Point(13, 26);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(36, 13);
		this.label17.TabIndex = 40;
		this.label17.Text = "Group";
		this.groupBox6.Controls.Add(this.priceIndexSKUNameCombobox);
		this.groupBox6.Controls.Add(this.label14);
		this.groupBox6.Controls.Add(this.priceIndexGroupNameCombobox);
		this.groupBox6.Controls.Add(this.label15);
		this.groupBox6.Location = new System.Drawing.Point(6, 161);
		this.groupBox6.Name = "groupBox6";
		this.groupBox6.Size = new System.Drawing.Size(332, 76);
		this.groupBox6.TabIndex = 9;
		this.groupBox6.TabStop = false;
		this.groupBox6.Text = "Price Index For ";
		this.priceIndexSKUNameCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.priceIndexSKUNameCombobox.FormattingEnabled = true;
		this.priceIndexSKUNameCombobox.Location = new System.Drawing.Point(55, 49);
		this.priceIndexSKUNameCombobox.Name = "priceIndexSKUNameCombobox";
		this.priceIndexSKUNameCombobox.Size = new System.Drawing.Size(271, 21);
		this.priceIndexSKUNameCombobox.TabIndex = 43;
		this.priceIndexSKUNameCombobox.SelectedIndexChanged += new System.EventHandler(priceIndexSKUNameCombobox_SelectedIndexChanged);
		this.label14.AutoSize = true;
		this.label14.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label14.Location = new System.Drawing.Point(13, 52);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(42, 13);
		this.label14.TabIndex = 42;
		this.label14.Text = "At SKU";
		this.priceIndexGroupNameCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.priceIndexGroupNameCombobox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.priceIndexGroupNameCombobox.FormattingEnabled = true;
		this.priceIndexGroupNameCombobox.Location = new System.Drawing.Point(56, 23);
		this.priceIndexGroupNameCombobox.Name = "priceIndexGroupNameCombobox";
		this.priceIndexGroupNameCombobox.Size = new System.Drawing.Size(270, 21);
		this.priceIndexGroupNameCombobox.TabIndex = 41;
		this.priceIndexGroupNameCombobox.SelectedIndexChanged += new System.EventHandler(priceIndexGroupNameCombobox_SelectedIndexChanged);
		this.label15.AutoSize = true;
		this.label15.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label15.Location = new System.Drawing.Point(13, 26);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(36, 13);
		this.label15.TabIndex = 40;
		this.label15.Text = "Group";
		this.groupBox5.Controls.Add(this.priceIndexScenariosDropdownmenu);
		this.groupBox5.Controls.Add(this.priceIndexScenarioLabel);
		this.groupBox5.Controls.Add(this.priceIndexConfigButton);
		this.groupBox5.Controls.Add(this.priceIndexNameTextbox);
		this.groupBox5.Controls.Add(this.label4);
		this.groupBox5.Controls.Add(this.label25);
		this.groupBox5.Controls.Add(this.priceIndexSKUsRadiobox);
		this.groupBox5.Controls.Add(this.priceIndexGroupsRadiobox);
		this.groupBox5.Controls.Add(this.priceIndexTotalCompanyRevenue);
		this.groupBox5.Controls.Add(this.priceIndexTotalCompanyShare);
		this.groupBox5.Controls.Add(this.priceIndexGroupTypeCombobox);
		this.groupBox5.Controls.Add(this.label13);
		this.groupBox5.Location = new System.Drawing.Point(6, 10);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(332, 145);
		this.groupBox5.TabIndex = 8;
		this.groupBox5.TabStop = false;
		this.groupBox5.Text = "Price Index && Revenus Global Configuration";
		this.priceIndexScenariosDropdownmenu.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
		this.priceIndexScenariosDropdownmenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.priceIndexScenariosDropdownmenu.ForeColor = System.Drawing.SystemColors.ControlText;
		this.priceIndexScenariosDropdownmenu.Image = MarketSimulator.Reports.Properties.Resources.downArrow;
		this.priceIndexScenariosDropdownmenu.Location = new System.Drawing.Point(309, 95);
		this.priceIndexScenariosDropdownmenu.Name = "priceIndexScenariosDropdownmenu";
		this.priceIndexScenariosDropdownmenu.Size = new System.Drawing.Size(17, 21);
		this.priceIndexScenariosDropdownmenu.TabIndex = 65;
		this.priceIndexScenariosDropdownmenu.UseVisualStyleBackColor = true;
		this.priceIndexScenariosDropdownmenu.Click += new System.EventHandler(scenariosDropdownButton_Click);
		this.priceIndexScenarioLabel.Location = new System.Drawing.Point(13, 89);
		this.priceIndexScenarioLabel.Margin = new System.Windows.Forms.Padding(0);
		this.priceIndexScenarioLabel.Name = "priceIndexScenarioLabel";
		this.priceIndexScenarioLabel.Size = new System.Drawing.Size(234, 32);
		this.priceIndexScenarioLabel.TabIndex = 66;
		this.priceIndexScenarioLabel.Text = "Current Scenario is set to Default Scenario";
		this.priceIndexScenarioLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.priceIndexConfigButton.ForeColor = System.Drawing.SystemColors.ControlText;
		this.priceIndexConfigButton.Location = new System.Drawing.Point(250, 94);
		this.priceIndexConfigButton.Name = "priceIndexConfigButton";
		this.priceIndexConfigButton.Size = new System.Drawing.Size(75, 23);
		this.priceIndexConfigButton.TabIndex = 64;
		this.priceIndexConfigButton.Text = "Configure";
		this.priceIndexConfigButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.priceIndexConfigButton.UseVisualStyleBackColor = true;
		this.priceIndexConfigButton.Click += new System.EventHandler(configSKUButton_Click);
		this.priceIndexNameTextbox.Location = new System.Drawing.Point(82, 68);
		this.priceIndexNameTextbox.Name = "priceIndexNameTextbox";
		this.priceIndexNameTextbox.Size = new System.Drawing.Size(244, 20);
		this.priceIndexNameTextbox.TabIndex = 47;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(41, 71);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(38, 13);
		this.label4.TabIndex = 46;
		this.label4.Text = "Name:";
		this.label25.AutoSize = true;
		this.label25.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label25.Location = new System.Drawing.Point(37, 21);
		this.label25.Name = "label25";
		this.label25.Size = new System.Drawing.Size(42, 13);
		this.label25.TabIndex = 45;
		this.label25.Text = "Output:";
		this.priceIndexSKUsRadiobox.AutoSize = true;
		this.priceIndexSKUsRadiobox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.priceIndexSKUsRadiobox.Location = new System.Drawing.Point(243, 19);
		this.priceIndexSKUsRadiobox.Name = "priceIndexSKUsRadiobox";
		this.priceIndexSKUsRadiobox.Size = new System.Drawing.Size(54, 17);
		this.priceIndexSKUsRadiobox.TabIndex = 44;
		this.priceIndexSKUsRadiobox.Text = "SKU's";
		this.priceIndexSKUsRadiobox.UseVisualStyleBackColor = true;
		this.priceIndexSKUsRadiobox.CheckedChanged += new System.EventHandler(priceIndexSKUsRadiobox_CheckedChanged);
		this.priceIndexGroupsRadiobox.AutoSize = true;
		this.priceIndexGroupsRadiobox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.priceIndexGroupsRadiobox.Location = new System.Drawing.Point(110, 19);
		this.priceIndexGroupsRadiobox.Name = "priceIndexGroupsRadiobox";
		this.priceIndexGroupsRadiobox.Size = new System.Drawing.Size(59, 17);
		this.priceIndexGroupsRadiobox.TabIndex = 43;
		this.priceIndexGroupsRadiobox.Text = "Groups";
		this.priceIndexGroupsRadiobox.UseVisualStyleBackColor = true;
		this.priceIndexGroupsRadiobox.CheckedChanged += new System.EventHandler(priceIndexGroupsRadiobox_CheckedChanged);
		this.priceIndexTotalCompanyRevenue.AutoSize = true;
		this.priceIndexTotalCompanyRevenue.ForeColor = System.Drawing.SystemColors.ControlText;
		this.priceIndexTotalCompanyRevenue.Location = new System.Drawing.Point(187, 122);
		this.priceIndexTotalCompanyRevenue.Name = "priceIndexTotalCompanyRevenue";
		this.priceIndexTotalCompanyRevenue.Size = new System.Drawing.Size(139, 17);
		this.priceIndexTotalCompanyRevenue.TabIndex = 38;
		this.priceIndexTotalCompanyRevenue.Text = "Add Company Revenue";
		this.priceIndexTotalCompanyRevenue.UseVisualStyleBackColor = true;
		this.priceIndexTotalCompanyShare.AutoSize = true;
		this.priceIndexTotalCompanyShare.ForeColor = System.Drawing.SystemColors.ControlText;
		this.priceIndexTotalCompanyShare.Location = new System.Drawing.Point(16, 122);
		this.priceIndexTotalCompanyShare.Name = "priceIndexTotalCompanyShare";
		this.priceIndexTotalCompanyShare.Size = new System.Drawing.Size(123, 17);
		this.priceIndexTotalCompanyShare.TabIndex = 37;
		this.priceIndexTotalCompanyShare.Text = "Add Company Share";
		this.priceIndexTotalCompanyShare.UseVisualStyleBackColor = true;
		this.priceIndexGroupTypeCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.priceIndexGroupTypeCombobox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.priceIndexGroupTypeCombobox.FormattingEnabled = true;
		this.priceIndexGroupTypeCombobox.Location = new System.Drawing.Point(82, 41);
		this.priceIndexGroupTypeCombobox.Name = "priceIndexGroupTypeCombobox";
		this.priceIndexGroupTypeCombobox.Size = new System.Drawing.Size(244, 21);
		this.priceIndexGroupTypeCombobox.TabIndex = 36;
		this.priceIndexGroupTypeCombobox.SelectedIndexChanged += new System.EventHandler(priceIndexGroupTypeCombobox_SelectedIndexChanged);
		this.label13.AutoSize = true;
		this.label13.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label13.Location = new System.Drawing.Point(13, 44);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(66, 13);
		this.label13.TabIndex = 35;
		this.label13.Text = "Group Type:";
		chartArea4.AxisX.LabelStyle.ForeColor = System.Drawing.Color.DimGray;
		chartArea4.AxisX.LineColor = System.Drawing.Color.DimGray;
		chartArea4.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
		chartArea4.AxisY.LabelStyle.ForeColor = System.Drawing.Color.DimGray;
		chartArea4.AxisY.LineColor = System.Drawing.Color.DimGray;
		chartArea4.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
		chartArea4.Name = "ChartArea1";
		this.chart4.ChartAreas.Add(chartArea4);
		this.chart4.Enabled = false;
		legend.Alignment = System.Drawing.StringAlignment.Center;
		legend.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
		legend.Name = "Legend1";
		this.chart4.Legends.Add(legend);
		this.chart4.Location = new System.Drawing.Point(347, 6);
		this.chart4.Name = "chart4";
		series4.ChartArea = "ChartArea1";
		series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
		series4.LabelForeColor = System.Drawing.Color.DarkGray;
		series4.Legend = "Legend1";
		series4.Name = "Series1";
		dataPoint13.IsEmpty = true;
		dataPoint14.IsEmpty = true;
		dataPoint15.IsEmpty = true;
		dataPoint16.IsEmpty = true;
		series4.Points.Add(dataPoint13);
		series4.Points.Add(dataPoint14);
		series4.Points.Add(dataPoint15);
		series4.Points.Add(dataPoint16);
		this.chart4.Series.Add(series4);
		this.chart4.Size = new System.Drawing.Size(431, 410);
		this.chart4.TabIndex = 7;
		this.chart4.Text = "chart4";
		title4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		title4.ForeColor = System.Drawing.Color.SteelBlue;
		title4.Name = "Title1";
		title4.Text = "Price Index & Revenues";
		this.chart4.Titles.Add(title4);
		this.tabPage8.BackColor = System.Drawing.Color.White;
		this.tabPage8.Controls.Add(this.textBox10);
		this.tabPage8.Controls.Add(this.groupBox2);
		this.tabPage8.Controls.Add(this.groupBox3);
		this.tabPage8.Controls.Add(this.chart10);
		this.tabPage8.Location = new System.Drawing.Point(4, 22);
		this.tabPage8.Name = "tabPage8";
		this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage8.Size = new System.Drawing.Size(784, 422);
		this.tabPage8.TabIndex = 7;
		this.tabPage8.Text = "Shares && Revenues";
		this.textBox10.BackColor = System.Drawing.Color.Gray;
		this.textBox10.Enabled = false;
		this.textBox10.Location = new System.Drawing.Point(344, 8);
		this.textBox10.Multiline = true;
		this.textBox10.Name = "textBox10";
		this.textBox10.Size = new System.Drawing.Size(1, 410);
		this.textBox10.TabIndex = 28;
		this.groupBox2.Controls.Add(this.sharesWeightedSOPIndexCheckbox);
		this.groupBox2.Controls.Add(this.sharesWeightedSOPCheckbox);
		this.groupBox2.Controls.Add(this.sharesValueShareIndexCheckbox);
		this.groupBox2.Controls.Add(this.sharesValueShareCheckbox);
		this.groupBox2.Controls.Add(this.sharesRevenueIndexCheckbox);
		this.groupBox2.Controls.Add(this.sharesVolumeShareIndexCheckbox);
		this.groupBox2.Controls.Add(this.sharesSeriesCombobox);
		this.groupBox2.Controls.Add(this.label40);
		this.groupBox2.Controls.Add(this.sharesRevenueCheckbox);
		this.groupBox2.Controls.Add(this.sharesVolumeShareCheckbox);
		this.groupBox2.Controls.Add(this.sharesSenariosCombobox);
		this.groupBox2.Controls.Add(this.sharesDoneButton);
		this.groupBox2.Controls.Add(this.label24);
		this.groupBox2.Location = new System.Drawing.Point(6, 147);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(332, 219);
		this.groupBox2.TabIndex = 27;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Chart Output Series";
		this.sharesRevenueIndexCheckbox.AutoSize = true;
		this.sharesRevenueIndexCheckbox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.sharesRevenueIndexCheckbox.Location = new System.Drawing.Point(184, 99);
		this.sharesRevenueIndexCheckbox.Name = "sharesRevenueIndexCheckbox";
		this.sharesRevenueIndexCheckbox.Size = new System.Drawing.Size(99, 17);
		this.sharesRevenueIndexCheckbox.TabIndex = 56;
		this.sharesRevenueIndexCheckbox.Text = "Revenue Index";
		this.sharesRevenueIndexCheckbox.UseVisualStyleBackColor = true;
		this.sharesVolumeShareIndexCheckbox.AutoSize = true;
		this.sharesVolumeShareIndexCheckbox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.sharesVolumeShareIndexCheckbox.Location = new System.Drawing.Point(184, 76);
		this.sharesVolumeShareIndexCheckbox.Name = "sharesVolumeShareIndexCheckbox";
		this.sharesVolumeShareIndexCheckbox.Size = new System.Drawing.Size(121, 17);
		this.sharesVolumeShareIndexCheckbox.TabIndex = 55;
		this.sharesVolumeShareIndexCheckbox.Text = "Volume Share Index";
		this.sharesVolumeShareIndexCheckbox.UseVisualStyleBackColor = true;
		checkBoxProperties7.ForeColor = System.Drawing.SystemColors.ControlText;
		this.sharesSeriesCombobox.CheckBoxProperties = checkBoxProperties7;
		this.sharesSeriesCombobox.DisplayMemberSingleItem = "";
		this.sharesSeriesCombobox.FormattingEnabled = true;
		this.sharesSeriesCombobox.Location = new System.Drawing.Point(70, 46);
		this.sharesSeriesCombobox.Name = "sharesSeriesCombobox";
		this.sharesSeriesCombobox.Size = new System.Drawing.Size(256, 21);
		this.sharesSeriesCombobox.TabIndex = 54;
		this.sharesSeriesCombobox.CheckBoxCheckedChanged += new System.EventHandler(sharesSeriesCombobox_CheckBoxCheckedChanged);
		this.label40.AutoSize = true;
		this.label40.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label40.Location = new System.Drawing.Point(29, 49);
		this.label40.Name = "label40";
		this.label40.Size = new System.Drawing.Size(36, 13);
		this.label40.TabIndex = 53;
		this.label40.Text = "Series";
		this.sharesRevenueCheckbox.AutoSize = true;
		this.sharesRevenueCheckbox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.sharesRevenueCheckbox.Location = new System.Drawing.Point(13, 99);
		this.sharesRevenueCheckbox.Name = "sharesRevenueCheckbox";
		this.sharesRevenueCheckbox.Size = new System.Drawing.Size(75, 17);
		this.sharesRevenueCheckbox.TabIndex = 52;
		this.sharesRevenueCheckbox.Text = "Revenues";
		this.sharesRevenueCheckbox.UseVisualStyleBackColor = true;
		this.sharesVolumeShareCheckbox.AutoSize = true;
		this.sharesVolumeShareCheckbox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.sharesVolumeShareCheckbox.Location = new System.Drawing.Point(13, 76);
		this.sharesVolumeShareCheckbox.Name = "sharesVolumeShareCheckbox";
		this.sharesVolumeShareCheckbox.Size = new System.Drawing.Size(97, 17);
		this.sharesVolumeShareCheckbox.TabIndex = 51;
		this.sharesVolumeShareCheckbox.Text = "Volume Shares";
		this.sharesVolumeShareCheckbox.UseVisualStyleBackColor = true;
		checkBoxProperties8.ForeColor = System.Drawing.SystemColors.ControlText;
		this.sharesSenariosCombobox.CheckBoxProperties = checkBoxProperties8;
		this.sharesSenariosCombobox.DisplayMemberSingleItem = "";
		this.sharesSenariosCombobox.FormattingEnabled = true;
		this.sharesSenariosCombobox.Location = new System.Drawing.Point(70, 19);
		this.sharesSenariosCombobox.Name = "sharesSenariosCombobox";
		this.sharesSenariosCombobox.Size = new System.Drawing.Size(256, 21);
		this.sharesSenariosCombobox.TabIndex = 3;
		this.sharesSenariosCombobox.Enter += new System.EventHandler(sharesSenariosCombobox_Enter);
		this.sharesSenariosCombobox.CheckBoxCheckedChanged += new System.EventHandler(sharesSenariosCombobox_CheckBoxCheckedChanged);
		this.sharesDoneButton.ForeColor = System.Drawing.SystemColors.ControlText;
		this.sharesDoneButton.Location = new System.Drawing.Point(123, 176);
		this.sharesDoneButton.Name = "sharesDoneButton";
		this.sharesDoneButton.Size = new System.Drawing.Size(87, 23);
		this.sharesDoneButton.TabIndex = 2;
		this.sharesDoneButton.Text = "Done";
		this.sharesDoneButton.UseVisualStyleBackColor = true;
		this.sharesDoneButton.Click += new System.EventHandler(sharesDoneButton_Click);
		this.label24.AutoSize = true;
		this.label24.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label24.Location = new System.Drawing.Point(12, 22);
		this.label24.Name = "label24";
		this.label24.Size = new System.Drawing.Size(54, 13);
		this.label24.TabIndex = 0;
		this.label24.Text = "Scenarios";
		this.groupBox3.Controls.Add(this.sharesNameTextBox);
		this.groupBox3.Controls.Add(this.label30);
		this.groupBox3.Controls.Add(this.sharesConfigSKUButton);
		this.groupBox3.Controls.Add(this.label27);
		this.groupBox3.Controls.Add(this.shareSKURadiobox);
		this.groupBox3.Controls.Add(this.sharesGroupRadiobox);
		this.groupBox3.Controls.Add(this.sharesGroupTypeCombobox);
		this.groupBox3.Controls.Add(this.label38);
		this.groupBox3.Location = new System.Drawing.Point(6, 9);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(332, 132);
		this.groupBox3.TabIndex = 26;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "Shares Vs. Revenues Global Configuration";
		this.sharesNameTextBox.Location = new System.Drawing.Point(79, 73);
		this.sharesNameTextBox.Name = "sharesNameTextBox";
		this.sharesNameTextBox.Size = new System.Drawing.Size(247, 20);
		this.sharesNameTextBox.TabIndex = 50;
		this.label30.AutoSize = true;
		this.label30.Location = new System.Drawing.Point(38, 76);
		this.label30.Name = "label30";
		this.label30.Size = new System.Drawing.Size(38, 13);
		this.label30.TabIndex = 49;
		this.label30.Text = "Name:";
		this.sharesConfigSKUButton.ForeColor = System.Drawing.SystemColors.ControlText;
		this.sharesConfigSKUButton.Location = new System.Drawing.Point(251, 100);
		this.sharesConfigSKUButton.Name = "sharesConfigSKUButton";
		this.sharesConfigSKUButton.Size = new System.Drawing.Size(75, 23);
		this.sharesConfigSKUButton.TabIndex = 48;
		this.sharesConfigSKUButton.Text = "Configure";
		this.sharesConfigSKUButton.UseVisualStyleBackColor = true;
		this.sharesConfigSKUButton.Click += new System.EventHandler(configSKUButton_Click);
		this.label27.AutoSize = true;
		this.label27.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label27.Location = new System.Drawing.Point(34, 25);
		this.label27.Name = "label27";
		this.label27.Size = new System.Drawing.Size(42, 13);
		this.label27.TabIndex = 42;
		this.label27.Text = "Output:";
		this.shareSKURadiobox.AutoSize = true;
		this.shareSKURadiobox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.shareSKURadiobox.Location = new System.Drawing.Point(243, 23);
		this.shareSKURadiobox.Name = "shareSKURadiobox";
		this.shareSKURadiobox.Size = new System.Drawing.Size(54, 17);
		this.shareSKURadiobox.TabIndex = 41;
		this.shareSKURadiobox.TabStop = true;
		this.shareSKURadiobox.Text = "SKU's";
		this.shareSKURadiobox.UseVisualStyleBackColor = true;
		this.shareSKURadiobox.CheckedChanged += new System.EventHandler(shareSKURadiobox_CheckedChanged);
		this.sharesGroupRadiobox.AutoSize = true;
		this.sharesGroupRadiobox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.sharesGroupRadiobox.Location = new System.Drawing.Point(106, 23);
		this.sharesGroupRadiobox.Name = "sharesGroupRadiobox";
		this.sharesGroupRadiobox.Size = new System.Drawing.Size(59, 17);
		this.sharesGroupRadiobox.TabIndex = 40;
		this.sharesGroupRadiobox.TabStop = true;
		this.sharesGroupRadiobox.Text = "Groups";
		this.sharesGroupRadiobox.UseVisualStyleBackColor = true;
		this.sharesGroupRadiobox.CheckedChanged += new System.EventHandler(sharesGroupRadiobox_CheckedChanged);
		this.sharesGroupTypeCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.sharesGroupTypeCombobox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.sharesGroupTypeCombobox.FormattingEnabled = true;
		this.sharesGroupTypeCombobox.Location = new System.Drawing.Point(79, 46);
		this.sharesGroupTypeCombobox.Name = "sharesGroupTypeCombobox";
		this.sharesGroupTypeCombobox.Size = new System.Drawing.Size(247, 21);
		this.sharesGroupTypeCombobox.TabIndex = 36;
		this.sharesGroupTypeCombobox.SelectedIndexChanged += new System.EventHandler(sharesGroupTypeCombobox_SelectedIndexChanged);
		this.label38.AutoSize = true;
		this.label38.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label38.Location = new System.Drawing.Point(10, 49);
		this.label38.Name = "label38";
		this.label38.Size = new System.Drawing.Size(66, 13);
		this.label38.TabIndex = 35;
		this.label38.Text = "Group Type:";
		chartArea5.AxisX.LabelStyle.ForeColor = System.Drawing.Color.DimGray;
		chartArea5.AxisX.LineColor = System.Drawing.Color.DimGray;
		chartArea5.AxisX.MajorGrid.Enabled = false;
		chartArea5.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
		chartArea5.AxisX2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
		chartArea5.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
		chartArea5.AxisY.LabelStyle.ForeColor = System.Drawing.Color.DimGray;
		chartArea5.AxisY.LineColor = System.Drawing.Color.DimGray;
		chartArea5.AxisY.MajorGrid.Enabled = false;
		chartArea5.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
		chartArea5.AxisY.ScaleBreakStyle.BreakLineStyle = System.Windows.Forms.DataVisualization.Charting.BreakLineStyle.None;
		chartArea5.Name = "ChartArea1";
		this.chart10.ChartAreas.Add(chartArea5);
		this.chart10.Enabled = false;
		legend2.Alignment = System.Drawing.StringAlignment.Center;
		legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
		legend2.Name = "Legend1";
		this.chart10.Legends.Add(legend2);
		this.chart10.Location = new System.Drawing.Point(346, 6);
		this.chart10.Name = "chart10";
		series5.ChartArea = "ChartArea1";
		series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.ErrorBar;
		series5.LabelForeColor = System.Drawing.Color.DarkGray;
		series5.Legend = "Legend1";
		series5.Name = "Series1";
		dataPoint17.IsEmpty = true;
		dataPoint18.IsEmpty = true;
		dataPoint19.IsEmpty = true;
		series5.Points.Add(dataPoint17);
		series5.Points.Add(dataPoint18);
		series5.Points.Add(dataPoint19);
		series5.YValuesPerPoint = 3;
		this.chart10.Series.Add(series5);
		this.chart10.Size = new System.Drawing.Size(434, 410);
		this.chart10.SuppressExceptions = true;
		this.chart10.TabIndex = 25;
		this.chart10.Text = "chart10";
		title5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		title5.ForeColor = System.Drawing.Color.SteelBlue;
		title5.Name = "Title1";
		title5.Text = "Shares & Revenues";
		this.chart10.Titles.Add(title5);
		this.tabPage3.BackColor = System.Drawing.Color.White;
		this.tabPage3.Controls.Add(this.textBox3);
		this.tabPage3.Controls.Add(this.groupBox10);
		this.tabPage3.Controls.Add(this.groupBox9);
		this.tabPage3.Controls.Add(this.chart5);
		this.tabPage3.Location = new System.Drawing.Point(4, 22);
		this.tabPage3.Name = "tabPage3";
		this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage3.Size = new System.Drawing.Size(784, 422);
		this.tabPage3.TabIndex = 2;
		this.tabPage3.Text = "Brand Interaction";
		this.textBox3.BackColor = System.Drawing.Color.Gray;
		this.textBox3.Enabled = false;
		this.textBox3.Location = new System.Drawing.Point(344, 8);
		this.textBox3.Multiline = true;
		this.textBox3.Name = "textBox3";
		this.textBox3.Size = new System.Drawing.Size(1, 410);
		this.textBox3.TabIndex = 13;
		this.groupBox10.Controls.Add(this.brandInteractionScenariosCombobox);
		this.groupBox10.Controls.Add(this.label46);
		this.groupBox10.Controls.Add(this.label43);
		this.groupBox10.Controls.Add(this.brandInteractionBasicScenarioCombobox);
		this.groupBox10.Controls.Add(this.brandInteractionSeriesCombobox);
		this.groupBox10.Controls.Add(this.brandInteractionDoneButton);
		this.groupBox10.Controls.Add(this.label35);
		this.groupBox10.Location = new System.Drawing.Point(6, 147);
		this.groupBox10.Name = "groupBox10";
		this.groupBox10.Size = new System.Drawing.Size(332, 137);
		this.groupBox10.TabIndex = 10;
		this.groupBox10.TabStop = false;
		this.groupBox10.Text = "Chart Output Series";
		checkBoxProperties9.ForeColor = System.Drawing.SystemColors.ControlText;
		this.brandInteractionScenariosCombobox.CheckBoxProperties = checkBoxProperties9;
		this.brandInteractionScenariosCombobox.DisplayMemberSingleItem = "";
		this.brandInteractionScenariosCombobox.FormattingEnabled = true;
		this.brandInteractionScenariosCombobox.Location = new System.Drawing.Point(79, 51);
		this.brandInteractionScenariosCombobox.Name = "brandInteractionScenariosCombobox";
		this.brandInteractionScenariosCombobox.Size = new System.Drawing.Size(247, 21);
		this.brandInteractionScenariosCombobox.TabIndex = 7;
		this.brandInteractionScenariosCombobox.Enter += new System.EventHandler(brandInteractionScenariosCombobox_Enter);
		this.brandInteractionScenariosCombobox.CheckBoxCheckedChanged += new System.EventHandler(brandInteractionScenariosCombobox_CheckBoxCheckedChanged);
		this.label46.AutoSize = true;
		this.label46.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label46.Location = new System.Drawing.Point(24, 54);
		this.label46.Name = "label46";
		this.label46.Size = new System.Drawing.Size(54, 13);
		this.label46.TabIndex = 6;
		this.label46.Text = "Scenarios";
		this.label43.AutoSize = true;
		this.label43.Location = new System.Drawing.Point(1, 29);
		this.label43.Name = "label43";
		this.label43.Size = new System.Drawing.Size(78, 13);
		this.label43.TabIndex = 5;
		this.label43.Text = "Basic Scenario";
		this.brandInteractionBasicScenarioCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.brandInteractionBasicScenarioCombobox.FormattingEnabled = true;
		this.brandInteractionBasicScenarioCombobox.Items.AddRange(new object[1] { "Default" });
		this.brandInteractionBasicScenarioCombobox.Location = new System.Drawing.Point(79, 26);
		this.brandInteractionBasicScenarioCombobox.Name = "brandInteractionBasicScenarioCombobox";
		this.brandInteractionBasicScenarioCombobox.Size = new System.Drawing.Size(247, 21);
		this.brandInteractionBasicScenarioCombobox.TabIndex = 4;
		this.brandInteractionBasicScenarioCombobox.Enter += new System.EventHandler(basicScenarioCombobox_Enter);
		checkBoxProperties10.ForeColor = System.Drawing.SystemColors.ControlText;
		this.brandInteractionSeriesCombobox.CheckBoxProperties = checkBoxProperties10;
		this.brandInteractionSeriesCombobox.DisplayMemberSingleItem = "";
		this.brandInteractionSeriesCombobox.FormattingEnabled = true;
		this.brandInteractionSeriesCombobox.Location = new System.Drawing.Point(79, 76);
		this.brandInteractionSeriesCombobox.Name = "brandInteractionSeriesCombobox";
		this.brandInteractionSeriesCombobox.Size = new System.Drawing.Size(247, 21);
		this.brandInteractionSeriesCombobox.TabIndex = 3;
		this.brandInteractionSeriesCombobox.CheckBoxCheckedChanged += new System.EventHandler(brandInteractionSeriesCombobox_CheckBoxCheckedChanged);
		this.brandInteractionDoneButton.ForeColor = System.Drawing.SystemColors.ControlText;
		this.brandInteractionDoneButton.Location = new System.Drawing.Point(129, 103);
		this.brandInteractionDoneButton.Name = "brandInteractionDoneButton";
		this.brandInteractionDoneButton.Size = new System.Drawing.Size(75, 23);
		this.brandInteractionDoneButton.TabIndex = 2;
		this.brandInteractionDoneButton.Text = "Done";
		this.brandInteractionDoneButton.UseVisualStyleBackColor = true;
		this.brandInteractionDoneButton.Click += new System.EventHandler(brandInteractionDoneButton_Click);
		this.label35.AutoSize = true;
		this.label35.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label35.Location = new System.Drawing.Point(42, 79);
		this.label35.Name = "label35";
		this.label35.Size = new System.Drawing.Size(36, 13);
		this.label35.TabIndex = 0;
		this.label35.Text = "Series";
		this.groupBox9.Controls.Add(this.brandIneractionNameTextbox);
		this.groupBox9.Controls.Add(this.label31);
		this.groupBox9.Controls.Add(this.label23);
		this.groupBox9.Controls.Add(this.brandInteractionSKURadiobox);
		this.groupBox9.Controls.Add(this.brandInteractionGroupRadiobox);
		this.groupBox9.Controls.Add(this.brandInteractionConfigSKUButton);
		this.groupBox9.Controls.Add(this.brandInteractionGroupTypeCombobox);
		this.groupBox9.Controls.Add(this.label22);
		this.groupBox9.Location = new System.Drawing.Point(6, 9);
		this.groupBox9.Name = "groupBox9";
		this.groupBox9.Size = new System.Drawing.Size(332, 132);
		this.groupBox9.TabIndex = 9;
		this.groupBox9.TabStop = false;
		this.groupBox9.Text = "Cross Brand Interaction Global Configuration";
		this.brandIneractionNameTextbox.Location = new System.Drawing.Point(79, 73);
		this.brandIneractionNameTextbox.Name = "brandIneractionNameTextbox";
		this.brandIneractionNameTextbox.Size = new System.Drawing.Size(247, 20);
		this.brandIneractionNameTextbox.TabIndex = 49;
		this.label31.AutoSize = true;
		this.label31.Location = new System.Drawing.Point(38, 76);
		this.label31.Name = "label31";
		this.label31.Size = new System.Drawing.Size(38, 13);
		this.label31.TabIndex = 48;
		this.label31.Text = "Name:";
		this.label23.AutoSize = true;
		this.label23.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label23.Location = new System.Drawing.Point(34, 25);
		this.label23.Name = "label23";
		this.label23.Size = new System.Drawing.Size(42, 13);
		this.label23.TabIndex = 42;
		this.label23.Text = "Output:";
		this.brandInteractionSKURadiobox.AutoSize = true;
		this.brandInteractionSKURadiobox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.brandInteractionSKURadiobox.Location = new System.Drawing.Point(243, 23);
		this.brandInteractionSKURadiobox.Name = "brandInteractionSKURadiobox";
		this.brandInteractionSKURadiobox.Size = new System.Drawing.Size(54, 17);
		this.brandInteractionSKURadiobox.TabIndex = 41;
		this.brandInteractionSKURadiobox.TabStop = true;
		this.brandInteractionSKURadiobox.Text = "SKU's";
		this.brandInteractionSKURadiobox.UseVisualStyleBackColor = true;
		this.brandInteractionSKURadiobox.CheckedChanged += new System.EventHandler(brandInteractionSKURadionbox_CheckedChanged);
		this.brandInteractionGroupRadiobox.AutoSize = true;
		this.brandInteractionGroupRadiobox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.brandInteractionGroupRadiobox.Location = new System.Drawing.Point(106, 23);
		this.brandInteractionGroupRadiobox.Name = "brandInteractionGroupRadiobox";
		this.brandInteractionGroupRadiobox.Size = new System.Drawing.Size(59, 17);
		this.brandInteractionGroupRadiobox.TabIndex = 40;
		this.brandInteractionGroupRadiobox.TabStop = true;
		this.brandInteractionGroupRadiobox.Text = "Groups";
		this.brandInteractionGroupRadiobox.UseVisualStyleBackColor = true;
		this.brandInteractionGroupRadiobox.CheckedChanged += new System.EventHandler(brandInteractionGroupRadiobox_CheckedChanged);
		this.brandInteractionConfigSKUButton.ForeColor = System.Drawing.SystemColors.ControlText;
		this.brandInteractionConfigSKUButton.Location = new System.Drawing.Point(251, 99);
		this.brandInteractionConfigSKUButton.Name = "brandInteractionConfigSKUButton";
		this.brandInteractionConfigSKUButton.Size = new System.Drawing.Size(75, 23);
		this.brandInteractionConfigSKUButton.TabIndex = 39;
		this.brandInteractionConfigSKUButton.Text = "Configure";
		this.brandInteractionConfigSKUButton.UseVisualStyleBackColor = true;
		this.brandInteractionConfigSKUButton.Click += new System.EventHandler(configSKUButton_Click);
		this.brandInteractionGroupTypeCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.brandInteractionGroupTypeCombobox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.brandInteractionGroupTypeCombobox.FormattingEnabled = true;
		this.brandInteractionGroupTypeCombobox.Location = new System.Drawing.Point(79, 46);
		this.brandInteractionGroupTypeCombobox.Name = "brandInteractionGroupTypeCombobox";
		this.brandInteractionGroupTypeCombobox.Size = new System.Drawing.Size(247, 21);
		this.brandInteractionGroupTypeCombobox.TabIndex = 36;
		this.brandInteractionGroupTypeCombobox.SelectedIndexChanged += new System.EventHandler(brandInteractionGroupTypeCombobox_SelectedIndexChanged);
		this.label22.AutoSize = true;
		this.label22.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label22.Location = new System.Drawing.Point(10, 49);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(66, 13);
		this.label22.TabIndex = 35;
		this.label22.Text = "Group Type:";
		chartArea6.AxisX.IsLabelAutoFit = false;
		chartArea6.AxisX.LabelStyle.ForeColor = System.Drawing.Color.DimGray;
		chartArea6.AxisX.LabelStyle.IsEndLabelVisible = false;
		chartArea6.AxisX.LineColor = System.Drawing.Color.DimGray;
		chartArea6.AxisX.MajorGrid.Enabled = false;
		chartArea6.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
		chartArea6.AxisX.MajorTickMark.Enabled = false;
		chartArea6.AxisX2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
		chartArea6.AxisY.Crossing = 0.0;
		chartArea6.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
		chartArea6.AxisY.IsStartedFromZero = false;
		chartArea6.AxisY.LabelStyle.ForeColor = System.Drawing.Color.DimGray;
		chartArea6.AxisY.LineColor = System.Drawing.Color.DimGray;
		chartArea6.AxisY.MajorGrid.Enabled = false;
		chartArea6.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
		chartArea6.AxisY.ScaleBreakStyle.BreakLineStyle = System.Windows.Forms.DataVisualization.Charting.BreakLineStyle.None;
		chartArea6.Name = "ChartArea1";
		this.chart5.ChartAreas.Add(chartArea6);
		this.chart5.Enabled = false;
		legend3.Alignment = System.Drawing.StringAlignment.Center;
		legend3.Name = "Legend1";
		this.chart5.Legends.Add(legend3);
		this.chart5.Location = new System.Drawing.Point(347, 6);
		this.chart5.Name = "chart5";
		series6.ChartArea = "ChartArea1";
		series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
		series6.LabelForeColor = System.Drawing.Color.DarkGray;
		series6.Legend = "Legend1";
		series6.Name = "Series1";
		dataPoint20.IsEmpty = true;
		series6.Points.Add(dataPoint20);
		this.chart5.Series.Add(series6);
		this.chart5.Size = new System.Drawing.Size(434, 410);
		this.chart5.TabIndex = 8;
		this.chart5.Text = "chart5";
		title6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		title6.ForeColor = System.Drawing.Color.SteelBlue;
		title6.Name = "Title1";
		title6.Text = "Brand Interaction";
		this.chart5.Titles.Add(title6);
		this.tabPage4.BackColor = System.Drawing.Color.White;
		this.tabPage4.Controls.Add(this.textBox4);
		this.tabPage4.Controls.Add(this.chart6);
		this.tabPage4.Controls.Add(this.groupBox19);
		this.tabPage4.Controls.Add(this.groupBox11);
		this.tabPage4.Location = new System.Drawing.Point(4, 22);
		this.tabPage4.Name = "tabPage4";
		this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage4.Size = new System.Drawing.Size(784, 422);
		this.tabPage4.TabIndex = 3;
		this.tabPage4.Text = "SOM Vs. SOP";
		this.textBox4.BackColor = System.Drawing.Color.Gray;
		this.textBox4.Enabled = false;
		this.textBox4.Location = new System.Drawing.Point(344, 8);
		this.textBox4.Multiline = true;
		this.textBox4.Name = "textBox4";
		this.textBox4.Size = new System.Drawing.Size(1, 410);
		this.textBox4.TabIndex = 44;
		chartArea7.AxisX.IsLabelAutoFit = false;
		chartArea7.AxisX.LabelStyle.ForeColor = System.Drawing.Color.DimGray;
		chartArea7.AxisX.LineColor = System.Drawing.Color.DimGray;
		chartArea7.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
		chartArea7.AxisY.LabelStyle.ForeColor = System.Drawing.Color.DimGray;
		chartArea7.AxisY.LabelStyle.IsEndLabelVisible = false;
		chartArea7.AxisY.LineColor = System.Drawing.Color.DimGray;
		chartArea7.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
		chartArea7.Name = "ChartArea1";
		this.chart6.ChartAreas.Add(chartArea7);
		this.chart6.Enabled = false;
		legend4.Alignment = System.Drawing.StringAlignment.Center;
		legend4.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
		legend4.Name = "Legend1";
		this.chart6.Legends.Add(legend4);
		this.chart6.Location = new System.Drawing.Point(347, 6);
		this.chart6.Name = "chart6";
		series7.ChartArea = "ChartArea1";
		series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
		series7.LabelForeColor = System.Drawing.Color.DarkGray;
		series7.Legend = "Legend1";
		series7.Name = "Series1";
		dataPoint21.IsEmpty = true;
		dataPoint22.IsEmpty = true;
		dataPoint23.IsEmpty = true;
		dataPoint24.IsEmpty = true;
		series7.Points.Add(dataPoint21);
		series7.Points.Add(dataPoint22);
		series7.Points.Add(dataPoint23);
		series7.Points.Add(dataPoint24);
		this.chart6.Series.Add(series7);
		this.chart6.Size = new System.Drawing.Size(431, 410);
		this.chart6.TabIndex = 43;
		this.chart6.Text = "chart6";
		title7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		title7.ForeColor = System.Drawing.Color.SteelBlue;
		title7.Name = "Title1";
		title7.Text = "SOM Vs. SOP";
		this.chart6.Titles.Add(title7);
		this.groupBox19.Controls.Add(this.wdCautionMessageLabel);
		this.groupBox19.Controls.Add(this.somsopDropdownButton);
		this.groupBox19.Controls.Add(this.somsopNameTextbox);
		this.groupBox19.Controls.Add(this.label39);
		this.groupBox19.Controls.Add(this.somsopScenariosLabel);
		this.groupBox19.Controls.Add(this.label28);
		this.groupBox19.Controls.Add(this.somsopSKURadiobox);
		this.groupBox19.Controls.Add(this.somsopGroupRadiobox);
		this.groupBox19.Controls.Add(this.somsopConfigSKUButton);
		this.groupBox19.Controls.Add(this.somsopGroupTypeCombobox);
		this.groupBox19.Controls.Add(this.label29);
		this.groupBox19.Location = new System.Drawing.Point(6, 9);
		this.groupBox19.Name = "groupBox19";
		this.groupBox19.Size = new System.Drawing.Size(332, 164);
		this.groupBox19.TabIndex = 14;
		this.groupBox19.TabStop = false;
		this.groupBox19.Text = "SOM Vs. SOP Global Configuration";
		this.wdCautionMessageLabel.ForeColor = System.Drawing.Color.Red;
		this.wdCautionMessageLabel.Location = new System.Drawing.Point(30, 131);
		this.wdCautionMessageLabel.Name = "wdCautionMessageLabel";
		this.wdCautionMessageLabel.Size = new System.Drawing.Size(281, 27);
		this.wdCautionMessageLabel.TabIndex = 64;
		this.wdCautionMessageLabel.Text = "Please be cautious when using WD in this chart as it needs validation";
		this.wdCautionMessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.somsopDropdownButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
		this.somsopDropdownButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.somsopDropdownButton.ForeColor = System.Drawing.SystemColors.ControlText;
		this.somsopDropdownButton.Image = MarketSimulator.Reports.Properties.Resources.downArrow;
		this.somsopDropdownButton.Location = new System.Drawing.Point(310, 101);
		this.somsopDropdownButton.Name = "somsopDropdownButton";
		this.somsopDropdownButton.Size = new System.Drawing.Size(17, 21);
		this.somsopDropdownButton.TabIndex = 62;
		this.somsopDropdownButton.UseVisualStyleBackColor = true;
		this.somsopDropdownButton.Click += new System.EventHandler(scenariosDropdownButton_Click);
		this.somsopNameTextbox.Location = new System.Drawing.Point(79, 73);
		this.somsopNameTextbox.Name = "somsopNameTextbox";
		this.somsopNameTextbox.Size = new System.Drawing.Size(247, 20);
		this.somsopNameTextbox.TabIndex = 49;
		this.label39.AutoSize = true;
		this.label39.Location = new System.Drawing.Point(38, 76);
		this.label39.Name = "label39";
		this.label39.Size = new System.Drawing.Size(38, 13);
		this.label39.TabIndex = 48;
		this.label39.Text = "Name:";
		this.somsopScenariosLabel.Location = new System.Drawing.Point(14, 97);
		this.somsopScenariosLabel.Margin = new System.Windows.Forms.Padding(0);
		this.somsopScenariosLabel.Name = "somsopScenariosLabel";
		this.somsopScenariosLabel.Size = new System.Drawing.Size(234, 32);
		this.somsopScenariosLabel.TabIndex = 63;
		this.somsopScenariosLabel.Text = "Current Scenario is set to Default Scenario";
		this.somsopScenariosLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label28.AutoSize = true;
		this.label28.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label28.Location = new System.Drawing.Point(34, 25);
		this.label28.Name = "label28";
		this.label28.Size = new System.Drawing.Size(42, 13);
		this.label28.TabIndex = 42;
		this.label28.Text = "Output:";
		this.somsopSKURadiobox.AutoSize = true;
		this.somsopSKURadiobox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.somsopSKURadiobox.Location = new System.Drawing.Point(243, 23);
		this.somsopSKURadiobox.Name = "somsopSKURadiobox";
		this.somsopSKURadiobox.Size = new System.Drawing.Size(54, 17);
		this.somsopSKURadiobox.TabIndex = 41;
		this.somsopSKURadiobox.TabStop = true;
		this.somsopSKURadiobox.Text = "SKU's";
		this.somsopSKURadiobox.UseVisualStyleBackColor = true;
		this.somsopSKURadiobox.CheckedChanged += new System.EventHandler(somsopSKURadiobox_CheckedChanged);
		this.somsopGroupRadiobox.AutoSize = true;
		this.somsopGroupRadiobox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.somsopGroupRadiobox.Location = new System.Drawing.Point(106, 23);
		this.somsopGroupRadiobox.Name = "somsopGroupRadiobox";
		this.somsopGroupRadiobox.Size = new System.Drawing.Size(59, 17);
		this.somsopGroupRadiobox.TabIndex = 40;
		this.somsopGroupRadiobox.TabStop = true;
		this.somsopGroupRadiobox.Text = "Groups";
		this.somsopGroupRadiobox.UseVisualStyleBackColor = true;
		this.somsopGroupRadiobox.CheckedChanged += new System.EventHandler(somsopGroupRadiobox_CheckedChanged);
		this.somsopConfigSKUButton.ForeColor = System.Drawing.SystemColors.ControlText;
		this.somsopConfigSKUButton.Location = new System.Drawing.Point(251, 100);
		this.somsopConfigSKUButton.Name = "somsopConfigSKUButton";
		this.somsopConfigSKUButton.Size = new System.Drawing.Size(75, 23);
		this.somsopConfigSKUButton.TabIndex = 39;
		this.somsopConfigSKUButton.Text = "Configure";
		this.somsopConfigSKUButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.somsopConfigSKUButton.UseVisualStyleBackColor = true;
		this.somsopConfigSKUButton.Click += new System.EventHandler(configSKUButton_Click);
		this.somsopGroupTypeCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.somsopGroupTypeCombobox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.somsopGroupTypeCombobox.FormattingEnabled = true;
		this.somsopGroupTypeCombobox.Location = new System.Drawing.Point(79, 46);
		this.somsopGroupTypeCombobox.Name = "somsopGroupTypeCombobox";
		this.somsopGroupTypeCombobox.Size = new System.Drawing.Size(247, 21);
		this.somsopGroupTypeCombobox.TabIndex = 36;
		this.somsopGroupTypeCombobox.SelectedIndexChanged += new System.EventHandler(somsopGroupTypeCombobox_SelectedIndexChanged);
		this.label29.AutoSize = true;
		this.label29.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label29.Location = new System.Drawing.Point(10, 49);
		this.label29.Name = "label29";
		this.label29.Size = new System.Drawing.Size(66, 13);
		this.label29.TabIndex = 35;
		this.label29.Text = "Group Type:";
		this.groupBox11.Controls.Add(this.somsopSeriesCombobox);
		this.groupBox11.Controls.Add(this.somsopDoneButton);
		this.groupBox11.Controls.Add(this.label36);
		this.groupBox11.Location = new System.Drawing.Point(6, 178);
		this.groupBox11.Name = "groupBox11";
		this.groupBox11.Size = new System.Drawing.Size(332, 87);
		this.groupBox11.TabIndex = 13;
		this.groupBox11.TabStop = false;
		this.groupBox11.Text = "Chart Output Series";
		checkBoxProperties11.ForeColor = System.Drawing.SystemColors.ControlText;
		this.somsopSeriesCombobox.CheckBoxProperties = checkBoxProperties11;
		this.somsopSeriesCombobox.DisplayMemberSingleItem = "";
		this.somsopSeriesCombobox.FormattingEnabled = true;
		this.somsopSeriesCombobox.Location = new System.Drawing.Point(79, 23);
		this.somsopSeriesCombobox.Name = "somsopSeriesCombobox";
		this.somsopSeriesCombobox.Size = new System.Drawing.Size(247, 21);
		this.somsopSeriesCombobox.TabIndex = 3;
		this.somsopSeriesCombobox.CheckBoxCheckedChanged += new System.EventHandler(somsopSeriesCombobox_CheckBoxCheckedChanged);
		this.somsopDoneButton.ForeColor = System.Drawing.SystemColors.ControlText;
		this.somsopDoneButton.Location = new System.Drawing.Point(129, 54);
		this.somsopDoneButton.Name = "somsopDoneButton";
		this.somsopDoneButton.Size = new System.Drawing.Size(75, 23);
		this.somsopDoneButton.TabIndex = 2;
		this.somsopDoneButton.Text = "Done";
		this.somsopDoneButton.UseVisualStyleBackColor = true;
		this.somsopDoneButton.Click += new System.EventHandler(somsopDoneButton_Click);
		this.label36.AutoSize = true;
		this.label36.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label36.Location = new System.Drawing.Point(37, 26);
		this.label36.Name = "label36";
		this.label36.Size = new System.Drawing.Size(36, 13);
		this.label36.TabIndex = 0;
		this.label36.Text = "Series";
		this.tabPage6.BackColor = System.Drawing.Color.White;
		this.tabPage6.Controls.Add(this.groupBox23);
		this.tabPage6.Controls.Add(this.textBox6);
		this.tabPage6.Controls.Add(this.groupBox14);
		this.tabPage6.Controls.Add(this.chart8);
		this.tabPage6.Location = new System.Drawing.Point(4, 22);
		this.tabPage6.Name = "tabPage6";
		this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage6.Size = new System.Drawing.Size(784, 422);
		this.tabPage6.TabIndex = 5;
		this.tabPage6.Text = "Fair Share";
		this.groupBox23.Controls.Add(this.fairShareScenarioCombobox);
		this.groupBox23.Controls.Add(this.fairShareDoneButton);
		this.groupBox23.Controls.Add(this.fairShareSeriesCombobox);
		this.groupBox23.Controls.Add(this.label48);
		this.groupBox23.Controls.Add(this.label49);
		this.groupBox23.Controls.Add(this.fairShareBasicScenarioCombobox);
		this.groupBox23.Controls.Add(this.label50);
		this.groupBox23.Location = new System.Drawing.Point(6, 197);
		this.groupBox23.Name = "groupBox23";
		this.groupBox23.Size = new System.Drawing.Size(332, 137);
		this.groupBox23.TabIndex = 22;
		this.groupBox23.TabStop = false;
		this.groupBox23.Text = "Chart Output Series";
		this.fairShareScenarioCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.fairShareScenarioCombobox.FormattingEnabled = true;
		this.fairShareScenarioCombobox.Items.AddRange(new object[1] { "Current" });
		this.fairShareScenarioCombobox.Location = new System.Drawing.Point(79, 53);
		this.fairShareScenarioCombobox.Name = "fairShareScenarioCombobox";
		this.fairShareScenarioCombobox.Size = new System.Drawing.Size(247, 21);
		this.fairShareScenarioCombobox.TabIndex = 7;
		this.fairShareScenarioCombobox.Enter += new System.EventHandler(fairShareScenarioCombobox_Enter);
		this.fairShareDoneButton.ForeColor = System.Drawing.SystemColors.ControlText;
		this.fairShareDoneButton.Location = new System.Drawing.Point(129, 107);
		this.fairShareDoneButton.Name = "fairShareDoneButton";
		this.fairShareDoneButton.Size = new System.Drawing.Size(75, 23);
		this.fairShareDoneButton.TabIndex = 2;
		this.fairShareDoneButton.Text = "Done";
		this.fairShareDoneButton.UseVisualStyleBackColor = true;
		this.fairShareDoneButton.Click += new System.EventHandler(fairShareDoneButton_Click);
		checkBoxProperties12.ForeColor = System.Drawing.SystemColors.ControlText;
		this.fairShareSeriesCombobox.CheckBoxProperties = checkBoxProperties12;
		this.fairShareSeriesCombobox.DisplayMemberSingleItem = "";
		this.fairShareSeriesCombobox.FormattingEnabled = true;
		this.fairShareSeriesCombobox.Location = new System.Drawing.Point(79, 80);
		this.fairShareSeriesCombobox.Name = "fairShareSeriesCombobox";
		this.fairShareSeriesCombobox.Size = new System.Drawing.Size(247, 21);
		this.fairShareSeriesCombobox.TabIndex = 3;
		this.fairShareSeriesCombobox.CheckBoxCheckedChanged += new System.EventHandler(fairShareSeriesCombobox_CheckBoxCheckedChanged);
		this.label48.AutoSize = true;
		this.label48.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label48.Location = new System.Drawing.Point(24, 56);
		this.label48.Name = "label48";
		this.label48.Size = new System.Drawing.Size(54, 13);
		this.label48.TabIndex = 6;
		this.label48.Text = "Scenarios";
		this.label49.AutoSize = true;
		this.label49.Location = new System.Drawing.Point(1, 29);
		this.label49.Name = "label49";
		this.label49.Size = new System.Drawing.Size(78, 13);
		this.label49.TabIndex = 5;
		this.label49.Text = "Basic Scenario";
		this.fairShareBasicScenarioCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.fairShareBasicScenarioCombobox.FormattingEnabled = true;
		this.fairShareBasicScenarioCombobox.Items.AddRange(new object[1] { "Default" });
		this.fairShareBasicScenarioCombobox.Location = new System.Drawing.Point(79, 26);
		this.fairShareBasicScenarioCombobox.Name = "fairShareBasicScenarioCombobox";
		this.fairShareBasicScenarioCombobox.Size = new System.Drawing.Size(247, 21);
		this.fairShareBasicScenarioCombobox.TabIndex = 4;
		this.fairShareBasicScenarioCombobox.Enter += new System.EventHandler(basicScenarioCombobox_Enter);
		this.label50.AutoSize = true;
		this.label50.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label50.Location = new System.Drawing.Point(42, 83);
		this.label50.Name = "label50";
		this.label50.Size = new System.Drawing.Size(36, 13);
		this.label50.TabIndex = 0;
		this.label50.Text = "Series";
		this.textBox6.BackColor = System.Drawing.Color.Gray;
		this.textBox6.Enabled = false;
		this.textBox6.Location = new System.Drawing.Point(344, 8);
		this.textBox6.Multiline = true;
		this.textBox6.Name = "textBox6";
		this.textBox6.Size = new System.Drawing.Size(1, 410);
		this.textBox6.TabIndex = 21;
		this.groupBox14.Controls.Add(this.fairShareTextbox);
		this.groupBox14.Controls.Add(this.label41);
		this.groupBox14.Controls.Add(this.fairshareChangeCombobox);
		this.groupBox14.Controls.Add(this.label20);
		this.groupBox14.Controls.Add(this.fairSharAttributeCombobox);
		this.groupBox14.Controls.Add(this.label19);
		this.groupBox14.Controls.Add(this.label32);
		this.groupBox14.Controls.Add(this.fairShareSKURadiobox);
		this.groupBox14.Controls.Add(this.fairShareGroupRadiobox);
		this.groupBox14.Controls.Add(this.fairShareConfigSKUButton);
		this.groupBox14.Controls.Add(this.fairShareGroupTypeCombobox);
		this.groupBox14.Controls.Add(this.label33);
		this.groupBox14.Location = new System.Drawing.Point(6, 9);
		this.groupBox14.Name = "groupBox14";
		this.groupBox14.Size = new System.Drawing.Size(332, 182);
		this.groupBox14.TabIndex = 20;
		this.groupBox14.TabStop = false;
		this.groupBox14.Text = "Fair Share Global Configuration";
		this.fairShareTextbox.Location = new System.Drawing.Point(79, 125);
		this.fairShareTextbox.Name = "fairShareTextbox";
		this.fairShareTextbox.Size = new System.Drawing.Size(247, 20);
		this.fairShareTextbox.TabIndex = 49;
		this.label41.AutoSize = true;
		this.label41.Location = new System.Drawing.Point(38, 128);
		this.label41.Name = "label41";
		this.label41.Size = new System.Drawing.Size(38, 13);
		this.label41.TabIndex = 48;
		this.label41.Text = "Name:";
		this.fairshareChangeCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.fairshareChangeCombobox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.fairshareChangeCombobox.FormattingEnabled = true;
		this.fairshareChangeCombobox.Location = new System.Drawing.Point(79, 99);
		this.fairshareChangeCombobox.Name = "fairshareChangeCombobox";
		this.fairshareChangeCombobox.Size = new System.Drawing.Size(247, 21);
		this.fairshareChangeCombobox.TabIndex = 44;
		this.label20.AutoSize = true;
		this.label20.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label20.Location = new System.Drawing.Point(29, 102);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(47, 13);
		this.label20.TabIndex = 43;
		this.label20.Text = "Change:";
		this.fairSharAttributeCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.fairSharAttributeCombobox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.fairSharAttributeCombobox.FormattingEnabled = true;
		this.fairSharAttributeCombobox.Location = new System.Drawing.Point(79, 72);
		this.fairSharAttributeCombobox.Name = "fairSharAttributeCombobox";
		this.fairSharAttributeCombobox.Size = new System.Drawing.Size(247, 21);
		this.fairSharAttributeCombobox.TabIndex = 44;
		this.label19.AutoSize = true;
		this.label19.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label19.Location = new System.Drawing.Point(27, 75);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(49, 13);
		this.label19.TabIndex = 43;
		this.label19.Text = "Attribute:";
		this.label32.AutoSize = true;
		this.label32.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label32.Location = new System.Drawing.Point(34, 25);
		this.label32.Name = "label32";
		this.label32.Size = new System.Drawing.Size(42, 13);
		this.label32.TabIndex = 42;
		this.label32.Text = "Output:";
		this.fairShareSKURadiobox.AutoSize = true;
		this.fairShareSKURadiobox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.fairShareSKURadiobox.Location = new System.Drawing.Point(243, 23);
		this.fairShareSKURadiobox.Name = "fairShareSKURadiobox";
		this.fairShareSKURadiobox.Size = new System.Drawing.Size(54, 17);
		this.fairShareSKURadiobox.TabIndex = 41;
		this.fairShareSKURadiobox.TabStop = true;
		this.fairShareSKURadiobox.Text = "SKU's";
		this.fairShareSKURadiobox.UseVisualStyleBackColor = true;
		this.fairShareSKURadiobox.CheckedChanged += new System.EventHandler(fairShareSKURadiobox_CheckedChanged);
		this.fairShareGroupRadiobox.AutoSize = true;
		this.fairShareGroupRadiobox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.fairShareGroupRadiobox.Location = new System.Drawing.Point(106, 23);
		this.fairShareGroupRadiobox.Name = "fairShareGroupRadiobox";
		this.fairShareGroupRadiobox.Size = new System.Drawing.Size(59, 17);
		this.fairShareGroupRadiobox.TabIndex = 40;
		this.fairShareGroupRadiobox.TabStop = true;
		this.fairShareGroupRadiobox.Text = "Groups";
		this.fairShareGroupRadiobox.UseVisualStyleBackColor = true;
		this.fairShareGroupRadiobox.CheckedChanged += new System.EventHandler(fairShareGroupRadiobox_CheckedChanged);
		this.fairShareConfigSKUButton.ForeColor = System.Drawing.SystemColors.ControlText;
		this.fairShareConfigSKUButton.Location = new System.Drawing.Point(251, 152);
		this.fairShareConfigSKUButton.Name = "fairShareConfigSKUButton";
		this.fairShareConfigSKUButton.Size = new System.Drawing.Size(75, 23);
		this.fairShareConfigSKUButton.TabIndex = 39;
		this.fairShareConfigSKUButton.Text = "Configure";
		this.fairShareConfigSKUButton.UseVisualStyleBackColor = true;
		this.fairShareConfigSKUButton.Click += new System.EventHandler(configSKUButton_Click);
		this.fairShareGroupTypeCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.fairShareGroupTypeCombobox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.fairShareGroupTypeCombobox.FormattingEnabled = true;
		this.fairShareGroupTypeCombobox.Location = new System.Drawing.Point(79, 46);
		this.fairShareGroupTypeCombobox.Name = "fairShareGroupTypeCombobox";
		this.fairShareGroupTypeCombobox.Size = new System.Drawing.Size(247, 21);
		this.fairShareGroupTypeCombobox.TabIndex = 36;
		this.fairShareGroupTypeCombobox.SelectedIndexChanged += new System.EventHandler(fairShareGroupTypeCombobox_SelectedIndexChanged);
		this.label33.AutoSize = true;
		this.label33.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label33.Location = new System.Drawing.Point(10, 49);
		this.label33.Name = "label33";
		this.label33.Size = new System.Drawing.Size(66, 13);
		this.label33.TabIndex = 35;
		this.label33.Text = "Group Type:";
		chartArea8.AxisX.Crossing = 1.0;
		chartArea8.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
		chartArea8.AxisX.IsLabelAutoFit = false;
		chartArea8.AxisX.LabelStyle.ForeColor = System.Drawing.Color.DimGray;
		chartArea8.AxisX.LabelStyle.IsEndLabelVisible = false;
		chartArea8.AxisX.LabelStyle.TruncatedLabels = true;
		chartArea8.AxisX.LineColor = System.Drawing.Color.DimGray;
		chartArea8.AxisX.MajorGrid.Enabled = false;
		chartArea8.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
		chartArea8.AxisX2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
		chartArea8.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
		chartArea8.AxisY.LabelStyle.ForeColor = System.Drawing.Color.DimGray;
		chartArea8.AxisY.LabelStyle.IsEndLabelVisible = false;
		chartArea8.AxisY.LineColor = System.Drawing.Color.DimGray;
		chartArea8.AxisY.MajorGrid.Enabled = false;
		chartArea8.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
		chartArea8.AxisY.MajorTickMark.TickMarkStyle = System.Windows.Forms.DataVisualization.Charting.TickMarkStyle.InsideArea;
		chartArea8.AxisY.ScaleBreakStyle.BreakLineStyle = System.Windows.Forms.DataVisualization.Charting.BreakLineStyle.None;
		chartArea8.Name = "ChartArea1";
		this.chart8.ChartAreas.Add(chartArea8);
		this.chart8.Enabled = false;
		legend5.Alignment = System.Drawing.StringAlignment.Center;
		legend5.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
		legend5.Name = "Legend1";
		this.chart8.Legends.Add(legend5);
		this.chart8.Location = new System.Drawing.Point(347, 6);
		this.chart8.Name = "chart8";
		series8.ChartArea = "ChartArea1";
		series8.LabelForeColor = System.Drawing.Color.DarkGray;
		series8.Legend = "Legend1";
		series8.Name = "Series1";
		dataPoint25.IsEmpty = true;
		series8.Points.Add(dataPoint25);
		this.chart8.Series.Add(series8);
		this.chart8.Size = new System.Drawing.Size(434, 410);
		this.chart8.TabIndex = 17;
		this.chart8.Text = "chart8";
		title8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		title8.ForeColor = System.Drawing.Color.SteelBlue;
		title8.Name = "Title1";
		title8.Text = "Fair Share";
		this.chart8.Titles.Add(title8);
		this.tabPage7.BackColor = System.Drawing.Color.White;
		this.tabPage7.Controls.Add(this.groupBox22);
		this.tabPage7.Controls.Add(this.textBox7);
		this.tabPage7.Controls.Add(this.groupBox1);
		this.tabPage7.Controls.Add(this.groupBox16);
		this.tabPage7.Controls.Add(this.chart9);
		this.tabPage7.Location = new System.Drawing.Point(4, 22);
		this.tabPage7.Name = "tabPage7";
		this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage7.Size = new System.Drawing.Size(784, 422);
		this.tabPage7.TabIndex = 6;
		this.tabPage7.Text = "Validation";
		this.groupBox22.Controls.Add(this.validationByMonthRadioButton);
		this.groupBox22.Controls.Add(this.validationByBrandRadiobutton);
		this.groupBox22.Location = new System.Drawing.Point(6, 8);
		this.groupBox22.Name = "groupBox22";
		this.groupBox22.Size = new System.Drawing.Size(332, 53);
		this.groupBox22.TabIndex = 26;
		this.groupBox22.TabStop = false;
		this.groupBox22.Text = "Calculate Validation Operation By";
		this.validationByMonthRadioButton.AutoSize = true;
		this.validationByMonthRadioButton.Location = new System.Drawing.Point(237, 23);
		this.validationByMonthRadioButton.Name = "validationByMonthRadioButton";
		this.validationByMonthRadioButton.Size = new System.Drawing.Size(60, 17);
		this.validationByMonthRadioButton.TabIndex = 1;
		this.validationByMonthRadioButton.Text = "Months";
		this.validationByMonthRadioButton.UseVisualStyleBackColor = true;
		this.validationByBrandRadiobutton.AutoSize = true;
		this.validationByBrandRadiobutton.Checked = true;
		this.validationByBrandRadiobutton.Location = new System.Drawing.Point(37, 23);
		this.validationByBrandRadiobutton.Name = "validationByBrandRadiobutton";
		this.validationByBrandRadiobutton.Size = new System.Drawing.Size(53, 17);
		this.validationByBrandRadiobutton.TabIndex = 0;
		this.validationByBrandRadiobutton.TabStop = true;
		this.validationByBrandRadiobutton.Text = "Brand";
		this.validationByBrandRadiobutton.UseVisualStyleBackColor = true;
		this.textBox7.BackColor = System.Drawing.Color.Gray;
		this.textBox7.Enabled = false;
		this.textBox7.Location = new System.Drawing.Point(344, 8);
		this.textBox7.Multiline = true;
		this.textBox7.Name = "textBox7";
		this.textBox7.Size = new System.Drawing.Size(1, 410);
		this.textBox7.TabIndex = 25;
		this.groupBox1.Controls.Add(this.validationScenarioCheckboxCombobox);
		this.groupBox1.Controls.Add(this.label44);
		this.groupBox1.Controls.Add(this.validationSeriesCombobox);
		this.groupBox1.Controls.Add(this.validationDoneButton);
		this.groupBox1.Controls.Add(this.label26);
		this.groupBox1.Location = new System.Drawing.Point(6, 181);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(332, 112);
		this.groupBox1.TabIndex = 24;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Chart Output Series";
		checkBoxProperties13.ForeColor = System.Drawing.SystemColors.ControlText;
		this.validationScenarioCheckboxCombobox.CheckBoxProperties = checkBoxProperties13;
		this.validationScenarioCheckboxCombobox.DisplayMemberSingleItem = "";
		this.validationScenarioCheckboxCombobox.FormattingEnabled = true;
		this.validationScenarioCheckboxCombobox.Location = new System.Drawing.Point(79, 23);
		this.validationScenarioCheckboxCombobox.Name = "validationScenarioCheckboxCombobox";
		this.validationScenarioCheckboxCombobox.Size = new System.Drawing.Size(247, 21);
		this.validationScenarioCheckboxCombobox.TabIndex = 5;
		this.validationScenarioCheckboxCombobox.Enter += new System.EventHandler(validationScenarioCheckboxCombobox_Enter);
		this.validationScenarioCheckboxCombobox.CheckBoxCheckedChanged += new System.EventHandler(validationScenarioCheckboxCombobox_CheckBoxCheckedChanged);
		this.label44.AutoSize = true;
		this.label44.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label44.Location = new System.Drawing.Point(19, 26);
		this.label44.Name = "label44";
		this.label44.Size = new System.Drawing.Size(54, 13);
		this.label44.TabIndex = 4;
		this.label44.Text = "Scenarios";
		checkBoxProperties14.ForeColor = System.Drawing.SystemColors.ControlText;
		this.validationSeriesCombobox.CheckBoxProperties = checkBoxProperties14;
		this.validationSeriesCombobox.DisplayMemberSingleItem = "";
		this.validationSeriesCombobox.FormattingEnabled = true;
		this.validationSeriesCombobox.Location = new System.Drawing.Point(79, 50);
		this.validationSeriesCombobox.Name = "validationSeriesCombobox";
		this.validationSeriesCombobox.Size = new System.Drawing.Size(247, 21);
		this.validationSeriesCombobox.TabIndex = 3;
		this.validationSeriesCombobox.CheckBoxCheckedChanged += new System.EventHandler(validationSeriesCombobox_CheckBoxCheckedChanged);
		this.validationDoneButton.ForeColor = System.Drawing.SystemColors.ControlText;
		this.validationDoneButton.Location = new System.Drawing.Point(129, 81);
		this.validationDoneButton.Name = "validationDoneButton";
		this.validationDoneButton.Size = new System.Drawing.Size(75, 23);
		this.validationDoneButton.TabIndex = 2;
		this.validationDoneButton.Text = "Done";
		this.validationDoneButton.UseVisualStyleBackColor = true;
		this.validationDoneButton.Click += new System.EventHandler(validationDoneButton_Click);
		this.label26.AutoSize = true;
		this.label26.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label26.Location = new System.Drawing.Point(37, 53);
		this.label26.Name = "label26";
		this.label26.Size = new System.Drawing.Size(36, 13);
		this.label26.TabIndex = 0;
		this.label26.Text = "Series";
		this.groupBox16.Controls.Add(this.validationConfigScenarioButton);
		this.groupBox16.Controls.Add(this.label34);
		this.groupBox16.Controls.Add(this.validationSKURadiobox);
		this.groupBox16.Controls.Add(this.validationGroupRadiobox);
		this.groupBox16.Controls.Add(this.validationGroupTypeCombobox);
		this.groupBox16.Controls.Add(this.label37);
		this.groupBox16.Location = new System.Drawing.Point(6, 67);
		this.groupBox16.Name = "groupBox16";
		this.groupBox16.Size = new System.Drawing.Size(332, 108);
		this.groupBox16.TabIndex = 23;
		this.groupBox16.TabStop = false;
		this.groupBox16.Text = "Validation Global Configuration";
		this.validationConfigScenarioButton.ForeColor = System.Drawing.SystemColors.ControlText;
		this.validationConfigScenarioButton.Location = new System.Drawing.Point(251, 75);
		this.validationConfigScenarioButton.Name = "validationConfigScenarioButton";
		this.validationConfigScenarioButton.Size = new System.Drawing.Size(75, 23);
		this.validationConfigScenarioButton.TabIndex = 48;
		this.validationConfigScenarioButton.Text = "Configure";
		this.validationConfigScenarioButton.UseVisualStyleBackColor = true;
		this.validationConfigScenarioButton.Click += new System.EventHandler(configSKUButton_Click);
		this.label34.AutoSize = true;
		this.label34.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label34.Location = new System.Drawing.Point(34, 25);
		this.label34.Name = "label34";
		this.label34.Size = new System.Drawing.Size(42, 13);
		this.label34.TabIndex = 42;
		this.label34.Text = "Output:";
		this.validationSKURadiobox.AutoSize = true;
		this.validationSKURadiobox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.validationSKURadiobox.Location = new System.Drawing.Point(243, 23);
		this.validationSKURadiobox.Name = "validationSKURadiobox";
		this.validationSKURadiobox.Size = new System.Drawing.Size(54, 17);
		this.validationSKURadiobox.TabIndex = 41;
		this.validationSKURadiobox.TabStop = true;
		this.validationSKURadiobox.Text = "SKU's";
		this.validationSKURadiobox.UseVisualStyleBackColor = true;
		this.validationSKURadiobox.CheckedChanged += new System.EventHandler(validationSKURadiobox_CheckedChanged);
		this.validationGroupRadiobox.AutoSize = true;
		this.validationGroupRadiobox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.validationGroupRadiobox.Location = new System.Drawing.Point(106, 23);
		this.validationGroupRadiobox.Name = "validationGroupRadiobox";
		this.validationGroupRadiobox.Size = new System.Drawing.Size(59, 17);
		this.validationGroupRadiobox.TabIndex = 40;
		this.validationGroupRadiobox.TabStop = true;
		this.validationGroupRadiobox.Text = "Groups";
		this.validationGroupRadiobox.UseVisualStyleBackColor = true;
		this.validationGroupRadiobox.CheckedChanged += new System.EventHandler(validationGroupRadiobox_CheckedChanged);
		this.validationGroupTypeCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.validationGroupTypeCombobox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.validationGroupTypeCombobox.FormattingEnabled = true;
		this.validationGroupTypeCombobox.Location = new System.Drawing.Point(79, 46);
		this.validationGroupTypeCombobox.Name = "validationGroupTypeCombobox";
		this.validationGroupTypeCombobox.Size = new System.Drawing.Size(247, 21);
		this.validationGroupTypeCombobox.TabIndex = 36;
		this.validationGroupTypeCombobox.SelectedIndexChanged += new System.EventHandler(validationGroupTypeCombobox_SelectedIndexChanged);
		this.label37.AutoSize = true;
		this.label37.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label37.Location = new System.Drawing.Point(10, 49);
		this.label37.Name = "label37";
		this.label37.Size = new System.Drawing.Size(66, 13);
		this.label37.TabIndex = 35;
		this.label37.Text = "Group Type:";
		chartArea9.AxisX.LabelStyle.ForeColor = System.Drawing.Color.DimGray;
		chartArea9.AxisX.LineColor = System.Drawing.Color.DimGray;
		chartArea9.AxisX.MajorGrid.Enabled = false;
		chartArea9.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
		chartArea9.AxisX2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
		chartArea9.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
		chartArea9.AxisY.IsStartedFromZero = false;
		chartArea9.AxisY.LabelStyle.ForeColor = System.Drawing.Color.DimGray;
		chartArea9.AxisY.LineColor = System.Drawing.Color.DimGray;
		chartArea9.AxisY.MajorGrid.Enabled = false;
		chartArea9.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
		chartArea9.AxisY.ScaleBreakStyle.BreakLineStyle = System.Windows.Forms.DataVisualization.Charting.BreakLineStyle.None;
		chartArea9.Name = "ChartArea1";
		this.chart9.ChartAreas.Add(chartArea9);
		this.chart9.Enabled = false;
		legend6.Alignment = System.Drawing.StringAlignment.Center;
		legend6.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
		legend6.Name = "Legend1";
		this.chart9.Legends.Add(legend6);
		this.chart9.Location = new System.Drawing.Point(346, 6);
		this.chart9.Name = "chart9";
		series9.ChartArea = "ChartArea1";
		series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.ErrorBar;
		series9.LabelForeColor = System.Drawing.Color.DarkGray;
		series9.Legend = "Legend1";
		series9.Name = "Series1";
		dataPoint26.IsEmpty = true;
		dataPoint27.IsEmpty = true;
		dataPoint28.IsEmpty = true;
		series9.Points.Add(dataPoint26);
		series9.Points.Add(dataPoint27);
		series9.Points.Add(dataPoint28);
		series9.YValuesPerPoint = 3;
		this.chart9.Series.Add(series9);
		this.chart9.Size = new System.Drawing.Size(434, 410);
		this.chart9.TabIndex = 20;
		this.chart9.Text = "chart9";
		title9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		title9.ForeColor = System.Drawing.Color.SteelBlue;
		title9.Name = "Title1";
		title9.Text = "Validation";
		this.chart9.Titles.Add(title9);
		this.tabPage5.BackColor = System.Drawing.Color.White;
		this.tabPage5.Controls.Add(this.groupBox20);
		this.tabPage5.Controls.Add(this.groupBox13);
		this.tabPage5.Controls.Add(this.textBox5);
		this.tabPage5.Controls.Add(this.groupBox12);
		this.tabPage5.Controls.Add(this.incrementalGainRunButton);
		this.tabPage5.Controls.Add(this.chart7);
		this.tabPage5.Location = new System.Drawing.Point(4, 22);
		this.tabPage5.Name = "tabPage5";
		this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage5.Size = new System.Drawing.Size(784, 422);
		this.tabPage5.TabIndex = 8;
		this.tabPage5.Text = "Introduction";
		this.groupBox20.Controls.Add(this.exclusiveIncrementalGainRadiobutton);
		this.groupBox20.Controls.Add(this.cumulativeIncrementalGainRadiobutton);
		this.groupBox20.Location = new System.Drawing.Point(5, 9);
		this.groupBox20.Name = "groupBox20";
		this.groupBox20.Size = new System.Drawing.Size(332, 55);
		this.groupBox20.TabIndex = 60;
		this.groupBox20.TabStop = false;
		this.groupBox20.Text = "Calculate Introduction Operation In";
		this.exclusiveIncrementalGainRadiobutton.AutoSize = true;
		this.exclusiveIncrementalGainRadiobutton.Location = new System.Drawing.Point(205, 23);
		this.exclusiveIncrementalGainRadiobutton.Name = "exclusiveIncrementalGainRadiobutton";
		this.exclusiveIncrementalGainRadiobutton.Size = new System.Drawing.Size(93, 17);
		this.exclusiveIncrementalGainRadiobutton.TabIndex = 62;
		this.exclusiveIncrementalGainRadiobutton.Text = "Exclusive Run";
		this.exclusiveIncrementalGainRadiobutton.UseVisualStyleBackColor = true;
		this.exclusiveIncrementalGainRadiobutton.CheckedChanged += new System.EventHandler(accumulativeIncrementalGainRadiobutton_CheckedChanged);
		this.cumulativeIncrementalGainRadiobutton.AutoSize = true;
		this.cumulativeIncrementalGainRadiobutton.Checked = true;
		this.cumulativeIncrementalGainRadiobutton.Location = new System.Drawing.Point(35, 23);
		this.cumulativeIncrementalGainRadiobutton.Name = "cumulativeIncrementalGainRadiobutton";
		this.cumulativeIncrementalGainRadiobutton.Size = new System.Drawing.Size(100, 17);
		this.cumulativeIncrementalGainRadiobutton.TabIndex = 61;
		this.cumulativeIncrementalGainRadiobutton.TabStop = true;
		this.cumulativeIncrementalGainRadiobutton.Text = "Cumulative Run";
		this.cumulativeIncrementalGainRadiobutton.UseVisualStyleBackColor = true;
		this.cumulativeIncrementalGainRadiobutton.CheckedChanged += new System.EventHandler(accumulativeIncrementalGainRadiobutton_CheckedChanged);
		this.groupBox13.Controls.Add(this.incrementalGainRevenuesCheckbox);
		this.groupBox13.Controls.Add(this.incrementalGainSharesCheckbox);
		this.groupBox13.Location = new System.Drawing.Point(5, 188);
		this.groupBox13.Name = "groupBox13";
		this.groupBox13.Size = new System.Drawing.Size(332, 54);
		this.groupBox13.TabIndex = 32;
		this.groupBox13.TabStop = false;
		this.groupBox13.Text = "Find Introduction In Terms Of";
		this.incrementalGainRevenuesCheckbox.AutoSize = true;
		this.incrementalGainRevenuesCheckbox.Location = new System.Drawing.Point(35, 23);
		this.incrementalGainRevenuesCheckbox.Name = "incrementalGainRevenuesCheckbox";
		this.incrementalGainRevenuesCheckbox.Size = new System.Drawing.Size(75, 17);
		this.incrementalGainRevenuesCheckbox.TabIndex = 56;
		this.incrementalGainRevenuesCheckbox.Text = "Revenues";
		this.incrementalGainRevenuesCheckbox.UseVisualStyleBackColor = true;
		this.incrementalGainSharesCheckbox.AutoSize = true;
		this.incrementalGainSharesCheckbox.Location = new System.Drawing.Point(205, 23);
		this.incrementalGainSharesCheckbox.Name = "incrementalGainSharesCheckbox";
		this.incrementalGainSharesCheckbox.Size = new System.Drawing.Size(90, 17);
		this.incrementalGainSharesCheckbox.TabIndex = 57;
		this.incrementalGainSharesCheckbox.Text = "Market Share";
		this.incrementalGainSharesCheckbox.UseVisualStyleBackColor = true;
		this.textBox5.BackColor = System.Drawing.Color.Gray;
		this.textBox5.Enabled = false;
		this.textBox5.Location = new System.Drawing.Point(344, 8);
		this.textBox5.Multiline = true;
		this.textBox5.Name = "textBox5";
		this.textBox5.Size = new System.Drawing.Size(1, 410);
		this.textBox5.TabIndex = 31;
		this.groupBox12.Controls.Add(this.incrementalGainDropdownButton);
		this.groupBox12.Controls.Add(this.incrementalGainIncrementalConfigurationButton);
		this.groupBox12.Controls.Add(this.incrementalGainScenarioLabel);
		this.groupBox12.Controls.Add(this.incrementalGainNameTextbox);
		this.groupBox12.Controls.Add(this.label45);
		this.groupBox12.Controls.Add(this.incrementalGainConfigureSKUButton);
		this.groupBox12.Location = new System.Drawing.Point(5, 70);
		this.groupBox12.Name = "groupBox12";
		this.groupBox12.Size = new System.Drawing.Size(332, 112);
		this.groupBox12.TabIndex = 30;
		this.groupBox12.TabStop = false;
		this.groupBox12.Text = "Introduction Global Configuration";
		this.incrementalGainDropdownButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
		this.incrementalGainDropdownButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.incrementalGainDropdownButton.ForeColor = System.Drawing.SystemColors.ControlText;
		this.incrementalGainDropdownButton.Image = MarketSimulator.Reports.Properties.Resources.downArrow;
		this.incrementalGainDropdownButton.Location = new System.Drawing.Point(310, 78);
		this.incrementalGainDropdownButton.Name = "incrementalGainDropdownButton";
		this.incrementalGainDropdownButton.Size = new System.Drawing.Size(17, 21);
		this.incrementalGainDropdownButton.TabIndex = 64;
		this.incrementalGainDropdownButton.UseVisualStyleBackColor = true;
		this.incrementalGainDropdownButton.Click += new System.EventHandler(scenariosDropdownButton_Click);
		this.incrementalGainIncrementalConfigurationButton.Location = new System.Drawing.Point(53, 49);
		this.incrementalGainIncrementalConfigurationButton.Name = "incrementalGainIncrementalConfigurationButton";
		this.incrementalGainIncrementalConfigurationButton.Size = new System.Drawing.Size(226, 22);
		this.incrementalGainIncrementalConfigurationButton.TabIndex = 58;
		this.incrementalGainIncrementalConfigurationButton.Text = "Set Current Line Up && SKU's To Be Added";
		this.incrementalGainIncrementalConfigurationButton.UseVisualStyleBackColor = true;
		this.incrementalGainIncrementalConfigurationButton.Click += new System.EventHandler(IncrementalGain_Click);
		this.incrementalGainScenarioLabel.Location = new System.Drawing.Point(14, 74);
		this.incrementalGainScenarioLabel.Margin = new System.Windows.Forms.Padding(0);
		this.incrementalGainScenarioLabel.Name = "incrementalGainScenarioLabel";
		this.incrementalGainScenarioLabel.Size = new System.Drawing.Size(234, 32);
		this.incrementalGainScenarioLabel.TabIndex = 65;
		this.incrementalGainScenarioLabel.Text = "Current Scenario is set to Default Scenario";
		this.incrementalGainScenarioLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.incrementalGainNameTextbox.Location = new System.Drawing.Point(69, 23);
		this.incrementalGainNameTextbox.Name = "incrementalGainNameTextbox";
		this.incrementalGainNameTextbox.Size = new System.Drawing.Size(257, 20);
		this.incrementalGainNameTextbox.TabIndex = 50;
		this.label45.AutoSize = true;
		this.label45.Location = new System.Drawing.Point(30, 26);
		this.label45.Name = "label45";
		this.label45.Size = new System.Drawing.Size(38, 13);
		this.label45.TabIndex = 49;
		this.label45.Text = "Name:";
		this.incrementalGainConfigureSKUButton.ForeColor = System.Drawing.SystemColors.ControlText;
		this.incrementalGainConfigureSKUButton.Location = new System.Drawing.Point(251, 77);
		this.incrementalGainConfigureSKUButton.Name = "incrementalGainConfigureSKUButton";
		this.incrementalGainConfigureSKUButton.Size = new System.Drawing.Size(75, 23);
		this.incrementalGainConfigureSKUButton.TabIndex = 48;
		this.incrementalGainConfigureSKUButton.Text = "Configure";
		this.incrementalGainConfigureSKUButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.incrementalGainConfigureSKUButton.UseVisualStyleBackColor = true;
		this.incrementalGainConfigureSKUButton.Click += new System.EventHandler(configSKUButton_Click);
		this.incrementalGainRunButton.Location = new System.Drawing.Point(135, 258);
		this.incrementalGainRunButton.Name = "incrementalGainRunButton";
		this.incrementalGainRunButton.Size = new System.Drawing.Size(75, 22);
		this.incrementalGainRunButton.TabIndex = 59;
		this.incrementalGainRunButton.Text = "Done";
		this.incrementalGainRunButton.UseVisualStyleBackColor = true;
		this.incrementalGainRunButton.Click += new System.EventHandler(incrementalGainRunButton_Click);
		chartArea10.AxisX.LabelStyle.ForeColor = System.Drawing.Color.DimGray;
		chartArea10.AxisX.LineColor = System.Drawing.Color.DimGray;
		chartArea10.AxisX.MajorGrid.Enabled = false;
		chartArea10.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
		chartArea10.AxisX2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
		chartArea10.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
		chartArea10.AxisY.IsStartedFromZero = false;
		chartArea10.AxisY.LabelStyle.ForeColor = System.Drawing.Color.DimGray;
		chartArea10.AxisY.LineColor = System.Drawing.Color.DimGray;
		chartArea10.AxisY.MajorGrid.Enabled = false;
		chartArea10.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
		chartArea10.AxisY.ScaleBreakStyle.BreakLineStyle = System.Windows.Forms.DataVisualization.Charting.BreakLineStyle.None;
		chartArea10.Name = "ChartArea1";
		this.chart7.ChartAreas.Add(chartArea10);
		this.chart7.Enabled = false;
		legend7.Alignment = System.Drawing.StringAlignment.Center;
		legend7.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
		legend7.Name = "Legend1";
		this.chart7.Legends.Add(legend7);
		this.chart7.Location = new System.Drawing.Point(346, 6);
		this.chart7.Name = "chart7";
		series10.ChartArea = "ChartArea1";
		series10.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.ErrorBar;
		series10.LabelForeColor = System.Drawing.Color.DarkGray;
		series10.Legend = "Legend1";
		series10.Name = "Series1";
		dataPoint29.IsEmpty = true;
		dataPoint30.IsEmpty = true;
		dataPoint31.IsEmpty = true;
		series10.Points.Add(dataPoint29);
		series10.Points.Add(dataPoint30);
		series10.Points.Add(dataPoint31);
		series10.YValuesPerPoint = 3;
		this.chart7.Series.Add(series10);
		this.chart7.Size = new System.Drawing.Size(434, 413);
		this.chart7.TabIndex = 29;
		this.chart7.Text = "chart7";
		title10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		title10.ForeColor = System.Drawing.Color.SteelBlue;
		title10.Name = "Title1";
		title10.Text = "Introduction";
		this.chart7.Titles.Add(title10);
		this.tabPage9.BackColor = System.Drawing.Color.White;
		this.tabPage9.Controls.Add(this.groupBox17);
		this.tabPage9.Controls.Add(this.groupBox21);
		this.tabPage9.Controls.Add(this.incrementalLossRunButton);
		this.tabPage9.Controls.Add(this.textBox12);
		this.tabPage9.Controls.Add(this.groupBox18);
		this.tabPage9.Controls.Add(this.chart11);
		this.tabPage9.Location = new System.Drawing.Point(4, 22);
		this.tabPage9.Name = "tabPage9";
		this.tabPage9.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage9.Size = new System.Drawing.Size(784, 422);
		this.tabPage9.TabIndex = 9;
		this.tabPage9.Text = "Removal";
		this.groupBox17.Controls.Add(this.exclusiveIncrementalLossRadiobutton);
		this.groupBox17.Controls.Add(this.cumulativeIncrementalLossRadiobutton);
		this.groupBox17.Location = new System.Drawing.Point(5, 9);
		this.groupBox17.Name = "groupBox17";
		this.groupBox17.Size = new System.Drawing.Size(332, 55);
		this.groupBox17.TabIndex = 62;
		this.groupBox17.TabStop = false;
		this.groupBox17.Text = "Calculate Removal In";
		this.exclusiveIncrementalLossRadiobutton.AutoSize = true;
		this.exclusiveIncrementalLossRadiobutton.Location = new System.Drawing.Point(205, 23);
		this.exclusiveIncrementalLossRadiobutton.Name = "exclusiveIncrementalLossRadiobutton";
		this.exclusiveIncrementalLossRadiobutton.Size = new System.Drawing.Size(93, 17);
		this.exclusiveIncrementalLossRadiobutton.TabIndex = 62;
		this.exclusiveIncrementalLossRadiobutton.Text = "Exclusive Run";
		this.exclusiveIncrementalLossRadiobutton.UseVisualStyleBackColor = true;
		this.exclusiveIncrementalLossRadiobutton.CheckedChanged += new System.EventHandler(accumulativeIncrementalGainRadiobutton_CheckedChanged);
		this.cumulativeIncrementalLossRadiobutton.AutoSize = true;
		this.cumulativeIncrementalLossRadiobutton.Checked = true;
		this.cumulativeIncrementalLossRadiobutton.Location = new System.Drawing.Point(35, 23);
		this.cumulativeIncrementalLossRadiobutton.Name = "cumulativeIncrementalLossRadiobutton";
		this.cumulativeIncrementalLossRadiobutton.Size = new System.Drawing.Size(100, 17);
		this.cumulativeIncrementalLossRadiobutton.TabIndex = 61;
		this.cumulativeIncrementalLossRadiobutton.TabStop = true;
		this.cumulativeIncrementalLossRadiobutton.Text = "Cumulative Run";
		this.cumulativeIncrementalLossRadiobutton.UseVisualStyleBackColor = true;
		this.cumulativeIncrementalLossRadiobutton.CheckedChanged += new System.EventHandler(accumulativeIncrementalGainRadiobutton_CheckedChanged);
		this.groupBox21.Controls.Add(this.incrementalLossRevenuesCheckbox);
		this.groupBox21.Controls.Add(this.incrementalLossSharesCheckbox);
		this.groupBox21.Location = new System.Drawing.Point(5, 186);
		this.groupBox21.Name = "groupBox21";
		this.groupBox21.Size = new System.Drawing.Size(332, 54);
		this.groupBox21.TabIndex = 61;
		this.groupBox21.TabStop = false;
		this.groupBox21.Text = "Find Removal In Terms Of";
		this.incrementalLossRevenuesCheckbox.AutoSize = true;
		this.incrementalLossRevenuesCheckbox.Location = new System.Drawing.Point(35, 23);
		this.incrementalLossRevenuesCheckbox.Name = "incrementalLossRevenuesCheckbox";
		this.incrementalLossRevenuesCheckbox.Size = new System.Drawing.Size(75, 17);
		this.incrementalLossRevenuesCheckbox.TabIndex = 56;
		this.incrementalLossRevenuesCheckbox.Text = "Revenues";
		this.incrementalLossRevenuesCheckbox.UseVisualStyleBackColor = true;
		this.incrementalLossSharesCheckbox.AutoSize = true;
		this.incrementalLossSharesCheckbox.Location = new System.Drawing.Point(205, 23);
		this.incrementalLossSharesCheckbox.Name = "incrementalLossSharesCheckbox";
		this.incrementalLossSharesCheckbox.Size = new System.Drawing.Size(90, 17);
		this.incrementalLossSharesCheckbox.TabIndex = 57;
		this.incrementalLossSharesCheckbox.Text = "Market Share";
		this.incrementalLossSharesCheckbox.UseVisualStyleBackColor = true;
		this.incrementalLossRunButton.Location = new System.Drawing.Point(135, 258);
		this.incrementalLossRunButton.Name = "incrementalLossRunButton";
		this.incrementalLossRunButton.Size = new System.Drawing.Size(75, 22);
		this.incrementalLossRunButton.TabIndex = 59;
		this.incrementalLossRunButton.Text = "Done";
		this.incrementalLossRunButton.UseVisualStyleBackColor = true;
		this.incrementalLossRunButton.Click += new System.EventHandler(incrementalLossRunButton_Click);
		this.textBox12.BackColor = System.Drawing.Color.Gray;
		this.textBox12.Enabled = false;
		this.textBox12.Location = new System.Drawing.Point(344, 8);
		this.textBox12.Multiline = true;
		this.textBox12.Name = "textBox12";
		this.textBox12.Size = new System.Drawing.Size(1, 410);
		this.textBox12.TabIndex = 35;
		this.groupBox18.Controls.Add(this.IncrementalLossDropdownButton);
		this.groupBox18.Controls.Add(this.incrementalLossIncrementalConfigurationButton);
		this.groupBox18.Controls.Add(this.incrementalLossScenarioLabel);
		this.groupBox18.Controls.Add(this.incrementalLossNameTextbox);
		this.groupBox18.Controls.Add(this.label53);
		this.groupBox18.Controls.Add(this.incrementalLossConfigureSKUButton);
		this.groupBox18.Location = new System.Drawing.Point(5, 70);
		this.groupBox18.Name = "groupBox18";
		this.groupBox18.Size = new System.Drawing.Size(332, 110);
		this.groupBox18.TabIndex = 34;
		this.groupBox18.TabStop = false;
		this.groupBox18.Text = "Removal Global Configuration";
		this.IncrementalLossDropdownButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
		this.IncrementalLossDropdownButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.IncrementalLossDropdownButton.ForeColor = System.Drawing.SystemColors.ControlText;
		this.IncrementalLossDropdownButton.Image = MarketSimulator.Reports.Properties.Resources.downArrow;
		this.IncrementalLossDropdownButton.Location = new System.Drawing.Point(310, 78);
		this.IncrementalLossDropdownButton.Name = "IncrementalLossDropdownButton";
		this.IncrementalLossDropdownButton.Size = new System.Drawing.Size(17, 21);
		this.IncrementalLossDropdownButton.TabIndex = 64;
		this.IncrementalLossDropdownButton.UseVisualStyleBackColor = true;
		this.IncrementalLossDropdownButton.Click += new System.EventHandler(scenariosDropdownButton_Click);
		this.incrementalLossIncrementalConfigurationButton.Location = new System.Drawing.Point(48, 49);
		this.incrementalLossIncrementalConfigurationButton.Name = "incrementalLossIncrementalConfigurationButton";
		this.incrementalLossIncrementalConfigurationButton.Size = new System.Drawing.Size(236, 22);
		this.incrementalLossIncrementalConfigurationButton.TabIndex = 58;
		this.incrementalLossIncrementalConfigurationButton.Text = "Set Current Line Up && SKU's To Be Removed";
		this.incrementalLossIncrementalConfigurationButton.UseVisualStyleBackColor = true;
		this.incrementalLossIncrementalConfigurationButton.Click += new System.EventHandler(IncrementalGain_Click);
		this.incrementalLossScenarioLabel.Location = new System.Drawing.Point(14, 74);
		this.incrementalLossScenarioLabel.Margin = new System.Windows.Forms.Padding(0);
		this.incrementalLossScenarioLabel.Name = "incrementalLossScenarioLabel";
		this.incrementalLossScenarioLabel.Size = new System.Drawing.Size(234, 32);
		this.incrementalLossScenarioLabel.TabIndex = 65;
		this.incrementalLossScenarioLabel.Text = "Current Scenario is set to Default Scenario";
		this.incrementalLossScenarioLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.incrementalLossNameTextbox.Location = new System.Drawing.Point(69, 23);
		this.incrementalLossNameTextbox.Name = "incrementalLossNameTextbox";
		this.incrementalLossNameTextbox.Size = new System.Drawing.Size(257, 20);
		this.incrementalLossNameTextbox.TabIndex = 50;
		this.label53.AutoSize = true;
		this.label53.Location = new System.Drawing.Point(30, 26);
		this.label53.Name = "label53";
		this.label53.Size = new System.Drawing.Size(38, 13);
		this.label53.TabIndex = 49;
		this.label53.Text = "Name:";
		this.incrementalLossConfigureSKUButton.ForeColor = System.Drawing.SystemColors.ControlText;
		this.incrementalLossConfigureSKUButton.Location = new System.Drawing.Point(251, 77);
		this.incrementalLossConfigureSKUButton.Name = "incrementalLossConfigureSKUButton";
		this.incrementalLossConfigureSKUButton.Size = new System.Drawing.Size(75, 23);
		this.incrementalLossConfigureSKUButton.TabIndex = 48;
		this.incrementalLossConfigureSKUButton.Text = "Configure ";
		this.incrementalLossConfigureSKUButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.incrementalLossConfigureSKUButton.UseVisualStyleBackColor = true;
		this.incrementalLossConfigureSKUButton.Click += new System.EventHandler(configSKUButton_Click);
		chartArea11.AxisX.LabelStyle.ForeColor = System.Drawing.Color.DimGray;
		chartArea11.AxisX.LineColor = System.Drawing.Color.DimGray;
		chartArea11.AxisX.MajorGrid.Enabled = false;
		chartArea11.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
		chartArea11.AxisX2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
		chartArea11.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
		chartArea11.AxisY.IsStartedFromZero = false;
		chartArea11.AxisY.LabelStyle.ForeColor = System.Drawing.Color.DimGray;
		chartArea11.AxisY.LineColor = System.Drawing.Color.DimGray;
		chartArea11.AxisY.MajorGrid.Enabled = false;
		chartArea11.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
		chartArea11.AxisY.ScaleBreakStyle.BreakLineStyle = System.Windows.Forms.DataVisualization.Charting.BreakLineStyle.None;
		chartArea11.Name = "ChartArea1";
		this.chart11.ChartAreas.Add(chartArea11);
		this.chart11.Enabled = false;
		legend8.Alignment = System.Drawing.StringAlignment.Center;
		legend8.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
		legend8.Name = "Legend1";
		this.chart11.Legends.Add(legend8);
		this.chart11.Location = new System.Drawing.Point(346, 6);
		this.chart11.Name = "chart11";
		series11.ChartArea = "ChartArea1";
		series11.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.ErrorBar;
		series11.LabelForeColor = System.Drawing.Color.DarkGray;
		series11.Legend = "Legend1";
		series11.Name = "Series1";
		dataPoint32.IsEmpty = true;
		dataPoint33.IsEmpty = true;
		dataPoint34.IsEmpty = true;
		series11.Points.Add(dataPoint32);
		series11.Points.Add(dataPoint33);
		series11.Points.Add(dataPoint34);
		series11.YValuesPerPoint = 3;
		this.chart11.Series.Add(series11);
		this.chart11.Size = new System.Drawing.Size(434, 410);
		this.chart11.TabIndex = 33;
		this.chart11.Text = "chart11";
		title11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		title11.ForeColor = System.Drawing.Color.SteelBlue;
		title11.Name = "Title1";
		title11.Text = "Removal";
		this.chart11.Titles.Add(title11);
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.label2.Font = new System.Drawing.Font("Tahoma", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.MidnightBlue;
		this.label2.Location = new System.Drawing.Point(13, 49);
		this.label2.Margin = new System.Windows.Forms.Padding(0);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(791, 38);
		this.label2.TabIndex = 4;
		this.label2.Text = "Customize the methods you want to automate then press the add button, Customize the chart appearance && after you finish press Start Process";
		this.button2.Font = new System.Drawing.Font("Tahoma", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button2.Location = new System.Drawing.Point(588, 551);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(161, 27);
		this.button2.TabIndex = 6;
		this.button2.Text = "Start Automation Process";
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.button3.Font = new System.Drawing.Font("Tahoma", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button3.Location = new System.Drawing.Point(752, 551);
		this.button3.Name = "button3";
		this.button3.Size = new System.Drawing.Size(51, 27);
		this.button3.TabIndex = 7;
		this.button3.Text = "Close";
		this.button3.UseVisualStyleBackColor = true;
		this.button3.Click += new System.EventHandler(button3_Click);
		this.textBox8.BackColor = System.Drawing.Color.Gray;
		this.textBox8.Enabled = false;
		this.textBox8.Location = new System.Drawing.Point(416, 36);
		this.textBox8.Multiline = true;
		this.textBox8.Name = "textBox8";
		this.textBox8.Size = new System.Drawing.Size(390, 1);
		this.textBox8.TabIndex = 9;
		this.button1.Font = new System.Drawing.Font("Tahoma", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button1.Location = new System.Drawing.Point(479, 551);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(106, 27);
		this.button1.TabIndex = 11;
		this.button1.Text = "Reset Charts";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.initializeChartsProjectBackgoundWorker.WorkerReportsProgress = true;
		this.initializeChartsProjectBackgoundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(backgroundWorker1_DoWork);
		this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
		this.pictureBox1.Image = MarketSimulator.Reports.Properties.Resources.Marketeers_Logo_Blue;
		this.pictureBox1.Location = new System.Drawing.Point(647, 6);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(164, 34);
		this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
		this.pictureBox1.TabIndex = 3;
		this.pictureBox1.TabStop = false;
		this.scenariosMenuStrip.Name = "contextMenuStrip1";
		this.scenariosMenuStrip.Size = new System.Drawing.Size(61, 4);
		this.textBox9.BackColor = System.Drawing.Color.Gray;
		this.textBox9.Enabled = false;
		this.textBox9.Location = new System.Drawing.Point(8, 544);
		this.textBox9.Multiline = true;
		this.textBox9.Name = "textBox9";
		this.textBox9.Size = new System.Drawing.Size(795, 1);
		this.textBox9.TabIndex = 10;
		this.sharesValueShareIndexCheckbox.AutoSize = true;
		this.sharesValueShareIndexCheckbox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.sharesValueShareIndexCheckbox.Location = new System.Drawing.Point(184, 122);
		this.sharesValueShareIndexCheckbox.Name = "sharesValueShareIndexCheckbox";
		this.sharesValueShareIndexCheckbox.Size = new System.Drawing.Size(113, 17);
		this.sharesValueShareIndexCheckbox.TabIndex = 58;
		this.sharesValueShareIndexCheckbox.Text = "Value Share Index";
		this.sharesValueShareIndexCheckbox.UseVisualStyleBackColor = true;
		this.sharesValueShareCheckbox.AutoSize = true;
		this.sharesValueShareCheckbox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.sharesValueShareCheckbox.Location = new System.Drawing.Point(13, 122);
		this.sharesValueShareCheckbox.Name = "sharesValueShareCheckbox";
		this.sharesValueShareCheckbox.Size = new System.Drawing.Size(84, 17);
		this.sharesValueShareCheckbox.TabIndex = 57;
		this.sharesValueShareCheckbox.Text = "Value Share";
		this.sharesValueShareCheckbox.UseVisualStyleBackColor = true;
		this.sharesWeightedSOPIndexCheckbox.AutoSize = true;
		this.sharesWeightedSOPIndexCheckbox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.sharesWeightedSOPIndexCheckbox.Location = new System.Drawing.Point(184, 145);
		this.sharesWeightedSOPIndexCheckbox.Name = "sharesWeightedSOPIndexCheckbox";
		this.sharesWeightedSOPIndexCheckbox.Size = new System.Drawing.Size(126, 17);
		this.sharesWeightedSOPIndexCheckbox.TabIndex = 60;
		this.sharesWeightedSOPIndexCheckbox.Text = "Weighted SOP Index";
		this.sharesWeightedSOPIndexCheckbox.UseVisualStyleBackColor = true;
		this.sharesWeightedSOPCheckbox.AutoSize = true;
		this.sharesWeightedSOPCheckbox.ForeColor = System.Drawing.SystemColors.ControlText;
		this.sharesWeightedSOPCheckbox.Location = new System.Drawing.Point(13, 145);
		this.sharesWeightedSOPCheckbox.Name = "sharesWeightedSOPCheckbox";
		this.sharesWeightedSOPCheckbox.Size = new System.Drawing.Size(97, 17);
		this.sharesWeightedSOPCheckbox.TabIndex = 59;
		this.sharesWeightedSOPCheckbox.Text = "Weighted SOP";
		this.sharesWeightedSOPCheckbox.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		base.ClientSize = new System.Drawing.Size(812, 587);
		base.Controls.Add(this.textBox8);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.textBox9);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.button3);
		base.Controls.Add(this.button2);
		base.Controls.Add(this.pictureBox1);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.tabControl1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "ChartForm";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = " Marketeers Charts Automation";
		base.Load += new System.EventHandler(ChartForm_Load);
		base.Paint += new System.Windows.Forms.PaintEventHandler(ChartForm_Paint);
		this.tabControl1.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.tabPage1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.chart3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chart2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chart1).EndInit();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		this.attributeElasticityPerCompanyGroupbox.ResumeLayout(false);
		this.attributeElasticityPerCompanyGroupbox.PerformLayout();
		this.attributeElasticityPerSKUGroupbox.ResumeLayout(false);
		this.attributeElasticityPerSKUGroupbox.PerformLayout();
		this.attributeElasticityPerGroupGroupbox.ResumeLayout(false);
		this.attributeElasticityPerGroupGroupbox.PerformLayout();
		this.tabPage2.ResumeLayout(false);
		this.tabPage2.PerformLayout();
		this.groupBox8.ResumeLayout(false);
		this.groupBox8.PerformLayout();
		this.groupBox7.ResumeLayout(false);
		this.groupBox7.PerformLayout();
		this.groupBox6.ResumeLayout(false);
		this.groupBox6.PerformLayout();
		this.groupBox5.ResumeLayout(false);
		this.groupBox5.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.chart4).EndInit();
		this.tabPage8.ResumeLayout(false);
		this.tabPage8.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.chart10).EndInit();
		this.tabPage3.ResumeLayout(false);
		this.tabPage3.PerformLayout();
		this.groupBox10.ResumeLayout(false);
		this.groupBox10.PerformLayout();
		this.groupBox9.ResumeLayout(false);
		this.groupBox9.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.chart5).EndInit();
		this.tabPage4.ResumeLayout(false);
		this.tabPage4.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.chart6).EndInit();
		this.groupBox19.ResumeLayout(false);
		this.groupBox19.PerformLayout();
		this.groupBox11.ResumeLayout(false);
		this.groupBox11.PerformLayout();
		this.tabPage6.ResumeLayout(false);
		this.tabPage6.PerformLayout();
		this.groupBox23.ResumeLayout(false);
		this.groupBox23.PerformLayout();
		this.groupBox14.ResumeLayout(false);
		this.groupBox14.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.chart8).EndInit();
		this.tabPage7.ResumeLayout(false);
		this.tabPage7.PerformLayout();
		this.groupBox22.ResumeLayout(false);
		this.groupBox22.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox16.ResumeLayout(false);
		this.groupBox16.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.chart9).EndInit();
		this.tabPage5.ResumeLayout(false);
		this.tabPage5.PerformLayout();
		this.groupBox20.ResumeLayout(false);
		this.groupBox20.PerformLayout();
		this.groupBox13.ResumeLayout(false);
		this.groupBox13.PerformLayout();
		this.groupBox12.ResumeLayout(false);
		this.groupBox12.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.chart7).EndInit();
		this.tabPage9.ResumeLayout(false);
		this.tabPage9.PerformLayout();
		this.groupBox17.ResumeLayout(false);
		this.groupBox17.PerformLayout();
		this.groupBox21.ResumeLayout(false);
		this.groupBox21.PerformLayout();
		this.groupBox18.ResumeLayout(false);
		this.groupBox18.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.chart11).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public ChartForm(bool showPreferenceShare, bool showRevenue)
	{
		InitializeComponent();
		ProjectCore.Project["BaseScenario"].initializeProjectMembers();
		ProjectCore.Project["ElasticityChartProject"].initializeProjectMembers();
		ProjectCore.Project["ElasticityChartProjectPerGroup"].initializeProjectMembers();
		ProjectCore.Project["ElasticityChartProjectPerCompany"].initializeProjectMembers();
		ProjectCore.Project["IndexAndRevenuesChartProject"].initializeProjectMembers();
		ProjectCore.Project["SharesChartProject"].initializeProjectMembers();
		ProjectCore.Project["BrandInteractionChartProject"].initializeProjectMembers();
		ProjectCore.Project["SOMVsSOPChartProject"].initializeProjectMembers();
		ProjectCore.Project["FairShareChartProject"].initializeProjectMembers();
		ProjectCore.Project["ValidationChartProject"].initializeProjectMembers();
		ProjectCore.Project["IncrementalGainChartProject"].initializeProjectMembers();
		ProjectCore.Project["IncrementalLossChartProject"].initializeProjectMembers();
		project = ProjectCore.Project["MainStudy"];
		InitializeIncrementalDataTable();
		initializeSpecialCharaters();
		if (!showPreferenceShare && tabControl1.TabPages.Contains(tabPage4))
		{
			tabControl1.TabPages.Remove(tabPage4);
		}
		if (showRevenue)
		{
			priceIndexTotalCompanyRevenue.Visible = true;
			sharesRevenueCheckbox.Visible = true;
		}
		if (!project.ContainsAttribute("Price"))
		{
			tabPage2.Enabled = false;
		}
	}

	private void ChartForm_Load(object sender, EventArgs e)
	{
		fairSharAttributeCombobox.DataSource = project.ConditionalAttributeNameList;
		elasticitySegmentationTypeCombobox.Items.Clear();
		foreach (KeyValuePair<string, ArrayList> item in project.loadSegmentsDictionary())
		{
			elasticitySegmentationTypeCombobox.Items.Add(item.Key);
		}
	}

	private void ChartForm_Paint(object sender, PaintEventArgs e)
	{
		Rectangle rect = new Rectangle(0, 0, base.ClientSize.Width, base.ClientSize.Height);
		LinearGradientBrush brush = new LinearGradientBrush(rect, Color.White, Color.FromArgb(45, Color.SteelBlue), LinearGradientMode.ForwardDiagonal);
		e.Graphics.FillRectangle(brush, rect);
	}

	private void initializeSpecialCharaters()
	{
		specialCharacter.Add('/');
		specialCharacter.Add('\\');
		specialCharacter.Add('?');
		specialCharacter.Add('*');
		specialCharacter.Add('[');
		specialCharacter.Add(']');
	}

	private void InitializeIncrementalDataTable()
	{
		dtIncrementalGain.Columns.Add("Current Line Up Products", typeof(string));
		dtIncrementalGain.Columns.Add("Products To Be Added", typeof(string));
		dtIncrementalLoss.Columns.Add("Current Line Up Products", typeof(string));
		dtIncrementalLoss.Columns.Add("Products To Be Removed", typeof(string));
		for (int i = 0; i < project.AllProducts.Count; i++)
		{
			dtIncrementalGain.Rows.Add(dtIncrementalGain.NewRow());
			dtIncrementalLoss.Rows.Add(dtIncrementalLoss.NewRow());
		}
	}

	private void elasticityGroupTypePerCompanyCombobox_SelectedIndexChanged(object sender, EventArgs e)
	{
		elasticityCompanySeriesCombobox.Clear();
		elasticityCompanySeriesCombobox.Items.Add("* All");
		elasticityCompanySeriesCombobox.Items.AddRange(project.GroupMember[elasticityGroupTypePerCompanyCombobox.SelectedItem.ToString()].ToArray());
	}

	private void checkBox3_CheckedChanged(object sender, EventArgs e)
	{
		if (elasticityPerCompanyGroupCheckbox.Checked)
		{
			elasticityAttributeNamePerCompanyCombobox.DataSource = project.ConditionalAttributeNameList.GetRange(0, project.ConditionalAttributeNameList.Count);
			elasticityGroupTypePerCompanyCombobox.DataSource = project.GroupTypeList.ToArray();
			attributeElasticityPerCompanyGroupbox.Enabled = true;
			elasticityGroupTypePerCompanyCombobox.Enabled = true;
			elasticityPerCompanySKUCheckbox.Checked = false;
		}
		else if (!elasticityPerCompanySKUCheckbox.Checked)
		{
			attributeElasticityPerCompanyGroupbox.Enabled = false;
		}
	}

	private void elasticityPerCompanySKUCheckbox_CheckedChanged(object sender, EventArgs e)
	{
		if (elasticityPerCompanySKUCheckbox.Checked)
		{
			elasticityAttributeNamePerCompanyCombobox.DataSource = project.ConditionalAttributeNameList.GetRange(0, project.ConditionalAttributeNameList.Count);
			attributeElasticityPerCompanyGroupbox.Enabled = true;
			elasticityGroupTypePerCompanyCombobox.Enabled = false;
			elasticityGroupTypePerCompanyCombobox.Text = "";
			elasticityPerCompanyGroupCheckbox.Checked = false;
			elasticityCompanySeriesCombobox.Clear();
			elasticityCompanySeriesCombobox.Items.Add("* All");
			elasticityCompanySeriesCombobox.Items.AddRange(project.AllProducts.ToArray());
		}
		else if (!elasticityPerCompanyGroupCheckbox.Checked)
		{
			attributeElasticityPerCompanyGroupbox.Enabled = false;
		}
	}

	private void checkBox2_CheckedChanged(object sender, EventArgs e)
	{
		if (elasticityPerGroupCheckbox.Checked)
		{
			attributeElasticityPerGroupGroupbox.Enabled = true;
			elasticityAttributeNamePerGroupCombobox.DataSource = project.ConditionalAttributeNameList.GetRange(0, project.ConditionalAttributeNameList.Count);
			elasticityGroupTypeCombobox.DataSource = project.GroupTypeList.GetRange(0, project.GroupTypeList.Count);
		}
		else
		{
			attributeElasticityPerGroupGroupbox.Enabled = false;
		}
	}

	private void checkBox1_CheckedChanged(object sender, EventArgs e)
	{
		if (elasticityPerSKUCheckbox.Checked)
		{
			attributeElasticityPerSKUGroupbox.Enabled = true;
			elasticityAttributeNamePerSKUCombobox.DataSource = project.ConditionalAttributeNameList.GetRange(0, project.ConditionalAttributeNameList.Count);
			elasticitySKUsCombobox.Items.AddRange(project.AllProducts.ToArray());
		}
		else
		{
			attributeElasticityPerSKUGroupbox.Enabled = false;
		}
	}

	private void elasticitySKUsCombobox_CheckBoxCheckedChanged(object sender, EventArgs e)
	{
		manageCheckboxComboboxAllItemState(sender, elasticitySKUsCombobox);
	}

	private void elasticityGroupsCombobox_CheckBoxCheckedChanged(object sender, EventArgs e)
	{
		manageCheckboxComboboxAllItemState(sender, elasticityGroupsCombobox);
	}

	private void elasticityCompanySKUsCombobox_CheckBoxCheckedChanged(object sender, EventArgs e)
	{
		manageCheckboxComboboxAllItemState(sender, elasticityCompanySeriesCombobox);
	}

	private void manageCheckboxComboboxAllItemState(object sender, CheckBoxComboBox comboboxControl)
	{
		CheckBoxComboBoxItem checkBoxComboBoxItem = sender as CheckBoxComboBoxItem;
		if (checkBoxComboBoxItem.Text == "* All" && checkBoxComboBoxItem.Checked)
		{
			foreach (CheckBoxComboBoxItem checkBoxItem in comboboxControl.CheckBoxItems)
			{
				checkBoxItem.Checked = true;
			}
		}
		else if (checkBoxComboBoxItem.Text == "* All" && !checkBoxComboBoxItem.Checked && checkAllItemsCombobox)
		{
			foreach (CheckBoxComboBoxItem checkBoxItem2 in comboboxControl.CheckBoxItems)
			{
				checkBoxItem2.Checked = false;
			}
		}
		else if (!checkBoxComboBoxItem.Checked)
		{
			checkAllItemsCombobox = false;
			comboboxControl.CheckBoxItems["* All"].Checked = false;
		}
		else if (checkBoxComboBoxItem.Checked)
		{
			bool flag = true;
			foreach (CheckBoxComboBoxItem checkBoxItem3 in comboboxControl.CheckBoxItems)
			{
				if (!checkBoxItem3.Checked && checkBoxItem3.Text != "* All")
				{
					flag = false;
				}
			}
			if (flag)
			{
				comboboxControl.CheckBoxItems["* All"].Checked = true;
			}
		}
		checkAllItemsCombobox = true;
	}

	private void elasticityGroupTypeCombobox_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (elasticityGroupTypeCombobox.Text != "")
		{
			elasticityGroupsCombobox.Items.Clear();
			elasticityGroupsCombobox.Items.Add("* All");
			elasticityGroupsCombobox.Items.AddRange(project.GroupMember[elasticityGroupTypeCombobox.Text].ToArray());
		}
	}

	private void priceIndexGroupsRadiobox_CheckedChanged(object sender, EventArgs e)
	{
		if (priceIndexGroupsRadiobox.Checked)
		{
			priceIndexGroupTypeCombobox.Enabled = true;
			priceIndexGroupNameCombobox.Enabled = true;
			priceIndexReferenceGroupNameCombobox.Enabled = true;
			priceIndexGroupTypeCombobox.DataSource = project.GroupTypeList.GetRange(0, project.GroupTypeList.Count);
			priceIndexSKUNameCombobox.Items.Clear();
			priceIndexReferenceSKUNameCombobox.Items.Clear();
			priceIndexScaleLabel.Tag = null;
			priceIndexScaleTextbox.Text = "";
		}
	}

	private void priceIndexSKUsRadiobox_CheckedChanged(object sender, EventArgs e)
	{
		if (priceIndexSKUsRadiobox.Checked)
		{
			priceIndexGroupTypeCombobox.Text = "";
			priceIndexGroupTypeCombobox.Enabled = false;
			priceIndexGroupNameCombobox.Text = "";
			priceIndexGroupNameCombobox.Enabled = false;
			priceIndexReferenceGroupNameCombobox.Text = "";
			priceIndexReferenceGroupNameCombobox.Enabled = false;
			priceIndexSKUNameCombobox.Items.Clear();
			priceIndexSKUNameCombobox.Items.AddRange(project.AllProducts.ToArray());
			priceIndexReferenceSKUNameCombobox.Items.Clear();
			priceIndexReferenceSKUNameCombobox.Items.AddRange(project.AllProducts.ToArray());
			priceIndexSeriesCombobox.Items.Clear();
			priceIndexSeriesCombobox.Items.Add("* All");
			priceIndexSeriesCombobox.CheckBoxItems[0].Checked = false;
			priceIndexSeriesCombobox.Items.AddRange(project.AllProducts.ToArray());
			priceIndexSeriesCombobox.Text = "";
			priceIndexScaleLabel.Tag = null;
			priceIndexScaleTextbox.Text = "";
		}
	}

	private void priceIndexGroupTypeCombobox_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (priceIndexGroupTypeCombobox.Text != "")
		{
			ProjectCore.Project["IndexAndRevenuesChartProject"].setGroupResearchStart(priceIndexGroupTypeCombobox.SelectedItem.ToString(), grouping: true);
			priceIndexGroupNameCombobox.Items.Clear();
			priceIndexGroupNameCombobox.Text = "";
			priceIndexGroupNameCombobox.Items.AddRange(project.GroupMember[priceIndexGroupTypeCombobox.Text].ToArray());
			priceIndexReferenceGroupNameCombobox.Items.Clear();
			priceIndexReferenceGroupNameCombobox.Text = "";
			priceIndexReferenceGroupNameCombobox.Items.AddRange(project.GroupMember[priceIndexGroupTypeCombobox.Text].ToArray());
			priceIndexSeriesCombobox.Items.Clear();
			priceIndexSeriesCombobox.Items.Add("* All");
			priceIndexSeriesCombobox.CheckBoxItems[0].Checked = false;
			priceIndexSeriesCombobox.Items.AddRange(project.GroupMember[priceIndexGroupTypeCombobox.Text].ToArray());
			priceIndexSeriesCombobox.Text = "";
		}
	}

	private void priceIndexGroupNameCombobox_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (priceIndexGroupNameCombobox.Text != "")
		{
			priceIndexSKUNameCombobox.Items.Clear();
			priceIndexSKUNameCombobox.Items.AddRange(project.GroupProducts[priceIndexGroupTypeCombobox.Text][priceIndexGroupNameCombobox.Text].ToArray());
		}
		if (priceIndexSKUNameCombobox.SelectedItem != null || priceIndexReferenceSKUNameCombobox.SelectedItem == null)
		{
			priceIndexScaleLabel.Tag = null;
			priceIndexScaleTextbox.Text = "";
		}
	}

	private void priceIndexReferenceGroupNameCombobox_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (priceIndexReferenceGroupNameCombobox.Text != "")
		{
			priceIndexReferenceSKUNameCombobox.Items.Clear();
			priceIndexReferenceSKUNameCombobox.Items.AddRange(project.GroupProducts[priceIndexGroupTypeCombobox.Text][priceIndexReferenceGroupNameCombobox.Text].ToArray());
		}
		if (priceIndexSKUNameCombobox.SelectedItem == null || priceIndexReferenceSKUNameCombobox.SelectedItem != null)
		{
			priceIndexScaleLabel.Tag = null;
			priceIndexScaleTextbox.Text = "";
		}
	}

	private void priceIndexSeriesCombobox_CheckBoxCheckedChanged(object sender, EventArgs e)
	{
		manageCheckboxComboboxAllItemState(sender, priceIndexSeriesCombobox);
	}

	private void brandInteractionGroupRadiobox_CheckedChanged(object sender, EventArgs e)
	{
		if (brandInteractionGroupRadiobox.Checked)
		{
			brandInteractionGroupTypeCombobox.Enabled = true;
			brandInteractionGroupTypeCombobox.DataSource = project.GroupTypeList.GetRange(0, project.GroupTypeList.Count);
			brandInteractionSeriesCombobox.CheckBoxItems[0].Checked = false;
		}
	}

	private void brandInteractionSKURadionbox_CheckedChanged(object sender, EventArgs e)
	{
		if (brandInteractionSKURadiobox.Checked)
		{
			brandInteractionGroupTypeCombobox.Enabled = false;
			brandInteractionGroupTypeCombobox.Text = "";
			brandInteractionSeriesCombobox.Items.Clear();
			brandInteractionSeriesCombobox.Text = "";
			brandInteractionSeriesCombobox.Items.Add("* All");
			brandInteractionSeriesCombobox.CheckBoxItems[0].Checked = false;
			brandInteractionSeriesCombobox.Items.AddRange(project.AllProducts.ToArray());
		}
	}

	private void brandInteractionGroupTypeCombobox_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (brandInteractionGroupTypeCombobox.Text != "")
		{
			ProjectCore.Project["BrandInteractionChartProject"].setGroupResearchStart(brandInteractionGroupTypeCombobox.SelectedItem.ToString(), grouping: true);
			brandInteractionSeriesCombobox.Items.Clear();
			brandInteractionSeriesCombobox.Text = "";
			brandInteractionSeriesCombobox.Items.Add("* All");
			brandInteractionSeriesCombobox.Items.AddRange(project.GroupMember[brandInteractionGroupTypeCombobox.Text].ToArray());
		}
	}

	private void brandInteractionSeriesCombobox_CheckBoxCheckedChanged(object sender, EventArgs e)
	{
		manageCheckboxComboboxAllItemState(sender, brandInteractionSeriesCombobox);
	}

	private void somsopGroupRadiobox_CheckedChanged(object sender, EventArgs e)
	{
		if (somsopGroupRadiobox.Checked)
		{
			somsopGroupTypeCombobox.Enabled = true;
			somsopGroupTypeCombobox.DataSource = project.GroupTypeList.GetRange(0, project.GroupTypeList.Count);
		}
	}

	private void somsopSKURadiobox_CheckedChanged(object sender, EventArgs e)
	{
		if (somsopSKURadiobox.Checked)
		{
			somsopGroupTypeCombobox.Enabled = false;
			somsopGroupTypeCombobox.Text = "";
			somsopSeriesCombobox.Items.Clear();
			somsopSeriesCombobox.Text = "";
			somsopSeriesCombobox.Items.Add("* All");
			somsopSeriesCombobox.CheckBoxItems[0].Checked = false;
			somsopSeriesCombobox.Items.AddRange(project.AllProducts.ToArray());
		}
	}

	private void somsopGroupTypeCombobox_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (somsopGroupTypeCombobox.Text != "")
		{
			ProjectCore.Project["SOMVsSOPChartProject"].setGroupResearchStart(somsopGroupTypeCombobox.SelectedItem.ToString(), grouping: true);
			somsopSeriesCombobox.Items.Clear();
			somsopSeriesCombobox.Text = "";
			somsopSeriesCombobox.Items.Add("* All");
			somsopSeriesCombobox.CheckBoxItems[0].Checked = false;
			somsopSeriesCombobox.Items.AddRange(project.GroupMember[somsopGroupTypeCombobox.Text].ToArray());
		}
	}

	private void somsopSeriesCombobox_CheckBoxCheckedChanged(object sender, EventArgs e)
	{
		manageCheckboxComboboxAllItemState(sender, somsopSeriesCombobox);
	}

	private void fairShareGroupRadiobox_CheckedChanged(object sender, EventArgs e)
	{
		if (fairShareGroupRadiobox.Checked)
		{
			fairShareGroupTypeCombobox.Enabled = true;
			fairShareGroupTypeCombobox.DataSource = project.GroupTypeList.GetRange(0, project.GroupTypeList.Count);
			fairShareSeriesCombobox.CheckBoxItems[0].Checked = false;
		}
	}

	private void fairShareSKURadiobox_CheckedChanged(object sender, EventArgs e)
	{
		if (fairShareSKURadiobox.Checked)
		{
			fairShareGroupTypeCombobox.Enabled = false;
			fairShareGroupTypeCombobox.Text = "";
			fairShareSeriesCombobox.Items.Clear();
			fairShareSeriesCombobox.Text = "";
			fairShareSeriesCombobox.Items.Add("* All");
			fairShareSeriesCombobox.CheckBoxItems[0].Checked = false;
			fairShareSeriesCombobox.Items.AddRange(project.AllProducts.ToArray());
			fairshareChangeCombobox.Items.Clear();
			fairshareChangeCombobox.Items.AddRange(project.AllProducts.ToArray());
			fairshareChangeCombobox.SelectedIndex = 0;
		}
	}

	private void fairShareGroupTypeCombobox_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (fairShareGroupTypeCombobox.Text != "")
		{
			ProjectCore.Project["FairShareChartProject"].setGroupResearchStart(fairShareGroupTypeCombobox.SelectedItem.ToString(), grouping: true);
			fairShareSeriesCombobox.Items.Clear();
			fairShareSeriesCombobox.Text = "";
			fairShareSeriesCombobox.Items.Add("* All");
			fairShareSeriesCombobox.Items.AddRange(project.GroupMember[fairShareGroupTypeCombobox.Text].ToArray());
			fairshareChangeCombobox.Items.Clear();
			fairshareChangeCombobox.Items.AddRange(project.GroupMember[fairShareGroupTypeCombobox.Text].ToArray());
			fairshareChangeCombobox.SelectedIndex = 0;
		}
	}

	private void fairShareSeriesCombobox_CheckBoxCheckedChanged(object sender, EventArgs e)
	{
		manageCheckboxComboboxAllItemState(sender, fairShareSeriesCombobox);
	}

	private void validationGroupRadiobox_CheckedChanged(object sender, EventArgs e)
	{
		if (validationGroupRadiobox.Checked)
		{
			validationGroupTypeCombobox.Enabled = true;
			validationGroupTypeCombobox.DataSource = project.GroupTypeList.GetRange(0, project.GroupTypeList.Count);
			validationSeriesCombobox.CheckBoxItems[0].Checked = false;
		}
	}

	private void validationSKURadiobox_CheckedChanged(object sender, EventArgs e)
	{
		if (validationSKURadiobox.Checked)
		{
			validationGroupTypeCombobox.Enabled = false;
			validationGroupTypeCombobox.Text = "";
			validationSeriesCombobox.Items.Clear();
			validationSeriesCombobox.Text = "";
			validationSeriesCombobox.Items.Add("* All");
			validationSeriesCombobox.CheckBoxItems[0].Checked = false;
			validationSeriesCombobox.Items.AddRange(project.AllProducts.ToArray());
		}
	}

	private void validationGroupTypeCombobox_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (validationGroupTypeCombobox.Text != "")
		{
			ProjectCore.Project["ValidationChartProject"].setGroupResearchStart(validationGroupTypeCombobox.SelectedItem.ToString(), grouping: true);
			validationSeriesCombobox.Items.Clear();
			validationSeriesCombobox.Text = "";
			validationSeriesCombobox.Items.Add("* All");
			validationSeriesCombobox.Items.AddRange(project.GroupMember[validationGroupTypeCombobox.Text].ToArray());
		}
	}

	private void validationSeriesCombobox_CheckBoxCheckedChanged(object sender, EventArgs e)
	{
		manageCheckboxComboboxAllItemState(sender, validationSeriesCombobox);
	}

	private void configSKUButton_Click(object sender, EventArgs e)
	{
		string text = "";
		Button button = sender as Button;
		switch (button.Name)
		{
		case "priceIndexConfigButton":
			text = "IndexAndRevenuesChartProject";
			break;
		case "sharesConfigSKUButton":
			text = "SharesChartProject";
			break;
		case "brandInteractionConfigSKUButton":
			text = "BrandInteractionChartProject";
			break;
		case "somsopConfigSKUButton":
			text = "SOMVsSOPChartProject";
			break;
		case "fairShareConfigSKUButton":
			text = "FairShareChartProject";
			break;
		case "validationConfigScenarioButton":
			text = "ValidationChartProject";
			break;
		case "incrementalGainConfigureSKUButton":
			text = "IncrementalGainChartProject";
			break;
		case "incrementalLossConfigureSKUButton":
			text = "IncrementalLossChartProject";
			break;
		}
		SKUAttributeConfiguration sKUAttributeConfiguration = new SKUAttributeConfiguration();
		sKUAttributeConfiguration.setSkuAttributeConfigurationForm(ProjectCore.Project[text], text);
		sKUAttributeConfiguration.setScenariosProjectDictionary(studyScenariosLibrary, validationScenarios, scenariosPath);
		sKUAttributeConfiguration.ShowDialog(this);
		switch (tabControl1.SelectedTab.Text)
		{
		case "Price Index":
			priceIndexScenarioLabel.Text = "Current Scenario is set to " + ProjectCore.Project[text].ScenarioName;
			break;
		case "SOM Vs. SOP":
			somsopScenariosLabel.Text = "Current Scenario is set to " + ProjectCore.Project[text].ScenarioName;
			break;
		case "Introduction":
			incrementalGainScenarioLabel.Text = "Current Scenario is set to " + ProjectCore.Project[text].ScenarioName;
			break;
		case "Removal":
			incrementalLossScenarioLabel.Text = "Current Scenario is set to " + ProjectCore.Project[text].ScenarioName;
			break;
		}
	}

	private void priceIndexReferenceSKUNameCombobox_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (priceIndexReferenceSKUNameCombobox.SelectedItem != null && priceIndexSKUNameCombobox.SelectedItem != null)
		{
			priceIndexScaleLabel.Text = preparePriceIndexScaleLabel();
			priceIndexRandomizeButton_Click(sender, e);
		}
	}

	private void priceIndexSKUNameCombobox_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (priceIndexReferenceSKUNameCombobox.SelectedItem != null && priceIndexSKUNameCombobox.SelectedItem != null)
		{
			priceIndexScaleLabel.Text = preparePriceIndexScaleLabel();
			priceIndexRandomizeButton_Click(sender, e);
		}
	}

	private void priceIndexRandomizeButton_Click(object sender, EventArgs e)
	{
		if (priceIndexSKUNameCombobox.SelectedItem == null || priceIndexReferenceSKUNameCombobox.SelectedItem == null)
		{
			return;
		}
		priceIndexScaleTextbox.Text = "";
		double value = project.getProductDefaultAttribute(priceIndexSKUNameCombobox.SelectedItem.ToString(), "Price") / project.getProductDefaultAttribute(priceIndexReferenceSKUNameCombobox.SelectedItem.ToString(), "Price");
		Random random = new Random();
		double[] array = new double[6]
		{
			Math.Round(value, 2),
			0.0,
			0.0,
			0.0,
			0.0,
			0.0
		};
		double[] attributeLevelsBoundries = project.getAttributeLevelsBoundries("Price");
		double productDefaultAttribute = project.getProductDefaultAttribute(priceIndexSKUNameCombobox.SelectedItem.ToString(), "Price");
		double num = productDefaultAttribute * (1.0 + attributeLevelsBoundries[0]);
		double num2 = productDefaultAttribute * (1.0 + attributeLevelsBoundries[1]);
		double productDefaultAttribute2 = project.getProductDefaultAttribute(priceIndexReferenceSKUNameCombobox.SelectedItem.ToString(), "Price");
		double num3 = num / productDefaultAttribute2;
		double num4 = num2 / productDefaultAttribute2;
		for (int i = 1; i < 6; i++)
		{
			double num5 = 0.0;
			double num6 = 0.0;
			do
			{
				num6 = random.Next((int)(attributeLevelsBoundries[0] * 100.0), (int)(attributeLevelsBoundries[1] * 100.0));
				num5 = project.getProductDefaultAttribute(priceIndexSKUNameCombobox.SelectedItem.ToString(), "Price") * (num6 / 100.0 + 1.0) / project.getProductDefaultAttribute(priceIndexReferenceSKUNameCombobox.SelectedItem.ToString(), "Price");
				num5 = Math.Round(num5, 2);
			}
			while (num5 < num3 || num5 > num4 || searchRandomScaleExist(num5, array));
			array[i] = num5;
		}
		arrangeOneDimArray(ref array);
		for (int i = 0; i < 6; i++)
		{
			priceIndexScaleTextbox.Text = priceIndexScaleTextbox.Text + array[i] + ",";
		}
		priceIndexScaleTextbox.Text = priceIndexScaleTextbox.Text.TrimEnd(',');
	}

	private void arrangeOneDimArray(ref double[] array)
	{
		for (int i = 0; i < array.Length; i++)
		{
			double num = array[i];
			for (int j = i + 1; j < array.Length; j++)
			{
				if (num > array[j])
				{
					num = array[j];
					array[j] = array[i];
					array[i] = num;
				}
			}
		}
	}

	private bool searchRandomScaleExist(double randomScale, double[] scaleNumbers)
	{
		for (int i = 0; i < scaleNumbers.Length; i++)
		{
			if (scaleNumbers[i] == randomScale)
			{
				return true;
			}
		}
		return false;
	}

	public void setScenariosProjectDictionary(Dictionary<string, string> ScenariosProject, Dictionary<string, string> validationScenarios, string scenariosPath)
	{
		studyScenariosLibrary = ScenariosProject;
		this.validationScenarios = validationScenarios;
		this.scenariosPath = scenariosPath;
	}

	private void button2_Click(object sender, EventArgs e)
	{
		base.DialogResult = MessageBox.Show(this, "Would you like to generate Power Point Slides for the selected Charts?", "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
		if (base.DialogResult != DialogResult.Cancel)
		{
			ExcelSheet excelSheet = new ExcelSheet();
			if (base.DialogResult == DialogResult.Yes)
			{
				excelSheet.automateDataset(dsCharts, drawChart: true, createPowerPoint: true);
			}
			else if (base.DialogResult == DialogResult.No)
			{
				excelSheet.automateDataset(dsCharts, drawChart: true, createPowerPoint: false);
			}
		}
		base.DialogResult = DialogResult.None;
	}

	private void button3_Click(object sender, EventArgs e)
	{
		base.Visible = false;
		base.DialogResult = DialogResult.OK;
	}

	private void elasticityDonePerSKUButton_Click(object sender, EventArgs e)
	{
		if (elasticitySKUsCombobox.Text != "")
		{
			System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
			DataTable dataTable = new DataTable();
			using (UndoRedoManager.Start(""))
			{
				if (elasticityScenarioCheckbox.Checked)
				{
					foreach (CheckBoxComboBoxItem checkBoxItem in elasticityScenarioCombobox.CheckBoxItems)
					{
						if (!checkBoxItem.Checked || checkBoxItem.Text.Equals("* All"))
						{
							continue;
						}
						ProjectCore.Project["ElasticityChartProject"].bindScenarioDatatableObjectToProjectProducts(getScenario(checkBoxItem.Text));
						if (elasticitySegmentationCheckbox.Checked)
						{
							foreach (CheckBoxComboBoxItem checkBoxItem2 in elasticitySegmentMembersCombobox.CheckBoxItems)
							{
								if (checkBoxItem2.Checked && !checkBoxItem2.Text.Equals("* All"))
								{
									ProjectCore.Project["ElasticityChartProject"].setSegmentation(elasticitySegmentationTypeCombobox.Text, checkBoxItem2.Text);
									dataTable = getElasticityDatatable(elasticitySKUsCombobox, "SKU", "ES(" + checkBoxItem2.Text + " at " + checkBoxItem.Text + ")");
								}
							}
						}
						else
						{
							ProjectCore.Project["ElasticityChartProject"].resetSegmentation();
							dataTable = getElasticityDatatable(elasticitySKUsCombobox, "SKU", "ES(total at " + checkBoxItem.Text + ")");
						}
					}
				}
				else
				{
					ProjectCore.Project["ElasticityChartProject"].resetProductValues();
					ProjectCore.Project["ElasticityChartProject"].resetSegmentation();
					if (elasticitySegmentationCheckbox.Checked)
					{
						foreach (CheckBoxComboBoxItem checkBoxItem3 in elasticitySegmentMembersCombobox.CheckBoxItems)
						{
							if (checkBoxItem3.Checked && !checkBoxItem3.Text.Equals("* All"))
							{
								ProjectCore.Project["ElasticityChartProject"].setSegmentation(elasticitySegmentationTypeCombobox.Text, checkBoxItem3.Text);
								dataTable = getElasticityDatatable(elasticitySKUsCombobox, "SKU", "ES(" + checkBoxItem3.Text + " at Default)");
							}
						}
					}
					else
					{
						ProjectCore.Project["ElasticityChartProject"].resetSegmentation();
						dataTable = getElasticityDatatable(elasticitySKUsCombobox, "SKU", "ES(total at Default)");
					}
				}
				UndoRedoManager.FlushHistory();
			}
			chart1.Series.Clear();
			foreach (DataRow row in dataTable.Rows)
			{
				string name = row["Name"].ToString();
				chart1.Series.Add(name);
				chart1.Series[name].ChartType = SeriesChartType.Line;
				chart1.Series[name].BorderWidth = 2;
				for (int i = 1; i < dataTable.Columns.Count; i++)
				{
					string columnName = dataTable.Columns[i].ColumnName;
					double num = double.Parse(row[columnName].ToString());
					chart1.Series[name].Points.AddXY(double.Parse(columnName).ToString("P0"), num);
				}
			}
			chart1.ChartAreas[0].AxisY.LabelStyle.Format = "P0";
		}
		else
		{
			MessageBox.Show(this, "You must specify at least one SKU", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void elasticityDonePerGroupButton_Click(object sender, EventArgs e)
	{
		if (elasticityGroupsCombobox.Text != "")
		{
			System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
			DataTable dataTable = new DataTable();
			using (UndoRedoManager.Start(""))
			{
				if (elasticityScenarioCheckbox.Checked)
				{
					foreach (CheckBoxComboBoxItem checkBoxItem in elasticityScenarioCombobox.CheckBoxItems)
					{
						if (!checkBoxItem.Checked || checkBoxItem.Text.Equals("* All"))
						{
							continue;
						}
						ProjectCore.Project["ElasticityChartProjectPerGroup"].bindScenarioDatatableObjectToProjectProducts(getScenario(checkBoxItem.Text));
						if (elasticitySegmentationCheckbox.Checked)
						{
							foreach (CheckBoxComboBoxItem checkBoxItem2 in elasticitySegmentMembersCombobox.CheckBoxItems)
							{
								if (checkBoxItem2.Checked && !checkBoxItem2.Text.Equals("* All"))
								{
									ProjectCore.Project["ElasticityChartProjectPerGroup"].setSegmentation(elasticitySegmentationTypeCombobox.Text, checkBoxItem2.Text);
									dataTable = getElasticityDatatable(elasticityGroupsCombobox, "Group", "EG(" + checkBoxItem2.Text + " at " + checkBoxItem.Text + ")");
								}
							}
						}
						else
						{
							ProjectCore.Project["ElasticityChartProjectPerGroup"].resetSegmentation();
							dataTable = getElasticityDatatable(elasticityGroupsCombobox, "Group", "EG(total at " + checkBoxItem.Text + ")");
						}
					}
				}
				else
				{
					ProjectCore.Project["ElasticityChartProjectPerGroup"].resetProductValues();
					ProjectCore.Project["ElasticityChartProjectPerGroup"].resetSegmentation();
					if (elasticitySegmentationCheckbox.Checked)
					{
						foreach (CheckBoxComboBoxItem checkBoxItem3 in elasticitySegmentMembersCombobox.CheckBoxItems)
						{
							if (checkBoxItem3.Checked && !checkBoxItem3.Text.Equals("* All"))
							{
								ProjectCore.Project["ElasticityChartProjectPerGroup"].setSegmentation(elasticitySegmentationTypeCombobox.Text, checkBoxItem3.Text);
								dataTable = getElasticityDatatable(elasticityGroupsCombobox, "Group", "EG(" + checkBoxItem3.Text + " at Default)");
							}
						}
					}
					else
					{
						ProjectCore.Project["ElasticityChartProjectPerGroup"].resetSegmentation();
						dataTable = getElasticityDatatable(elasticityGroupsCombobox, "Group", "EG(total at Default)");
					}
				}
				UndoRedoManager.FlushHistory();
			}
			chart2.Series.Clear();
			foreach (DataRow row in dataTable.Rows)
			{
				string name = row["Name"].ToString();
				chart2.Series.Add(name);
				chart2.Series[name].ChartType = SeriesChartType.Line;
				chart2.Series[name].BorderWidth = 2;
				for (int i = 1; i < dataTable.Columns.Count; i++)
				{
					string columnName = dataTable.Columns[i].ColumnName;
					double num = double.Parse(row[columnName].ToString());
					chart2.Series[name].Points.AddXY(double.Parse(columnName).ToString("P0"), num);
				}
			}
			chart2.ChartAreas[0].AxisY.LabelStyle.Format = "P0";
		}
		else
		{
			MessageBox.Show(this, "You must specify at least one group", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void elasticityDonePerCompanyButton_Click(object sender, EventArgs e)
	{
		if (elasticityCompanySeriesCombobox.Text != "")
		{
			System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
			DataTable dataTable = new DataTable();
			using (UndoRedoManager.Start(""))
			{
				if (elasticityScenarioCheckbox.Checked)
				{
					foreach (CheckBoxComboBoxItem checkBoxItem in elasticityScenarioCombobox.CheckBoxItems)
					{
						if (!checkBoxItem.Checked || checkBoxItem.Text.Equals("* All"))
						{
							continue;
						}
						ProjectCore.Project["ElasticityChartProjectPerCompany"].bindScenarioDatatableObjectToProjectProducts(getScenario(checkBoxItem.Text));
						if (elasticitySegmentationCheckbox.Checked)
						{
							foreach (CheckBoxComboBoxItem checkBoxItem2 in elasticitySegmentMembersCombobox.CheckBoxItems)
							{
								if (checkBoxItem2.Checked && !checkBoxItem2.Text.Equals("* All"))
								{
									ProjectCore.Project["ElasticityChartProjectPerCompany"].setSegmentation(elasticitySegmentationTypeCombobox.Text, checkBoxItem2.Text);
									if (elasticityPerCompanySKUCheckbox.Checked)
									{
										dataTable = getElasticityDatatable(elasticityCompanySeriesCombobox, "CompanySKU", "EC(" + checkBoxItem2.Text + " at " + checkBoxItem.Text + ")");
									}
									else if (elasticityPerCompanyGroupCheckbox.Checked)
									{
										dataTable = getElasticityDatatable(elasticityCompanySeriesCombobox, "CompanyGroup", "EC(" + checkBoxItem2.Text + " at " + checkBoxItem.Text + ")");
									}
								}
							}
						}
						else
						{
							ProjectCore.Project["ElasticityChartProjectPerCompany"].resetSegmentation();
							if (elasticityPerCompanySKUCheckbox.Checked)
							{
								dataTable = getElasticityDatatable(elasticityCompanySeriesCombobox, "CompanySKU", "EC(total at " + checkBoxItem.Text + ")");
							}
							else if (elasticityPerCompanyGroupCheckbox.Checked)
							{
								dataTable = getElasticityDatatable(elasticityCompanySeriesCombobox, "CompanyGroup", "EC(total at " + checkBoxItem.Text + ")");
							}
						}
					}
				}
				else
				{
					ProjectCore.Project["ElasticityChartProjectPerCompany"].resetProductValues();
					ProjectCore.Project["ElasticityChartProjectPerCompany"].resetSegmentation();
					if (elasticitySegmentationCheckbox.Checked)
					{
						foreach (CheckBoxComboBoxItem checkBoxItem3 in elasticitySegmentMembersCombobox.CheckBoxItems)
						{
							if (checkBoxItem3.Checked && !checkBoxItem3.Text.Equals("* All"))
							{
								ProjectCore.Project["ElasticityChartProjectPerCompany"].setSegmentation(elasticitySegmentationTypeCombobox.Text, checkBoxItem3.Text);
								if (elasticityPerCompanySKUCheckbox.Checked)
								{
									dataTable = getElasticityDatatable(elasticityCompanySeriesCombobox, "CompanySKU", "EC(" + checkBoxItem3.Text + " at Default)");
								}
								else if (elasticityPerCompanyGroupCheckbox.Checked)
								{
									dataTable = getElasticityDatatable(elasticityCompanySeriesCombobox, "CompanyGroup", "EC(" + checkBoxItem3.Text + " at Default)");
								}
							}
						}
					}
					else
					{
						ProjectCore.Project["ElasticityChartProjectPerCompany"].resetSegmentation();
						if (elasticityPerCompanySKUCheckbox.Checked)
						{
							dataTable = getElasticityDatatable(elasticityCompanySeriesCombobox, "CompanySKU", "EC(total at Default)");
						}
						else if (elasticityPerCompanyGroupCheckbox.Checked)
						{
							dataTable = getElasticityDatatable(elasticityCompanySeriesCombobox, "CompanyGroup", "EC(total at Default)");
						}
					}
				}
				UndoRedoManager.FlushHistory();
			}
			chart3.Series.Clear();
			foreach (DataRow row in dataTable.Rows)
			{
				string name = row["Name"].ToString();
				chart3.Series.Add(name);
				chart3.Series[name].ChartType = SeriesChartType.Line;
				chart3.Series[name].BorderWidth = 2;
				for (int i = 1; i < dataTable.Columns.Count; i++)
				{
					string columnName = dataTable.Columns[i].ColumnName;
					double num = double.Parse(row[columnName].ToString());
					chart3.Series[name].Points.AddXY(double.Parse(columnName).ToString("P0"), num.ToString("P0"));
				}
			}
			chart3.ChartAreas[0].AxisY.LabelStyle.Format = "P0";
		}
		else
		{
			MessageBox.Show(this, "You must specify at least one SKU", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void priceIndexDoneButton_Click(object sender, EventArgs e)
	{
		if (!validateChartName(priceIndexNameTextbox.Text))
		{
			return;
		}
		if (priceIndexSeriesCombobox.Text != "")
		{
			System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
			DataTable dataTable = null;
			DataTable dataTable2 = null;
			string[] array = priceIndexScaleTextbox.Text.Split(new string[1] { "," }, StringSplitOptions.RemoveEmptyEntries);
			if (priceIndexGroupsRadiobox.Checked)
			{
				dataTable = ProjectCore.Project["IndexAndRevenuesChartProject"].priceIndexAndRevenues(priceIndexGroupTypeCombobox.SelectedItem.ToString(), priceIndexGroupNameCombobox.SelectedItem.ToString(), priceIndexSKUNameCombobox.SelectedItem.ToString(), priceIndexReferenceSKUNameCombobox.SelectedItem.ToString(), array);
				dataTable2 = dataTable.Clone();
				foreach (string item in ProjectCore.Project["IndexAndRevenuesChartProject"].GroupMember[priceIndexGroupTypeCombobox.SelectedItem.ToString()])
				{
					if (!priceIndexSeriesCombobox.CheckBoxItems[item].Checked)
					{
						continue;
					}
					double[] array2 = new double[array.Length];
					foreach (DataRow row in dataTable.Rows)
					{
						for (int i = 0; i < array.Length; i++)
						{
							if (ProjectCore.Project["IndexAndRevenuesChartProject"].GroupProducts[priceIndexGroupTypeCombobox.SelectedItem.ToString()][item].Contains(row["Name"].ToString()))
							{
								array2[i] += double.Parse(row[array[i]].ToString());
							}
						}
					}
					DataRow dataRow2 = dataTable2.NewRow();
					dataRow2["Name"] = item;
					for (int i = 0; i < array.Length; i++)
					{
						dataRow2[i + 1] = array2[i];
					}
					dataTable2.Rows.Add(dataRow2);
				}
			}
			else if (priceIndexSKUsRadiobox.Checked)
			{
				dataTable = ProjectCore.Project["IndexAndRevenuesChartProject"].priceIndexAndRevenues(priceIndexSKUNameCombobox.SelectedItem.ToString(), priceIndexReferenceSKUNameCombobox.SelectedItem.ToString(), array);
				dataTable2 = dataTable.Clone();
				for (int i = 0; i < dataTable.Rows.Count - 2; i++)
				{
					if (priceIndexSeriesCombobox.CheckBoxItems[dataTable.Rows[i]["Name"].ToString()].Checked)
					{
						dataTable2.Rows.Add(dataTable.Rows[i].ItemArray);
					}
				}
			}
			if (priceIndexTotalCompanyShare.Checked)
			{
				dataTable2.Rows.Add(dataTable.Rows[dataTable.Rows.Count - 2].ItemArray);
			}
			if (priceIndexTotalCompanyRevenue.Checked)
			{
				dataTable2.Rows.Add(dataTable.Rows[dataTable.Rows.Count - 1].ItemArray);
			}
			chart4.Series.Clear();
			foreach (DataRow row2 in dataTable2.Rows)
			{
				string text = row2["Name"].ToString();
				chart4.Series.Add(text);
				chart4.Series[text].ChartType = SeriesChartType.Line;
				chart4.Series[text].MarkerStyle = MarkerStyle.Circle;
				chart4.Series[text].BorderWidth = 2;
				chart4.Series[text].SmartLabelStyle.Enabled = true;
				chart4.Series[text].IsValueShownAsLabel = true;
				if (text.EndsWith(" Revenues"))
				{
					chart4.Series[text].YAxisType = AxisType.Secondary;
					chart4.ChartAreas[0].AxisY2.MajorGrid.Enabled = false;
					chart4.Series[text].LabelFormat = "0.00";
				}
				else
				{
					chart4.Series[text].LabelFormat = "0.0%";
				}
				for (int j = 1; j < dataTable2.Columns.Count; j++)
				{
					string columnName = dataTable2.Columns[j].ColumnName;
					double num = double.Parse(row2[columnName].ToString());
					if (!double.IsNaN(num) && !double.IsInfinity(num))
					{
						chart4.Series[text].Points.AddXY(columnName, num);
					}
				}
			}
			chart4.ChartAreas[0].AxisY.LabelStyle.Format = "P0";
			if (!priceIndexNameTextbox.Text.Trim().Equals(""))
			{
				dataTable2.TableName = priceIndexNameTextbox.Text;
			}
			if (dsCharts.Tables.Contains(dataTable2.TableName))
			{
				dsCharts.Tables.Remove(dataTable2.TableName);
			}
			dsCharts.Tables.Add(dataTable2);
		}
		else
		{
			MessageBox.Show(this, "You must specify at least one Series", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void brandInteractionDoneButton_Click(object sender, EventArgs e)
	{
		if (!validateChartName(brandIneractionNameTextbox.Text))
		{
			return;
		}
		if (brandInteractionSeriesCombobox.Text != "")
		{
			System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
			Dictionary<string, ProjectCore> dictionary = new Dictionary<string, ProjectCore>();
			foreach (CheckBoxComboBoxItem checkBoxItem in brandInteractionScenariosCombobox.CheckBoxItems)
			{
				if (!checkBoxItem.Text.Equals("* All") && checkBoxItem.Checked)
				{
					ProjectCore projectCore = new ProjectCore();
					projectCore.initializeProjectMembers();
					using (UndoRedoManager.Start(""))
					{
						projectCore.bindScenarioDatatableObjectToProjectProducts(getScenario(checkBoxItem.Text));
						UndoRedoManager.FlushHistory();
					}
					dictionary.Add(checkBoxItem.Text, projectCore);
				}
			}
			DataTable dataTable = null;
			if (brandInteractionGroupRadiobox.Checked)
			{
				dataTable = ((!brandInteractionBasicScenarioCombobox.SelectedItem.ToString().Equals("Default")) ? ProjectCore.Project["BrandInteractionChartProject"].crossBrandInteractionPerGroup(brandInteractionGroupTypeCombobox.SelectedItem.ToString(), getBaseScenarioProject(brandInteractionBasicScenarioCombobox.Text), dictionary) : ProjectCore.Project["BrandInteractionChartProject"].crossBrandInteractionPerGroup(brandInteractionGroupTypeCombobox.SelectedItem.ToString(), dictionary));
			}
			else if (brandInteractionSKURadiobox.Checked)
			{
				dataTable = ((!brandInteractionBasicScenarioCombobox.SelectedItem.ToString().Equals("Default")) ? ProjectCore.Project["BrandInteractionChartProject"].crossBrandInteractionPerProduct(getBaseScenarioProject(brandInteractionBasicScenarioCombobox.Text), dictionary) : ProjectCore.Project["BrandInteractionChartProject"].crossBrandInteractionPerProduct(dictionary));
			}
			DataTable dataTable2 = dataTable.Clone();
			foreach (DataRow row in dataTable.Rows)
			{
				if (brandInteractionSeriesCombobox.CheckBoxItems[row["Name"].ToString()].Checked)
				{
					dataTable2.Rows.Add(row.ItemArray);
				}
			}
			chart5.Series.Clear();
			foreach (DataRow row2 in dataTable2.Rows)
			{
				string name = row2["Name"].ToString();
				chart5.Series.Add(name);
				chart5.Series[name].ChartType = SeriesChartType.StackedColumn;
				chart5.Series[name].BorderWidth = 2;
				chart5.Series[name].IsValueShownAsLabel = true;
				chart5.Series[name].LabelFormat = "0.00%";
				chart5.Series[name].SmartLabelStyle.Enabled = true;
				for (int i = 1; i < dataTable2.Columns.Count; i++)
				{
					string columnName = dataTable2.Columns[i].ColumnName;
					double num = double.Parse(row2[columnName].ToString());
					chart5.Series[name].Points.AddXY(columnName, num);
				}
			}
			chart5.ChartAreas[0].AxisY.LineDashStyle = ChartDashStyle.NotSet;
			chart5.ChartAreas[0].AxisY.LabelStyle.Enabled = false;
			chart5.ChartAreas[0].AxisY.MajorTickMark.Enabled = false;
			chart5.ChartAreas[0].AxisX.MajorTickMark.Enabled = true;
			chart5.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
			if (!brandIneractionNameTextbox.Text.Trim().Equals(""))
			{
				dataTable2.TableName = brandIneractionNameTextbox.Text;
			}
			if (dsCharts.Tables.Contains(dataTable2.TableName))
			{
				dsCharts.Tables.Remove(dataTable2.TableName);
			}
			dsCharts.Tables.Add(dataTable2);
		}
		else
		{
			MessageBox.Show(this, "You must specify at least one Series", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void somsopDoneButton_Click(object sender, EventArgs e)
	{
		if (!validateChartName(somsopNameTextbox.Text))
		{
			return;
		}
		if (somsopSeriesCombobox.Text != "")
		{
			System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
			DataTable dataTable = ProjectCore.Project["SOMVsSOPChartProject"].SoMvsSoP();
			DataTable dataTable2 = dataTable.Clone();
			if (somsopGroupRadiobox.Checked)
			{
				foreach (string item in project.GroupMember[somsopGroupTypeCombobox.SelectedItem.ToString()])
				{
					if (!somsopSeriesCombobox.CheckBoxItems[item].Checked)
					{
						continue;
					}
					double[] array = new double[dataTable.Columns.Count - 1];
					foreach (DataRow row in dataTable.Rows)
					{
						for (int i = 0; i < dataTable.Columns.Count - 1; i++)
						{
							if (project.GroupProducts[somsopGroupTypeCombobox.SelectedItem.ToString()][item].Contains(row["Name"].ToString()))
							{
								array[i] += double.Parse(row[i + 1].ToString());
							}
						}
					}
					DataRow dataRow2 = dataTable2.NewRow();
					dataRow2["Name"] = item;
					for (int i = 0; i < dataTable2.Columns.Count - 1; i++)
					{
						dataRow2[i + 1] = array[i];
					}
					dataTable2.Rows.Add(dataRow2);
				}
			}
			else if (somsopSKURadiobox.Checked)
			{
				foreach (DataRow row2 in dataTable.Rows)
				{
					if (somsopSeriesCombobox.CheckBoxItems[row2["Name"].ToString()].Checked)
					{
						dataTable2.Rows.Add(row2.ItemArray);
					}
				}
			}
			chart6.Series.Clear();
			for (int i = 1; i < dataTable2.Columns.Count; i++)
			{
				string text = dataTable2.Columns[i].ToString();
				chart6.Series.Add(text);
				if (text.Equals("SOM"))
				{
					chart6.Series[text].ChartType = SeriesChartType.Column;
					chart6.Series[text]["LabelStyle"] = "Bottom";
				}
				else if (text.Equals("Brand Share"))
				{
					chart6.Series[text].ChartType = SeriesChartType.Line;
					chart6.Series[text].MarkerStyle = MarkerStyle.Circle;
					chart6.Series[text]["LabelStyle"] = "Top";
				}
				chart6.Series[text].BorderWidth = 2;
				chart6.Series[text].IsValueShownAsLabel = true;
				chart6.Series[text].SmartLabelStyle.Enabled = true;
				chart6.Series[text].LabelFormat = "0.0%";
				foreach (DataRow row3 in dataTable2.Rows)
				{
					string xValue = row3["Name"].ToString();
					double num = double.Parse(row3[i].ToString());
					chart6.Series[text].Points.AddXY(xValue, num);
				}
			}
			chart6.ChartAreas[0].AxisY.LabelStyle.Format = "P0";
			if (!somsopNameTextbox.Text.Trim().Equals(""))
			{
				dataTable2.TableName = somsopNameTextbox.Text;
			}
			if (dsCharts.Tables.Contains(dataTable2.TableName))
			{
				dsCharts.Tables.Remove(dataTable2.TableName);
			}
			dsCharts.Tables.Add(dataTable2);
		}
		else
		{
			MessageBox.Show(this, "You must specify at least one Series", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void fairShareDoneButton_Click(object sender, EventArgs e)
	{
		if (!validateChartName(fairShareTextbox.Text))
		{
			return;
		}
		if (fairShareSeriesCombobox.Text != "")
		{
			if (fairSharAttributeCombobox.SelectedItem != null)
			{
				System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
				DataTable dataTable = null;
				ProjectCore basicScenarioProject = ((!fairShareBasicScenarioCombobox.Text.Equals("Default")) ? getBaseScenarioProject(fairShareBasicScenarioCombobox.Text) : ProjectCore.Project["BaseScenario"]);
				ProjectCore scenarioProject = ((!fairShareScenarioCombobox.Text.Equals("Current")) ? getBaseScenarioProject(fairShareScenarioCombobox.Text) : ProjectCore.Project["FairShareChartProject"]);
				if (fairShareGroupRadiobox.Checked)
				{
					dataTable = ProjectCore.Project["FairShareChartProject"].FairShareOnBrands(fairSharAttributeCombobox.Text, fairShareGroupTypeCombobox.Text, fairshareChangeCombobox.Text, basicScenarioProject, scenarioProject);
				}
				else if (fairShareSKURadiobox.Checked)
				{
					dataTable = ProjectCore.Project["FairShareChartProject"].FairShareOnSKUs(fairSharAttributeCombobox.Text, fairshareChangeCombobox.Text, basicScenarioProject, scenarioProject);
				}
				chart8.Series.Clear();
				string name = "FairShare";
				chart8.Series.Add(name);
				chart8.Series[name].ChartType = SeriesChartType.Bar;
				chart8.Series[name].BorderWidth = 2;
				foreach (DataRow row in dataTable.Rows)
				{
					string xValue = row["Name"].ToString();
					if (row["Index"].ToString().Equals(""))
					{
						chart8.Series[name].Points.AddXY(xValue, "");
						continue;
					}
					double num = double.Parse(row["Index"].ToString());
					if (num != 0.0)
					{
						chart8.Series[name].Points.AddXY(xValue, Math.Round(num, 2));
					}
				}
				chart8.ChartAreas[0].AxisX.Crossing = -1.0;
				chart8.ChartAreas[0].AxisY.Crossing = 1.0;
				if (!fairShareTextbox.Text.Trim().Equals(""))
				{
					dataTable.TableName = fairShareTextbox.Text;
				}
				if (dsCharts.Tables.Contains(dataTable.TableName))
				{
					dsCharts.Tables.Remove(dataTable.TableName);
				}
				dsCharts.Tables.Add(dataTable);
			}
			else
			{
				MessageBox.Show(this, "Choose the attribute on which measuring the fair share", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		else
		{
			MessageBox.Show(this, "You must specify at least one Series", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void validationDoneButton_Click(object sender, EventArgs e)
	{
		System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
		DataTable dataTable = null;
		if (!string.IsNullOrEmpty(validationSeriesCombobox.Text))
		{
			if (!string.IsNullOrEmpty(validationScenarioCheckboxCombobox.Text))
			{
				ProjectCore projectCore;
				if (validationByBrandRadiobutton.Checked)
				{
					foreach (CheckBoxComboBoxItem checkBoxItem in validationSeriesCombobox.CheckBoxItems)
					{
						if (!checkBoxItem.Checked || checkBoxItem.Text.Equals("* All"))
						{
							continue;
						}
						dataTable = new DataTable(checkBoxItem.Text + "Validation");
						dataTable.Columns.Add(checkBoxItem.Text, typeof(string));
						dataTable.Columns.Add("Audit Share", typeof(string));
						dataTable.Columns.Add("Rebased Simulator Share", typeof(string));
						foreach (CheckBoxComboBoxItem checkBoxItem2 in validationScenarioCheckboxCombobox.CheckBoxItems)
						{
							if (checkBoxItem2.Checked && !checkBoxItem2.Text.Equals("* All"))
							{
								using (UndoRedoManager.Start(""))
								{
									ProjectCore.Project["ValidationChartProject"].bindScenarioDatatableObjectToProjectProducts(getScenario(checkBoxItem2.Text));
									projectCore = ProjectCore.Project["ValidationChartProject"];
									projectCore.findPredictedShares();
									UndoRedoManager.FlushHistory();
								}
								projectCore.rebaseProductNewShare(projectCore.getTotalProductsAuditShare());
								DataRow dataRow = dataTable.NewRow();
								dataRow[checkBoxItem.Text] = checkBoxItem2.Text;
								if (validationGroupRadiobox.Checked)
								{
									dataRow["Audit Share"] = projectCore.getGroupShare("AuditShare", validationGroupTypeCombobox.SelectedItem.ToString(), projectCore.getGroupMemberIndex(validationGroupTypeCombobox.SelectedItem.ToString(), checkBoxItem.Text)) / 100.0;
									dataRow["Rebased Simulator Share"] = projectCore.getGroupShare("NewShare", validationGroupTypeCombobox.SelectedItem.ToString(), projectCore.getGroupMemberIndex(validationGroupTypeCombobox.SelectedItem.ToString(), checkBoxItem.Text)) / 100.0;
								}
								else if (validationSKURadiobox.Checked)
								{
									dataRow["Audit Share"] = projectCore.getProductShare("AuditShare", checkBoxItem.Text) / 100.0;
									dataRow["Rebased Simulator Share"] = projectCore.getProductShare("NewShare", checkBoxItem.Text) / 100.0;
								}
								dataTable.Rows.Add(dataRow);
							}
						}
						if (dsCharts.Tables.Contains(dataTable.TableName))
						{
							dsCharts.Tables.Remove(dataTable.TableName);
						}
						dataTable.ExtendedProperties.Add("ChartName", "Validation");
						dsCharts.Tables.Add(dataTable);
					}
				}
				else if (validationByMonthRadioButton.Checked)
				{
					foreach (CheckBoxComboBoxItem checkBoxItem3 in validationScenarioCheckboxCombobox.CheckBoxItems)
					{
						if (!checkBoxItem3.Checked || checkBoxItem3.Text.Equals("* All"))
						{
							continue;
						}
						dataTable = new DataTable(checkBoxItem3.Text + "Validation");
						dataTable.Columns.Add(checkBoxItem3.Text, typeof(string));
						dataTable.Columns.Add("Audit Share", typeof(string));
						dataTable.Columns.Add("Rebased Simulator Share", typeof(string));
						using (UndoRedoManager.Start(""))
						{
							ProjectCore.Project["ValidationChartProject"].bindScenarioDatatableObjectToProjectProducts(getScenario(checkBoxItem3.Text));
							projectCore = ProjectCore.Project["ValidationChartProject"];
							projectCore.findPredictedShares();
							UndoRedoManager.FlushHistory();
						}
						projectCore.rebaseProductNewShare(projectCore.getTotalProductsAuditShare());
						foreach (CheckBoxComboBoxItem checkBoxItem4 in validationSeriesCombobox.CheckBoxItems)
						{
							if (checkBoxItem4.Checked && !checkBoxItem4.Text.Equals("* All"))
							{
								DataRow dataRow = dataTable.NewRow();
								dataRow[checkBoxItem3.Text] = checkBoxItem4.Text;
								if (validationGroupRadiobox.Checked)
								{
									dataRow["Audit Share"] = projectCore.getGroupShare("AuditShare", validationGroupTypeCombobox.SelectedItem.ToString(), projectCore.getGroupMemberIndex(validationGroupTypeCombobox.SelectedItem.ToString(), checkBoxItem4.Text)) / 100.0;
									dataRow["Rebased Simulator Share"] = projectCore.getGroupShare("NewShare", validationGroupTypeCombobox.SelectedItem.ToString(), projectCore.getGroupMemberIndex(validationGroupTypeCombobox.SelectedItem.ToString(), checkBoxItem4.Text)) / 100.0;
								}
								else if (validationSKURadiobox.Checked)
								{
									dataRow["Audit Share"] = projectCore.getProductShare("AuditShare", checkBoxItem4.Text) / 100.0;
									dataRow["Rebased Simulator Share"] = projectCore.getProductShare("NewShare", checkBoxItem4.Text) / 100.0;
								}
								dataTable.Rows.Add(dataRow);
							}
						}
						if (dsCharts.Tables.Contains(dataTable.TableName))
						{
							dsCharts.Tables.Remove(dataTable.TableName);
						}
						dataTable.ExtendedProperties.Add("ChartName", "Validation");
						dsCharts.Tables.Add(dataTable);
					}
				}
				chart9.Series.Clear();
				for (int i = 1; i < dataTable.Columns.Count; i++)
				{
					string name = dataTable.Columns[i].ToString();
					chart9.Series.Add(name);
					chart9.Series[name].ChartType = SeriesChartType.Line;
					chart9.Series[name].MarkerStyle = MarkerStyle.Circle;
					chart9.Series[name].BorderWidth = 2;
					foreach (DataRow row in dataTable.Rows)
					{
						string xValue = row[0].ToString();
						double num = double.Parse(row[i].ToString());
						chart9.Series[name].Points.AddXY(xValue, num);
					}
				}
				chart9.ChartAreas[0].AxisY.LabelStyle.Format = "P0";
			}
			else
			{
				MessageBox.Show(this, "You must specify at least one Scenario", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		else
		{
			MessageBox.Show(this, "You must specify at least one Series", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
	{
	}

	private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
	{
		switch (e.TabPage.Text)
		{
		case "Price Index":
			if (!priceIndexGroupsRadiobox.Checked && !priceIndexSKUsRadiobox.Checked)
			{
				priceIndexGroupsRadiobox.Checked = true;
			}
			break;
		case "Shares && Revenues":
			if (!sharesGroupRadiobox.Checked && !shareSKURadiobox.Checked)
			{
				sharesGroupRadiobox.Checked = true;
			}
			break;
		case "Brand Interaction":
			if (!brandInteractionGroupRadiobox.Checked && !brandInteractionSKURadiobox.Checked)
			{
				brandInteractionGroupRadiobox.Checked = true;
				brandInteractionBasicScenarioCombobox.SelectedIndex = 0;
			}
			break;
		case "SOM Vs. SOP":
			if (!somsopGroupRadiobox.Checked && !somsopSKURadiobox.Checked)
			{
				somsopGroupRadiobox.Checked = true;
			}
			break;
		case "Fair Share":
			if (!fairShareGroupRadiobox.Checked && !fairShareSKURadiobox.Checked)
			{
				fairShareGroupRadiobox.Checked = true;
				fairShareBasicScenarioCombobox.SelectedIndex = 0;
				fairShareScenarioCombobox.SelectedIndex = 0;
			}
			break;
		case "Validation":
			if (!validationGroupRadiobox.Checked && !validationSKURadiobox.Checked)
			{
				validationGroupRadiobox.Checked = true;
			}
			break;
		}
	}

	private void sharesGroupRadiobox_CheckedChanged(object sender, EventArgs e)
	{
		if (sharesGroupRadiobox.Checked)
		{
			sharesGroupTypeCombobox.Enabled = true;
			sharesGroupTypeCombobox.DataSource = project.GroupTypeList.GetRange(0, project.GroupTypeList.Count);
			sharesSeriesCombobox.CheckBoxItems[0].Checked = false;
		}
	}

	private void shareSKURadiobox_CheckedChanged(object sender, EventArgs e)
	{
		if (shareSKURadiobox.Checked)
		{
			sharesGroupTypeCombobox.Enabled = false;
			sharesGroupTypeCombobox.Text = "";
			sharesSeriesCombobox.Items.Clear();
			sharesSeriesCombobox.Text = "";
			sharesSeriesCombobox.Items.Add("* All");
			sharesSeriesCombobox.CheckBoxItems[0].Checked = false;
			sharesSeriesCombobox.Items.AddRange(project.AllProducts.ToArray());
			sharesSeriesCombobox.Items.Add("Company");
		}
	}

	private void sharesGroupTypeCombobox_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (sharesGroupTypeCombobox.Text != "")
		{
			ProjectCore.Project["SharesChartProject"].setGroupResearchStart(sharesGroupTypeCombobox.SelectedItem.ToString(), grouping: true);
			sharesSeriesCombobox.Items.Clear();
			sharesSeriesCombobox.Text = "";
			sharesSeriesCombobox.Items.Add("* All");
			sharesSeriesCombobox.Items.AddRange(project.GroupMember[sharesGroupTypeCombobox.Text].ToArray());
			sharesSeriesCombobox.Items.Add("Company");
		}
	}

	private void sharesDoneButton_Click(object sender, EventArgs e)
	{
		if (!validateChartName(sharesNameTextBox.Text))
		{
			return;
		}
		if (!string.IsNullOrEmpty(sharesSenariosCombobox.Text) && !string.IsNullOrEmpty(sharesSeriesCombobox.Text))
		{
			System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
			ProjectCore projectCore = ProjectCore.Project["SharesChartProject"];
			DataTable dataTable = new DataTable("Shares And R.Index");
			DataTable dataTable2 = new DataTable("Revenues");
			DataTable dataTable3 = new DataTable("S.Index And Revenues");
			DataTable dataTable4 = new DataTable("Index Revenues");
			DataTable dataTable5 = new DataTable("Value Share And Index");
			DataTable dataTable6 = new DataTable("Index Value Share");
			DataTable dataTable7 = new DataTable("Weighted SOP And Index");
			DataTable dataTable8 = new DataTable("Index Weighted SOP");
			dataTable.Columns.Add("Name", typeof(string));
			dataTable2.Columns.Add("Name", typeof(string));
			dataTable3.Columns.Add("Name", typeof(string));
			dataTable4.Columns.Add("Name", typeof(string));
			dataTable5.Columns.Add("Name", typeof(string));
			dataTable6.Columns.Add("Name", typeof(string));
			dataTable7.Columns.Add("Name", typeof(string));
			dataTable8.Columns.Add("Name", typeof(string));
			foreach (CheckBoxComboBoxItem checkBoxItem in sharesSeriesCombobox.CheckBoxItems)
			{
				if (checkBoxItem.Checked && !checkBoxItem.Text.Equals("* All"))
				{
					DataRow dataRow = dataTable.NewRow();
					DataRow dataRow2 = dataTable2.NewRow();
					DataRow dataRow3 = dataTable3.NewRow();
					DataRow dataRow4 = dataTable4.NewRow();
					DataRow dataRow5 = dataTable5.NewRow();
					DataRow dataRow6 = dataTable6.NewRow();
					DataRow dataRow7 = dataTable7.NewRow();
					DataRow dataRow8 = dataTable8.NewRow();
					if (string.Equals(checkBoxItem.Text, "Company"))
					{
						dataRow["Name"] = project.CompanyName + " Shares";
						dataRow2["Name"] = project.CompanyName + " Revenues";
						dataRow3["Name"] = project.CompanyName + " S.Index";
						dataRow4["Name"] = project.CompanyName + " R.Index";
						dataRow5["Name"] = project.CompanyName + " Value Share";
						dataRow6["Name"] = project.CompanyName + " VS.Index";
						dataRow7["Name"] = project.CompanyName + " Penetration";
						dataRow8["Name"] = project.CompanyName + " P.Index";
					}
					else
					{
						dataRow["Name"] = checkBoxItem.Text;
						dataRow2["Name"] = checkBoxItem.Text;
						dataRow3["Name"] = checkBoxItem.Text + " S.Index";
						dataRow4["Name"] = checkBoxItem.Text + " R.Index";
						dataRow5["Name"] = checkBoxItem.Text;
						dataRow6["Name"] = checkBoxItem.Text + " VS.Index";
						dataRow7["Name"] = checkBoxItem.Text;
						dataRow8["Name"] = checkBoxItem.Text + " P.Index";
					}
					dataTable.Rows.Add(dataRow);
					dataTable2.Rows.Add(dataRow2);
					dataTable3.Rows.Add(dataRow3);
					dataTable4.Rows.Add(dataRow4);
					dataTable5.Rows.Add(dataRow5);
					dataTable6.Rows.Add(dataRow6);
					dataTable7.Rows.Add(dataRow7);
					dataTable8.Rows.Add(dataRow8);
				}
			}
			foreach (CheckBoxComboBoxItem checkBoxItem2 in sharesSenariosCombobox.CheckBoxItems)
			{
				if (!checkBoxItem2.Checked || checkBoxItem2.Text.Equals("* All"))
				{
					continue;
				}
				dataTable.Columns.Add(checkBoxItem2.Text, typeof(string));
				dataTable2.Columns.Add(checkBoxItem2.Text, typeof(string));
				dataTable3.Columns.Add(checkBoxItem2.Text, typeof(string));
				dataTable4.Columns.Add(checkBoxItem2.Text, typeof(string));
				dataTable5.Columns.Add(checkBoxItem2.Text, typeof(string));
				dataTable6.Columns.Add(checkBoxItem2.Text, typeof(string));
				dataTable7.Columns.Add(checkBoxItem2.Text, typeof(string));
				dataTable8.Columns.Add(checkBoxItem2.Text, typeof(string));
				using (UndoRedoManager.Start(""))
				{
					if (string.Equals(checkBoxItem2.Text, "Current"))
					{
						projectCore.resetProductValues();
						projectCore.resetSegmentation();
					}
					else
					{
						ProjectCore.Project["SharesChartProject"].bindScenarioDatatableObjectToProjectProducts(getScenario(checkBoxItem2.Text));
						projectCore = ProjectCore.Project["SharesChartProject"];
					}
					projectCore.findPredictedShares();
					UndoRedoManager.FlushHistory();
				}
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					double num = 0.0;
					double num2 = 0.0;
					double num3 = 0.0;
					double num4 = 0.0;
					double num5 = 0.0;
					double num6 = 0.0;
					double num7 = 0.0;
					double num8 = 0.0;
					if (!dataTable.Rows[i]["Name"].ToString().EndsWith(" Shares"))
					{
						if (sharesGroupRadiobox.Checked)
						{
							num = projectCore.getGroupShare("NewShare", sharesGroupTypeCombobox.SelectedItem.ToString(), projectCore.getGroupMemberIndex(sharesGroupTypeCombobox.SelectedItem.ToString(), dataTable.Rows[i]["Name"].ToString()));
							num2 = projectCore.getGroupShare("BaseMarketShare", sharesGroupTypeCombobox.SelectedItem.ToString(), projectCore.getGroupMemberIndex(sharesGroupTypeCombobox.SelectedItem.ToString(), dataTable.Rows[i]["Name"].ToString()));
							num3 = projectCore.getGroupRevenue(sharesGroupTypeCombobox.SelectedItem.ToString(), projectCore.getGroupMemberIndex(sharesGroupTypeCombobox.SelectedItem.ToString(), dataTable.Rows[i]["Name"].ToString()));
							num4 = projectCore.getGroupBaseRevenue(sharesGroupTypeCombobox.SelectedItem.ToString(), projectCore.getGroupMemberIndex(sharesGroupTypeCombobox.SelectedItem.ToString(), dataTable.Rows[i]["Name"].ToString()));
							num5 = projectCore.getGroupValueShare(sharesGroupTypeCombobox.SelectedItem.ToString(), projectCore.getGroupMemberIndex(sharesGroupTypeCombobox.SelectedItem.ToString(), dataTable.Rows[i]["Name"].ToString()));
							num6 = projectCore.getGroupBaseValueShare(sharesGroupTypeCombobox.SelectedItem.ToString(), projectCore.getGroupMemberIndex(sharesGroupTypeCombobox.SelectedItem.ToString(), dataTable.Rows[i]["Name"].ToString()));
							num7 = projectCore.getGroupPenetration(sharesGroupTypeCombobox.SelectedItem.ToString(), projectCore.getGroupMemberIndex(sharesGroupTypeCombobox.SelectedItem.ToString(), dataTable.Rows[i]["Name"].ToString()));
							num8 = projectCore.getGroupBasePenetration(sharesGroupTypeCombobox.SelectedItem.ToString(), projectCore.getGroupMemberIndex(sharesGroupTypeCombobox.SelectedItem.ToString(), dataTable.Rows[i]["Name"].ToString()));
						}
						else if (shareSKURadiobox.Checked)
						{
							num = projectCore.getProductShare("NewShare", dataTable.Rows[i]["Name"].ToString());
							num2 = projectCore.getProductShare("BaseMarketShare", dataTable.Rows[i]["Name"].ToString());
							num3 = projectCore.getProductRevenue(dataTable.Rows[i]["Name"].ToString());
							num4 = projectCore.getProductBaseRevenue(dataTable.Rows[i]["Name"].ToString());
							num5 = projectCore.getProductValueShare(dataTable.Rows[i]["Name"].ToString());
							num6 = projectCore.getProductBaseValueShare(dataTable.Rows[i]["Name"].ToString());
							num7 = projectCore.getProductPenetration(dataTable.Rows[i]["Name"].ToString());
							num8 = projectCore.getProductBasePenetration(dataTable.Rows[i]["Name"].ToString());
						}
					}
					else
					{
						num = projectCore.getCompanyShare("NewShare");
						num2 = projectCore.getCompanyShare("BaseMarketShare");
						num3 = projectCore.getCompanyRevenue();
						num4 = projectCore.getCompanyBaseRevenue();
						num5 = projectCore.getCompanyValueShare();
						num6 = projectCore.getCompanyBaseValueShare();
						num7 = projectCore.getCompanyPenetration();
						num8 = projectCore.getCompanyBasePenetration();
					}
					dataTable.Rows[i][checkBoxItem2.Text] = num / 100.0;
					dataTable3.Rows[i][checkBoxItem2.Text] = num / num2;
					dataTable2.Rows[i][checkBoxItem2.Text] = num3;
					dataTable4.Rows[i][checkBoxItem2.Text] = num3 / num4;
					dataTable5.Rows[i][checkBoxItem2.Text] = num5;
					dataTable6.Rows[i][checkBoxItem2.Text] = num5 / num6;
					dataTable7.Rows[i][checkBoxItem2.Text] = num7;
					dataTable8.Rows[i][checkBoxItem2.Text] = num7 / num8;
				}
			}
			chart10.Series.Clear();
			if (sharesVolumeShareCheckbox.Checked)
			{
				foreach (DataRow row in dataTable.Rows)
				{
					string name = row["Name"].ToString() + " Shares";
					chart10.Series.Add(name);
					chart10.Series[name].ChartType = SeriesChartType.RangeColumn;
					chart10.Series[name].MarkerStyle = MarkerStyle.Circle;
					chart10.Series[name].YAxisType = AxisType.Primary;
					chart10.Series[name].BorderWidth = 2;
					chart10.Series[name].SmartLabelStyle.Enabled = true;
					chart10.Series[name].IsValueShownAsLabel = true;
					chart10.Series[name].LabelFormat = "0.0%";
					for (int j = 1; j < dataTable.Columns.Count; j++)
					{
						string columnName = dataTable.Columns[j].ColumnName;
						double num9 = double.Parse(row[columnName].ToString());
						chart10.Series[name].Points.AddXY(columnName, num9);
					}
				}
				chart10.ChartAreas[0].AxisY.LabelStyle.Format = "P0";
			}
			if (sharesRevenueIndexCheckbox.Checked)
			{
				foreach (DataRow row2 in dataTable4.Rows)
				{
					string name = row2["Name"].ToString();
					chart10.Series.Add(name);
					chart10.Series[name].ChartType = SeriesChartType.Line;
					chart10.Series[name].MarkerStyle = MarkerStyle.Circle;
					if (sharesVolumeShareCheckbox.Checked)
					{
						chart10.Series[name].YAxisType = AxisType.Secondary;
					}
					chart10.Series[name].BorderWidth = 2;
					chart10.Series[name].SmartLabelStyle.Enabled = true;
					chart10.Series[name].IsValueShownAsLabel = true;
					chart10.Series[name].LabelFormat = "0.0";
					for (int j = 1; j < dataTable4.Columns.Count; j++)
					{
						string columnName = dataTable4.Columns[j].ColumnName;
						double num9 = double.Parse(row2[columnName].ToString());
						chart10.Series[name].Points.AddXY(columnName, num9);
					}
				}
				if (!sharesVolumeShareCheckbox.Checked)
				{
					chart10.ChartAreas[0].AxisY.LabelStyle.Format = "0";
				}
			}
			if (!sharesVolumeShareCheckbox.Checked)
			{
				dataTable.Rows.Clear();
			}
			dataTable.Rows.Add(dataTable.NewRow());
			if (sharesRevenueIndexCheckbox.Checked)
			{
				for (int i = 0; i < dataTable4.Rows.Count; i++)
				{
					dataTable.Rows.Add(dataTable4.Rows[i].ItemArray);
				}
			}
			if (!sharesVolumeShareIndexCheckbox.Checked)
			{
				dataTable3.Rows.Clear();
			}
			if (sharesRevenueCheckbox.Checked)
			{
				if (sharesVolumeShareIndexCheckbox.Checked)
				{
					dataTable3.Rows.Add(dataTable3.NewRow());
				}
				for (int i = 0; i < dataTable2.Rows.Count; i++)
				{
					dataTable3.Rows.Add(dataTable2.Rows[i].ItemArray);
				}
			}
			if (sharesValueShareIndexCheckbox.Checked)
			{
				if (sharesValueShareCheckbox.Checked)
				{
					dataTable5.Rows.Add(dataTable5.NewRow());
				}
				else
				{
					dataTable5.Rows.Clear();
				}
				for (int i = 0; i < dataTable6.Rows.Count; i++)
				{
					dataTable5.Rows.Add(dataTable6.Rows[i].ItemArray);
				}
			}
			if (sharesWeightedSOPIndexCheckbox.Checked)
			{
				if (sharesWeightedSOPCheckbox.Checked)
				{
					dataTable7.Rows.Add(dataTable7.NewRow());
				}
				else
				{
					dataTable7.Rows.Clear();
				}
				for (int i = 0; i < dataTable8.Rows.Count; i++)
				{
					dataTable7.Rows.Add(dataTable8.Rows[i].ItemArray);
				}
			}
			dataTable.ExtendedProperties.Add("ChartName", "Shares And R.Index");
			dataTable3.ExtendedProperties.Add("ChartName", "S.Index And Revenues");
			dataTable5.ExtendedProperties.Add("ChartName", "V.Share And Index");
			dataTable7.ExtendedProperties.Add("ChartName", "Penetration And Index");
			if (!sharesNameTextBox.Text.Trim().Equals(""))
			{
				dataTable.TableName = sharesNameTextBox.Text;
				dataTable3.TableName = "Index " + sharesNameTextBox.Text;
			}
			if (sharesVolumeShareCheckbox.Checked || sharesRevenueIndexCheckbox.Checked)
			{
				if (dsCharts.Tables.Contains(dataTable.TableName))
				{
					dsCharts.Tables.Remove(dataTable.TableName);
				}
				dsCharts.Tables.Add(dataTable);
			}
			if (sharesVolumeShareIndexCheckbox.Checked || sharesRevenueCheckbox.Checked)
			{
				if (dsCharts.Tables.Contains(dataTable3.TableName))
				{
					dsCharts.Tables.Remove(dataTable3.TableName);
				}
				dsCharts.Tables.Add(dataTable3);
			}
			if (sharesValueShareCheckbox.Checked || sharesValueShareIndexCheckbox.Checked)
			{
				if (dsCharts.Tables.Contains(dataTable5.TableName))
				{
					dsCharts.Tables.Remove(dataTable5.TableName);
				}
				dsCharts.Tables.Add(dataTable5);
			}
			if (sharesWeightedSOPCheckbox.Checked || sharesWeightedSOPIndexCheckbox.Checked)
			{
				if (dsCharts.Tables.Contains(dataTable7.TableName))
				{
					dsCharts.Tables.Remove(dataTable7.TableName);
				}
				dsCharts.Tables.Add(dataTable7);
			}
		}
		else
		{
			MessageBox.Show(this, "You must specify at least one series & one scenario", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void sharesSeriesCombobox_CheckBoxCheckedChanged(object sender, EventArgs e)
	{
		manageCheckboxComboboxAllItemState(sender, sharesSeriesCombobox);
	}

	private void sharesSenariosCombobox_CheckBoxCheckedChanged(object sender, EventArgs e)
	{
		manageCheckboxComboboxAllItemState(sender, sharesSenariosCombobox);
	}

	private void sharesSenariosCombobox_Enter(object sender, EventArgs e)
	{
		sharesSenariosCombobox.Items.Clear();
		sharesSenariosCombobox.Items.Add("* All");
		sharesSenariosCombobox.Items.Add("Current");
		foreach (KeyValuePair<string, string> item in studyScenariosLibrary)
		{
			if (!string.Equals(item.Key, "Current") && !string.Equals(item.Key, "Base"))
			{
				sharesSenariosCombobox.Items.Add(item.Key);
			}
		}
	}

	private void validationScenarioCheckboxCombobox_CheckBoxCheckedChanged(object sender, EventArgs e)
	{
		manageCheckboxComboboxAllItemState(sender, validationScenarioCheckboxCombobox);
	}

	private void validationScenarioCheckboxCombobox_Enter(object sender, EventArgs e)
	{
		validationScenarioCheckboxCombobox.Items.Clear();
		if (studyScenariosLibrary.Count != 0)
		{
			validationScenarioCheckboxCombobox.Items.Add("* All");
			validationSeriesCombobox.CheckBoxItems[0].Checked = false;
		}
		foreach (KeyValuePair<string, string> validationScenario in validationScenarios)
		{
			validationScenarioCheckboxCombobox.Items.Add(validationScenario.Key);
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
		base.DialogResult = MessageBox.Show(this, "Are you sure, you want to clear the charts?", "Proceed", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
		if (base.DialogResult == DialogResult.OK)
		{
			dsCharts.Tables.Clear();
			MessageBox.Show(this, "Charts Reset Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		base.DialogResult = DialogResult.None;
	}

	private void IncrementalGain_Click(object sender, EventArgs e)
	{
		Button button = sender as Button;
		switch (button.Name)
		{
		case "incrementalGainIncrementalConfigurationButton":
		{
			IncrementalConfiguration incrementalConfiguration = new IncrementalConfiguration(ProjectCore.Project["IncrementalGainChartProject"]);
			incrementalConfiguration.Text = "Introduction Configuration";
			incrementalConfiguration.dataGridView1.DataSource = dtIncrementalGain;
			incrementalConfiguration.incrementalConfigurationGroupTypeCombobox.SelectedItem = incrementalGainGroupType;
			incrementalConfiguration.incrementalConfigurationGroupMemberCombobox.SelectedItem = incrementalGainGroupMember;
			base.DialogResult = incrementalConfiguration.ShowDialog(this);
			incrementalGainGroupType = incrementalConfiguration.incrementalConfigurationGroupTypeCombobox.SelectedItem.ToString();
			if (incrementalConfiguration.incrementalConfigurationGroupMemberCombobox.SelectedItem != null)
			{
				incrementalGainGroupMember = incrementalConfiguration.incrementalConfigurationGroupMemberCombobox.SelectedItem.ToString();
			}
			if (base.DialogResult == DialogResult.Cancel)
			{
				clearDatatable(dtIncrementalGain);
			}
			break;
		}
		case "incrementalLossIncrementalConfigurationButton":
		{
			IncrementalConfiguration incrementalConfiguration = new IncrementalConfiguration(ProjectCore.Project["IncrementalLossChartProject"]);
			incrementalConfiguration.Text = "Removal Configuration";
			incrementalConfiguration.dataGridView1.DataSource = dtIncrementalLoss;
			incrementalConfiguration.incrementalConfigurationGroupTypeCombobox.SelectedItem = incrementalLossGroupType;
			incrementalConfiguration.incrementalConfigurationGroupMemberCombobox.SelectedItem = incrementalLossGroupMember;
			base.DialogResult = incrementalConfiguration.ShowDialog(this);
			incrementalLossGroupType = incrementalConfiguration.incrementalConfigurationGroupTypeCombobox.SelectedItem.ToString();
			if (incrementalConfiguration.incrementalConfigurationGroupMemberCombobox.SelectedItem != null)
			{
				incrementalLossGroupMember = incrementalConfiguration.incrementalConfigurationGroupMemberCombobox.SelectedItem.ToString();
			}
			if (base.DialogResult == DialogResult.Cancel)
			{
				clearDatatable(dtIncrementalLoss);
			}
			break;
		}
		}
		base.DialogResult = DialogResult.None;
	}

	private Dictionary<string, List<string>> fillIncrementalListsFromDatatable(DataTable datatable)
	{
		Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		foreach (DataRow row in datatable.Rows)
		{
			if (!string.IsNullOrEmpty(row[0].ToString()))
			{
				list.Add(row[0].ToString());
			}
			if (!string.IsNullOrEmpty(row[1].ToString()))
			{
				list2.Add(row[1].ToString());
			}
		}
		dictionary.Add("CurrentLineUpProducts", list);
		dictionary.Add("IncrementalProducts", list2);
		return dictionary;
	}

	private void incrementalGainRunButton_Click(object sender, EventArgs e)
	{
		if (!validateChartName(incrementalGainNameTextbox.Text))
		{
			return;
		}
		Cursor = Cursors.WaitCursor;
		DataTable dataTable = null;
		ProjectCore projectCore = ProjectCore.Project["IncrementalGainChartProject"];
		int num = 0;
		if (cumulativeIncrementalGainRadiobutton.Checked)
		{
			if (incrementalGainSharesCheckbox.Checked)
			{
				Dictionary<string, List<string>> dictionary = fillIncrementalListsFromDatatable(dtIncrementalGain);
				dataTable = projectCore.cumIncrementalGainLoss("Intro", "Market Share", incrementalGainGroupType, incrementalGainGroupMember, dictionary["CurrentLineUpProducts"], dictionary["IncrementalProducts"]);
				if (!incrementalGainNameTextbox.Text.Trim().Equals(""))
				{
					dataTable.TableName = incrementalGainNameTextbox.Text + " Market Share";
				}
				if (dsCharts.Tables.Contains(dataTable.TableName))
				{
					dsCharts.Tables.Remove(dataTable.TableName);
				}
				dsCharts.Tables.Add(dataTable);
			}
			if (incrementalGainRevenuesCheckbox.Checked)
			{
				Dictionary<string, List<string>> dictionary = fillIncrementalListsFromDatatable(dtIncrementalGain);
				dataTable = projectCore.cumIncrementalGainLoss("Intro", "Revenue", incrementalGainGroupType, incrementalGainGroupMember, dictionary["CurrentLineUpProducts"], dictionary["IncrementalProducts"]);
				if (!incrementalGainNameTextbox.Text.Trim().Equals(""))
				{
					dataTable.TableName = incrementalGainNameTextbox.Text + " Revenue";
				}
				if (dsCharts.Tables.Contains(dataTable.TableName))
				{
					dsCharts.Tables.Remove(dataTable.TableName);
				}
				dsCharts.Tables.Add(dataTable);
			}
			num = dataTable.Columns.Count - 1;
		}
		else if (exclusiveIncrementalGainRadiobutton.Checked)
		{
			Dictionary<string, List<string>> dictionary = fillIncrementalListsFromDatatable(dtIncrementalGain);
			dataTable = projectCore.exIncrementalGainLoss("Intro", incrementalGainGroupType, incrementalGainGroupMember, dictionary["CurrentLineUpProducts"], dictionary["IncrementalProducts"]);
			if (!incrementalGainNameTextbox.Text.Trim().Equals(""))
			{
				dataTable.TableName = incrementalGainNameTextbox.Text;
			}
			if (dsCharts.Tables.Contains(dataTable.TableName))
			{
				dsCharts.Tables.Remove(dataTable.TableName);
			}
			dsCharts.Tables.Add(dataTable);
			num = dataTable.Columns.Count - 2;
		}
		chart7.Series.Clear();
		for (int i = 1; i < num; i++)
		{
			string text = dataTable.Columns[i].ToString();
			chart7.Series.Add(text);
			chart7.Series[text].ChartType = SeriesChartType.Line;
			if (text.Equals("Revenue"))
			{
				chart7.Series[text].YAxisType = AxisType.Secondary;
				chart7.Series[text].MarkerStyle = MarkerStyle.Circle;
			}
			else if (text.Equals("Market Share"))
			{
				chart7.Series[text].MarkerStyle = MarkerStyle.Diamond;
				chart7.Series[text].LabelFormat = "0.00%";
			}
			chart7.Series[text].BorderWidth = 2;
			chart7.Series[text].IsValueShownAsLabel = true;
			chart7.Series[text].SmartLabelStyle.Enabled = true;
			foreach (DataRow row in dataTable.Rows)
			{
				string xValue = row["Product Name"].ToString();
				if (!string.Equals(row[i].ToString(), "Reversal Effect Happened") && !string.IsNullOrEmpty(row[i].ToString()))
				{
					double num2 = double.Parse(row[i].ToString());
					chart7.Series[text].Points.AddXY(xValue, num2);
				}
			}
		}
		chart7.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
		chart7.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
		chart7.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
		chart7.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
		chart7.ChartAreas[0].AxisY2.MajorGrid.Enabled = false;
		chart7.ChartAreas[0].AxisY.LabelStyle.Format = "P0";
		Cursor = Cursors.Default;
	}

	private void incrementalLossRunButton_Click(object sender, EventArgs e)
	{
		if (!validateChartName(incrementalLossNameTextbox.Text))
		{
			return;
		}
		Cursor = Cursors.WaitCursor;
		DataTable dataTable = null;
		ProjectCore projectCore = ProjectCore.Project["IncrementalLossChartProject"];
		int num = 0;
		if (cumulativeIncrementalLossRadiobutton.Checked)
		{
			if (incrementalLossSharesCheckbox.Checked)
			{
				Dictionary<string, List<string>> dictionary = fillIncrementalListsFromDatatable(dtIncrementalLoss);
				dataTable = projectCore.cumIncrementalGainLoss("Removal", "Market Share", incrementalLossGroupType, incrementalLossGroupMember, dictionary["CurrentLineUpProducts"], dictionary["IncrementalProducts"]);
				if (!incrementalLossNameTextbox.Text.Trim().Equals(""))
				{
					dataTable.TableName = incrementalLossNameTextbox.Text + " Market Share";
				}
				if (dsCharts.Tables.Contains(dataTable.TableName))
				{
					dsCharts.Tables.Remove(dataTable.TableName);
				}
				dsCharts.Tables.Add(dataTable);
			}
			if (incrementalLossRevenuesCheckbox.Checked)
			{
				Dictionary<string, List<string>> dictionary = fillIncrementalListsFromDatatable(dtIncrementalLoss);
				dataTable = projectCore.cumIncrementalGainLoss("Removal", "Revenue", incrementalLossGroupType, incrementalLossGroupMember, dictionary["CurrentLineUpProducts"], dictionary["IncrementalProducts"]);
				if (!incrementalLossNameTextbox.Text.Trim().Equals(""))
				{
					dataTable.TableName = incrementalLossNameTextbox.Text + " Revenue";
				}
				if (dsCharts.Tables.Contains(dataTable.TableName))
				{
					dsCharts.Tables.Remove(dataTable.TableName);
				}
				dsCharts.Tables.Add(dataTable);
			}
			num = dataTable.Columns.Count - 1;
		}
		else if (exclusiveIncrementalLossRadiobutton.Checked)
		{
			Dictionary<string, List<string>> dictionary = fillIncrementalListsFromDatatable(dtIncrementalLoss);
			dataTable = projectCore.exIncrementalGainLoss("Removal", incrementalLossGroupType, incrementalLossGroupMember, dictionary["CurrentLineUpProducts"], dictionary["IncrementalProducts"]);
			if (!incrementalLossNameTextbox.Text.Trim().Equals(""))
			{
				dataTable.TableName = incrementalLossNameTextbox.Text;
			}
			if (dsCharts.Tables.Contains(dataTable.TableName))
			{
				dsCharts.Tables.Remove(dataTable.TableName);
			}
			dsCharts.Tables.Add(dataTable);
			num = dataTable.Columns.Count - 2;
		}
		chart11.Series.Clear();
		for (int i = 1; i < num; i++)
		{
			string text = dataTable.Columns[i].ToString();
			chart11.Series.Add(text);
			chart11.Series[text].ChartType = SeriesChartType.Line;
			if (text.Equals("Revenue"))
			{
				chart11.Series[text].YAxisType = AxisType.Secondary;
				chart11.Series[text].MarkerStyle = MarkerStyle.Circle;
			}
			else
			{
				chart11.Series[text].MarkerStyle = MarkerStyle.Diamond;
				chart11.Series[text].LabelFormat = "0.00%";
			}
			chart11.Series[text].BorderWidth = 2;
			chart11.Series[text].IsValueShownAsLabel = true;
			chart11.Series[text].SmartLabelStyle.Enabled = true;
			foreach (DataRow row in dataTable.Rows)
			{
				string xValue = row["Product Name"].ToString();
				if (!string.Equals(row[i].ToString(), "Reversal Effect Happened") && !string.IsNullOrEmpty(row[i].ToString()))
				{
					double num2 = double.Parse(row[i].ToString());
					chart11.Series[text].Points.AddXY(xValue, num2);
				}
			}
		}
		chart11.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
		chart11.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
		chart11.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
		chart11.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
		chart11.ChartAreas[0].AxisY2.MajorGrid.Enabled = false;
		chart11.ChartAreas[0].AxisY.LabelStyle.Format = "P0";
		Cursor = Cursors.Default;
	}

	private string preparePriceIndexScaleLabel()
	{
		double[] attributeLevelsBoundries = project.getAttributeLevelsBoundries("Price");
		double num = project.getProductDefaultAttribute(priceIndexSKUNameCombobox.SelectedItem.ToString(), "Price") / project.getProductSizeUnit(priceIndexSKUNameCombobox.SelectedItem.ToString());
		double num2 = num * (1.0 + attributeLevelsBoundries[0]);
		double num3 = num * (1.0 + attributeLevelsBoundries[1]);
		double num4 = project.getProductDefaultAttribute(priceIndexReferenceSKUNameCombobox.SelectedItem.ToString(), "Price") / project.getProductSizeUnit(priceIndexReferenceSKUNameCombobox.SelectedItem.ToString());
		double value = num / num4;
		double value2 = num2 / num4;
		double value3 = num3 / num4;
		return "Boundaires: Min is " + roundDoubleValue(value2, "UP") + " && Max is " + roundDoubleValue(value3, "DOWN") + " at Current = " + Math.Round(value, 2);
	}

	private double roundDoubleValue(double value, string mode)
	{
		if (string.Equals(mode, "UP"))
		{
			if (Math.Round(value, 2) < value)
			{
				return Math.Round(value, 2) + 0.01;
			}
		}
		else if (string.Equals(mode, "DOWN") && Math.Round(value, 2) > value)
		{
			return Math.Round(value, 2) - 0.01;
		}
		return Math.Round(value, 2);
	}

	private void clearDatatable(DataTable table)
	{
		foreach (DataRow row in table.Rows)
		{
			foreach (DataColumn column in table.Columns)
			{
				row[column] = "";
			}
		}
	}

	private bool validateChartName(string name)
	{
		foreach (char item in specialCharacter)
		{
			if (name.Contains(item.ToString()))
			{
				MessageBox.Show(this, "Name must not contain any of /,\\,?,*,[,]", "Not Valid", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return false;
			}
		}
		if (name.Length > 24)
		{
			MessageBox.Show(this, "Name must not exceed 24 characters including spaces", "Not Valid", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return false;
		}
		return true;
	}

	private void accumulativeIncrementalGainRadiobutton_CheckedChanged(object sender, EventArgs e)
	{
		RadioButton radioButton = sender as RadioButton;
		switch (radioButton.Name)
		{
		case "cumulativeIncrementalGainRadiobutton":
			if (radioButton.Checked)
			{
				groupBox13.Enabled = true;
			}
			break;
		case "exclusiveIncrementalGainRadiobutton":
			if (radioButton.Checked)
			{
				groupBox13.Enabled = false;
			}
			break;
		case "cumulativeIncrementalLossRadiobutton":
			if (radioButton.Checked)
			{
				groupBox21.Enabled = true;
			}
			break;
		case "exclusiveIncrementalLossRadiobutton":
			if (radioButton.Checked)
			{
				groupBox21.Enabled = false;
			}
			break;
		}
	}

	private void elasticitySegmentationCheckbox_CheckedChanged(object sender, EventArgs e)
	{
		if (elasticitySegmentationCheckbox.Checked)
		{
			if (elasticitySegmentationTypeCombobox.Items.Count != 0)
			{
				elasticitySegmentationTypeCombobox.Enabled = true;
				elasticitySegmentationTypeCombobox.SelectedIndex = 0;
				elasticitySegmentMembersCombobox.Enabled = true;
			}
			else
			{
				MessageBox.Show(this, "You can't use segmentation because no segments in the study\nSegmentation will be disabled", "Segments Missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				elasticitySegmentationCheckbox.Checked = false;
			}
			elasticityScenarioCheckbox.Checked = false;
		}
		else
		{
			elasticitySegmentationTypeCombobox.Enabled = false;
			elasticitySegmentMembersCombobox.Enabled = false;
		}
	}

	private void brandInteractionScenariosCombobox_Enter(object sender, EventArgs e)
	{
		brandInteractionScenariosCombobox.Items.Clear();
		if (studyScenariosLibrary.Count != 0)
		{
			brandInteractionScenariosCombobox.Items.Add("* All");
		}
		foreach (KeyValuePair<string, string> item in studyScenariosLibrary)
		{
			if (!string.Equals(item.Key, "Current") && !string.Equals(item.Key, "Default") && !string.Equals(item.Key, "Base"))
			{
				brandInteractionScenariosCombobox.Items.Add(item.Key);
			}
		}
	}

	private void brandInteractionScenariosCombobox_CheckBoxCheckedChanged(object sender, EventArgs e)
	{
		manageCheckboxComboboxAllItemState(sender, brandInteractionScenariosCombobox);
	}

	private void scenariosDropdownButton_Click(object sender, EventArgs e)
	{
		switch (tabControl1.SelectedTab.Text)
		{
		case "Price Index":
			priceIndexConfigButton.Focus();
			break;
		case "SOM Vs. SOP":
			somsopConfigSKUButton.Focus();
			break;
		case "Fair Share":
			fairShareConfigSKUButton.Focus();
			break;
		case "Introduction":
			incrementalGainConfigureSKUButton.Focus();
			break;
		case "Removal":
			incrementalLossConfigureSKUButton.Focus();
			break;
		}
		scenariosMenuStrip.Items.Clear();
		scenariosMenuStrip.Items.Add("Default", null, scenarioMenuStrip_Item_Click);
		foreach (KeyValuePair<string, string> item in studyScenariosLibrary)
		{
			if (!string.Equals(item.Key, "Current") && !string.Equals(item.Key, "Default") && !string.Equals(item.Key, "Base"))
			{
				scenariosMenuStrip.Items.Add(item.Key, null, scenarioMenuStrip_Item_Click);
			}
		}
		scenariosMenuStrip.Show((Control)sender, default(Point));
	}

	private void scenarioMenuStrip_Item_Click(object sender, EventArgs e)
	{
		if (!(sender is ToolStripItem toolStripItem))
		{
			return;
		}
		string projectName = "";
		switch (tabControl1.SelectedTab.Text)
		{
		case "Price Index":
			priceIndexScenarioLabel.Text = "Current Scenario is set to " + toolStripItem.Text;
			projectName = "IndexAndRevenuesChartProject";
			break;
		case "SOM Vs. SOP":
			somsopScenariosLabel.Text = "Current Scenario is set to " + toolStripItem.Text;
			projectName = "SOMVsSOPChartProject";
			break;
		case "Introduction":
			incrementalGainScenarioLabel.Text = "Current Scenario is set to " + toolStripItem.Text;
			projectName = "IncrementalGainChartProject";
			break;
		case "Removal":
			incrementalLossScenarioLabel.Text = "Current Scenario is set to " + toolStripItem.Text;
			projectName = "IncrementalLossChartProject";
			break;
		}
		using (UndoRedoManager.Start(""))
		{
			if (toolStripItem.Text.Equals("Default"))
			{
				ProjectCore.Project[projectName].resetProductValues();
				ProjectCore.Project[projectName].resetSegmentation();
			}
			else
			{
				ProjectCore.Project[projectName].bindScenarioDatatableObjectToProjectProducts(getScenario(toolStripItem.Text));
			}
			UndoRedoManager.FlushHistory();
		}
	}

	private void elasticityScenarioCheckbox_CheckedChanged(object sender, EventArgs e)
	{
		if (elasticityScenarioCheckbox.Checked)
		{
			elasticityScenarioCombobox.Enabled = true;
			elasticitySegmentationCheckbox.Checked = false;
		}
		else
		{
			elasticityScenarioCombobox.Enabled = false;
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

	private ProjectCore getBaseScenarioProject(string scenarioName)
	{
		ProjectCore projectCore = new ProjectCore();
		projectCore.initializeProjectMembers();
		using (UndoRedoManager.Start(""))
		{
			projectCore.bindScenarioDatatableObjectToProjectProducts(getScenario(scenarioName));
			UndoRedoManager.FlushHistory();
		}
		return projectCore;
	}

	private void elasticitySegmentMembersCombobox_Enter(object sender, EventArgs e)
	{
		elasticitySegmentMembersCombobox.Items.Clear();
		elasticitySegmentMembersCombobox.Items.Add("* All");
		foreach (object item in project.loadSegmentsDictionary()[elasticitySegmentationTypeCombobox.SelectedItem.ToString()])
		{
			elasticitySegmentMembersCombobox.Items.Add(item);
		}
	}

	private void elasticitySegmentMembersCombobox_CheckBoxCheckedChanged(object sender, EventArgs e)
	{
		manageCheckboxComboboxAllItemState(sender, elasticitySegmentMembersCombobox);
	}

	private void elasticityScenarioCombobox_Enter(object sender, EventArgs e)
	{
		elasticityScenarioCombobox.Items.Clear();
		if (studyScenariosLibrary.Count != 0)
		{
			elasticityScenarioCombobox.Items.Add("* All");
		}
		foreach (KeyValuePair<string, string> item in studyScenariosLibrary)
		{
			if (!string.Equals(item.Key, "Current") && !string.Equals(item.Key, "Base"))
			{
				elasticityScenarioCombobox.Items.Add(item.Key);
			}
		}
	}

	private void elasticityScenarioCombobox_CheckBoxCheckedChanged(object sender, EventArgs e)
	{
		manageCheckboxComboboxAllItemState(sender, elasticityScenarioCombobox);
	}

	private DataTable getElasticityDatatable(object seriesCombobox, string type, string chartName)
	{
		DataTable dataTable = new DataTable();
		switch (type)
		{
		case "SKU":
			dataTable = ProjectCore.Project["ElasticityChartProject"].conditionalAttributeElasticityPerProduct(elasticityAttributeNamePerSKUCombobox.Text);
			break;
		case "Group":
			dataTable = ProjectCore.Project["ElasticityChartProjectPerGroup"].conditionalAttributeElasticityPerGroup(elasticityAttributeNamePerGroupCombobox.Text, elasticityGroupTypeCombobox.Text);
			break;
		case "CompanyGroup":
			dataTable = ProjectCore.Project["ElasticityChartProjectPerCompany"].conditionalAttributeElasticityPerCompanyGroup(elasticityAttributeNamePerCompanyCombobox.Text, elasticityGroupTypePerCompanyCombobox.Text);
			break;
		case "CompanySKU":
			dataTable = ProjectCore.Project["ElasticityChartProjectPerCompany"].conditionalAttributeElasticityPerCompanySKU(elasticityAttributeNamePerCompanyCombobox.Text);
			break;
		}
		CheckBoxComboBox checkBoxComboBox = seriesCombobox as CheckBoxComboBox;
		DataTable dataTable2 = dataTable.Clone();
		foreach (DataRow row in dataTable.Rows)
		{
			if (checkBoxComboBox.CheckBoxItems[row["Name"].ToString()].Checked)
			{
				dataTable2.Rows.Add(row.ItemArray);
			}
		}
		dataTable2.TableName = chartName;
		if (dsCharts.Tables.Contains(chartName))
		{
			dsCharts.Tables.Remove(chartName);
		}
		dsCharts.Tables.Add(dataTable2);
		return dataTable2;
	}

	private void basicScenarioCombobox_Enter(object sender, EventArgs e)
	{
		ComboBox comboBox = sender as ComboBox;
		comboBox.Items.Clear();
		comboBox.Items.Add("Default");
		foreach (KeyValuePair<string, string> item in studyScenariosLibrary)
		{
			if (!string.Equals(item.Key, "Current") && !string.Equals(item.Key, "Base"))
			{
				comboBox.Items.Add(item.Key);
			}
		}
		comboBox.SelectedIndex = 0;
	}

	private void fairShareScenarioCombobox_Enter(object sender, EventArgs e)
	{
		fairShareScenarioCombobox.Items.Clear();
		fairShareScenarioCombobox.Items.Add("Current");
		foreach (KeyValuePair<string, string> item in studyScenariosLibrary)
		{
			if (!string.Equals(item.Key, "Current") && !string.Equals(item.Key, "Default") && !string.Equals(item.Key, "Base"))
			{
				fairShareScenarioCombobox.Items.Add(item.Key);
			}
		}
	}
}
