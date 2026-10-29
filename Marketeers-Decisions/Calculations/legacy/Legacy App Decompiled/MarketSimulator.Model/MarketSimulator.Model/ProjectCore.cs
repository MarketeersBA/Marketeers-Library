using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using MarketSimulator.Model.BusinessLogic;
using MarketSimulator.Model.DataAccess;
using MarketSimulator.Model.Database;
using UndoRedoFramework;

namespace MarketSimulator.Model;

[Serializable]
public class ProjectCore : ISerializable, IEnumerable
{
	private const string dbNoSegmentationColumnName = "Total";

	private Dictionary<string, ProjectCore> projectString = new Dictionary<string, ProjectCore>();

	private static ProjectCore project = new ProjectCore();

	private static string studyPath;

	private static Dictionary<string, string> excelColorNumberFormat = new Dictionary<string, string>();

	private static bool enlargeAll;

	private static string numberFormat = "0.##";

	private static int decimalCount = 2;

	private string projectName = "";

	private double valueShareUnit = 0.0;

	private double baseValueShareUnit = 0.0;

	private string studyName;

	private string studyDescription;

	private string companyName;

	private string companyLocation;

	private string researchLogo = "";

	private double MSU;

	private double VSU;

	private double SU;

	private double exponentFactor;

	private bool analysis;

	private int skuAttributeIndex;

	private int priceAttributeIndex;

	private int sizeAttributeIndex;

	private int respondentsCount;

	private bool useGroupng;

	private bool useSegmenting;

	private bool financial;

	private double[,] productTotalUtility;

	private bool[] respondentSelection;

	private double[] respondentsWeight;

	private DataTable dtDisplayResult;

	private DataTable dtGroupDisplayResult;

	private Attributes attribute;

	private Products product;

	private Groups group;

	private Segments segment;

	private List<string> groupTypeList = new List<string>();

	private List<string> segmentTypeList = new List<string>();

	private List<string> conditionalAttributeNameList = new List<string>();

	private List<string> discreteAttributeNameList = new List<string>();

	private List<string> allProducts = new List<string>();

	private List<string> companyProducts = new List<string>();

	private Dictionary<string, List<string>> groupMember = new Dictionary<string, List<string>>();

	private Dictionary<string, List<string>> segmentMember = new Dictionary<string, List<string>>();

	private Dictionary<string, Dictionary<string, List<string>>> groupProducts = new Dictionary<string, Dictionary<string, List<string>>>();

	private string scenarioName = "Default";

	private UndoRedo<string> studySegmentTypeStart = new UndoRedo<string>();

	public static bool EnlargeAll
	{
		get
		{
			return enlargeAll;
		}
		set
		{
			enlargeAll = value;
		}
	}

	public static Dictionary<string, string> ExcelColorNumberFormat
	{
		get
		{
			return excelColorNumberFormat;
		}
		set
		{
			excelColorNumberFormat = value;
		}
	}

	public string StudyName
	{
		get
		{
			return studyName;
		}
		set
		{
			studyName = value;
		}
	}

	public static string StudyPath
	{
		get
		{
			return studyPath;
		}
		set
		{
			studyPath = value;
		}
	}

	public static ProjectCore Project => project;

	public bool CanUndo => UndoRedoManager.CanUndo;

	public bool CanRedo => UndoRedoManager.CanRedo;

	public string UndoCommandType
	{
		get
		{
			IEnumerator enumerator = UndoRedoManager.UndoCommands.GetEnumerator();
			if (enumerator.MoveNext())
			{
				return enumerator.Current.ToString();
			}
			return null;
		}
	}

	public string RedoCommandType
	{
		get
		{
			IEnumerator enumerator = UndoRedoManager.RedoCommands.GetEnumerator();
			if (enumerator.MoveNext())
			{
				return enumerator.Current.ToString();
			}
			return null;
		}
	}

	public bool Financial
	{
		get
		{
			return financial;
		}
		set
		{
			financial = value;
		}
	}

	public bool UseGroupng
	{
		get
		{
			return useGroupng;
		}
		set
		{
			useGroupng = value;
		}
	}

	public string CompanyName
	{
		get
		{
			return companyName;
		}
		set
		{
			companyName = value;
		}
	}

	public DataTable DtDisplayResult
	{
		get
		{
			return dtDisplayResult;
		}
		set
		{
			dtDisplayResult = value;
		}
	}

	public DataTable DtGroupDisplayResult
	{
		get
		{
			return dtGroupDisplayResult;
		}
		set
		{
			dtGroupDisplayResult = value;
		}
	}

	public List<string> GroupTypeList
	{
		get
		{
			return groupTypeList;
		}
		set
		{
			groupTypeList = value;
		}
	}

	public List<string> SegmentTypeList
	{
		get
		{
			return segmentTypeList;
		}
		set
		{
			segmentTypeList = value;
		}
	}

	public List<string> ConditionalAttributeNameList
	{
		get
		{
			return conditionalAttributeNameList;
		}
		set
		{
			conditionalAttributeNameList = value;
		}
	}

	public List<string> DiscreteAttributeNameList
	{
		get
		{
			return discreteAttributeNameList;
		}
		set
		{
			discreteAttributeNameList = value;
		}
	}

	public List<string> AllProducts
	{
		get
		{
			return allProducts;
		}
		set
		{
			allProducts = value;
		}
	}

	public List<string> CompanyProducts
	{
		get
		{
			return companyProducts;
		}
		set
		{
			companyProducts = value;
		}
	}

	public Dictionary<string, List<string>> GroupMember
	{
		get
		{
			return groupMember;
		}
		set
		{
			groupMember = value;
		}
	}

	public Dictionary<string, List<string>> SegmentMember
	{
		get
		{
			return segmentMember;
		}
		set
		{
			segmentMember = value;
		}
	}

	public Dictionary<string, Dictionary<string, List<string>>> GroupProducts
	{
		get
		{
			return groupProducts;
		}
		set
		{
			groupProducts = value;
		}
	}

	public string ScenarioName
	{
		get
		{
			return scenarioName;
		}
		set
		{
			scenarioName = value;
		}
	}

	public static string NumberFormat => numberFormat;

	public static int DecimalCount
	{
		get
		{
			return decimalCount;
		}
		set
		{
			decimalCount = value;
		}
	}

	public int Count => projectString.Count;

	public ProjectCore this[string projectName]
	{
		get
		{
			this.projectName = projectName;
			return projectString[projectName];
		}
		set
		{
			projectString[projectName] = value;
		}
	}

	internal string StudySegmentTypeStart
	{
		get
		{
			return studySegmentTypeStart.Value;
		}
		set
		{
			studySegmentTypeStart.Value = value;
		}
	}

	public ProjectCore()
	{
	}

	public ProjectCore(SerializationInfo info, StreamingContext ctxt)
	{
		getDataRow(0);
		createGroupDatatableSchema();
		createDisplayResultTableSchema();
		attribute = (Attributes)info.GetValue("Attributes", typeof(Attributes));
		product = (Products)info.GetValue("Products", typeof(Products));
		group = (Groups)info.GetValue("Groups", typeof(Groups));
		segment = (Segments)info.GetValue("Segments", typeof(Segments));
		AllProducts = info.GetValue("AllProducts", typeof(List<string>)) as List<string>;
		GroupMember = info.GetValue("GroupMember", typeof(Dictionary<string, List<string>>)) as Dictionary<string, List<string>>;
		GroupTypeList = info.GetValue("GroupTypeList", typeof(List<string>)) as List<string>;
		GroupProducts = info.GetValue("GroupProducts", typeof(Dictionary<string, Dictionary<string, List<string>>>)) as Dictionary<string, Dictionary<string, List<string>>>;
		ConditionalAttributeNameList = info.GetValue("ConditionalAttributeList", typeof(List<string>)) as List<string>;
		DiscreteAttributeNameList = info.GetValue("DiscreteAttributeList", typeof(List<string>)) as List<string>;
		respondentSelection = info.GetValue("RespondentSelection", typeof(bool[])) as bool[];
	}

	public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
	{
		info.AddValue("Attributes", attribute);
		info.AddValue("Products", product);
		info.AddValue("Groups", group);
		info.AddValue("Segments", segment);
		info.AddValue("AllProducts", AllProducts);
		info.AddValue("GroupMember", GroupMember);
		info.AddValue("GroupTypeList", GroupTypeList);
		info.AddValue("GroupProducts", GroupProducts);
		info.AddValue("ConditionalAttributeList", ConditionalAttributeNameList);
		info.AddValue("DiscreteAttributeList", DiscreteAttributeNameList);
		info.AddValue("RespondentSelection", respondentSelection);
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return projectString.GetEnumerator();
	}

	public void clearProjects()
	{
		projectString.Clear();
	}

	public void setDatabaseConnectionString(string databaseFilePath)
	{
		DataTransaction.InformationTransfer.ConnectionString = "Provider=Microsoft.Jet.OlEDB.4.0;Data Source=" + databaseFilePath + ";Jet OleDB:Database Password=smartera;";
	}

	public void setDataset(DataSet dataset)
	{
		DataTransaction.InformationTransfer.MarketSimulatorDataSet = dataset;
	}

	public void initializeDataset()
	{
		DataTransaction.InformationTransfer.initializeDataTable();
	}

	public void clearDataSet()
	{
		DataTransaction.InformationTransfer.clearDataset();
	}

	public void initializeProjectParameters()
	{
		setDatabaseConnectionString(studyPath);
		DataTransaction.InformationTransfer.clearDataset();
		initializeDataset();
	}

	public void initializeProjectMembers()
	{
		initializeAttributes();
		using (UndoRedoManager.Start("init"))
		{
			getDataRow(0);
			initializeProducts(attribute.Count);
			UndoRedoManager.FlushHistory();
		}
		initializeGroups();
		using (UndoRedoManager.Start("ChangeSegment"))
		{
			initializeSegments();
			resetSegmentation();
			UndoRedoManager.FlushHistory();
		}
		initializeGroupTypeList();
		initializeSegmentTypeList();
		initializeConditionalAttributeNameList();
		initializeDiscreteAttributeNameList();
		initializeAllProductList();
		initializeCompanyProductList();
		initializeGroupMember();
		initializeSegmentMember();
		initializeGroupProducts();
		setProductsBaseRevenue();
		setProductsBaseValueSalesUnit();
		setProductsBaseValueShare();
		createGroupDatatableSchema();
		createDisplayResultTableSchema();
	}

	public void initializeProjects()
	{
		Project["MainStudy"] = new ProjectCore();
		Project["BaseScenario"] = new ProjectCore();
		Project["ElasticityChartProject"] = new ProjectCore();
		Project["ElasticityChartProjectPerGroup"] = new ProjectCore();
		Project["ElasticityChartProjectPerCompany"] = new ProjectCore();
		Project["IndexAndRevenuesChartProject"] = new ProjectCore();
		Project["SharesChartProject"] = new ProjectCore();
		Project["BrandInteractionChartProject"] = new ProjectCore();
		Project["SOMVsSOPChartProject"] = new ProjectCore();
		Project["FairShareChartProject"] = new ProjectCore();
		Project["ValidationChartProject"] = new ProjectCore();
		project["IncrementalGainChartProject"] = new ProjectCore();
		project["IncrementalLossChartProject"] = new ProjectCore();
	}

	public void initializeRespondentSelection()
	{
		DataRowCollection rows = DataTransaction.InformationTransfer.DtHBUData.Rows;
		int num = 0;
		foreach (DataRow item in rows)
		{
			respondentSelection[num] = true;
			num++;
		}
		calculateBasePreferenceShareOnBaseMarketProducts();
	}

	public void respondentSegmentationProcess(string segmentType, string segmentMember)
	{
		foreach (KeyValuePair<int, string> segmentMember2 in segment[segmentType].SegmentMembers)
		{
			if (!(segmentMember2.Value == segmentMember))
			{
				continue;
			}
			int num = 0;
			foreach (KeyValuePair<int, int> item in segment[segmentType].RespondentSegmentation)
			{
				if (item.Value == segmentMember2.Key)
				{
					respondentSelection[num] = true;
				}
				else
				{
					respondentSelection[num] = false;
				}
				num++;
			}
			break;
		}
		calculateBasePreferenceShareOnBaseMarketProducts();
		scenarioName = "Configured Scenario";
	}

	private void calculateBasePreferenceShareOnBaseMarketProducts()
	{
		List<string> list = new List<string>();
		foreach (DataRow row in DataTransaction.InformationTransfer.DtProducts.Rows)
		{
			if (product[row["ProductName"].ToString()].Included)
			{
				list.Add(row["ProductName"].ToString());
			}
			product[row["ProductName"].ToString()].Included = bool.Parse(row["Included"].ToString());
		}
		loadPreferenceShare("BasePreferenceShare");
		setProductsBasePenetration();
		for (int i = 0; i < product.Count; i++)
		{
			if (list.Contains(product[i].ProductName))
			{
				product[i].Included = true;
			}
			else
			{
				product[i].Included = false;
			}
		}
	}

	public void includeExcludeProductScenarios(string productName, bool include)
	{
		if (include)
		{
			if (product[productName].BaseProduct != 0)
			{
				for (int i = 0; i < product.Count; i++)
				{
					if (product[productName].BaseProduct == product[i].BaseProduct)
					{
						product[i].Included = false;
						product[i].NewShare = 0.0;
						product[i].NewPreferenceShare = 0.0;
					}
				}
				product[product[productName].BaseProduct - 1].Included = false;
				product[product[productName].BaseProduct - 1].NewShare = 0.0;
				product[product[productName].BaseProduct - 1].NewPreferenceShare = 0.0;
			}
			else
			{
				for (int i = 0; i < product.Count; i++)
				{
					if (product[productName].ProductID == product[i].BaseProduct)
					{
						product[i].Included = false;
						product[i].NewShare = 0.0;
						product[i].NewPreferenceShare = 0.0;
					}
				}
			}
			product[productName].Included = true;
		}
		else
		{
			product[productName].Included = false;
			product[productName].NewShare = 0.0;
			product[productName].NewPreferenceShare = 0.0;
		}
		scenarioName = "Configured Scenario";
	}

	public bool getProductIncludedStatus(string productName)
	{
		return product[productName].Included;
	}

	public void changeProductAttributeCombination(string productName, string attributeName, string value)
	{
		if (attributeName.Equals("Audit Share"))
		{
			product[productName].AuditShare = double.Parse(value.Replace("%", ""));
		}
		else if (attributeName == "WD")
		{
			product[productName].NewWD = double.Parse(value);
			if (double.Parse(value) == 0.0)
			{
				product[productName].Included = false;
				product[productName].NewShare = 0.0;
				product[productName].NewPreferenceShare = 0.0;
			}
		}
		else
		{
			int attributeIndex = attribute[attributeName].AttributeID - 1;
			if (attribute[attributeName].IsDicrete)
			{
				for (int i = 0; i < attribute[attributeName].AttributeLevelsName.Length; i++)
				{
					if (attribute[attributeName].AttributeLevelsName[i] == value)
					{
						product[productName].setProductAttribute(attributeIndex, i + 1);
						break;
					}
				}
			}
			else if (attribute[attributeName].IsConditional)
			{
				if (double.Parse(value.Replace("%", "")) != 0.0)
				{
					if (value.Contains("%"))
					{
						double value2 = product[productName].getProductAttribute(attributeIndex) * (1.0 + double.Parse(value.TrimEnd('%')) / 100.0);
						product[productName].setProductAttribute(attributeIndex, value2);
					}
					else
					{
						product[productName].setProductAttribute(attributeIndex, double.Parse(value));
					}
				}
				else
				{
					product[productName].Included = false;
					product[productName].NewShare = 0.0;
					product[productName].NewPreferenceShare = 0.0;
				}
			}
		}
		scenarioName = "Configured Scenario";
	}

	public List<string> loadGroupsList()
	{
		List<string> list = new List<string>();
		for (int i = 0; i < group.Count; i++)
		{
			list.Add(group[i].GroupType);
		}
		return list;
	}

	public Dictionary<string, ArrayList> loadSegmentsDictionary()
	{
		Dictionary<string, ArrayList> dictionary = new Dictionary<string, ArrayList>();
		if (useSegmenting)
		{
			for (int i = 0; i < segment.Count; i++)
			{
				string[] array = new string[segment[i].MembersCount];
				segment[i].SegmentMembers.Values.CopyTo(array, 0);
				dictionary.Add(segment[i].SegmentType, new ArrayList(array));
			}
		}
		return dictionary;
	}

	public void setGroupResearchStart(string newGroupType, bool grouping)
	{
		string groupResearchStart = getGroupResearchStart();
		if (groupResearchStart != "")
		{
			group[groupResearchStart].ResearchStart = false;
		}
		if (newGroupType != "")
		{
			group[newGroupType].ResearchStart = true;
		}
		UseGroupng = grouping;
		group[newGroupType].setGroupExcludedSalesUnit(getSegmentation()[1]);
	}

	public string getGroupResearchStart()
	{
		for (int i = 0; i < group.Count; i++)
		{
			if (group[i].ResearchStart)
			{
				return group[i].GroupType;
			}
		}
		return "";
	}

	public void setSegmentation(string type, string member)
	{
		StudySegmentTypeStart = type;
		if (!type.Equals(""))
		{
			segment[type].SegmentMemberStart = member;
			setSegmentsShare(member);
			setSegmentsPrice(member);
			setSegmentsWD(member);
			group[getGroupResearchStart()].setGroupExcludedSalesUnit(member);
			setMSU(member);
			setVSU(member);
			respondentSegmentationProcess(type, member);
			setProductsBaseRevenue();
			setProductsBaseValueSalesUnit();
			setProductsBaseValueShare();
			return;
		}
		throw new Exception("Segment Type Cannot be empty value");
	}

	private void setSegmentsShare(string name)
	{
		DataRowCollection rows;
		string columnName;
		if (DataTransaction.InformationTransfer.DtSegmentsShares.Columns.Contains(name))
		{
			rows = DataTransaction.InformationTransfer.DtSegmentsShares.Rows;
			columnName = name;
		}
		else
		{
			rows = DataTransaction.InformationTransfer.DtProducts.Rows;
			columnName = "BaseMarketShare";
		}
		foreach (DataRow item in rows)
		{
			product[item["ProductName"].ToString()].BaseMarketShare = double.Parse(item[columnName].ToString());
		}
		resetProductTypes();
	}

	private void setSegmentsPrice(string name)
	{
		DataRowCollection rows;
		string columnName;
		if (DataTransaction.InformationTransfer.DtSegmentsPrice.Columns.Contains(name))
		{
			rows = DataTransaction.InformationTransfer.DtSegmentsPrice.Rows;
			columnName = name;
		}
		else
		{
			rows = DataTransaction.InformationTransfer.DtProducts.Rows;
			columnName = "Base" + attribute[priceAttributeIndex].AttributeName;
		}
		foreach (DataRow item in rows)
		{
			product[item["ProductName"].ToString()].setProductDefaultAttribute(priceAttributeIndex, double.Parse(item[columnName].ToString()));
			product[item["ProductName"].ToString()].setProductAttribute(priceAttributeIndex, double.Parse(item[columnName].ToString()));
		}
	}

	private void setSegmentsWD(string name)
	{
		DataRowCollection rows;
		string columnName;
		if (DataTransaction.InformationTransfer.DtSegmentsWD.Columns.Contains(name))
		{
			rows = DataTransaction.InformationTransfer.DtSegmentsWD.Rows;
			columnName = name;
		}
		else
		{
			rows = DataTransaction.InformationTransfer.DtProducts.Rows;
			columnName = "WD";
		}
		foreach (DataRow item in rows)
		{
			product[item["ProductName"].ToString()].OriginalWD = double.Parse(item[columnName].ToString());
			product[item["ProductName"].ToString()].NewWD = product[item["ProductName"].ToString()].OriginalWD;
		}
	}

	private void setMSU(string segmentName)
	{
		DataRow dataRow = DataTransaction.InformationTransfer.DtSegmentsMSU.Rows[0];
		string columnName = ((!DataTransaction.InformationTransfer.DtSegmentsMSU.Columns.Contains(segmentName)) ? "Total" : segmentName);
		MSU = (double)dataRow[columnName];
	}

	private void setVSU(string segmentName)
	{
		DataRow dataRow = DataTransaction.InformationTransfer.DtSegmentsVSU.Rows[0];
		string columnName = ((!DataTransaction.InformationTransfer.DtSegmentsVSU.Columns.Contains(segmentName)) ? "Total" : segmentName);
		VSU = (double)dataRow[columnName];
	}

	public bool isAttributeIncluded(string attributeName)
	{
		return attribute[attributeName].AttributeIncluded;
	}

	public bool isAttributeVisible(string attributeName)
	{
		return attribute[attributeName].AttributeVisible;
	}

	public void getPredictedSharesDatatable()
	{
		findPredictedShares();
		fillDisplayResultTable();
	}

	public void findPredictedShares()
	{
		List<string> list = new List<string>();
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].Included && product[i].ProductType.Equals("NewProduct"))
			{
				list.Add(product[i].ProductName);
				product[i].Included = false;
			}
			product[i].CurrentShare = product[i].BaseMarketShare;
			product[i].CurrentPreferenceShare = product[i].BasePreferenceShare;
		}
		loadPreferenceShare("NewPreferenceShare");
		product.calculateNewShare(sizeAttributeIndex);
		if (list.Count > 0)
		{
			for (int i = 0; i < product.Count; i++)
			{
				if (product[i].Included)
				{
					if (product[i].ProductType.Equals("ReplacedProduct"))
					{
						product[product[i].BaseProduct - 1].CurrentShare = product[i].NewShare;
						product[product[i].BaseProduct - 1].CurrentPreferenceShare = product[i].NewPreferenceShare;
					}
					else
					{
						product[i].CurrentShare = product[i].NewShare;
						product[i].CurrentPreferenceShare = product[i].NewPreferenceShare;
					}
					continue;
				}
				List<string> alternativeProductList = getAlternativeProductList(product[i].ProductID);
				bool flag = false;
				if (alternativeProductList.Count > 0)
				{
					foreach (string item in alternativeProductList)
					{
						if (product[item].Included)
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					product[i].CurrentShare = 0.0;
				}
			}
			foreach (string item2 in list)
			{
				product[item2].Included = true;
			}
			loadPreferenceShare("NewPreferenceShare");
			product.calculateNewShare(sizeAttributeIndex);
		}
		setProductsRevenue();
		setProductsValueSalesUnit();
		setProductsValueShare();
		setProductsPenetration();
	}

	internal List<string> getAlternativeProductList(int productID)
	{
		List<string> list = new List<string>();
		for (int i = 0; i < product.Count; i++)
		{
			if (productID == product[i].BaseProduct)
			{
				list.Add(product[i].ProductName);
			}
		}
		return list;
	}

	public DataTable getTableSchema(string tableName)
	{
		return DataTransaction.InformationTransfer.getTableSchema(tableName);
	}

	public string alterDatabaseDesign(string databaseFilePath, string[] baseAttribute, string[] condtionalAttributes, string[] AttributeLevelsName, string[] groupTypes, string[] segementTypes, string[] SegmentsSharesNames, string[] SegmentsPriceNames, string[] SegmentsWDsNames, string[] groupsExcludedSalesUnitSegmentsName, string[] segmentsMSU, string[] segmentsVSU)
	{
		DatabaseDesign databaseDesign = new DatabaseDesign(databaseFilePath);
		setDatabaseConnectionString(databaseFilePath);
		return databaseDesign.executeCommand(baseAttribute, condtionalAttributes, AttributeLevelsName, groupTypes, segementTypes, SegmentsSharesNames, SegmentsPriceNames, SegmentsWDsNames, groupsExcludedSalesUnitSegmentsName, segmentsMSU, segmentsVSU);
	}

	public void initializeDataset(string[] conditionalAttributeArray, bool useGroupingOption, bool useSegmentingOption)
	{
		DataTransaction.InformationTransfer.initializeDataTable(conditionalAttributeArray, useGroupingOption, useSegmentingOption);
	}

	public void fillDatabase(string[] condtionalAttributeArray, bool useGroupingOption, bool useSegmentingOption)
	{
		DataTransaction.InformationTransfer.updateMarketSimulatorDataSet(condtionalAttributeArray, useGroupingOption, useSegmentingOption);
	}

	private void resetProductTypes()
	{
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].BaseMarketShare != 0.0 && product[i].BaseProduct == 0)
			{
				product[i].Included = true;
				product[i].BaseIncluded = true;
				product[i].ProductType = "ExistingProduct";
			}
			else
			{
				product[i].Included = false;
				product[i].BaseIncluded = false;
			}
			if (product[i].BaseMarketShare == 0.0)
			{
				if (product[i].BaseProduct > 0)
				{
					product[i].ReplacedProduct = true;
					product[i].NewProduct = false;
					product[i].ProductType = "ReplacedProduct";
				}
				else if (product[i].BaseProduct == 0)
				{
					product[i].NewProduct = true;
					product[i].ReplacedProduct = false;
					product[i].ProductType = "NewProduct";
				}
			}
		}
	}

	public void resetProductValues()
	{
		resetProductTypes();
		for (int i = 0; i < product.Count; i++)
		{
			for (int j = 0; j < attribute.Count; j++)
			{
				product[i].setProductAttribute(j, product[i].getProductDefaultAttribute(j));
				product[i].NewWD = product[i].OriginalWD;
				product[i].AuditShare = 0.0;
				product[i].CurrentShare = product[i].BaseMarketShare;
				product[i].NewShare = 0.0;
				product[i].NewPreferenceShare = 0.0;
			}
		}
		scenarioName = "Default";
	}

	public string[] getSegmentation()
	{
		string[] array = new string[2];
		if (!StudySegmentTypeStart.Equals(""))
		{
			array[0] = StudySegmentTypeStart;
			array[1] = segment[StudySegmentTypeStart].SegmentMemberStart;
		}
		return array;
	}

	public void resetSegmentation()
	{
		StudySegmentTypeStart = "";
		foreach (DataRow row in DataTransaction.InformationTransfer.DtProducts.Rows)
		{
			product[row["ProductName"].ToString()].BaseMarketShare = double.Parse(row["BaseMarketShare"].ToString());
			product[row["ProductName"].ToString()].OriginalWD = double.Parse(row["WD"].ToString());
			product[row["ProductName"].ToString()].NewWD = product[row["ProductName"].ToString()].OriginalWD;
			product[row["ProductName"].ToString()].setProductDefaultAttribute(priceAttributeIndex, double.Parse(row["Base" + attribute[priceAttributeIndex].AttributeName].ToString()));
			product[row["ProductName"].ToString()].setProductAttribute(priceAttributeIndex, double.Parse(row["Base" + attribute[priceAttributeIndex].AttributeName].ToString()));
		}
		setMSU("Total");
		setVSU("Total");
		group[getGroupResearchStart()].setGroupExcludedSalesUnit("Total");
		resetProductTypes();
		initializeRespondentSelection();
		setProductsBaseRevenue();
	}

	public int getGroupMemberIndex(string groupType, string groupName)
	{
		foreach (KeyValuePair<int, string> groupMember in group[groupType].GroupMembers)
		{
			if (groupMember.Value == groupName)
			{
				return groupMember.Key;
			}
		}
		return -1;
	}

	public double getProductDefaultAttribute(string productName, string attributeName)
	{
		return product[productName].getProductDefaultAttribute(attribute[attributeName].AttributeID - 1);
	}

	public double getProductSizeUnit(string productName)
	{
		return product[productName].SizeUnit;
	}

	public double getProductAttribute(string productName, string attributeName)
	{
		return product[productName].getProductAttribute(attribute[attributeName].AttributeID - 1);
	}

	public string getDiscreteAttributeLevel(string attributeName, int levelIndex)
	{
		return attribute[attributeName].AttributeLevelsName[levelIndex - 1];
	}

	public string[] getDiscreteAttributeLevelsNameArray(string attributeName)
	{
		return attribute[attributeName].AttributeLevelsName;
	}

	public void updateFinancial()
	{
		setDataRow();
		for (int i = 0; i < product.Count; i++)
		{
			product[i].setDataRow(i);
		}
		DataTransaction.InformationTransfer.updateFinancial();
	}

	public void bindNOSAndTDCValues(string productName, string type, string Value)
	{
		if (type == "NOS")
		{
			product[productName].NOS = double.Parse(Value);
		}
		else if (type == "TDC")
		{
			product[productName].TDC = double.Parse(Value);
		}
	}

	public double[] getAttributeLevelsBoundries(string attributeName)
	{
		double[] array = new double[2];
		if (ConditionalAttributeNameList.Contains(attributeName))
		{
			array[0] = double.Parse(attribute[attributeName].AttributeLevelsName[0]);
			array[1] = double.Parse(attribute[attributeName].AttributeLevelsName[attribute[attributeName].AttributeLevelCount - 1]);
		}
		return array;
	}

	public double[] getProductMinMaxConditionalValidator(int productIndex, string attributeName)
	{
		return new double[2]
		{
			attribute[attributeName].ConditionalAttributeListValues[productIndex, 0],
			attribute[attributeName].ConditionalAttributeListValues[productIndex, attribute[attributeName].AttributeLevelCount - 1]
		};
	}

	public double getProductWD(string productName)
	{
		return product[productName].NewWD;
	}

	public double getProductOriginalWD(string productName)
	{
		return product[productName].OriginalWD;
	}

	public void undoOperation()
	{
		UndoRedoManager.Undo();
	}

	public void redoOperation()
	{
		UndoRedoManager.Redo();
	}

	public void flushUndoRedoHistory()
	{
		UndoRedoManager.FlushHistory();
	}

	public bool ContainsAttribute(string attributeName)
	{
		if (attribute.Contains(attributeName))
		{
			return true;
		}
		return false;
	}

	public void fillDisplayResultTable()
	{
		dtDisplayResult.Clear();
		dtDisplayResult.AcceptChanges();
		if (useGroupng)
		{
			string groupResearchStart = getGroupResearchStart();
			foreach (string item in GroupMember[groupResearchStart])
			{
				fillGroupDisplayResultRow(groupResearchStart, item);
				foreach (string item2 in GroupProducts[groupResearchStart][item])
				{
					fillProductDisplayResultRow(item2);
				}
			}
		}
		else
		{
			for (int i = 0; i < product.Count; i++)
			{
				fillProductDisplayResultRow(product[i].ProductName);
			}
		}
		dtDisplayResult.AcceptChanges();
	}

	public DataRow getGroupDataRow(string groupName)
	{
		foreach (DataRow row in dtGroupDisplayResult.Rows)
		{
			if (row["Name"].Equals(groupName))
			{
				return row;
			}
		}
		return null;
	}

	public void fillGroupsDisplayResult(string groupName)
	{
		double num = 0.0;
		double num2 = 0.0;
		double num3 = 0.0;
		double num4 = 0.0;
		double num5 = 0.0;
		double num6 = 0.0;
		double num7 = 0.0;
		double num8 = 0.0;
		double num9 = 0.0;
		dtGroupDisplayResult.Rows.Clear();
		if (string.IsNullOrEmpty(groupName))
		{
			return;
		}
		foreach (KeyValuePair<int, string> groupMember in group[groupName].GroupMembers)
		{
			DataRow dataRow = dtGroupDisplayResult.NewRow();
			num = getGroupShare("BaseMarketShare", groupName, groupMember.Key);
			num2 = getGroupShare("NewShare", groupName, groupMember.Key);
			num3 = getGroupBaseValueShare(groupName, groupMember.Key);
			num4 = getGroupValueShare(groupName, groupMember.Key);
			num5 = getGroupRevenue(groupName, groupMember.Key);
			num6 = getGroupBaseRevenue(groupName, groupMember.Key);
			num7 = getGroupContribution(groupName, groupMember.Key);
			dataRow["Name"] = groupMember.Value;
			dataRow["B.Share"] = num;
			dataRow["N.Share"] = num2;
			dataRow["S.Index"] = num2 / num;
			dataRow["Revenue"] = num5;
			dataRow["R.Index"] = num5 / num6;
			dataRow["V.Share"] = num4;
			dataRow["VS.Index"] = num4 / num3;
			dataRow["%"] = "%";
			dtGroupDisplayResult.Rows.Add(dataRow);
		}
		DataRow dataRow2 = dtGroupDisplayResult.NewRow();
		dataRow2["Name"] = "Total " + companyName;
		dataRow2["B.Share"] = getCompanyShare("BaseMarketShare");
		dataRow2["N.Share"] = getCompanyShare("NewShare");
		dataRow2["S.Index"] = double.Parse(dataRow2["N.Share"].ToString()) / double.Parse(dataRow2["B.Share"].ToString());
		dataRow2["Revenue"] = getCompanyRevenue().ToString(numberFormat);
		dataRow2["R.Index"] = double.Parse(dataRow2["Revenue"].ToString()) / getCompanyBaseRevenue();
		num9 = getCompanyValueShare();
		num8 = getCompanyBaseValueShare();
		dataRow2["V.Share"] = num9;
		dataRow2["VS.Index"] = num9 / num8;
		dataRow2["MM$"] = getCompanyContribution();
		dataRow2["%"] = "%";
		dtGroupDisplayResult.Rows.Add(dataRow2);
	}

	public double getCompanyShare(string shareType)
	{
		double num = 0.0;
		for (int i = 0; i < product.Count; i++)
		{
			if (!product[i].IsCompany)
			{
				continue;
			}
			switch (shareType)
			{
			case "BaseMarketShare":
				num += product[i].BaseMarketShare;
				break;
			case "NewShare":
				if (product[i].Included)
				{
					num += product[i].NewShare;
				}
				break;
			}
		}
		return num;
	}

	public double getCompanyValueShare()
	{
		double num = 0.0;
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].IsCompany)
			{
				num += product[i].ValueShare;
			}
		}
		return num;
	}

	public double getCompanyBaseValueShare()
	{
		double num = 0.0;
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].IsCompany)
			{
				num += product[i].BaseValueShare;
			}
		}
		return num;
	}

	public double getCompanyPenetration()
	{
		double num = 0.0;
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].IsCompany)
			{
				num += product[i].Penetration;
			}
		}
		return num;
	}

	public double getCompanyBasePenetration()
	{
		double num = 0.0;
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].IsCompany)
			{
				num += product[i].BasePenetration;
			}
		}
		return num;
	}

	public double getCompanyRevenue()
	{
		double num = 0.0;
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].Included && product[i].IsCompany)
			{
				num += product[i].Revenue;
			}
		}
		return num;
	}

	public double getCompanyBaseRevenue()
	{
		double num = 0.0;
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].BaseIncluded && product[i].IsCompany)
			{
				num += product[i].BaseRevenue;
			}
		}
		return num;
	}

	public double getProductShare(string shareType, string productName)
	{
		switch (shareType)
		{
		case "BaseMarketShare":
			if (product[productName].ReplacedProduct)
			{
				return product[product[productName].BaseProduct - 1].BaseMarketShare;
			}
			return product[productName].BaseMarketShare;
		case "NewShare":
			return product[productName].NewShare;
		case "PreferenceShare":
			return product[productName].NewPreferenceShare;
		case "AuditShare":
			return product[productName].AuditShare;
		default:
			return -1.0;
		}
	}

	public double getProductValueSalesUnit(string productName)
	{
		return product[productName].ValueSalesUnit;
	}

	public double getProductValueShare(string productName)
	{
		return product[productName].ValueShare;
	}

	public double getProductBaseValueShare(string productName)
	{
		return product[productName].BaseValueShare;
	}

	public double getProductPenetration(string productName)
	{
		return product[productName].Penetration;
	}

	public double getProductBasePenetration(string productName)
	{
		return product[productName].BasePenetration;
	}

	public double getProductRevenue(string productName)
	{
		return product[productName].Revenue;
	}

	public double getProductBaseRevenue(string productName)
	{
		return product[productName].BaseRevenue;
	}

	private double getProductsTotalRevenue()
	{
		double num = 0.0;
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].Included)
			{
				num += product[i].Revenue;
			}
		}
		return num;
	}

	private double getProductsTotalBaseRevenue()
	{
		double num = 0.0;
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].BaseIncluded)
			{
				num += product[i].BaseRevenue;
			}
		}
		return num;
	}

	private double getProductsTotalValueSalesUnit()
	{
		double num = 0.0;
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].Included)
			{
				num += product[i].ValueSalesUnit;
			}
		}
		return num;
	}

	private double getProductsTotalBaseValueSalesUnit()
	{
		double num = 0.0;
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].Included)
			{
				num += product[i].BaseValueSalesUnit;
			}
		}
		return num;
	}

	public List<string> getNewProductList()
	{
		List<string> list = new List<string>();
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].ProductType.Equals("NewProduct"))
			{
				list.Add(product[i].ProductName);
			}
		}
		return list;
	}

	public void setProductsRevenue()
	{
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].Included)
			{
				if (attribute.Contains("Price"))
				{
					product[i].PriceUnit = product[i].getProductAttribute(priceAttributeIndex);
				}
				product[i].Revenue = MSU * SU * product[i].NewShare * product[i].PriceUnit / (product[i].SizeUnit * 100.0);
			}
		}
	}

	public void setProductsBaseRevenue()
	{
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].BaseIncluded)
			{
				if (attribute.Contains("Price"))
				{
					product[i].PriceUnit = product[i].getProductDefaultAttribute(priceAttributeIndex);
				}
				product[i].BaseRevenue = MSU * SU * product[i].BaseMarketShare * product[i].PriceUnit / (product[i].SizeUnit * 100.0);
			}
		}
	}

	private void setProductsValueSalesUnit()
	{
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].Included)
			{
				product[i].ValueSalesUnit = MSU * product[i].NewShare * product[i].getProductAttribute(priceAttributeIndex) / (100.0 * product[i].SizeUnit);
			}
		}
	}

	private void setProductsBaseValueSalesUnit()
	{
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].Included)
			{
				product[i].BaseValueSalesUnit = MSU * product[i].BaseMarketShare * product[i].getProductDefaultAttribute(priceAttributeIndex) / (100.0 * product[i].SizeUnit);
			}
		}
	}

	private void setProductsValueShare()
	{
		valueShareUnit = getProductsTotalValueSalesUnit() + VSU - getProductsTotalBaseRevenue();
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].Included)
			{
				product[i].ValueShare = product[i].ValueSalesUnit / valueShareUnit * 100.0;
			}
		}
	}

	private void setProductsBaseValueShare()
	{
		baseValueShareUnit = getProductsTotalBaseValueSalesUnit() + VSU - getProductsTotalBaseRevenue();
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].Included)
			{
				product[i].BaseValueShare = product[i].BaseValueSalesUnit / baseValueShareUnit * 100.0;
			}
		}
	}

	private void setProductsPenetration()
	{
		double num = 0.0;
		double num2 = 0.0;
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].Included)
			{
				num2 += product[i].NewPreferenceShare * product[i].NewWD;
				num += product[i].NewShare;
			}
		}
		double num3 = num / num2;
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].Included)
			{
				product[i].Penetration = product[i].NewPreferenceShare * product[i].NewWD * num3;
			}
		}
	}

	private void setProductsBasePenetration()
	{
		double num = 0.0;
		double num2 = 0.0;
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].Included)
			{
				num2 += product[i].BasePreferenceShare * product[i].OriginalWD;
				num += product[i].BaseMarketShare;
			}
		}
		double num3 = num / num2;
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].Included)
			{
				product[i].BasePenetration = product[i].BasePreferenceShare * product[i].OriginalWD * num3;
			}
		}
	}

	public double getProductVaribles(string type, string productName)
	{
		return type switch
		{
			"Base WD" => product[productName].OriginalWD, 
			"New WD" => product[productName].NewWD, 
			"NOS" => product[productName].NOS, 
			"TDC" => product[productName].TDC, 
			_ => 0.0, 
		};
	}

	public double getGroupShare(string shareType, string groupType, int groupID)
	{
		double num = 0.0;
		for (int i = 0; i < product.Count; i++)
		{
			if (group[groupType].ProductGrouping[product[i].ProductID] != groupID)
			{
				continue;
			}
			switch (shareType)
			{
			case "BaseMarketShare":
				num += product[i].BaseMarketShare;
				break;
			case "NewShare":
				if (product[i].Included)
				{
					num += product[i].NewShare;
				}
				break;
			case "AuditShare":
				num += product[i].AuditShare;
				break;
			}
		}
		return num;
	}

	public double getGroupRevenue(string groupType, int groupID)
	{
		double num = 0.0;
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].Included && group[groupType].ProductGrouping[product[i].ProductID] == groupID)
			{
				num += product[i].Revenue;
			}
		}
		return num;
	}

	public double getGroupBaseRevenue(string groupType, int groupID)
	{
		double num = 0.0;
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].BaseIncluded && group[groupType].ProductGrouping[product[i].ProductID] == groupID)
			{
				num += product[i].BaseRevenue;
			}
		}
		return num;
	}

	public double getGroupValueSalesUnit(string groupType, int groupID)
	{
		double num = 0.0;
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].Included && group[groupType].ProductGrouping[product[i].ProductID] == groupID)
			{
				num += product[i].ValueSalesUnit;
			}
		}
		return num;
	}

	private double getGroupBaseValueSalesUnit(string groupType, int groupID)
	{
		double num = 0.0;
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].Included && group[groupType].ProductGrouping[product[i].ProductID] == groupID)
			{
				num += product[i].BaseValueSalesUnit;
			}
		}
		return num;
	}

	public string getProductName(int productID)
	{
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].ProductID == productID)
			{
				return product[i].ProductName;
			}
		}
		return null;
	}

	public double getTotalProductsAuditShare()
	{
		double num = 0.0;
		for (int i = 0; i < product.Count; i++)
		{
			num += product[i].AuditShare;
		}
		return num;
	}

	public void rebaseProductNewShare(double rebaseValue)
	{
		double totalProductsNewShare = getTotalProductsNewShare();
		for (int i = 0; i < product.Count; i++)
		{
			product[i].NewShare = product[i].NewShare * rebaseValue / totalProductsNewShare;
		}
	}

	public void setNumberFormat()
	{
		if (decimalCount == 0)
		{
			numberFormat = "0";
		}
		else if (decimalCount > 0 && decimalCount < 11)
		{
			numberFormat = "0.";
			for (int i = 0; i < decimalCount; i++)
			{
				numberFormat += "#";
			}
		}
		else
		{
			decimalCount = 10;
		}
	}

	public DataTable getScenarioDatatableObject(string scenarioName)
	{
		this.scenarioName = scenarioName;
		DataTable dataTable = new DataTable(scenarioName);
		dataTable.Columns.Add("Product Name", typeof(string));
		dataTable.Columns.Add("Included", typeof(string));
		foreach (string conditionalAttributeName in ConditionalAttributeNameList)
		{
			dataTable.Columns.Add(conditionalAttributeName, typeof(string));
		}
		foreach (string discreteAttributeName in DiscreteAttributeNameList)
		{
			dataTable.Columns.Add(discreteAttributeName, typeof(string));
		}
		dataTable.Columns.Add("WD", typeof(string));
		dataTable.Columns.Add("AuditShare", typeof(string));
		dataTable.Columns.Add("NOS", typeof(string));
		dataTable.Columns.Add("TDC", typeof(string));
		fillScenarioDataTableObject(dataTable);
		string[] segmentation = getSegmentation();
		dataTable.ExtendedProperties.Add("SegmentType", segmentation[0]);
		dataTable.ExtendedProperties.Add("SegmentMember", segmentation[1]);
		return dataTable;
	}

	public void bindScenarioDatatableObjectToProjectProducts(DataTable scenarioTable)
	{
		if (scenarioTable.ExtendedProperties.ContainsKey("SegmentType"))
		{
			string text = scenarioTable.ExtendedProperties["SegmentType"].ToString();
			string text2 = scenarioTable.ExtendedProperties["SegmentMember"].ToString();
			if (text != null && !text.Equals("") && text2 != null && !text2.Equals(""))
			{
				setSegmentation(text, text2);
			}
			else
			{
				resetSegmentation();
			}
		}
		foreach (DataRow row in scenarioTable.Rows)
		{
			string productName = row["Product Name"].ToString();
			product[productName].Included = bool.Parse(row["Included"].ToString());
			foreach (string conditionalAttributeName in ConditionalAttributeNameList)
			{
				int attributeIndex = attribute[conditionalAttributeName].AttributeID - 1;
				product[productName].setProductAttribute(attributeIndex, double.Parse(row[conditionalAttributeName].ToString()));
			}
			foreach (string discreteAttributeName in DiscreteAttributeNameList)
			{
				int attributeIndex = attribute[discreteAttributeName].AttributeID - 1;
				product[productName].setProductAttribute(attributeIndex, double.Parse(row[discreteAttributeName].ToString()));
			}
			product[productName].NewWD = double.Parse(row["WD"].ToString());
			product[productName].AuditShare = double.Parse(row["AuditShare"].ToString());
			product[productName].NOS = double.Parse(row["NOS"].ToString());
			product[productName].TDC = double.Parse(row["TDC"].ToString());
		}
		scenarioName = scenarioTable.TableName;
	}

	private void getDataRow(int index)
	{
		DataRow dataRow = DataTransaction.InformationTransfer.DtModule.Rows[index];
		studyName = dataRow["StudyName"].ToString();
		studyDescription = dataRow["StudyDescription"].ToString();
		companyName = dataRow["CompanyName"].ToString();
		companyLocation = dataRow["CompanyLocation"].ToString();
		researchLogo += dataRow["ResearchLogo"].ToString();
		SU = double.Parse(dataRow["SU"].ToString());
		exponentFactor = double.Parse(dataRow["ExponentFactor"].ToString());
		analysis = bool.Parse(dataRow["Analysis"].ToString());
		sizeAttributeIndex = int.Parse(dataRow["SizeAttribute"].ToString()) - 1;
		skuAttributeIndex = int.Parse(dataRow["SKUAttribute"].ToString());
		respondentsCount = int.Parse(dataRow["RespondentsCount"].ToString());
		respondentSelection = new bool[respondentsCount];
		UseGroupng = bool.Parse(dataRow["UseGrouping"].ToString());
		useSegmenting = bool.Parse(dataRow["UseSegmenting"].ToString());
		financial = bool.Parse(dataRow["Financial"].ToString());
		respondentsWeight = new double[respondentsCount];
		DataRowCollection rows = DataTransaction.InformationTransfer.DtHBUData.Rows;
		for (int i = 0; i < respondentsCount; i++)
		{
			respondentsWeight[i] = (double)rows[i]["RespondentWeight"];
		}
		setMSU("Total");
		setVSU("Total");
	}

	private void setDataRow()
	{
		DataRow dataRow = DataTransaction.InformationTransfer.DtModule.Rows[0];
		dataRow["StudyName"] = studyName;
		dataRow["StudyDescription"] = studyDescription;
		dataRow["CompanyName"] = companyName;
		dataRow["CompanyLocation"] = companyLocation;
		dataRow["ResearchLogo"] = researchLogo;
		dataRow["MSU"] = MSU.ToString();
		dataRow["SU"] = SU.ToString();
		dataRow["ExponentFactor"] = exponentFactor.ToString();
		dataRow["SizeAttribute"] = sizeAttributeIndex.ToString();
		dataRow["ProductsCount"] = product.Count.ToString();
		dataRow["RespondentsCount"] = respondentsCount.ToString();
		dataRow["SKUAttribute"] = skuAttributeIndex.ToString();
		dataRow["UseGrouping"] = UseGroupng.ToString();
		dataRow["UseSegmenting"] = useSegmenting.ToString();
		dataRow["Financial"] = financial.ToString();
	}

	private void initializeAttributes()
	{
		attribute = new Attributes();
		int num = 0;
		foreach (DataRow row in DataTransaction.InformationTransfer.DtAttributes.Rows)
		{
			attribute[row["AttributeName"].ToString()] = new Attributes();
			attribute[row["AttributeName"].ToString()].getDataRow(num);
			if (row["AttributeName"].ToString().ToLower().Trim()
				.Equals("price"))
			{
				priceAttributeIndex = num;
			}
			num++;
		}
	}

	private void initializeProducts(int projectAttributeCount)
	{
		product = new Products(projectAttributeCount);
		int num = 0;
		foreach (DataRow row in DataTransaction.InformationTransfer.DtProducts.Rows)
		{
			product[row["ProductName"].ToString()] = new Products(projectAttributeCount);
			product[row["ProductName"].ToString()].getDataRow(DataTransaction.InformationTransfer.DtProducts.Rows.IndexOf(row));
			num++;
		}
	}

	private void initializeGroups()
	{
		group = new Groups();
		int num = 0;
		if (!UseGroupng)
		{
			return;
		}
		foreach (DataRow row in DataTransaction.InformationTransfer.DtGroupingTypes.Rows)
		{
			group[row["GroupType"].ToString()] = new Groups();
			group[row["GroupType"].ToString()].getDataRow(num);
			num++;
		}
	}

	private void initializeSegments()
	{
		segment = new Segments();
		int num = 0;
		if (!useSegmenting)
		{
			return;
		}
		foreach (DataRow row in DataTransaction.InformationTransfer.DtSegmentingTypes.Rows)
		{
			segment[row["segmentType"].ToString()] = new Segments();
			segment[row["segmentType"].ToString()].getDataRow(num);
			num++;
		}
	}

	private void initializeCompanyProductList()
	{
		CompanyProducts.Clear();
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].IsCompany)
			{
				CompanyProducts.Add(product[i].ProductName);
			}
		}
	}

	private void initializeGroupTypeList()
	{
		groupTypeList.Clear();
		for (int i = 0; i < group.Count; i++)
		{
			groupTypeList.Add(group[i].GroupType);
		}
	}

	private void initializeSegmentTypeList()
	{
		segmentTypeList.Clear();
		for (int i = 0; i < segment.Count; i++)
		{
			segmentTypeList.Add(segment[i].SegmentType);
		}
	}

	private void initializeConditionalAttributeNameList()
	{
		conditionalAttributeNameList.Clear();
		for (int i = 0; i < attribute.Count; i++)
		{
			if (attribute[i].IsConditional)
			{
				conditionalAttributeNameList.Add(attribute[i].AttributeName);
			}
		}
	}

	private void initializeDiscreteAttributeNameList()
	{
		discreteAttributeNameList.Clear();
		for (int i = 0; i < attribute.Count; i++)
		{
			if (attribute[i].IsDicrete && attribute[i].AttributeName != "SKU")
			{
				discreteAttributeNameList.Add(attribute[i].AttributeName);
			}
		}
	}

	private void initializeAllProductList()
	{
		allProducts.Clear();
		for (int i = 0; i < product.Count; i++)
		{
			allProducts.Add(product[i].ProductName);
		}
	}

	private void initializeGroupMember()
	{
		groupMember.Clear();
		for (int i = 0; i < group.Count; i++)
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<int, string> groupMember in group[i].GroupMembers)
			{
				list.Add(groupMember.Value);
			}
			this.groupMember.Add(group[i].GroupType, list);
		}
	}

	private void initializeSegmentMember()
	{
		segmentMember.Clear();
		for (int i = 0; i < segment.Count; i++)
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<int, string> segmentMember in segment[i].SegmentMembers)
			{
				list.Add(segmentMember.Value);
			}
			this.segmentMember.Add(segment[i].SegmentType, list);
		}
	}

	private void initializeGroupProducts()
	{
		groupProducts.Clear();
		for (int i = 0; i < group.Count; i++)
		{
			Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
			foreach (KeyValuePair<int, string> groupMember in group[i].GroupMembers)
			{
				List<string> list = new List<string>();
				foreach (KeyValuePair<int, int> item in group[i].ProductGrouping)
				{
					if (groupMember.Key == item.Value)
					{
						list.Add(getProductName(item.Key));
					}
				}
				dictionary.Add(groupMember.Value, list);
			}
			groupProducts.Add(group[i].GroupType, dictionary);
		}
	}

	private void loadUtilityMatrix(string preferenceShareType)
	{
		productTotalUtility = new double[respondentsCount, product.Count];
		double num = 0.0;
		for (int i = 0; i < respondentsCount; i++)
		{
			for (int j = 0; j < product.Count; j++)
			{
				num = getProductUtility(i, j, preferenceShareType);
				productTotalUtility[i, j] = Math.Exp(num * exponentFactor);
			}
		}
		for (int i = 0; i < respondentsCount; i++)
		{
			if (!respondentSelection[i])
			{
				continue;
			}
			double num2 = 0.0;
			for (int j = 0; j < product.Count; j++)
			{
				if (product[j].Included)
				{
					num2 += productTotalUtility[i, j];
				}
			}
			for (int j = 0; j < product.Count; j++)
			{
				if (product[j].Included)
				{
					productTotalUtility[i, j] /= num2;
				}
			}
		}
	}

	private double getProductUtility(int respondentIndex, int productIndex, string preferenceShareType)
	{
		double num = 0.0;
		for (int i = 0; i < attribute.Count; i++)
		{
			if (attribute[i].AttributeIncluded)
			{
				double baseProductAttributeValue = 0.0;
				if (preferenceShareType == "BasePreferenceShare")
				{
					baseProductAttributeValue = product[productIndex].getProductDefaultAttribute(i);
				}
				else if (preferenceShareType == "NewPreferenceShare")
				{
					baseProductAttributeValue = product[productIndex].getProductAttribute(i);
				}
				double attributeLevelUtility = attribute[i].getAttributeLevelUtility(respondentIndex, productIndex, baseProductAttributeValue);
				if (double.IsNaN(attributeLevelUtility))
				{
					throw new Exception(product[productIndex].ProductName + " has a base level out of boundry values");
				}
				num += attributeLevelUtility;
			}
		}
		return num * respondentsWeight[respondentIndex];
	}

	private double getPreferenceShare(int productIndex)
	{
		double num = 0.0;
		int num2 = 0;
		for (int i = 0; i < respondentsCount; i++)
		{
			if (respondentSelection[i])
			{
				num += productTotalUtility[i, productIndex];
				num2++;
			}
		}
		return num * 100.0 / (double)num2;
	}

	private void loadPreferenceShare(string type)
	{
		loadUtilityMatrix(type);
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].Included)
			{
				switch (type)
				{
				case "BasePreferenceShare":
					product[i].BasePreferenceShare = getPreferenceShare(i);
					break;
				case "NewPreferenceShare":
					product[i].NewPreferenceShare = getPreferenceShare(i);
					break;
				}
			}
		}
	}

	private bool checkGroupProductsIncluded(string groupType, string groupTypeMember)
	{
		foreach (string item in GroupProducts[groupType][groupTypeMember])
		{
			if (string.Equals(product[item].ProductType, "ExistingProduct") && !product[item].Included && !searchReplacedProductIncluded(item))
			{
				return false;
			}
		}
		return true;
	}

	private bool searchReplacedProductIncluded(string productName)
	{
		bool result = false;
		for (int i = 0; i < product.Count; i++)
		{
			if (string.Equals(product[i].ProductType, "ReplacedProduct") && product[i].BaseProduct == product[productName].ProductID && product[i].Included)
			{
				result = true;
			}
		}
		return result;
	}

	private void fillGroupDisplayResultRow(string groupType, string groupTypeMember)
	{
		double groupShare = getGroupShare("BaseMarketShare", groupType, getGroupMemberIndex(groupType, groupTypeMember));
		double groupShare2 = getGroupShare("NewShare", groupType, getGroupMemberIndex(groupType, groupTypeMember));
		DataRow dataRow = dtDisplayResult.NewRow();
		dataRow["Product Index"] = "-1";
		dataRow["Selected"] = checkGroupProductsIncluded(groupType, groupTypeMember).ToString();
		dataRow["Product Name"] = groupTypeMember;
		dataRow["B.Share"] = groupShare.ToString(numberFormat) + " %";
		dataRow["N.Share"] = groupShare2.ToString(numberFormat) + " %";
		dataRow["S.Index"] = (groupShare2 / groupShare).ToString(numberFormat);
		dataRow["Preference Share"] = getGroupNewPreferenceShare(groupType, getGroupMemberIndex(groupType, groupTypeMember)).ToString(numberFormat) + " %";
		dataRow["Penetration"] = getGroupPenetration(groupType, getGroupMemberIndex(groupType, groupTypeMember)).ToString(numberFormat) + " %";
		dataRow["B.P.Share"] = getGroupBasePreferenceShare(groupType, getGroupMemberIndex(groupType, groupTypeMember)).ToString(numberFormat) + " %";
		dataRow["Revenue"] = getGroupRevenue(groupType, getGroupMemberIndex(groupType, groupTypeMember)).ToString(numberFormat);
		dataRow["R.Index"] = (getGroupRevenue(groupType, getGroupMemberIndex(groupType, groupTypeMember)) / getGroupBaseRevenue(groupType, getGroupMemberIndex(groupType, groupTypeMember))).ToString(numberFormat);
		dataRow["VSU"] = getGroupValueSalesUnit(groupType, getGroupMemberIndex(groupType, groupTypeMember)).ToString(numberFormat);
		dataRow["Value Share"] = getGroupValueShare(groupType, getGroupMemberIndex(groupType, groupTypeMember)).ToString(numberFormat) + " %";
		dtDisplayResult.Rows.Add(dataRow);
	}

	private void fillProductDisplayResultRow(string productName)
	{
		DataRow dataRow = dtDisplayResult.NewRow();
		dataRow["Product Index"] = product[productName].ProductID.ToString();
		dataRow["Selected"] = product[productName].Included.ToString();
		dataRow["Product Name"] = product[productName].ProductName;
		dataRow["Base Product"] = product[productName].BaseProduct;
		dataRow["B.Share"] = product[productName].BaseMarketShare.ToString(numberFormat) + " %";
		dataRow["N.Share"] = product[productName].NewShare.ToString(numberFormat) + " %";
		dataRow["S.Index"] = (product[productName].NewShare / product[productName].BaseMarketShare).ToString(numberFormat);
		dataRow["Preference Share"] = product[productName].NewPreferenceShare.ToString(numberFormat) + " %";
		dataRow["Penetration"] = product[productName].Penetration.ToString(numberFormat) + " %";
		dataRow["B.P.Share"] = product[productName].BasePreferenceShare.ToString(numberFormat) + " %";
		dataRow["Type"] = product[productName].ProductType;
		dataRow["Base WD"] = product[productName].OriginalWD.ToString(numberFormat);
		dataRow["New WD"] = product[productName].NewWD.ToString(numberFormat);
		for (int i = 0; i < attribute.Count; i++)
		{
			if (!attribute[i].AttributeVisible)
			{
				continue;
			}
			if (attribute[i].IsConditional)
			{
				dataRow["Base " + attribute[i].AttributeName] = product[productName].getProductDefaultAttribute(i).ToString(numberFormat);
				if (attribute[i].AttributeIncluded)
				{
					dataRow["New " + attribute[i].AttributeName] = product[productName].getProductAttribute(i).ToString(numberFormat);
				}
			}
			else if (attribute[i].IsDicrete && attribute[i].AttributeName != "SKU")
			{
				int num = (int)product[productName].getProductDefaultAttribute(i);
				dataRow["Base " + attribute[i].AttributeName] = attribute[i].AttributeLevelsName[num - 1];
			}
		}
		dataRow["Revenue"] = product[productName].Revenue.ToString(numberFormat);
		dataRow["R.Index"] = (product[productName].Revenue / product[productName].BaseRevenue).ToString(numberFormat);
		dataRow["VSU"] = product[productName].ValueSalesUnit.ToString(numberFormat);
		dataRow["Value Share"] = product[productName].ValueShare.ToString(numberFormat) + " %";
		dataRow["Audit Share"] = product[productName].AuditShare.ToString(numberFormat) + " %";
		dataRow["NOS"] = product[productName].NOS.ToString(numberFormat);
		dataRow["TDC"] = product[productName].TDC.ToString(numberFormat);
		dtDisplayResult.Rows.Add(dataRow);
	}

	private double[,] minMaxConditionalValidator(string name)
	{
		double[,] array = new double[product.Count, 3];
		for (int i = 0; i < product.Count; i++)
		{
			array[i, 0] = i + 1;
			array[i, 1] = attribute[name].ConditionalAttributeListValues[i, 0];
			array[i, 2] = attribute[name].ConditionalAttributeListValues[i, attribute[name].AttributeLevelCount - 1];
		}
		return array;
	}

	private Dictionary<int, string> newDiscreteAttributeDictionary(string attributeName)
	{
		Dictionary<int, string> dictionary = new Dictionary<int, string>();
		for (int i = 0; i < product.Count; i++)
		{
			for (int j = 0; j < attribute.Count; j++)
			{
				if (attribute[j].AttributeID == attribute[attributeName].AttributeID)
				{
					int num = (int)product[i].getProductAttribute(j);
					dictionary.Add(product[i].ProductID, attribute[attributeName].AttributeLevelsName[num - 1]);
				}
			}
		}
		return dictionary;
	}

	private double getGroupContribution(string groupType, int groupID)
	{
		double num = 0.0;
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].Included && group[groupType].ProductGrouping[product[i].ProductID] == groupID)
			{
				num += (product[i].NOS - product[i].TDC) * product[i].NewShare * MSU / 100.0;
			}
		}
		return Math.Round(num, 0);
	}

	private double getCompanyContribution()
	{
		double num = 0.0;
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].Included && product[i].IsCompany)
			{
				num += (product[i].NOS - product[i].TDC) * product[i].NewShare * MSU / 100.0;
			}
		}
		return Math.Round(num, 0);
	}

	public double getGroupNewPreferenceShare(string groupType, int groupID)
	{
		double num = 0.0;
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].Included && group[groupType].ProductGrouping[product[i].ProductID] == groupID)
			{
				num += product[i].NewPreferenceShare;
			}
		}
		return num;
	}

	public double getGroupBasePreferenceShare(string groupType, int groupID)
	{
		double num = 0.0;
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].Included && group[groupType].ProductGrouping[product[i].ProductID] == groupID)
			{
				num += product[i].BasePreferenceShare;
			}
		}
		return num;
	}

	public double getGroupPenetration(string groupType, int groupID)
	{
		double num = 0.0;
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].Included && group[groupType].ProductGrouping[product[i].ProductID] == groupID)
			{
				num += product[i].Penetration;
			}
		}
		return num;
	}

	public double getGroupBasePenetration(string groupType, int groupID)
	{
		double num = 0.0;
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].Included && group[groupType].ProductGrouping[product[i].ProductID] == groupID)
			{
				num += product[i].BasePenetration;
			}
		}
		return num;
	}

	public double getGroupValueShare(string groupType, int groupID)
	{
		double groupValueSalesUnit = getGroupValueSalesUnit(groupType, groupID);
		return (groupValueSalesUnit + group[groupType].getGroupExcludedSalesUnit(group[groupType].GroupMembers[groupID])) * 100.0 / valueShareUnit;
	}

	public double getGroupBaseValueShare(string groupType, int groupID)
	{
		double groupBaseValueSalesUnit = getGroupBaseValueSalesUnit(groupType, groupID);
		return (groupBaseValueSalesUnit + group[groupType].getGroupExcludedSalesUnit(group[groupType].GroupMembers[groupID])) * 100.0 / baseValueShareUnit;
	}

	private double getTotalProductsNewShare()
	{
		double num = 0.0;
		for (int i = 0; i < product.Count; i++)
		{
			num += product[i].NewShare;
		}
		return num;
	}

	internal void createGroupDatatableSchema()
	{
		dtGroupDisplayResult = new DataTable("GroupDatatable");
		dtGroupDisplayResult.Columns.AddRange(new DataColumn[10]
		{
			new DataColumn("Name", typeof(string)),
			new DataColumn("B.Share", typeof(double)),
			new DataColumn("N.Share", typeof(double)),
			new DataColumn("S.Index", typeof(double)),
			new DataColumn("Revenue", typeof(double)),
			new DataColumn("R.Index", typeof(double)),
			new DataColumn("V.Share", typeof(double)),
			new DataColumn("VS.index", typeof(double)),
			new DataColumn("MM$", typeof(double)),
			new DataColumn("%", typeof(string))
		});
	}

	internal void createDisplayResultTableSchema()
	{
		dtDisplayResult = new DataTable("DisplayResult");
		DataColumn dataColumn = new DataColumn("Product Index", typeof(int));
		dataColumn.ReadOnly = true;
		dtDisplayResult.Columns.Add(dataColumn);
		DataColumn dataColumn2 = new DataColumn("Selected", typeof(bool));
		dataColumn2.ReadOnly = true;
		dtDisplayResult.Columns.Add(dataColumn2);
		DataColumn dataColumn3 = new DataColumn("Product Name", typeof(string));
		dataColumn3.ReadOnly = false;
		dtDisplayResult.Columns.Add(dataColumn3);
		DataColumn dataColumn4 = new DataColumn("Base Product", typeof(string));
		dataColumn4.ReadOnly = true;
		dtDisplayResult.Columns.Add(dataColumn4);
		DataColumn dataColumn5 = new DataColumn("B.Share", typeof(string));
		dataColumn5.ReadOnly = true;
		dtDisplayResult.Columns.Add(dataColumn5);
		DataColumn dataColumn6 = new DataColumn("N.Share", typeof(string));
		dataColumn6.ReadOnly = true;
		dtDisplayResult.Columns.Add(dataColumn6);
		DataColumn dataColumn7 = new DataColumn("S.Index", typeof(string));
		dataColumn7.ReadOnly = true;
		dtDisplayResult.Columns.Add(dataColumn7);
		DataColumn dataColumn8 = new DataColumn("B.P.Share", typeof(string));
		dataColumn8.ReadOnly = true;
		dtDisplayResult.Columns.Add(dataColumn8);
		DataColumn dataColumn9 = new DataColumn("Preference Share", typeof(string));
		dataColumn9.ReadOnly = true;
		dtDisplayResult.Columns.Add(dataColumn9);
		DataColumn dataColumn10 = new DataColumn("Penetration", typeof(string));
		dataColumn10.ReadOnly = true;
		dtDisplayResult.Columns.Add(dataColumn10);
		DataColumn dataColumn11 = new DataColumn("Type", typeof(string));
		dataColumn11.ReadOnly = true;
		dtDisplayResult.Columns.Add(dataColumn11);
		DataColumn dataColumn12 = new DataColumn("Base WD", typeof(string));
		dataColumn12.ReadOnly = true;
		dtDisplayResult.Columns.Add(dataColumn12);
		DataColumn dataColumn13 = new DataColumn("New WD", typeof(string));
		dataColumn13.ReadOnly = false;
		dtDisplayResult.Columns.Add(dataColumn13);
		DataRow dataRow = DataTransaction.InformationTransfer.DtModule.Rows[0];
		string text = dataRow["SKUAttribute"].ToString();
		foreach (DataRow row in DataTransaction.InformationTransfer.DtAttributes.Rows)
		{
			if (bool.Parse(row["IsVisible"].ToString()) && bool.Parse(row["IsConditional"].ToString()))
			{
				DataColumn dataColumn14 = new DataColumn("Base " + row["AttributeName"].ToString(), typeof(string));
				dataColumn14.ReadOnly = true;
				dtDisplayResult.Columns.Add(dataColumn14);
				if (bool.Parse(row["IsIncluded"].ToString()))
				{
					DataColumn dataColumn15 = new DataColumn("New " + row["AttributeName"].ToString(), typeof(string));
					dataColumn15.ReadOnly = false;
					dtDisplayResult.Columns.Add(dataColumn15);
				}
			}
		}
		foreach (DataRow row2 in DataTransaction.InformationTransfer.DtAttributes.Rows)
		{
			if (bool.Parse(row2["IsVisible"].ToString()) && text != row2["AttributeID"].ToString() && bool.Parse(row2["IsDiscrete"].ToString()))
			{
				DataColumn dataColumn14 = new DataColumn("Base " + row2["AttributeName"].ToString(), typeof(string));
				dataColumn14.ReadOnly = true;
				dtDisplayResult.Columns.Add(dataColumn14);
			}
		}
		DataColumn dataColumn16 = new DataColumn("Revenue", typeof(double));
		dataColumn16.ReadOnly = true;
		dtDisplayResult.Columns.Add(dataColumn16);
		DataColumn dataColumn17 = new DataColumn("R.Index", typeof(string));
		dataColumn17.ReadOnly = true;
		dtDisplayResult.Columns.Add(dataColumn17);
		DataColumn dataColumn18 = new DataColumn("VSU", typeof(string));
		dataColumn18.ReadOnly = true;
		dtDisplayResult.Columns.Add(dataColumn18);
		DataColumn dataColumn19 = new DataColumn("Value Share", typeof(string));
		dataColumn19.ReadOnly = true;
		dtDisplayResult.Columns.Add(dataColumn19);
		DataColumn dataColumn20 = new DataColumn("Audit Share", typeof(string));
		dataColumn20.ReadOnly = false;
		dtDisplayResult.Columns.Add(dataColumn20);
		DataColumn dataColumn21 = new DataColumn("NOS", typeof(double));
		dataColumn21.ReadOnly = false;
		dtDisplayResult.Columns.Add(dataColumn21);
		DataColumn dataColumn22 = new DataColumn("TDC", typeof(double));
		dataColumn22.ReadOnly = false;
		dtDisplayResult.Columns.Add(dataColumn22);
	}

	private string[] getHightestIncrementalProduct(string incrementalFunction, List<string> incrementalProducts, Dictionary<string, double> incrementalProductValues, double baseScenarioValue, string incrementalType, string incrementalGroupType, string incrementalGroupMember)
	{
		string[] array = new string[2];
		Dictionary<string, double> dictionary = new Dictionary<string, double>();
		Dictionary<string, double>.Enumerator enumerator = incrementalProductValues.GetEnumerator();
		enumerator.MoveNext();
		double num = enumerator.Current.Value - baseScenarioValue;
		foreach (KeyValuePair<string, double> incrementalProductValue in incrementalProductValues)
		{
			dictionary.Add(incrementalProductValue.Key, incrementalProductValue.Value - baseScenarioValue);
			if (num < incrementalProductValue.Value - baseScenarioValue)
			{
				num = incrementalProductValue.Value - baseScenarioValue;
			}
		}
		List<string> list = searchHighestIncrementalProducts(dictionary, num);
		if (list.Count > 1)
		{
			array[0] = getTemporaryHighestIncrementalProduct(incrementalFunction, incrementalProducts, list, incrementalType, incrementalGroupType, incrementalGroupMember);
		}
		else
		{
			array[0] = list[0];
		}
		array[1] = num.ToString();
		return array;
	}

	private string getTemporaryHighestIncrementalProduct(string incrementalFunction, List<string> incrementalProducts, List<string> highestIncrementalProducts, string incrementalType, string incrementalGroupType, string incrementalGroupMember)
	{
		double num = 0.0;
		double num2 = 0.0;
		string result = "";
		string[] array = new string[2];
		List<string> list = new List<string>();
		foreach (string highestIncrementalProduct in highestIncrementalProducts)
		{
			if (string.Equals(incrementalFunction, "Intro"))
			{
				product[highestIncrementalProduct].Included = true;
			}
			else if (string.Equals(incrementalFunction, "Removal"))
			{
				product[highestIncrementalProduct].Included = false;
			}
			try
			{
				num = getScenarioValue(incrementalType, incrementalGroupType, incrementalGroupMember);
				foreach (string incrementalProduct in incrementalProducts)
				{
					if (string.Equals(highestIncrementalProduct, incrementalProduct))
					{
						list.Add(incrementalProduct);
					}
				}
				Dictionary<string, double> incrementalProductValues = includeProductsToCurrentLineUp(list, incrementalType, incrementalGroupType, incrementalGroupMember);
				array = getHightestIncrementalProduct(incrementalFunction, list, incrementalProductValues, num, incrementalType, incrementalGroupType, incrementalGroupMember);
				if (highestIncrementalProducts.IndexOf(highestIncrementalProduct) == 0)
				{
					num2 = double.Parse(array[1]);
					result = array[0];
				}
				else if (num2 < double.Parse(array[1]))
				{
					num2 = double.Parse(array[1]);
					result = array[0];
				}
			}
			catch
			{
			}
			if (string.Equals(incrementalFunction, "Intro"))
			{
				product[highestIncrementalProduct].Included = false;
			}
			else if (string.Equals(incrementalFunction, "Removal"))
			{
				product[highestIncrementalProduct].Included = true;
			}
		}
		return result;
	}

	private List<string> searchHighestIncrementalProducts(Dictionary<string, double> incrementalFunctionResults, double highestIncremental)
	{
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, double> incrementalFunctionResult in incrementalFunctionResults)
		{
			if (highestIncremental == incrementalFunctionResult.Value)
			{
				list.Add(incrementalFunctionResult.Key);
			}
		}
		return list;
	}

	private Dictionary<string, double> includeProductsToCurrentLineUp(List<string> incrementalProducts, string incrementalType, string incrementalGroupType, string incrementalGroupMember)
	{
		Dictionary<string, double> dictionary = new Dictionary<string, double>();
		foreach (string incrementalProduct in incrementalProducts)
		{
			product[incrementalProduct].Included = true;
			try
			{
				dictionary.Add(incrementalProduct, getScenarioValue(incrementalType, incrementalGroupType, incrementalGroupMember));
			}
			catch
			{
			}
			product[incrementalProduct].Included = false;
		}
		return dictionary;
	}

	private Dictionary<string, double> excludeProductsFromCurrentLineUp(List<string> incrementalProducts, string incrementalType, string incrementalGroupType, string incrementalGroupMember)
	{
		Dictionary<string, double> dictionary = new Dictionary<string, double>();
		foreach (string incrementalProduct in incrementalProducts)
		{
			product[incrementalProduct].Included = false;
			try
			{
				dictionary.Add(incrementalProduct, getScenarioValue(incrementalType, incrementalGroupType, incrementalGroupMember));
			}
			catch
			{
			}
			product[incrementalProduct].Included = true;
		}
		return dictionary;
	}

	private void setCurrentMarketWithLineUpProducts(string incrementalGroupType, string IncrementalGroupMember, List<string> currentLineUpProducts)
	{
		if (string.Equals(incrementalGroupType, "Company Products"))
		{
			foreach (string companyProduct in CompanyProducts)
			{
				if (currentLineUpProducts.Contains(companyProduct))
				{
					product[companyProduct].Included = true;
				}
				else
				{
					product[companyProduct].Included = false;
				}
			}
			return;
		}
		foreach (string item in GroupProducts[incrementalGroupType][IncrementalGroupMember])
		{
			if (currentLineUpProducts.Contains(item))
			{
				product[item].Included = true;
			}
			else
			{
				product[item].Included = false;
			}
		}
	}

	private double getScenarioValue(string incrementalType, string incrementalGroupType, string IncrementalGroupMember)
	{
		double result = 0.0;
		findPredictedShares();
		if (string.Equals(incrementalType, "Revenue"))
		{
			result = ((!string.Equals(incrementalGroupType, "Company Products")) ? getGroupRevenue(incrementalGroupType, getGroupMemberIndex(incrementalGroupType, IncrementalGroupMember)) : getCompanyRevenue());
		}
		else if (string.Equals(incrementalType, "Market Share"))
		{
			result = ((!string.Equals(incrementalGroupType, "Company Products")) ? getGroupShare("NewShare", incrementalGroupType, getGroupMemberIndex(incrementalGroupType, IncrementalGroupMember)) : getCompanyShare("NewShare"));
		}
		return result;
	}

	private DataTable fillScenarioDataTableObject(DataTable scenarioTable)
	{
		for (int i = 0; i < product.Count; i++)
		{
			DataRow dataRow = scenarioTable.NewRow();
			dataRow["Product Name"] = product[i].ProductName;
			dataRow["Included"] = product[i].Included.ToString();
			foreach (string conditionalAttributeName in ConditionalAttributeNameList)
			{
				int attributeIndex = attribute[conditionalAttributeName].AttributeID - 1;
				dataRow[conditionalAttributeName] = product[i].getProductAttribute(attributeIndex);
			}
			foreach (string discreteAttributeName in DiscreteAttributeNameList)
			{
				int attributeIndex = attribute[discreteAttributeName].AttributeID - 1;
				int num = (int)product[i].getProductAttribute(attributeIndex);
				dataRow[discreteAttributeName] = num;
			}
			dataRow["WD"] = product[i].NewWD;
			dataRow["AuditShare"] = product[i].AuditShare;
			dataRow["NOS"] = product[i].NOS;
			dataRow["TDC"] = product[i].TDC;
			scenarioTable.Rows.Add(dataRow);
		}
		return scenarioTable;
	}

	public DataTable conditionalAttributeElasticityPerProduct(string attributeName)
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("Name", typeof(string));
		for (int i = 0; i < attribute[attributeName].AttributeLevelCount; i++)
		{
			dataTable.Columns.Add(attribute[attributeName].AttributeLevelsName[i], typeof(string));
		}
		findPredictedShares();
		Dictionary<int, double> dictionary = new Dictionary<int, double>();
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].Included)
			{
				dictionary.Add(product[i].ProductID, product[i].NewShare);
			}
		}
		for (int i = 0; i < product.Count; i++)
		{
			DataRow dataRow = null;
			if (product[i].Included)
			{
				dataRow = dataTable.NewRow();
				dataRow["Name"] = product[i].ProductName;
				for (int j = 0; j < attribute[attributeName].AttributeLevelCount; j++)
				{
					product[i].setProductAttribute(attribute[attributeName].AttributeID - 1, attribute[attributeName].ConditionalAttributeListValues[i, j]);
					findPredictedShares();
					dataRow[attribute[attributeName].AttributeLevelsName[j]] = ((product[i].NewShare - dictionary[product[i].ProductID]) / dictionary[product[i].ProductID]).ToString("0.####");
				}
				dataTable.Rows.Add(dataRow);
				product[i].setProductAttribute(attribute[attributeName].AttributeID - 1, product[i].getProductDefaultAttribute(attribute[attributeName].AttributeID - 1));
			}
		}
		dataTable.ExtendedProperties.Add("ChartName", "Elasticity_Product");
		return dataTable;
	}

	public DataTable conditionalAttributeElasticityPerGroup(string attributeName, string groupType)
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("Name", typeof(string));
		for (int i = 0; i < attribute[attributeName].AttributeLevelCount; i++)
		{
			dataTable.Columns.Add(attribute[attributeName].AttributeLevelsName[i], typeof(string));
		}
		findPredictedShares();
		Dictionary<int, double> dictionary = new Dictionary<int, double>();
		foreach (KeyValuePair<int, string> groupMember in group[groupType].GroupMembers)
		{
			dictionary.Add(groupMember.Key, getGroupShare("NewShare", groupType, groupMember.Key));
		}
		foreach (KeyValuePair<int, string> groupMember2 in group[groupType].GroupMembers)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["Name"] = groupMember2.Value;
			for (int j = 0; j < attribute[attributeName].AttributeLevelCount; j++)
			{
				for (int k = 0; k < product.Count; k++)
				{
					if (product[k].Included && group[groupType].ProductGrouping[product[k].ProductID] == groupMember2.Key)
					{
						product[k].setProductAttribute(attribute[attributeName].AttributeID - 1, attribute[attributeName].ConditionalAttributeListValues[k, j]);
					}
				}
				findPredictedShares();
				if (dictionary[groupMember2.Key] == 0.0)
				{
					dataRow[attribute[attributeName].AttributeLevelsName[j]] = "0";
				}
				else
				{
					dataRow[attribute[attributeName].AttributeLevelsName[j]] = ((getGroupShare("NewShare", groupType, groupMember2.Key) - dictionary[groupMember2.Key]) / dictionary[groupMember2.Key]).ToString("0.####");
				}
			}
			dataTable.Rows.Add(dataRow);
			for (int k = 0; k < product.Count; k++)
			{
				if (product[k].Included && group[groupType].ProductGrouping[product[k].ProductID] == groupMember2.Key)
				{
					product[k].setProductAttribute(attribute[attributeName].AttributeID - 1, product[k].getProductDefaultAttribute(attribute[attributeName].AttributeID - 1));
				}
			}
		}
		dataTable.ExtendedProperties.Add("ChartName", "Elasticity_Group");
		return dataTable;
	}

	public DataTable conditionalAttributeElasticityPerCompanyGroup(string attributeName, string groupType)
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("Name", typeof(string));
		for (int i = 0; i < attribute[attributeName].AttributeLevelCount; i++)
		{
			dataTable.Columns.Add(attribute[attributeName].AttributeLevelsName[i], typeof(string));
		}
		findPredictedShares();
		Dictionary<int, double> dictionary = new Dictionary<int, double>();
		foreach (KeyValuePair<int, string> groupMember in group[groupType].GroupMembers)
		{
			dictionary.Add(groupMember.Key, getCompanyShare("NewShare"));
		}
		foreach (KeyValuePair<int, string> groupMember2 in group[groupType].GroupMembers)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["Name"] = groupMember2.Value;
			for (int j = 0; j < attribute[attributeName].AttributeLevelCount; j++)
			{
				for (int k = 0; k < product.Count; k++)
				{
					if (product[k].Included && group[groupType].ProductGrouping[product[k].ProductID] == groupMember2.Key)
					{
						product[k].setProductAttribute(attribute[attributeName].AttributeID - 1, attribute[attributeName].ConditionalAttributeListValues[k, j]);
					}
				}
				findPredictedShares();
				if (dictionary[groupMember2.Key] == 0.0)
				{
					dataRow[attribute[attributeName].AttributeLevelsName[j]] = "0";
				}
				else
				{
					dataRow[attribute[attributeName].AttributeLevelsName[j]] = ((getCompanyShare("NewShare") - dictionary[groupMember2.Key]) / dictionary[groupMember2.Key]).ToString("0.####");
				}
			}
			dataTable.Rows.Add(dataRow);
			for (int k = 0; k < product.Count; k++)
			{
				if (product[k].Included && group[groupType].ProductGrouping[product[k].ProductID] == groupMember2.Key)
				{
					product[k].setProductAttribute(attribute[attributeName].AttributeID - 1, product[k].getProductDefaultAttribute(attribute[attributeName].AttributeID - 1));
				}
			}
		}
		dataTable.ExtendedProperties.Add("ChartName", "Elasticity_Company");
		return dataTable;
	}

	public DataTable conditionalAttributeElasticityPerCompanySKU(string attributeName)
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("Name", typeof(string));
		for (int i = 0; i < attribute[attributeName].AttributeLevelCount; i++)
		{
			dataTable.Columns.Add(attribute[attributeName].AttributeLevelsName[i], typeof(string));
		}
		findPredictedShares();
		Dictionary<int, double> dictionary = new Dictionary<int, double>();
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].Included)
			{
				dictionary.Add(product[i].ProductID, getCompanyShare("NewShare"));
			}
		}
		for (int i = 0; i < product.Count; i++)
		{
			DataRow dataRow = null;
			if (!product[i].Included)
			{
				continue;
			}
			dataRow = dataTable.NewRow();
			dataRow["Name"] = product[i].ProductName;
			for (int j = 0; j < attribute[attributeName].AttributeLevelCount; j++)
			{
				product[i].setProductAttribute(attribute[attributeName].AttributeID - 1, attribute[attributeName].ConditionalAttributeListValues[i, j]);
				findPredictedShares();
				if (getCompanyShare("BaseMarketShare") == 0.0)
				{
					dataRow[attribute[attributeName].AttributeLevelsName[j]] = "0";
				}
				else
				{
					dataRow[attribute[attributeName].AttributeLevelsName[j]] = ((getCompanyShare("NewShare") - dictionary[product[i].ProductID]) / dictionary[product[i].ProductID]).ToString("0.####");
				}
			}
			dataTable.Rows.Add(dataRow);
			product[i].setProductAttribute(attribute[attributeName].AttributeID - 1, product[i].getProductDefaultAttribute(attribute[attributeName].AttributeID - 1));
		}
		dataTable.ExtendedProperties.Add("ChartName", "Elasticity_Company");
		return dataTable;
	}

	public DataTable priceIndexAndRevenues(string productName, string referenceProductName, string[] scale)
	{
		DataTable dataTable = new DataTable("Price Index And Revenues");
		dataTable.Columns.Add("Name", typeof(string));
		string[] array = scale;
		foreach (string columnName in array)
		{
			dataTable.Columns.Add(columnName, typeof(string));
		}
		using (UndoRedoManager.Start("Price Index"))
		{
			for (int j = 0; j < product.Count; j++)
			{
				if (product[j].Included)
				{
					DataRow dataRow = dataTable.NewRow();
					dataRow["Name"] = product[j].ProductName;
					dataTable.Rows.Add(dataRow);
				}
			}
			DataRow dataRow2 = dataTable.NewRow();
			dataRow2["Name"] = CompanyName + " Shares";
			dataTable.Rows.Add(dataRow2);
			dataRow2 = dataTable.NewRow();
			dataRow2["Name"] = CompanyName + " Revenues";
			dataTable.Rows.Add(dataRow2);
			array = scale;
			foreach (string columnName in array)
			{
				double num = product[referenceProductName].getProductAttribute(attribute["Price"].AttributeID - 1) / product[referenceProductName].SizeUnit;
				changeProductAttributeCombination(productName, "Price", (num * double.Parse(columnName) * product[productName].SizeUnit).ToString());
				findPredictedShares();
				for (int j = 0; j < dataTable.Rows.Count - 2; j++)
				{
					dataTable.Rows[j][columnName] = product[dataTable.Rows[j]["Name"].ToString()].NewShare / 100.0;
				}
				dataTable.Rows[dataTable.Rows.Count - 2][columnName] = getCompanyShare("NewShare") / 100.0;
				dataTable.Rows[dataTable.Rows.Count - 1][columnName] = getCompanyRevenue();
			}
			UndoRedoManager.FlushHistory();
		}
		dataTable.ExtendedProperties.Add("ChartName", "Price Index And Revenues");
		return dataTable;
	}

	public DataTable priceIndexAndRevenues(string groupTypeName, string groupTypeMember, string productName, string referenceProductName, string[] scale)
	{
		DataTable dataTable = new DataTable("Price Index And Revenues");
		dataTable.Columns.Add("Name", typeof(string));
		string[] array = scale;
		foreach (string columnName in array)
		{
			dataTable.Columns.Add(columnName, typeof(string));
		}
		using (UndoRedoManager.Start("Price Index"))
		{
			for (int j = 0; j < product.Count; j++)
			{
				if (product[j].Included)
				{
					DataRow dataRow = dataTable.NewRow();
					dataRow["Name"] = product[j].ProductName;
					dataTable.Rows.Add(dataRow);
				}
			}
			DataRow dataRow2 = dataTable.NewRow();
			dataRow2["Name"] = CompanyName + " Shares";
			dataTable.Rows.Add(dataRow2);
			dataRow2 = dataTable.NewRow();
			dataRow2["Name"] = CompanyName + " Revenues";
			dataTable.Rows.Add(dataRow2);
			double productAttribute = product[productName].getProductAttribute(attribute["Price"].AttributeID - 1);
			double[] array2 = new double[GroupProducts[groupTypeName][groupTypeMember].Count];
			int num = 0;
			foreach (string item in GroupProducts[groupTypeName][groupTypeMember])
			{
				array2[num] = product[item].getProductAttribute(attribute["Price"].AttributeID - 1) / productAttribute;
				num++;
			}
			array = scale;
			foreach (string columnName in array)
			{
				double num2 = product[referenceProductName].getProductAttribute(attribute["Price"].AttributeID - 1) / product[referenceProductName].SizeUnit;
				double num3 = num2 * double.Parse(columnName) * product[productName].SizeUnit;
				changeProductAttributeCombination(productName, "Price", num3.ToString());
				num = 0;
				foreach (string item2 in GroupProducts[groupTypeName][groupTypeMember])
				{
					changeProductAttributeCombination(item2, "Price", (array2[num] * num3).ToString());
					num++;
				}
				findPredictedShares();
				for (int j = 0; j < dataTable.Rows.Count - 2; j++)
				{
					dataTable.Rows[j][columnName] = product[dataTable.Rows[j]["Name"].ToString()].NewShare / 100.0;
				}
				dataTable.Rows[dataTable.Rows.Count - 2][columnName] = getCompanyShare("NewShare") / 100.0;
				dataTable.Rows[dataTable.Rows.Count - 1][columnName] = getCompanyRevenue();
			}
			UndoRedoManager.FlushHistory();
		}
		dataTable.ExtendedProperties.Add("ChartName", "Price Index And Revenues");
		return dataTable;
	}

	public DataTable crossBrandInteractionPerProduct(ProjectCore basicScenario, Dictionary<string, ProjectCore> interactingScenarios)
	{
		DataTable dataTable = new DataTable("Brand Interaction");
		dataTable.Columns.Add("Name", typeof(string));
		using (UndoRedoManager.Start(""))
		{
			basicScenario.findPredictedShares();
			UndoRedoManager.FlushHistory();
		}
		foreach (KeyValuePair<string, ProjectCore> interactingScenario in interactingScenarios)
		{
			dataTable.Columns.Add(interactingScenario.Key, typeof(string));
			using (UndoRedoManager.Start(""))
			{
				interactingScenario.Value.findPredictedShares();
				UndoRedoManager.FlushHistory();
			}
		}
		for (int i = 0; i < product.Count; i++)
		{
			DataRow dataRow = dataTable.NewRow();
			foreach (KeyValuePair<string, ProjectCore> interactingScenario2 in interactingScenarios)
			{
				if (interactingScenario2.Value.product[i].Included)
				{
					dataRow["Name"] = product[i].ProductName;
					dataTable.Rows.Add(dataRow);
					break;
				}
			}
		}
		foreach (DataRow row in dataTable.Rows)
		{
			foreach (KeyValuePair<string, ProjectCore> interactingScenario3 in interactingScenarios)
			{
				row[interactingScenario3.Key] = (interactingScenario3.Value.getProductShare("NewShare", row["Name"].ToString()) - basicScenario.getProductShare("NewShare", row["Name"].ToString())) / 100.0;
			}
		}
		dataTable.ExtendedProperties.Add("ChartName", "Brand Interaction");
		return dataTable;
	}

	public DataTable crossBrandInteractionPerGroup(string groupTypeName, ProjectCore basicScenario, Dictionary<string, ProjectCore> interactingScenarios)
	{
		DataTable dataTable = new DataTable("Brand Interaction");
		dataTable.Columns.Add("Name", typeof(string));
		using (UndoRedoManager.Start(""))
		{
			basicScenario.findPredictedShares();
			UndoRedoManager.FlushHistory();
		}
		foreach (KeyValuePair<string, ProjectCore> interactingScenario in interactingScenarios)
		{
			dataTable.Columns.Add(interactingScenario.Key, typeof(string));
			using (UndoRedoManager.Start(""))
			{
				interactingScenario.Value.findPredictedShares();
				UndoRedoManager.FlushHistory();
			}
		}
		foreach (string item in GroupMember[groupTypeName])
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["Name"] = item;
			foreach (KeyValuePair<string, ProjectCore> interactingScenario2 in interactingScenarios)
			{
				dataRow[interactingScenario2.Key] = (interactingScenario2.Value.getGroupShare("NewShare", groupTypeName, getGroupMemberIndex(groupTypeName, item)) - basicScenario.getGroupShare("NewShare", groupTypeName, getGroupMemberIndex(groupTypeName, item))) / 100.0;
			}
			dataTable.Rows.Add(dataRow);
		}
		dataTable.ExtendedProperties.Add("ChartName", "Brand Interaction");
		return dataTable;
	}

	public DataTable crossBrandInteractionPerProduct(Dictionary<string, ProjectCore> interactingScenarios)
	{
		DataTable dataTable = new DataTable("Brand Interaction");
		dataTable.Columns.Add("Name", typeof(string));
		foreach (KeyValuePair<string, ProjectCore> interactingScenario in interactingScenarios)
		{
			dataTable.Columns.Add(interactingScenario.Key, typeof(string));
			using (UndoRedoManager.Start(""))
			{
				interactingScenario.Value.findPredictedShares();
				UndoRedoManager.FlushHistory();
			}
		}
		for (int i = 0; i < product.Count; i++)
		{
			DataRow dataRow = dataTable.NewRow();
			foreach (KeyValuePair<string, ProjectCore> interactingScenario2 in interactingScenarios)
			{
				if (interactingScenario2.Value.product[i].Included)
				{
					dataRow["Name"] = product[i].ProductName;
					dataTable.Rows.Add(dataRow);
					break;
				}
			}
		}
		foreach (DataRow row in dataTable.Rows)
		{
			foreach (KeyValuePair<string, ProjectCore> interactingScenario3 in interactingScenarios)
			{
				row[interactingScenario3.Key] = (interactingScenario3.Value.getProductShare("NewShare", row["Name"].ToString()) - getProductShare("BaseMarketShare", row["Name"].ToString())) / 100.0;
			}
		}
		dataTable.ExtendedProperties.Add("ChartName", "Brand Interaction");
		return dataTable;
	}

	public DataTable crossBrandInteractionPerGroup(string groupTypeName, Dictionary<string, ProjectCore> interactingScenarios)
	{
		DataTable dataTable = new DataTable("Brand Interaction");
		dataTable.Columns.Add("Name", typeof(string));
		foreach (KeyValuePair<string, ProjectCore> interactingScenario in interactingScenarios)
		{
			dataTable.Columns.Add(interactingScenario.Key, typeof(string));
			using (UndoRedoManager.Start(""))
			{
				interactingScenario.Value.findPredictedShares();
				UndoRedoManager.FlushHistory();
			}
		}
		foreach (string item in GroupMember[groupTypeName])
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["Name"] = item;
			foreach (KeyValuePair<string, ProjectCore> interactingScenario2 in interactingScenarios)
			{
				dataRow[interactingScenario2.Key] = (interactingScenario2.Value.getGroupShare("NewShare", groupTypeName, getGroupMemberIndex(groupTypeName, item)) - getGroupShare("BaseMarketShare", groupTypeName, getGroupMemberIndex(groupTypeName, item))) / 100.0;
			}
			dataTable.Rows.Add(dataRow);
		}
		dataTable.ExtendedProperties.Add("ChartName", "Brand Interaction");
		return dataTable;
	}

	public DataTable SoMvsSoP()
	{
		DataTable dataTable = new DataTable("SOM VS Brand Share");
		dataTable.Columns.Add("Name", typeof(string));
		dataTable.Columns.Add("SOM", typeof(string));
		dataTable.Columns.Add("Brand Share", typeof(string));
		using (UndoRedoManager.Start(""))
		{
			findPredictedShares();
			UndoRedoManager.FlushHistory();
		}
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].Included)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["Name"] = product[i].ProductName;
				dataRow["Brand Share"] = product[i].Penetration / 100.0;
				dataRow["SOM"] = product[i].NewShare / 100.0;
				dataTable.Rows.Add(dataRow);
			}
		}
		dataTable.ExtendedProperties.Add("ChartName", "SOM VS Brand Share");
		return dataTable;
	}

	public DataTable FairShareOnSKUs(string attributeName, string memberChange, ProjectCore basicScenarioProject, ProjectCore scenarioProject)
	{
		DataTable dataTable = new DataTable("Fair Share");
		dataTable.Columns.Add("Name", typeof(string));
		dataTable.Columns.Add("Index", typeof(string));
		dataTable.Columns.Add("Rebased Current Share", typeof(string));
		dataTable.Columns.Add("Rebaseto100Wot", typeof(string));
		dataTable.Columns.Add("New Share Before", typeof(string));
		dataTable.Columns.Add("New Share", typeof(string));
		dataTable.Columns.Add("Actual Share", typeof(string));
		dataTable.Columns.Add("Fair Share", typeof(string));
		using (UndoRedoManager.Start(""))
		{
			basicScenarioProject.findPredictedShares();
			scenarioProject.findPredictedShares();
			UndoRedoManager.FlushHistory();
		}
		double num = 0.0;
		double num2 = 0.0;
		for (int i = 0; i < product.Count; i++)
		{
			if (product[i].BaseMarketShare != 0.0 || product[i].NewShare != 0.0)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["Name"] = product[i].ProductName;
				if (product.ReplacedProduct)
				{
					dataRow["Rebased Current Share"] = basicScenarioProject.product[product[i].BaseProduct - 1].NewShare;
					num += basicScenarioProject.product[product[i].BaseProduct - 1].NewShare;
				}
				else
				{
					dataRow["Rebased Current Share"] = basicScenarioProject.product[i].NewShare;
					num += basicScenarioProject.product[i].NewShare;
				}
				if (product[i].Included)
				{
					dataRow["New Share Before"] = scenarioProject.product[i].NewShare;
					dataRow["New Share"] = scenarioProject.product[i].NewShare;
					num2 += scenarioProject.product[i].NewShare;
				}
				else
				{
					dataRow["New Share Before"] = 0;
					dataRow["New Share"] = 0;
				}
				dataTable.Rows.Add(dataRow);
			}
		}
		double num3 = 0.0;
		foreach (DataRow row in dataTable.Rows)
		{
			row["Rebased Current Share"] = double.Parse(row["Rebased Current Share"].ToString()) * 100.0 / num;
			row["New Share"] = double.Parse(row["New Share"].ToString()) * 100.0 / num2;
			if (row["Name"].ToString() != memberChange)
			{
				num3 += double.Parse(row["Rebased Current Share"].ToString());
			}
		}
		double num4 = 0.0;
		foreach (DataRow row2 in dataTable.Rows)
		{
			row2["Actual Share"] = Math.Abs(double.Parse(row2["New Share"].ToString()) - double.Parse(row2["Rebased Current Share"].ToString()));
			if (row2["Name"].ToString() == memberChange)
			{
				num4 = double.Parse(row2["Actual Share"].ToString());
				row2["Rebaseto100Wot"] = row2["Rebased Current Share"].ToString();
			}
			else
			{
				row2["Rebaseto100Wot"] = double.Parse(row2["Rebased Current Share"].ToString()) * 100.0 / num3;
			}
		}
		foreach (DataRow row3 in dataTable.Rows)
		{
			if (row3["Name"].ToString() != memberChange)
			{
				row3["Fair Share"] = Math.Abs(double.Parse(row3["Rebaseto100Wot"].ToString()) * num4) / 100.0;
				double num5 = double.Parse(row3["Actual Share"].ToString()) / double.Parse(row3["Fair Share"].ToString());
				if (double.IsInfinity(num5) || double.IsNaN(num5))
				{
					row3["Index"] = "";
				}
				else
				{
					row3["Index"] = num5;
				}
			}
			else
			{
				row3["Fair Share"] = 0;
				row3["Index"] = 0;
			}
		}
		dataTable.ExtendedProperties.Add("ChartName", "Fair Share");
		return dataTable;
	}

	public DataTable FairShareOnBrands(string attributeName, string groupType, string changeMember, ProjectCore basicScenarioProject, ProjectCore scenarioProject)
	{
		DataTable dataTable = new DataTable("Fair Share");
		dataTable.Columns.Add("Name", typeof(string));
		dataTable.Columns.Add("Index", typeof(string));
		dataTable.Columns.Add("Rebased Current Share", typeof(string));
		dataTable.Columns.Add("Rebaseto100Wot", typeof(string));
		dataTable.Columns.Add("New Share Before", typeof(string));
		dataTable.Columns.Add("New Share", typeof(string));
		dataTable.Columns.Add("Actual Share", typeof(string));
		dataTable.Columns.Add("Fair Share", typeof(string));
		using (UndoRedoManager.Start(""))
		{
			basicScenarioProject.findPredictedShares();
			scenarioProject.findPredictedShares();
			UndoRedoManager.FlushHistory();
		}
		double num = 0.0;
		double num2 = 0.0;
		double num3 = 0.0;
		double num4 = 0.0;
		foreach (string item in GroupMember[groupType])
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["Name"] = item;
			num3 = basicScenarioProject.getGroupShare("NewShare", groupType, getGroupMemberIndex(groupType, item));
			dataRow["Rebased Current Share"] = num3;
			num += num3;
			num4 = scenarioProject.getGroupShare("NewShare", groupType, getGroupMemberIndex(groupType, item));
			dataRow["New Share Before"] = num4;
			dataRow["New Share"] = num4;
			num2 += num4;
			dataTable.Rows.Add(dataRow);
		}
		double num5 = 0.0;
		foreach (DataRow row in dataTable.Rows)
		{
			row["Rebased Current Share"] = double.Parse(row["Rebased Current Share"].ToString()) * 100.0 / num;
			row["New Share"] = double.Parse(row["New Share"].ToString()) * 100.0 / num2;
			if (row["Name"].ToString() != changeMember)
			{
				num5 += double.Parse(row["Rebased Current Share"].ToString());
			}
		}
		double num6 = 0.0;
		foreach (DataRow row2 in dataTable.Rows)
		{
			row2["Actual Share"] = Math.Abs(double.Parse(row2["New Share"].ToString()) - double.Parse(row2["Rebased Current Share"].ToString()));
			if (row2["Name"].ToString() == changeMember)
			{
				num6 = double.Parse(row2["Actual Share"].ToString());
				row2["Rebaseto100Wot"] = row2["Rebased Current Share"].ToString();
			}
			else
			{
				row2["Rebaseto100Wot"] = double.Parse(row2["Rebased Current Share"].ToString()) * 100.0 / num5;
			}
		}
		foreach (DataRow row3 in dataTable.Rows)
		{
			if (row3["Name"].ToString() != changeMember)
			{
				row3["Fair Share"] = Math.Abs(double.Parse(row3["Rebaseto100Wot"].ToString()) * num6) / 100.0;
				double num7 = double.Parse(row3["Actual Share"].ToString()) / double.Parse(row3["Fair Share"].ToString());
				if (double.IsInfinity(num7) || double.IsNaN(num7))
				{
					row3["Index"] = "";
				}
				else
				{
					row3["Index"] = num7;
				}
			}
			else
			{
				row3["Fair Share"] = 0;
				row3["Index"] = 0;
			}
		}
		dataTable.ExtendedProperties.Add("ChartName", "Fair Share");
		return dataTable;
	}

	public DataTable exIncrementalGainLoss(string incrementalFunction, string incrementalGroupType, string incrementalGroupMember, List<string> currentLineUpProducts, List<string> incrementalProducts)
	{
		DataTable dataTable = new DataTable("EX " + incrementalFunction);
		dataTable.Columns.Add("Product Name", typeof(string));
		dataTable.Columns.Add("Revenue", typeof(string));
		dataTable.Columns.Add("Market Share", typeof(string));
		dataTable.Columns.Add("Revenue " + incrementalFunction, typeof(string));
		dataTable.Columns.Add("Market Share " + incrementalFunction, typeof(string));
		using (UndoRedoManager.Start("LOTScenario"))
		{
			DataRow dataRow = dataTable.NewRow();
			setCurrentMarketWithLineUpProducts(incrementalGroupType, incrementalGroupMember, currentLineUpProducts);
			double num = 0.0;
			double num2 = 0.0;
			try
			{
				dataRow["Product Name"] = "Current";
				num = getScenarioValue("Revenue", incrementalGroupType, incrementalGroupMember);
				num2 = getScenarioValue("Market Share", incrementalGroupType, incrementalGroupMember);
				dataRow["Revenue"] = Math.Round(num, 4);
				dataRow["Market Share"] = Math.Round(num2, 4) / 100.0;
				dataTable.Rows.Add(dataRow);
				Dictionary<string, double> dictionary = null;
				Dictionary<string, double> dictionary2 = null;
				switch (incrementalFunction)
				{
				case "Intro":
					dictionary = includeProductsToCurrentLineUp(incrementalProducts, "Revenue", incrementalGroupType, incrementalGroupMember);
					dictionary2 = includeProductsToCurrentLineUp(incrementalProducts, "Market Share", incrementalGroupType, incrementalGroupMember);
					break;
				case "Removal":
					dictionary = excludeProductsFromCurrentLineUp(incrementalProducts, "Revenue", incrementalGroupType, incrementalGroupMember);
					dictionary2 = excludeProductsFromCurrentLineUp(incrementalProducts, "Market Share", incrementalGroupType, incrementalGroupMember);
					break;
				}
				foreach (KeyValuePair<string, double> item in dictionary)
				{
					dataRow = dataTable.NewRow();
					dataRow["Product Name"] = item.Key;
					dataRow["Revenue"] = Math.Round(item.Value, 4);
					dataRow["Market Share"] = Math.Round(dictionary2[item.Key], 4) / 100.0;
					dataRow["Revenue " + incrementalFunction] = Math.Round(item.Value - num, 4);
					dataRow["Market Share " + incrementalFunction] = Math.Round(dictionary2[item.Key] - num2, 4) / 100.0;
					dataTable.Rows.Add(dataRow);
				}
				foreach (string incrementalProduct in incrementalProducts)
				{
					if (!dictionary.ContainsKey(incrementalProduct))
					{
						dataRow = dataTable.NewRow();
						dataRow["Product Name"] = incrementalProduct;
						dataRow["Revenue"] = "Reversal Effect Happened";
						dataTable.Rows.Add(dataRow);
					}
				}
			}
			catch (Exception ex)
			{
				if (string.Equals(ex.Message, "Reversal Effect will Happen"))
				{
					foreach (string incrementalProduct2 in incrementalProducts)
					{
						dataRow = dataTable.NewRow();
						dataRow["Product Name"] = incrementalProduct2;
						dataRow["Revenue"] = "Reversal Effect Happened";
						dataTable.Rows.Add(dataRow);
					}
				}
			}
			UndoRedoManager.FlushHistory();
		}
		dataTable.ExtendedProperties.Add("ChartName", "EX " + incrementalFunction);
		return dataTable;
	}

	public DataTable cumIncrementalGainLoss(string incrementalFunction, string incrementalType, string incrementalGroupType, string incrementalGroupMember, List<string> currentLineUpProducts, List<string> incrementalProducts)
	{
		DataTable dataTable = new DataTable("CUM " + incrementalFunction + " " + incrementalType);
		dataTable.Columns.Add("Product Name", typeof(string));
		dataTable.Columns.Add("Revenue", typeof(string));
		dataTable.Columns.Add("Market Share", typeof(string));
		dataTable.Columns.Add(incrementalType + " " + incrementalFunction, typeof(string));
		using (UndoRedoManager.Start("LOTScenario"))
		{
			DataRow dataRow = dataTable.NewRow();
			setCurrentMarketWithLineUpProducts(incrementalGroupType, incrementalGroupMember, currentLineUpProducts);
			double baseScenarioValue = 0.0;
			try
			{
				dataRow["Product Name"] = "Current";
				dataRow["Revenue"] = getScenarioValue("Revenue", incrementalGroupType, incrementalGroupMember);
				dataRow["Market Share"] = getScenarioValue("Market Share", incrementalGroupType, incrementalGroupMember);
				if (string.Equals(incrementalType, "Revenue"))
				{
					baseScenarioValue = double.Parse(dataRow["Revenue"].ToString());
				}
				else if (string.Equals(incrementalType, "Market Share"))
				{
					baseScenarioValue = double.Parse(dataRow["Market Share"].ToString());
				}
				dataRow["Revenue"] = Math.Round(double.Parse(dataRow["Revenue"].ToString()), 4);
				dataRow["Market Share"] = Math.Round(double.Parse(dataRow["Market Share"].ToString()), 4) / 100.0;
				dataTable.Rows.Add(dataRow);
				while (incrementalProducts.Count > 0)
				{
					string[] array = null;
					Dictionary<string, double> dictionary = null;
					switch (incrementalFunction)
					{
					case "Intro":
						dictionary = includeProductsToCurrentLineUp(incrementalProducts, incrementalType, incrementalGroupType, incrementalGroupMember);
						if (dictionary.Count == 0)
						{
							throw new Exception("Reversal Effect will Happen");
						}
						array = getHightestIncrementalProduct(incrementalFunction, incrementalProducts, dictionary, baseScenarioValue, incrementalType, incrementalGroupType, incrementalGroupMember);
						currentLineUpProducts.Add(array[0]);
						break;
					case "Removal":
						dictionary = excludeProductsFromCurrentLineUp(incrementalProducts, incrementalType, incrementalGroupType, incrementalGroupMember);
						if (dictionary.Count == 0)
						{
							throw new Exception("Reversal Effect will Happen");
						}
						array = getHightestIncrementalProduct(incrementalFunction, incrementalProducts, dictionary, baseScenarioValue, incrementalType, incrementalGroupType, incrementalGroupMember);
						currentLineUpProducts.Remove(array[0]);
						break;
					}
					incrementalProducts.Remove(array[0]);
					setCurrentMarketWithLineUpProducts(incrementalGroupType, incrementalGroupMember, currentLineUpProducts);
					dataRow = dataTable.NewRow();
					dataRow["Product Name"] = array[0];
					dataRow["Revenue"] = getScenarioValue("Revenue", incrementalGroupType, incrementalGroupMember);
					dataRow["Market Share"] = getScenarioValue("Market Share", incrementalGroupType, incrementalGroupMember);
					dataRow[incrementalType + " " + incrementalFunction] = array[1];
					if (string.Equals(incrementalType, "Revenue"))
					{
						baseScenarioValue = double.Parse(dataRow["Revenue"].ToString());
					}
					else if (string.Equals(incrementalType, "Market Share"))
					{
						baseScenarioValue = double.Parse(dataRow["Market Share"].ToString());
					}
					dataRow["Revenue"] = Math.Round(double.Parse(dataRow["Revenue"].ToString()), 4);
					dataRow["Market Share"] = Math.Round(double.Parse(dataRow["Market Share"].ToString()), 4) / 100.0;
					dataTable.Rows.Add(dataRow);
				}
			}
			catch (Exception ex)
			{
				if (string.Equals(ex.Message, "Reversal Effect will Happen"))
				{
					foreach (string incrementalProduct in incrementalProducts)
					{
						dataRow = dataTable.NewRow();
						dataRow["Product Name"] = incrementalProduct;
						dataRow["Revenue"] = "Reversal Effect Happened";
						dataTable.Rows.Add(dataRow);
					}
				}
			}
			UndoRedoManager.FlushHistory();
		}
		dataTable.ExtendedProperties.Add("ChartName", "CUM " + incrementalFunction + " " + incrementalType);
		return dataTable;
	}
}
