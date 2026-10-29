using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using MarketSimulator.Model.DataAccess;

namespace MarketSimulator.Model.BusinessLogic;

[Serializable]
internal class Groups : IEnumerable
{
	private int groupTypeID;

	private string groupType;

	private int membersCount;

	private bool researchStart;

	private Dictionary<int, string> groupMembers = new Dictionary<int, string>();

	private Dictionary<int, double> groupExcludedSalesUnit = new Dictionary<int, double>();

	private Dictionary<int, int> productGrouping = new Dictionary<int, int>();

	private Dictionary<string, Groups> groupString = new Dictionary<string, Groups>();

	private Dictionary<int, Groups> groupIndex = new Dictionary<int, Groups>();

	public int GroupTypeID
	{
		get
		{
			return groupTypeID;
		}
		set
		{
			groupTypeID = value;
		}
	}

	public string GroupType
	{
		get
		{
			return groupType;
		}
		set
		{
			groupType = value;
		}
	}

	public int MembersCount
	{
		get
		{
			return membersCount;
		}
		set
		{
			membersCount = value;
		}
	}

	public bool ResearchStart
	{
		get
		{
			return researchStart;
		}
		set
		{
			researchStart = value;
		}
	}

	public Dictionary<int, string> GroupMembers
	{
		get
		{
			return groupMembers;
		}
		set
		{
			groupMembers = value;
		}
	}

	public Dictionary<int, int> ProductGrouping
	{
		get
		{
			return productGrouping;
		}
		set
		{
			productGrouping = value;
		}
	}

	public Groups this[string groupType]
	{
		get
		{
			return groupString[groupType];
		}
		set
		{
			groupString[groupType] = value;
			groupIndex[groupString.Count - 1] = value;
		}
	}

	public Groups this[int index]
	{
		get
		{
			return groupIndex[index];
		}
		set
		{
			groupIndex[index] = value;
		}
	}

	public int Count => groupString.Count;

	IEnumerator IEnumerable.GetEnumerator()
	{
		return groupString.GetEnumerator();
	}

	public void clearGroups()
	{
		groupString.Clear();
		groupIndex.Clear();
	}

	internal void getDataRow(int index)
	{
		DataRow dataRow = DataTransaction.InformationTransfer.DtGroupingTypes.Rows[index];
		groupTypeID = int.Parse(dataRow["TypeID"].ToString());
		groupType = dataRow["GroupType"].ToString();
		membersCount = int.Parse(dataRow["MembersCount"].ToString());
		ResearchStart = bool.Parse(dataRow["ResearchStart"].ToString());
		DataRowCollection rows = DataTransaction.InformationTransfer.DtGroups.Rows;
		foreach (DataRow item in rows)
		{
			if (int.Parse(item["GroupTypeID"].ToString()) == groupTypeID)
			{
				groupMembers.Add(int.Parse(item["GroupID"].ToString()), item["GroupName"].ToString());
			}
		}
		rows = DataTransaction.InformationTransfer.DtProductsGrouping.Rows;
		foreach (DataRow item2 in rows)
		{
			productGrouping.Add(int.Parse(item2["ProductID"].ToString()), int.Parse(item2["Group" + groupTypeID].ToString()));
		}
	}

	public void setGroupExcludedSalesUnit(string segmentName)
	{
		string columnName = ((string.IsNullOrEmpty(segmentName) || !DataTransaction.InformationTransfer.DtGroupsExcludedSalesUnit.Columns.Contains(segmentName)) ? "Total" : segmentName);
		groupExcludedSalesUnit.Clear();
		foreach (DataRow row in DataTransaction.InformationTransfer.DtGroupsExcludedSalesUnit.Rows)
		{
			if ((int)row["GroupTypeID"] == groupTypeID)
			{
				groupExcludedSalesUnit.Add((int)row["GroupID"], (double)row[columnName]);
			}
		}
	}

	public double getGroupExcludedSalesUnit(string name)
	{
		int groupMemberID = getGroupMemberID(name);
		if (groupExcludedSalesUnit.ContainsKey(groupMemberID))
		{
			return groupExcludedSalesUnit[groupMemberID];
		}
		return 0.0;
	}

	public int getGroupMemberID(string name)
	{
		foreach (KeyValuePair<int, string> groupMember in GroupMembers)
		{
			if (groupMember.Value.Equals(name))
			{
				return groupMember.Key;
			}
		}
		return -1;
	}
}
