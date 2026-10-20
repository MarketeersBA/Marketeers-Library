using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using MarketSimulator.Model.DataAccess;
using UndoRedoFramework;

namespace MarketSimulator.Model.BusinessLogic;

[Serializable]
internal class Segments : IEnumerable
{
	private int segmentTypeID;

	private string segmentType;

	private int membersCount;

	private Dictionary<int, string> segmentMembers;

	private Dictionary<int, int> respondentSegmentation;

	private readonly UndoRedo<string> segmentMemberStart = new UndoRedo<string>();

	private Dictionary<string, Segments> segmentString = new Dictionary<string, Segments>();

	private Dictionary<int, Segments> segmentIndex = new Dictionary<int, Segments>();

	public int SegmentTypeID
	{
		get
		{
			return segmentTypeID;
		}
		set
		{
			segmentTypeID = value;
		}
	}

	public string SegmentType
	{
		get
		{
			return segmentType;
		}
		set
		{
			segmentType = value;
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

	public string SegmentMemberStart
	{
		get
		{
			return segmentMemberStart.Value;
		}
		set
		{
			segmentMemberStart.Value = value;
		}
	}

	public Dictionary<int, string> SegmentMembers
	{
		get
		{
			return segmentMembers;
		}
		set
		{
			segmentMembers = value;
		}
	}

	public Dictionary<int, int> RespondentSegmentation
	{
		get
		{
			return respondentSegmentation;
		}
		set
		{
			respondentSegmentation = value;
		}
	}

	public Segments this[string segmentType]
	{
		get
		{
			return segmentString[segmentType];
		}
		set
		{
			segmentString[segmentType] = value;
			segmentIndex[segmentString.Count - 1] = value;
		}
	}

	public Segments this[int index]
	{
		get
		{
			return segmentIndex[index];
		}
		set
		{
			segmentIndex[index] = value;
		}
	}

	public int Count => segmentString.Count;

	IEnumerator IEnumerable.GetEnumerator()
	{
		return segmentString.GetEnumerator();
	}

	public void clearSegments()
	{
		segmentString.Clear();
		segmentIndex.Clear();
	}

	public Segments()
	{
		segmentMembers = new Dictionary<int, string>();
		respondentSegmentation = new Dictionary<int, int>();
	}

	internal void getDataRow(int index)
	{
		DataRow dataRow = DataTransaction.InformationTransfer.DtSegmentingTypes.Rows[index];
		segmentTypeID = int.Parse(dataRow["TypeID"].ToString());
		segmentType = dataRow["SegmentType"].ToString();
		membersCount = int.Parse(dataRow["MembersCount"].ToString());
		DataRowCollection rows = DataTransaction.InformationTransfer.DtSegments.Rows;
		foreach (DataRow item in rows)
		{
			if (int.Parse(item["SegmentTypeID"].ToString()) == segmentTypeID)
			{
				segmentMembers.Add(int.Parse(item["SegmentID"].ToString()), item["SegmentName"].ToString());
			}
		}
		rows = DataTransaction.InformationTransfer.DtRespondentSegmentation.Rows;
		foreach (DataRow item2 in rows)
		{
			respondentSegmentation.Add(int.Parse(item2["RespondentID"].ToString()), int.Parse(item2["Segment" + segmentTypeID].ToString()));
		}
	}
}
