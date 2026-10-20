using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace MarketSimulator.Model.DataAccess;

internal class DataTransaction
{
	private static DataTransaction informationTransfer;

	private DataSet marketSimulatorDataSet;

	private OleDbDataAdapter daAttributes;

	private DataTable dtAttributes;

	private OleDbDataAdapter daProducts;

	private DataTable dtProducts;

	private OleDbDataAdapter daSemgmentsShares;

	private DataTable dtSegmentsShares;

	private OleDbDataAdapter daSemgmentsPrice;

	private DataTable dtSegmentsPrice;

	private OleDbDataAdapter daGroupsExcludedSalesUnit;

	private DataTable dtGroupsExcludedSalesUnit;

	private OleDbDataAdapter daSegmentsWD;

	private DataTable dtSegmentsWD;

	private OleDbDataAdapter daSegmentsMSU;

	private DataTable dtSegmentsMSU;

	private OleDbDataAdapter daSegmentsVSU;

	private DataTable dtSegmentsVSU;

	private OleDbDataAdapter daModule;

	private DataTable dtModule;

	private OleDbDataAdapter daHBUData;

	private DataTable dtHBUData;

	private OleDbDataAdapter daGroups;

	private DataTable dtGroups;

	private OleDbDataAdapter daGroupingTypes;

	private DataTable dtGroupingTypes;

	private OleDbDataAdapter daProductsGrouping;

	private DataTable dtProductsGrouping;

	private OleDbDataAdapter daSegmentingTypes;

	private DataTable dtSegmentingTypes;

	private OleDbDataAdapter daSegments;

	private DataTable dtSegments;

	private OleDbDataAdapter daRespondentSegmentation;

	private DataTable dtRespondentSegmentation;

	private OleDbDataAdapter[] daConditionalAttributeArray;

	private Dictionary<string, DataTable> dtConditionalAttribute;

	private string connectionString;

	internal string ConnectionString
	{
		get
		{
			return connectionString;
		}
		set
		{
			connectionString = value;
		}
	}

	internal static DataTransaction InformationTransfer
	{
		get
		{
			if (informationTransfer == null)
			{
				informationTransfer = new DataTransaction();
			}
			return informationTransfer;
		}
		set
		{
			informationTransfer = value;
		}
	}

	internal DataSet MarketSimulatorDataSet
	{
		get
		{
			if (marketSimulatorDataSet == null)
			{
				marketSimulatorDataSet = new DataSet("MarketSimulatorDataSet");
			}
			return marketSimulatorDataSet;
		}
		set
		{
			marketSimulatorDataSet = value;
		}
	}

	internal DataTable DtAttributes
	{
		get
		{
			return dtAttributes;
		}
		set
		{
			dtAttributes = value;
		}
	}

	internal DataTable DtProducts
	{
		get
		{
			return dtProducts;
		}
		set
		{
			dtProducts = value;
		}
	}

	internal DataTable DtSegmentsShares
	{
		get
		{
			return dtSegmentsShares;
		}
		set
		{
			dtSegmentsShares = value;
		}
	}

	internal DataTable DtSegmentsPrice
	{
		get
		{
			return dtSegmentsPrice;
		}
		set
		{
			dtSegmentsPrice = value;
		}
	}

	internal DataTable DtSegmentsWD
	{
		get
		{
			return dtSegmentsWD;
		}
		set
		{
			dtSegmentsWD = value;
		}
	}

	internal DataTable DtGroupsExcludedSalesUnit
	{
		get
		{
			return dtGroupsExcludedSalesUnit;
		}
		set
		{
			dtGroupsExcludedSalesUnit = value;
		}
	}

	internal DataTable DtSegmentsMSU
	{
		get
		{
			return dtSegmentsMSU;
		}
		set
		{
			dtSegmentsMSU = value;
		}
	}

	internal DataTable DtSegmentsVSU
	{
		get
		{
			return dtSegmentsVSU;
		}
		set
		{
			dtSegmentsVSU = value;
		}
	}

	internal DataTable DtModule
	{
		get
		{
			return dtModule;
		}
		set
		{
			dtModule = value;
		}
	}

	internal DataTable DtHBUData
	{
		get
		{
			return dtHBUData;
		}
		set
		{
			dtHBUData = value;
		}
	}

	public DataTable DtGroupingTypes
	{
		get
		{
			return dtGroupingTypes;
		}
		set
		{
			dtGroupingTypes = value;
		}
	}

	internal DataTable DtGroups
	{
		get
		{
			return dtGroups;
		}
		set
		{
			dtGroups = value;
		}
	}

	internal DataTable DtProductsGrouping
	{
		get
		{
			return dtProductsGrouping;
		}
		set
		{
			dtProductsGrouping = value;
		}
	}

	public DataTable DtSegmentingTypes
	{
		get
		{
			return dtSegmentingTypes;
		}
		set
		{
			dtSegmentingTypes = value;
		}
	}

	internal DataTable DtSegments
	{
		get
		{
			return dtSegments;
		}
		set
		{
			dtSegments = value;
		}
	}

	internal DataTable DtRespondentSegmentation
	{
		get
		{
			return dtRespondentSegmentation;
		}
		set
		{
			dtRespondentSegmentation = value;
		}
	}

	public Dictionary<string, DataTable> DtConditionalAttribute
	{
		get
		{
			return dtConditionalAttribute;
		}
		set
		{
			dtConditionalAttribute = value;
		}
	}

	internal DataTransaction()
	{
	}

	private DataTable FillSchema(string tableName, string sortColumn, ref OleDbDataAdapter adapter)
	{
		DataTable dataTable = new DataTable(tableName);
		OleDbConnection oleDbConnection = new OleDbConnection(ConnectionString);
		try
		{
			oleDbConnection.Open();
			adapter = new OleDbDataAdapter(new OleDbCommand("Select * from [" + tableName + "] order by " + sortColumn, oleDbConnection));
			OleDbCommandBuilder oleDbCommandBuilder = new OleDbCommandBuilder(adapter);
			adapter.FillSchema(dataTable, SchemaType.Source);
			adapter.Fill(dataTable);
			MarketSimulatorDataSet.Tables.Add(dataTable);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
		finally
		{
			oleDbConnection.Close();
		}
		return dataTable;
	}

	internal void initializeDataTable()
	{
		OleDbConnection oleDbConnection = new OleDbConnection(ConnectionString);
		try
		{
			oleDbConnection.Open();
			MarketSimulatorDataSet = new DataSet();
			DtAttributes = FillSchema("Attributes", "AttributeID", ref daAttributes);
			DtProducts = FillSchema("Products", "ProductID", ref daProducts);
			DtSegmentsShares = FillSchema("SegmentsShares", "ProductID", ref daSemgmentsShares);
			DtSegmentsPrice = FillSchema("SegmentsPrice", "ProductID", ref daSemgmentsPrice);
			DtSegmentsWD = FillSchema("SegmentsWD", "ProductID", ref daSegmentsWD);
			DtGroupsExcludedSalesUnit = FillSchema("GroupsExcludedSalesUnit", "GroupTypeID", ref daGroupsExcludedSalesUnit);
			DtSegmentsMSU = FillSchema("SegmentsMSU", "ID", ref daSegmentsMSU);
			DtSegmentsVSU = FillSchema("SegmentsVSU", "ID", ref daSegmentsVSU);
			DtModule = FillSchema("Study", "StudyID", ref daModule);
			DtHBUData = FillSchema("HBUData", "RespondentID", ref daHBUData);
			ArrayList arrayList = new ArrayList();
			foreach (DataRow row in DtAttributes.Rows)
			{
				if (bool.Parse(row["IsConditional"].ToString()))
				{
					arrayList.Add(row["AttributeName"]);
				}
			}
			if (arrayList.Count > 0)
			{
				dtConditionalAttribute = new Dictionary<string, DataTable>();
				daConditionalAttributeArray = new OleDbDataAdapter[arrayList.Count];
				for (int i = 0; i < arrayList.Count; i++)
				{
					dtConditionalAttribute.Add(arrayList[i].ToString(), FillSchema("Conditional" + arrayList[i].ToString() + "List", "ProductID", ref daConditionalAttributeArray[i]));
				}
			}
			if (bool.Parse(DtModule.Rows[0]["UseGrouping"].ToString()))
			{
				DtGroupingTypes = FillSchema("GroupingTypes", "TypeID", ref daGroupingTypes);
				DtGroups = FillSchema("Groups", "GroupID", ref daGroups);
				DtProductsGrouping = FillSchema("ProductsGrouping", "ProductID", ref daProductsGrouping);
			}
			if (bool.Parse(DtModule.Rows[0]["UseSegmenting"].ToString()))
			{
				DtSegmentingTypes = FillSchema("SegmentingTypes", "TypeID", ref daSegmentingTypes);
				DtSegments = FillSchema("Segments", "SegmentID", ref daSegments);
				DtRespondentSegmentation = FillSchema("RespondentSegmentation", "RespondentID", ref daRespondentSegmentation);
			}
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
		finally
		{
			oleDbConnection.Close();
		}
	}

	internal void clearDataset()
	{
		if (marketSimulatorDataSet == null || marketSimulatorDataSet.Tables.Count == 0)
		{
			return;
		}
		foreach (DataTable table in marketSimulatorDataSet.Tables)
		{
			table.Rows.Clear();
		}
	}

	internal void initializeDataTable(string[] conditionalAttributeArray, bool useGrouping, bool useSegmenting)
	{
		OleDbConnection oleDbConnection = new OleDbConnection(ConnectionString);
		try
		{
			oleDbConnection.Open();
			MarketSimulatorDataSet = new DataSet();
			DtAttributes = FillSchema("Attributes", "AttributeID", ref daAttributes);
			DtProducts = FillSchema("Products", "ProductID", ref daProducts);
			DtSegmentsShares = FillSchema("SegmentsShares", "ProductID", ref daSemgmentsShares);
			DtSegmentsPrice = FillSchema("SegmentsPrice", "ProductID", ref daSemgmentsPrice);
			DtSegmentsWD = FillSchema("SegmentsWD", "ProductID", ref daSegmentsWD);
			DtGroupsExcludedSalesUnit = FillSchema("GroupsExcludedSalesUnit", "GroupTypeID", ref daGroupsExcludedSalesUnit);
			DtSegmentsMSU = FillSchema("SegmentsMSU", "ID", ref daSegmentsMSU);
			DtSegmentsVSU = FillSchema("SegmentsVSU", "ID", ref daSegmentsVSU);
			DtModule = FillSchema("Study", "StudyID", ref daModule);
			DtHBUData = FillSchema("HBUData", "RespondentID", ref daHBUData);
			if (conditionalAttributeArray.Length > 0)
			{
				dtConditionalAttribute = new Dictionary<string, DataTable>();
				daConditionalAttributeArray = new OleDbDataAdapter[conditionalAttributeArray.Length];
				dtConditionalAttribute.Clear();
				for (int i = 0; i < conditionalAttributeArray.Length; i++)
				{
					dtConditionalAttribute.Add(conditionalAttributeArray[i].ToString(), FillSchema("Conditional" + conditionalAttributeArray[i] + "List", "ProductID", ref daConditionalAttributeArray[i]));
				}
			}
			if (useGrouping)
			{
				DtGroupingTypes = FillSchema("GroupingTypes", "TypeID", ref daGroupingTypes);
				DtGroups = FillSchema("Groups", "GroupID", ref daGroups);
				DtProductsGrouping = FillSchema("ProductsGrouping", "ProductID", ref daProductsGrouping);
			}
			if (useSegmenting)
			{
				DtSegmentingTypes = FillSchema("SegmentingTypes", "TypeID", ref daSegmentingTypes);
				DtSegments = FillSchema("Segments", "SegmentID", ref daSegments);
				DtRespondentSegmentation = FillSchema("RespondentSegmentation", "RespondentID", ref daRespondentSegmentation);
			}
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
		finally
		{
			oleDbConnection.Close();
		}
	}

	internal DataTable getTableSchema(string tableName)
	{
		switch (tableName)
		{
		case "Attributes":
			return DtAttributes;
		case "Products":
			return DtProducts;
		case "SegmentsShares":
			return DtSegmentsShares;
		case "SegmentsPrice":
			return DtSegmentsPrice;
		case "SegmentsWD":
			return DtSegmentsWD;
		case "GroupsExcludedSalesUnit":
			return DtGroupsExcludedSalesUnit;
		case "SegmentsMSU":
			return DtSegmentsMSU;
		case "SegmentsVSU":
			return DtSegmentsVSU;
		case "Study":
			return DtModule;
		case "HBUData":
			return DtHBUData;
		case "GroupingTypes":
			return DtGroupingTypes;
		case "Groups":
			return DtGroups;
		case "ProductsGrouping":
			return DtProductsGrouping;
		case "SegmentingTypes":
			return DtSegmentingTypes;
		case "Segments":
			return DtSegments;
		case "RespondentSegmentation":
			return DtRespondentSegmentation;
		default:
			foreach (KeyValuePair<string, DataTable> item in dtConditionalAttribute)
			{
				if (item.Value.TableName == tableName)
				{
					return item.Value;
				}
			}
			return null;
		}
	}

	internal void updateMarketSimulatorDataSet(string[] conditionalAttributeArray, bool useGrouping, bool useSegmenting)
	{
		OleDbConnection oleDbConnection = new OleDbConnection(ConnectionString);
		try
		{
			oleDbConnection.Open();
			daAttributes.Update(MarketSimulatorDataSet, "Attributes");
			daHBUData.Update(MarketSimulatorDataSet, "HBUData");
			daModule.Update(MarketSimulatorDataSet, "Study");
			daProducts.Update(MarketSimulatorDataSet, "Products");
			daSemgmentsShares.Update(MarketSimulatorDataSet, "SegmentsShares");
			daSemgmentsPrice.Update(MarketSimulatorDataSet, "SegmentsPrice");
			daSegmentsWD.Update(MarketSimulatorDataSet, "SegmentsWD");
			daGroupsExcludedSalesUnit.Update(marketSimulatorDataSet, "GroupsExcludedSalesUnit");
			daSegmentsMSU.Update(marketSimulatorDataSet, "SegmentsMSU");
			daSegmentsVSU.Update(marketSimulatorDataSet, "SegmentsVSU");
			if (conditionalAttributeArray.Length > 0)
			{
				for (int i = 0; i < conditionalAttributeArray.Length; i++)
				{
					daConditionalAttributeArray[i].Update(marketSimulatorDataSet, "Conditional" + conditionalAttributeArray[i] + "List");
				}
			}
			if (useGrouping)
			{
				daGroupingTypes.Update(MarketSimulatorDataSet, "GroupingTypes");
				daGroups.Update(MarketSimulatorDataSet, "Groups");
				daProductsGrouping.Update(MarketSimulatorDataSet, "ProductsGrouping");
			}
			if (useSegmenting)
			{
				daSegmentingTypes.Update(MarketSimulatorDataSet, "SegmentingTypes");
				daSegments.Update(MarketSimulatorDataSet, "Segments");
				daRespondentSegmentation.Update(MarketSimulatorDataSet, "RespondentSegmentation");
			}
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
		finally
		{
			oleDbConnection.Close();
		}
	}

	internal void updateFinancial()
	{
		foreach (DataRow row in dtProducts.Rows)
		{
			daProducts.UpdateCommand = new OleDbCommand("UPDATE Products SET NOS = " + row["NOS"].ToString() + ", TDC = " + row["TDC"].ToString() + " Where ProductID = " + row["ProductID"].ToString());
			daProducts.Update(dtProducts);
		}
		daModule.UpdateCommand = new OleDbCommand("UPDATE Study SET Financial=" + dtModule.Rows[0]["Financial"].ToString() + " Where StudyID = 0");
		daModule.Update(dtModule);
	}
}
