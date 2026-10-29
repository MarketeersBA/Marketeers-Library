using System;
using System.Data.OleDb;
using System.IO;
using System.Text;
using MarketSimulator.Model.DataAccess;
using MarketSimulator.Model.Properties;

namespace MarketSimulator.Model.Database;

public class DatabaseDesign2
{
	public DatabaseDesign2()
	{
	}

	public DatabaseDesign2(string databaseFilePath)
	{
		byte[] market_Simulator = Resources.Market_Simulator;
		FileStream fileStream = new FileStream(databaseFilePath, FileMode.Create);
		fileStream.Write(market_Simulator, 0, market_Simulator.Length);
		fileStream.Close();
	}

	private string alterProductsTableStatement(string[] baseAttribute)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("ALTER TABLE Products ADD ");
		if (baseAttribute.Length != 0)
		{
			foreach (string text in baseAttribute)
			{
				stringBuilder.Append("Base" + text + " Double,");
			}
		}
		bool flag = false;
		bool flag2 = false;
		for (int j = 0; j < baseAttribute.Length; j++)
		{
			if (string.Equals(baseAttribute[j], "Price"))
			{
				flag = true;
			}
			if (string.Equals(baseAttribute[j], "Size"))
			{
				flag2 = true;
			}
		}
		if (!flag)
		{
			stringBuilder.Append("BasePrice Double,");
		}
		if (!flag2)
		{
			stringBuilder.Append("BaseSize Double,");
		}
		stringBuilder.Append("ReplacedProduct Bit,");
		stringBuilder.Append("NewProduct Bit,");
		stringBuilder.Append("BaseMarketShare Double,");
		stringBuilder.Append("WD Double,");
		stringBuilder.Append("BaseProduct Short,");
		stringBuilder.Append("PromotionCalibration Double,");
		stringBuilder.Append("SOR Double,");
		stringBuilder.Append("ConsumptionCalibration Double,");
		stringBuilder.Append("AwarenessLevel Double,");
		stringBuilder.Append("RepeateRate Double,");
		stringBuilder.Append("NOS Double,");
		stringBuilder.Append("TDC Double");
		return stringBuilder.ToString();
	}

	private string alterConditionalAttributesList(string conditionalAttributeName)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("CREATE TABLE Conditional" + conditionalAttributeName + "List(");
		stringBuilder.Append("ProductID Short,");
		stringBuilder.Append("ProductCombination Memo,");
		stringBuilder.Append("PRIMARY KEY (ProductID))");
		return stringBuilder.ToString();
	}

	private string alterHBUDataTableStatement(string[] LevelsName)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("ALTER TABLE HBUData ADD ");
		if (LevelsName.Length != 0)
		{
			foreach (string text in LevelsName)
			{
				stringBuilder = stringBuilder.Append(text.Replace(" ", "") + " Text,");
			}
			stringBuilder.Remove(stringBuilder.Length - 1, 1);
		}
		return stringBuilder.ToString();
	}

	private string createProductsGroupingTableStatement(string[] groupTypes)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("CREATE TABLE ProductsGrouping(");
		stringBuilder.Append("ProductID Short,");
		foreach (string text in groupTypes)
		{
			stringBuilder.Append("[" + text + "] Short,");
		}
		stringBuilder.Append("PRIMARY KEY (ProductID))");
		return stringBuilder.ToString();
	}

	private string createRespondentSegmentingTableStatement(string[] SegmentTypes)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("CREATE TABLE RespondentSegmentation(");
		stringBuilder.Append("RespondentID Short,");
		foreach (string text in SegmentTypes)
		{
			stringBuilder.Append(text + " Short,");
		}
		stringBuilder.Append("PRIMARY KEY (RespondentID))");
		return stringBuilder.ToString();
	}

	internal string executeCommand(string[] baseAttribute, string[] conditionalAtrributes, string[] AttributeLevelsName, string[] groupTypes, string[] segementTypes)
	{
		OleDbConnection oleDbConnection = new OleDbConnection();
		oleDbConnection.ConnectionString = DataTransaction.InformationTransfer.ConnectionString;
		try
		{
			oleDbConnection.Open();
			OleDbCommand oleDbCommand = new OleDbCommand();
			oleDbCommand.Connection = oleDbConnection;
			oleDbCommand.CommandText = alterProductsTableStatement(baseAttribute);
			oleDbCommand.ExecuteNonQuery();
			oleDbCommand.CommandText = alterHBUDataTableStatement(AttributeLevelsName);
			oleDbCommand.ExecuteNonQuery();
			foreach (string conditionalAttributeName in conditionalAtrributes)
			{
				oleDbCommand.CommandText = alterConditionalAttributesList(conditionalAttributeName);
				oleDbCommand.ExecuteNonQuery();
			}
			if (groupTypes.Length != 0)
			{
				oleDbCommand.CommandText = createProductsGroupingTableStatement(groupTypes);
				oleDbCommand.ExecuteNonQuery();
			}
			if (segementTypes.Length != 0)
			{
				oleDbCommand.CommandText = createRespondentSegmentingTableStatement(segementTypes);
				oleDbCommand.ExecuteNonQuery();
			}
		}
		catch (Exception ex)
		{
			return ex.ToString();
		}
		finally
		{
			oleDbConnection.Close();
		}
		return true.ToString();
	}
}
