using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using MarketSimulator.Model.DataAccess;
using UndoRedoFramework;

namespace MarketSimulator.Model.BusinessLogic;

[Serializable]
internal class Products : IEnumerable
{
	private int productID;

	private string productName;

	private string productType;

	private double currentShare;

	private double baseMarketShare;

	private double newShare;

	private double diffVolumeShare;

	private double originalWD;

	private double currentPreferenceShare;

	private double basePreferenceShare;

	private double newPreferenceShare;

	private double penetration;

	private double basePenetration;

	private double consumptionCalibration;

	private double promotionCalibration;

	private double awarenessLevel;

	private double repeateRate;

	private double sor;

	private int baseProduct;

	private bool isCompany;

	private bool baseIncluded;

	private bool newProduct;

	private bool replacedProduct;

	private double sizeUnit;

	private double priceUnit;

	private double revenue;

	private double baseRevenue;

	private double valueSalesUnit;

	private double baseValueSalesUnit;

	private double valueShare;

	private double baseValueShare;

	private readonly UndoRedo<double> newWD = new UndoRedo<double>();

	private readonly UndoRedo<double> nos = new UndoRedo<double>();

	private readonly UndoRedo<double> tdc = new UndoRedo<double>();

	private readonly UndoRedo<bool> included = new UndoRedo<bool>();

	private readonly UndoRedo<double> auditShare = new UndoRedo<double>();

	private readonly UndoRedo<double[]> productCombination = new UndoRedo<double[]>();

	private double[] defaultProductCombination;

	private Dictionary<string, Products> productString = new Dictionary<string, Products>();

	private Dictionary<int, Products> productIndex = new Dictionary<int, Products>();

	internal int ProductID
	{
		get
		{
			return productID;
		}
		set
		{
			productID = value;
		}
	}

	public string ProductName
	{
		get
		{
			return productName;
		}
		set
		{
			productName = value;
		}
	}

	public string ProductType
	{
		get
		{
			return productType;
		}
		set
		{
			productType = value;
		}
	}

	public double BaseMarketShare
	{
		get
		{
			return baseMarketShare;
		}
		set
		{
			baseMarketShare = value;
		}
	}

	public double NewShare
	{
		get
		{
			return newShare;
		}
		set
		{
			newShare = value;
		}
	}

	public double DiffVolumeShare
	{
		get
		{
			return diffVolumeShare;
		}
		set
		{
			diffVolumeShare = value;
		}
	}

	public double CurrentShare
	{
		get
		{
			return currentShare;
		}
		set
		{
			currentShare = value;
		}
	}

	public double CurrentPreferenceShare
	{
		get
		{
			return currentPreferenceShare;
		}
		set
		{
			currentPreferenceShare = value;
		}
	}

	public double OriginalWD
	{
		get
		{
			return originalWD;
		}
		set
		{
			originalWD = value;
		}
	}

	public double NewWD
	{
		get
		{
			return newWD.Value;
		}
		set
		{
			newWD.Value = value;
		}
	}

	public double AuditShare
	{
		get
		{
			return auditShare.Value;
		}
		set
		{
			auditShare.Value = value;
		}
	}

	public double BasePreferenceShare
	{
		get
		{
			return basePreferenceShare;
		}
		set
		{
			basePreferenceShare = value;
		}
	}

	public double NewPreferenceShare
	{
		get
		{
			return newPreferenceShare;
		}
		set
		{
			newPreferenceShare = value;
		}
	}

	public double Penetration
	{
		get
		{
			return penetration;
		}
		set
		{
			penetration = value;
		}
	}

	public double BasePenetration
	{
		get
		{
			return basePenetration;
		}
		set
		{
			basePenetration = value;
		}
	}

	public double ConsumptionCalibration
	{
		get
		{
			return consumptionCalibration;
		}
		set
		{
			consumptionCalibration = value;
		}
	}

	public double PromotionCalibration
	{
		get
		{
			return promotionCalibration;
		}
		set
		{
			promotionCalibration = value;
		}
	}

	public double AwarenessLevel
	{
		get
		{
			return awarenessLevel;
		}
		set
		{
			awarenessLevel = value;
		}
	}

	public double RepeateRate
	{
		get
		{
			return repeateRate;
		}
		set
		{
			repeateRate = value;
		}
	}

	public double SOR
	{
		get
		{
			return sor;
		}
		set
		{
			sor = value;
		}
	}

	public double NOS
	{
		get
		{
			return nos.Value;
		}
		set
		{
			nos.Value = value;
		}
	}

	public double TDC
	{
		get
		{
			return tdc.Value;
		}
		set
		{
			tdc.Value = value;
		}
	}

	public double SizeUnit
	{
		get
		{
			return sizeUnit;
		}
		set
		{
			sizeUnit = value;
		}
	}

	public double PriceUnit
	{
		get
		{
			return priceUnit;
		}
		set
		{
			priceUnit = value;
		}
	}

	public double[] DefaultProductCombination => defaultProductCombination;

	public double[] ProductCombination
	{
		get
		{
			return productCombination.Value;
		}
		set
		{
			productCombination.Value = value;
		}
	}

	public int BaseProduct
	{
		get
		{
			return baseProduct;
		}
		set
		{
			baseProduct = value;
		}
	}

	public bool IsCompany
	{
		get
		{
			return isCompany;
		}
		set
		{
			isCompany = value;
		}
	}

	public bool Included
	{
		get
		{
			return included.Value;
		}
		set
		{
			included.Value = value;
		}
	}

	public bool NewProduct
	{
		get
		{
			return newProduct;
		}
		set
		{
			newProduct = value;
		}
	}

	public bool ReplacedProduct
	{
		get
		{
			return replacedProduct;
		}
		set
		{
			replacedProduct = value;
		}
	}

	public bool BaseIncluded
	{
		get
		{
			return baseIncluded;
		}
		set
		{
			baseIncluded = value;
		}
	}

	public double Revenue
	{
		get
		{
			return revenue;
		}
		set
		{
			revenue = value;
		}
	}

	public double BaseRevenue
	{
		get
		{
			return baseRevenue;
		}
		set
		{
			baseRevenue = value;
		}
	}

	public double ValueSalesUnit
	{
		get
		{
			return valueSalesUnit;
		}
		set
		{
			valueSalesUnit = value;
		}
	}

	public double BaseValueSalesUnit
	{
		get
		{
			return baseValueSalesUnit;
		}
		set
		{
			baseValueSalesUnit = value;
		}
	}

	public double ValueShare
	{
		get
		{
			return valueShare;
		}
		set
		{
			valueShare = value;
		}
	}

	public double BaseValueShare
	{
		get
		{
			return baseValueShare;
		}
		set
		{
			baseValueShare = value;
		}
	}

	public Products this[string productName]
	{
		get
		{
			return productString[productName];
		}
		set
		{
			productString[productName] = value;
			productIndex[productString.Count - 1] = value;
		}
	}

	public Products this[int index]
	{
		get
		{
			return productIndex[index];
		}
		set
		{
			productIndex[index] = value;
		}
	}

	public int Count => productString.Count;

	IEnumerator IEnumerable.GetEnumerator()
	{
		return productString.GetEnumerator();
	}

	public void clearProducts()
	{
		productString.Clear();
		productIndex.Clear();
	}

	public Products(int attributesCount)
	{
		ProductCombination = new double[attributesCount];
		defaultProductCombination = new double[attributesCount];
	}

	internal void getDataRow(int index)
	{
		DataRow dataRow = DataTransaction.InformationTransfer.DtProducts.Rows[index];
		productID = int.Parse(dataRow["ProductID"].ToString());
		productName = dataRow["ProductName"].ToString();
		isCompany = bool.Parse(dataRow["Company"].ToString());
		bool flag = (Included = bool.Parse(dataRow["Included"].ToString()));
		baseIncluded = flag;
		for (int i = 0; i < ProductCombination.Length; i++)
		{
			ProductCombination[i] = double.Parse(dataRow["Base" + getAttributeName(i)].ToString());
			defaultProductCombination[i] = double.Parse(dataRow["Base" + getAttributeName(i)].ToString());
		}
		replacedProduct = bool.Parse(dataRow["ReplacedProduct"].ToString());
		newProduct = bool.Parse(dataRow["NewProduct"].ToString());
		BaseMarketShare = double.Parse(dataRow["BaseMarketShare"].ToString());
		originalWD = double.Parse(dataRow["WD"].ToString());
		NewWD = double.Parse(dataRow["WD"].ToString());
		baseProduct = int.Parse(dataRow["BaseProduct"].ToString());
		promotionCalibration = double.Parse(dataRow["PromotionCalibration"].ToString());
		sor = double.Parse(dataRow["SOR"].ToString());
		consumptionCalibration = double.Parse(dataRow["ConsumptionCalibration"].ToString());
		awarenessLevel = double.Parse(dataRow["AwarenessLevel"].ToString());
		repeateRate = double.Parse(dataRow["RepeateRate"].ToString());
		priceUnit = double.Parse(dataRow["BasePrice"].ToString());
		sizeUnit = double.Parse(dataRow["BaseSize"].ToString());
		productType = "ExistingProduct";
		if (BaseMarketShare == 0.0)
		{
			if (!Included && baseProduct > 0)
			{
				productType = "ReplacedProduct";
			}
			else if (!Included && baseProduct == 0)
			{
				productType = "NewProduct";
			}
		}
		try
		{
			if (dataRow["NOS"].ToString() != "")
			{
				NOS = double.Parse(dataRow["NOS"].ToString());
			}
			if (dataRow["TDC"].ToString() != "")
			{
				TDC = double.Parse(dataRow["TDC"].ToString());
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message.ToString());
		}
	}

	internal void setDataRow(int index)
	{
		DataRow dataRow = DataTransaction.InformationTransfer.DtProducts.Rows[index];
		dataRow["ProductID"] = productID.ToString();
		dataRow["ProductName"] = productName;
		dataRow["Company"] = isCompany.ToString();
		dataRow["Included"] = Included.ToString();
		for (int i = 0; i < ProductCombination.Length; i++)
		{
			dataRow["Base" + getAttributeName(i)] = ProductCombination[i].ToString();
		}
		dataRow["ReplacedProduct"] = replacedProduct.ToString();
		dataRow["NewProduct"] = newProduct.ToString();
		dataRow["BaseMarketShare"] = BaseMarketShare.ToString();
		dataRow["WD"] = originalWD.ToString();
		dataRow["BaseProduct"] = baseProduct.ToString();
		dataRow["PromotionCalibration"] = promotionCalibration.ToString();
		dataRow["SOR"] = sor.ToString();
		dataRow["ConsumptionCalibration"] = consumptionCalibration.ToString();
		dataRow["AwarenessLevel"] = awarenessLevel.ToString();
		dataRow["RepeateRate"] = repeateRate.ToString();
		dataRow["NOS"] = NOS.ToString();
		dataRow["TDC"] = TDC.ToString();
	}

	private string getAttributeName(int index)
	{
		return DataTransaction.InformationTransfer.DtAttributes.Rows[index]["AttributeName"].ToString();
	}

	internal double getProductAttribute(int attributeIndex)
	{
		if (attributeIndex == -1)
		{
			return 1.0;
		}
		return ProductCombination[attributeIndex];
	}

	internal void setProductAttribute(int attributeIndex, double value)
	{
		double[] array = ProductCombination.Clone() as double[];
		array[attributeIndex] = value;
		ProductCombination = array.Clone() as double[];
	}

	internal double getProductDefaultAttribute(int attributeIndex)
	{
		if (attributeIndex == -1)
		{
			return 1.0;
		}
		return defaultProductCombination[attributeIndex];
	}

	internal void setProductDefaultAttribute(int attributeIndex, double value)
	{
		double[] array = defaultProductCombination.Clone() as double[];
		array[attributeIndex] = value;
		defaultProductCombination = array.Clone() as double[];
	}

	public void calculateNewShare(int sizeAttributeIndex)
	{
		double num = 0.0;
		for (int i = 0; i < Count; i++)
		{
			if (productIndex[i].Included)
			{
				if (productIndex[i].productType.Equals("NewProduct"))
				{
					productIndex[i].NewPreferenceShare = productIndex[i].NewPreferenceShare * (productIndex[i].getProductAttribute(sizeAttributeIndex) / productIndex[i].getProductDefaultAttribute(sizeAttributeIndex));
				}
				else
				{
					productIndex[i].NewPreferenceShare = productIndex[i].NewPreferenceShare * (productIndex[i].NewWD / productIndex[i].OriginalWD) * (productIndex[i].getProductAttribute(sizeAttributeIndex) / productIndex[i].getProductDefaultAttribute(sizeAttributeIndex));
				}
				num += productIndex[i].NewPreferenceShare;
			}
		}
		for (int i = 0; i < Count; i++)
		{
			if (productIndex[i].Included)
			{
				productIndex[i].NewPreferenceShare = productIndex[i].NewPreferenceShare / num * 100.0;
			}
		}
		for (int i = 0; i < Count; i++)
		{
			if (productIndex[i].Included)
			{
				if (productIndex[i].productType == "ReplacedProduct")
				{
					productIndex[i].NewShare = productIndex[productIndex[i].BaseProduct - 1].CurrentShare * productIndex[i].NewPreferenceShare / productIndex[productIndex[i].BaseProduct - 1].CurrentPreferenceShare;
				}
				else if (productIndex[i].productType == "NewProduct")
				{
					productIndex[i].NewShare = productIndex[i].NewPreferenceShare * productIndex[i].NewWD * productIndex[i].ConsumptionCalibration * productIndex[i].SOR * productIndex[i].AwarenessLevel * productIndex[i].RepeateRate / 100.0;
				}
				else
				{
					productIndex[i].NewShare = productIndex[i].CurrentShare * (productIndex[i].NewPreferenceShare / productIndex[i].CurrentPreferenceShare);
				}
				productIndex[i].NewShare *= productIndex[i].promotionCalibration;
			}
		}
		rebaseNewShare();
	}

	internal void rebaseNewShare()
	{
		double num = 0.0;
		double num2 = 0.0;
		double num3 = 0.0;
		double num4 = 0.0;
		double num5 = 0.0;
		for (int i = 0; i < Count; i++)
		{
			num2 += productIndex[i].CurrentShare;
		}
		for (int i = 0; i < Count; i++)
		{
			if (productIndex[i].Included)
			{
				if (productIndex[i].productType == "ReplacedProduct")
				{
					productIndex[i].DiffVolumeShare = productIndex[i].NewShare - productIndex[productIndex[i].baseProduct - 1].CurrentShare;
					num5 += productIndex[productIndex[i].baseProduct - 1].CurrentShare;
				}
				else
				{
					productIndex[i].DiffVolumeShare = productIndex[i].NewShare - productIndex[i].CurrentShare;
				}
				num += productIndex[i].NewShare;
				if (productIndex[i].DiffVolumeShare > 0.0)
				{
					num4 += productIndex[i].DiffVolumeShare;
				}
				else if (productIndex[i].DiffVolumeShare < 0.0)
				{
					num5 += productIndex[i].DiffVolumeShare;
				}
			}
			else if (!productIndex[i].Included && productIndex[i].CurrentShare > 0.0)
			{
				num5 -= productIndex[i].CurrentShare;
			}
		}
		num3 = num - num2;
		num = 0.0;
		for (int i = 0; i < Count; i++)
		{
			if (!productIndex[i].Included)
			{
				continue;
			}
			if (num3 > 0.0)
			{
				if (productIndex[i].DiffVolumeShare > 0.0)
				{
					productIndex[i].DiffVolumeShare = productIndex[i].DiffVolumeShare * Math.Abs(num5 / num4);
					if (productIndex[i].productType == "ReplacedProduct")
					{
						productIndex[i].NewShare = productIndex[productIndex[i].baseProduct - 1].CurrentShare + productIndex[i].DiffVolumeShare;
					}
					else
					{
						productIndex[i].NewShare = productIndex[i].CurrentShare + productIndex[i].DiffVolumeShare;
					}
				}
			}
			else if (num3 < 0.0 && productIndex[i].DiffVolumeShare < 0.0)
			{
				productIndex[i].DiffVolumeShare = productIndex[i].DiffVolumeShare * Math.Abs(num4 / num5);
				if (productIndex[i].productType == "ReplacedProduct")
				{
					productIndex[i].NewShare = productIndex[productIndex[i].baseProduct - 1].CurrentShare + productIndex[i].DiffVolumeShare;
				}
				else
				{
					productIndex[i].NewShare = productIndex[i].CurrentShare + productIndex[i].DiffVolumeShare;
				}
			}
			num += productIndex[i].NewShare;
		}
		for (int i = 0; i < Count; i++)
		{
			if (productIndex[i].Included)
			{
				productIndex[i].NewShare = productIndex[i].NewShare * num2 / num;
			}
		}
	}
}
