using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MarketSimulator.Model;

[Serializable]
public class Engine
{
	private bool packSimulator;

	private bool preferenceShare;

	private bool penetration;

	private bool volumeShare;

	private bool valueShare;

	private bool vsu;

	private bool nosAndTdc;

	private bool charts;

	private bool revenue;

	private DateTime startTime = default(DateTime);

	private DateTime expireDate = default(DateTime);

	private DateTime yesterday = default(DateTime);

	private int dayCount;

	private bool threeMonthMessage;

	private bool twoMonthMessage;

	private List<string> products = new List<string>();

	public bool PackSimulator
	{
		get
		{
			return packSimulator;
		}
		set
		{
			packSimulator = value;
		}
	}

	public bool PreferenceShare
	{
		get
		{
			return preferenceShare;
		}
		set
		{
			preferenceShare = value;
		}
	}

	public bool Penetration
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

	public bool ValueShare
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

	public bool VSU
	{
		get
		{
			return vsu;
		}
		set
		{
			vsu = value;
		}
	}

	public bool NosAndTdc
	{
		get
		{
			return nosAndTdc;
		}
		set
		{
			nosAndTdc = value;
		}
	}

	public bool Charts
	{
		get
		{
			return charts;
		}
		set
		{
			charts = value;
		}
	}

	public bool Revenue
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

	public DateTime StartTime
	{
		get
		{
			return startTime;
		}
		set
		{
			startTime = value;
		}
	}

	public int DayCount
	{
		get
		{
			return dayCount;
		}
		set
		{
			dayCount = value;
		}
	}

	public DateTime ExpireDate
	{
		get
		{
			return expireDate;
		}
		set
		{
			expireDate = value;
		}
	}

	public DateTime Yesterday
	{
		get
		{
			return yesterday;
		}
		set
		{
			yesterday = value;
		}
	}

	public bool TwoMonthMessage
	{
		get
		{
			return twoMonthMessage;
		}
		set
		{
			twoMonthMessage = value;
		}
	}

	public bool ThreeMonthMessage
	{
		get
		{
			return threeMonthMessage;
		}
		set
		{
			threeMonthMessage = value;
		}
	}

	public bool VolumeShare
	{
		get
		{
			return volumeShare;
		}
		set
		{
			volumeShare = value;
		}
	}

	public List<string> Products
	{
		get
		{
			return products;
		}
		set
		{
			products = value;
		}
	}

	public Engine()
	{
	}

	public Engine(SerializationInfo info, StreamingContext ctxt)
	{
		packSimulator = info.GetBoolean("PackSimulator");
		preferenceShare = info.GetBoolean("PreferenceShare");
		penetration = info.GetBoolean("Penetration");
		volumeShare = info.GetBoolean("VolumeShare");
		valueShare = info.GetBoolean("ValueShare");
		vsu = info.GetBoolean("VSU");
		nosAndTdc = info.GetBoolean("NosAndTdc");
		charts = info.GetBoolean("Charts");
		revenue = info.GetBoolean("Revenue");
		startTime = info.GetDateTime("StartTime");
		expireDate = info.GetDateTime("ExpireDate");
		yesterday = info.GetDateTime("Yesterday");
		dayCount = info.GetInt32("DayCount");
		twoMonthMessage = info.GetBoolean("DisplayMessage");
		products = info.GetValue("Products", typeof(List<string>)) as List<string>;
	}

	public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
	{
		info.AddValue("PackSimulator", packSimulator);
		info.AddValue("PreferenceShare", preferenceShare);
		info.AddValue("Penetration", penetration);
		info.AddValue("VolumeShare", volumeShare);
		info.AddValue("ValueShare", valueShare);
		info.AddValue("VSU", vsu);
		info.AddValue("NosAndTdc", nosAndTdc);
		info.AddValue("Charts", charts);
		info.AddValue("Revenue", revenue);
		info.AddValue("StartTime", startTime);
		info.AddValue("ExpireDate", expireDate);
		info.AddValue("Yesterday", yesterday);
		info.AddValue("DayCount", dayCount);
		info.AddValue("DisplayMessage", twoMonthMessage);
		info.AddValue("Products", products);
	}
}
