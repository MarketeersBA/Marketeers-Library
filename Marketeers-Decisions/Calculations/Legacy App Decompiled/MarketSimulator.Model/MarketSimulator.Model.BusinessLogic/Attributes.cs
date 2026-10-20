using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using MarketSimulator.Model.DataAccess;

namespace MarketSimulator.Model.BusinessLogic;

[Serializable]
internal class Attributes : IEnumerable
{
	private int attributeID;

	private string attributeName;

	private bool isDicrete;

	private bool isConditional;

	private int attributeLevelCount;

	private bool attributeVisible;

	private bool attributeIncluded;

	private string attributeBaseName;

	private string[] attributeLevelsName;

	private double[,] conditionalAttributeListValues;

	private double[,] attributeUtility;

	private Dictionary<string, Attributes> attributeString = new Dictionary<string, Attributes>();

	private Dictionary<int, Attributes> attributeIndex = new Dictionary<int, Attributes>();

	public int AttributeID
	{
		get
		{
			return attributeID;
		}
		set
		{
			attributeID = value;
		}
	}

	public string AttributeName
	{
		get
		{
			return attributeName;
		}
		set
		{
			attributeName = value;
		}
	}

	public bool IsDicrete
	{
		get
		{
			return isDicrete;
		}
		set
		{
			isDicrete = value;
		}
	}

	public bool IsConditional
	{
		get
		{
			return isConditional;
		}
		set
		{
			isConditional = value;
		}
	}

	public int AttributeLevelCount
	{
		get
		{
			return attributeLevelCount;
		}
		set
		{
			attributeLevelCount = value;
		}
	}

	public bool AttributeVisible
	{
		get
		{
			return attributeVisible;
		}
		set
		{
			attributeVisible = value;
		}
	}

	public bool AttributeIncluded
	{
		get
		{
			return attributeIncluded;
		}
		set
		{
			attributeIncluded = value;
		}
	}

	public string AttributeBaseName
	{
		get
		{
			return attributeBaseName;
		}
		set
		{
			attributeBaseName = value;
		}
	}

	public string[] AttributeLevelsName
	{
		get
		{
			return attributeLevelsName;
		}
		set
		{
			attributeLevelsName = value;
		}
	}

	public double[,] ConditionalAttributeListValues
	{
		get
		{
			return conditionalAttributeListValues;
		}
		set
		{
			conditionalAttributeListValues = value;
		}
	}

	public double[,] AttributeUtility
	{
		get
		{
			return attributeUtility;
		}
		set
		{
			attributeUtility = value;
		}
	}

	public Attributes this[string name]
	{
		get
		{
			return attributeString[name];
		}
		set
		{
			attributeString[name] = value;
			attributeIndex[attributeString.Count - 1] = value;
		}
	}

	public Attributes this[int index]
	{
		get
		{
			return attributeIndex[index];
		}
		set
		{
			attributeIndex[index] = value;
		}
	}

	public int Count => attributeString.Count;

	IEnumerator IEnumerable.GetEnumerator()
	{
		return attributeString.GetEnumerator();
	}

	public void clearAttribute()
	{
		attributeString.Clear();
		attributeIndex.Clear();
	}

	public bool Contains(string searchAttributeName)
	{
		if (attributeString.ContainsKey(searchAttributeName))
		{
			return true;
		}
		return false;
	}

	internal void getDataRow(int index)
	{
		DataRow dataRow = DataTransaction.InformationTransfer.DtAttributes.Rows[index];
		attributeID = int.Parse(dataRow["AttributeID"].ToString());
		attributeName = dataRow["AttributeName"].ToString();
		isDicrete = bool.Parse(dataRow["IsDiscrete"].ToString());
		isConditional = bool.Parse(dataRow["IsConditional"].ToString());
		attributeLevelCount = int.Parse(dataRow["AttributeLevelCount"].ToString());
		attributeLevelsName = getAttributeLevelNamesArray(dataRow["AttributeLevelsName"].ToString());
		attributeBaseName = dataRow["AttributeBaseName"].ToString();
		attributeVisible = bool.Parse(dataRow["IsVisible"].ToString());
		attributeIncluded = bool.Parse(dataRow["IsIncluded"].ToString());
		DataRowCollection rows = DataTransaction.InformationTransfer.DtHBUData.Rows;
		int num = 0;
		int num2 = 0;
		attributeUtility = new double[rows.Count, attributeLevelsName.Length];
		foreach (DataRow item in rows)
		{
			int num3 = 1;
			string[] array = attributeLevelsName;
			foreach (string text in array)
			{
				if (attributeName == "SKU")
				{
					attributeUtility[num, num2] = double.Parse(item["ProductID" + text].ToString());
				}
				else
				{
					attributeUtility[num, num2] = double.Parse(item[attributeName + "Level" + num3].ToString());
					num3++;
				}
				num2++;
			}
			num2 = 0;
			num++;
		}
		if (!isConditional)
		{
			return;
		}
		DataRowCollection rows2 = DataTransaction.InformationTransfer.DtConditionalAttribute[AttributeName].Rows;
		conditionalAttributeListValues = new double[rows2.Count, attributeLevelsName.Length];
		num = 0;
		foreach (DataRow item2 in rows2)
		{
			string[] array2 = item2["ProductCombination"].ToString().Split(new string[1] { "," }, StringSplitOptions.RemoveEmptyEntries);
			for (int j = 0; j < array2.Length; j++)
			{
				conditionalAttributeListValues[num, j] = double.Parse(array2[j]);
			}
			num++;
		}
	}

	private string setAttributeLevelNameString(string[] attributeLevelsName)
	{
		string text = "";
		foreach (string text2 in attributeLevelsName)
		{
			text = text + text2 + ",";
		}
		return text.Remove(text.LastIndexOf(','));
	}

	private string[] getAttributeLevelNamesArray(string attributeLevelName)
	{
		return attributeLevelName.Split(new string[1] { "," }, StringSplitOptions.RemoveEmptyEntries);
	}

	internal double getAttributeLevelUtility(int respondentIndex, int productIndex, double baseProductAttributeValue)
	{
		double num = 0.0;
		double num2 = 0.0;
		double num3 = 0.0;
		double num4 = 0.0;
		if (isDicrete)
		{
			return attributeUtility[respondentIndex, (int)baseProductAttributeValue - 1];
		}
		if (isConditional)
		{
			for (int i = 0; i < attributeLevelCount; i++)
			{
				num = conditionalAttributeListValues[productIndex, i];
				if (num == baseProductAttributeValue)
				{
					return attributeUtility[respondentIndex, i];
				}
				if (baseProductAttributeValue < num)
				{
					if (i > 0)
					{
						num2 = conditionalAttributeListValues[productIndex, i - 1];
						num3 = attributeUtility[respondentIndex, i];
						num4 = attributeUtility[respondentIndex, i - 1];
						return interpolateUtiltiy(num2, num, baseProductAttributeValue, num4, num3);
					}
					return double.NaN;
				}
			}
		}
		return double.NaN;
	}

	private double interpolateUtiltiy(double previousLevelValue, double nextLevelValue, double baseProductAttributeValue, double previousUtility, double nextUtility)
	{
		return nextUtility - (nextLevelValue - baseProductAttributeValue) * (nextUtility - previousUtility) / (nextLevelValue - previousLevelValue);
	}
}
