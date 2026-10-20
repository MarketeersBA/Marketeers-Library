using System;
using System.Data;
using System.IO;
using System.Reflection;
using MarketSimulator.Model;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Graph;
using Microsoft.Office.Interop.PowerPoint;

namespace MarketSimulator.Reports;

public class ExcelSheet
{
	private Microsoft.Office.Interop.Excel.Application oXL;

	private _Workbook oWB;

	private _Worksheet oSheet;

	private Microsoft.Office.Interop.PowerPoint.Application powerpointApplication;

	private Presentation pptPresentation;

	private Microsoft.Office.Interop.Excel.Range oResizeRange = null;

	public ExcelSheet()
	{
		oXL = new Microsoft.Office.Interop.Excel.ApplicationClass();
		oXL.Visible = true;
		oWB = oXL.Workbooks.Add(Missing.Value);
		foreach (_Worksheet sheet in oWB.Sheets)
		{
			if (oWB.Sheets.Count > 1)
			{
				sheet.Delete();
			}
		}
	}

	public void automateDataset(DataSet dscharts, bool drawChart, bool createPowerPoint)
	{
		foreach (System.Data.DataTable table in dscharts.Tables)
		{
			oSheet = (_Worksheet)oWB.Sheets.Add(Missing.Value, Missing.Value, 1, Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);
			if (table.ExtendedProperties.Count != 0)
			{
				oSheet.Name = table.TableName;
			}
			else
			{
				oSheet.Name = table.TableName;
			}
			for (int i = 0; i < table.Columns.Count; i++)
			{
				oSheet.Cells[1, i + 1] = table.Columns[i].ColumnName;
			}
			Microsoft.Office.Interop.Excel.Range range = oSheet.get_Range(oSheet.Cells[1, 1], oSheet.Cells[1, table.Columns.Count]);
			range.Interior.Color = 12419407;
			range.Font.Bold = true;
			range.Font.Size = 11;
			range.Font.Color = 16777215;
			range.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
			range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
			range.AutoFilter(1, Missing.Value, XlAutoFilterOperator.xlFilterValues, Missing.Value, Missing.Value);
			range.Borders.get_Item(XlBordersIndex.xlEdgeRight).Color = 12419407;
			range.Borders.get_Item(XlBordersIndex.xlEdgeRight).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
			range.Borders.get_Item(XlBordersIndex.xlEdgeBottom).Color = 12419407;
			range.Borders.get_Item(XlBordersIndex.xlEdgeBottom).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
			range.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, Missing.Value);
			range = oSheet.get_Range(oSheet.Cells[1, 1], oSheet.Cells[1, table.Columns.Count]);
			range.Cells[1, 1] = table.TableName;
			range.Merge(false);
			range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
			range.Interior.Color = 6108951;
			range.Font.Bold = true;
			range.Font.Color = 16777215;
			range.Font.Size = 16;
			range.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
			range = oSheet.get_Range(oSheet.Cells[2, 1], oSheet.Cells[2, 1]);
			for (int j = 0; j < table.Rows.Count; j++)
			{
				range = oSheet.get_Range(oSheet.Cells[j + 3, 1], oSheet.Cells[j + 3, table.Columns.Count]);
				range.Value2 = table.Rows[j].ItemArray;
				range.Font.Size = 10;
				range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
				range.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
				range.Borders.get_Item(XlBordersIndex.xlEdgeRight).Color = 12419407;
				range.Borders.get_Item(XlBordersIndex.xlEdgeRight).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
				range.Borders.get_Item(XlBordersIndex.xlEdgeBottom).Color = 12419407;
				range.Borders.get_Item(XlBordersIndex.xlEdgeBottom).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
			}
			range = oSheet.get_Range(oSheet.Cells[3, 1], oSheet.Cells[table.Rows.Count + 2, 1]);
			range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
			range.Interior.Color = 15853019;
			range.Columns.AutoFit();
			range = oSheet.get_Range(oSheet.Cells[1, 1], oSheet.Cells[table.Rows.Count + 2, table.Columns.Count]);
			range.Columns.AutoFit();
			oSheet.UsedRange.Font.Name = "Tw Cen MT";
			if (!drawChart && !createPowerPoint)
			{
				for (int j = 2; j <= table.Columns.Count; j++)
				{
					range = oSheet.get_Range(oSheet.Cells[3, j], oSheet.Cells[table.Rows.Count + 2, j]);
					if (range.Cells.NumberFormat != null && !range.Cells.NumberFormat.ToString().Equals("0.00%"))
					{
						range.Cells.NumberFormat = "0.00";
					}
				}
			}
			if (drawChart)
			{
				if (!table.ExtendedProperties["ChartName"].ToString().Equals("S.Index And Revenues"))
				{
					createChart(table);
					continue;
				}
				oResizeRange = oSheet.get_Range(oSheet.Cells[3, 2], oSheet.Cells[table.Rows.Count + 2, table.Columns.Count]);
				oResizeRange.Cells.NumberFormat = "0.00";
			}
		}
		if (createPowerPoint)
		{
			oWB.Application.DisplayAlerts = false;
			File.Delete(Path.GetTempPath() + "Automated charts.xls");
			if (string.Equals(oXL.Version, "12.0"))
			{
				oWB.SaveCopyAs(Path.GetTempPath() + "New Automated charts.xlsx");
				Microsoft.Office.Interop.Excel.Application application = new Microsoft.Office.Interop.Excel.ApplicationClass();
				application.Visible = false;
				_Workbook workbook = application.Workbooks.Open(Path.GetTempPath() + "New Automated charts", Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, false, Missing.Value, Missing.Value);
				workbook.Application.DisplayAlerts = false;
				workbook.SaveAs(Path.GetTempPath() + "Automated charts", XlFileFormat.xlWorkbookNormal, Missing.Value, Missing.Value, false, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, false, Missing.Value, Missing.Value, Missing.Value);
				File.Delete(Path.GetTempPath() + "New Automated charts.xls");
				application.Quit();
			}
			else
			{
				oWB.SaveCopyAs(Path.GetTempPath() + "Automated charts.xls");
			}
			createPowerPointSlide();
			oWB.Application.DisplayAlerts = true;
			oWB.Saved = false;
		}
	}

	private void createChart(System.Data.DataTable chartTable)
	{
		Microsoft.Office.Interop.Excel.Series series = null;
		Microsoft.Office.Interop.Excel.Chart chart = null;
		ChartObjects chartObjects = (ChartObjects)oSheet.ChartObjects(Missing.Value);
		chart = (Microsoft.Office.Interop.Excel.Chart)oWB.Charts.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);
		switch (chartTable.ExtendedProperties["ChartName"].ToString())
		{
		case "Price Index And Revenues":
		{
			chart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLineMarkers;
			oResizeRange = oSheet.get_Range(oSheet.Cells[3, 2], oSheet.Cells[chartTable.Rows.Count + 2, chartTable.Columns.Count]);
			oResizeRange.Cells.NumberFormat = "0.00%";
			oResizeRange = oSheet.get_Range(oSheet.Cells[3, 1], oSheet.Cells[chartTable.Rows.Count + 2, chartTable.Columns.Count]);
			chart.SetSourceData(oResizeRange, Microsoft.Office.Interop.Excel.XlRowCol.xlRows);
			series = (Microsoft.Office.Interop.Excel.Series)chart.SeriesCollection(1);
			series.XValues = oSheet.get_Range(oSheet.Cells[2, 2], oSheet.Cells[2, chartTable.Columns.Count]);
			Microsoft.Office.Interop.Excel.Axis axis;
			if (chartTable.Rows[chartTable.Rows.Count - 1][0].ToString().EndsWith(" Revenues"))
			{
				oResizeRange = oSheet.get_Range(oSheet.Cells[chartTable.Rows.Count + 2, 2], oSheet.Cells[chartTable.Rows.Count + 2, chartTable.Columns.Count]);
				oResizeRange.NumberFormat = "0.00";
				series = (Microsoft.Office.Interop.Excel.Series)chart.SeriesCollection(chartTable.Rows.Count);
				series.AxisGroup = Microsoft.Office.Interop.Excel.XlAxisGroup.xlSecondary;
				series.Border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlDash;
				series.Border.Weight = 3;
				series.MarkerStyle = Microsoft.Office.Interop.Excel.XlMarkerStyle.xlMarkerStyleSquare;
				axis = (Microsoft.Office.Interop.Excel.Axis)chart.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlValue, Microsoft.Office.Interop.Excel.XlAxisGroup.xlSecondary);
				axis.TickLabels.NumberFormat = "0.00";
				axis.HasTitle = true;
				axis.AxisTitle.Text = "Revenue Indicator (in millions)";
				axis.AxisTitle.Orientation = Microsoft.Office.Interop.Excel.XlOrientation.xlUpward;
			}
			if (string.Equals(chartTable.Rows[chartTable.Rows.Count - 1][0].ToString(), "Company Shares"))
			{
				series = (Microsoft.Office.Interop.Excel.Series)chart.SeriesCollection(chartTable.Rows.Count);
				series.Border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlDash;
				series.Border.Weight = 3;
				series.MarkerStyle = Microsoft.Office.Interop.Excel.XlMarkerStyle.xlMarkerStyleTriangle;
			}
			else if (chartTable.Rows.Count > 1 && string.Equals(chartTable.Rows[chartTable.Rows.Count - 2][0].ToString(), "Company Shares"))
			{
				series = (Microsoft.Office.Interop.Excel.Series)chart.SeriesCollection(chartTable.Rows.Count - 1);
				series.Border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlDash;
				series.Border.Weight = 3;
				series.MarkerStyle = Microsoft.Office.Interop.Excel.XlMarkerStyle.xlMarkerStyleTriangle;
			}
			axis = (Microsoft.Office.Interop.Excel.Axis)chart.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlValue);
			axis.TickLabels.NumberFormat = "0.0%";
			axis.HasTitle = true;
			axis.AxisTitle.Text = "Volume Share (%)";
			axis.AxisTitle.Orientation = Microsoft.Office.Interop.Excel.XlOrientation.xlUpward;
			axis.HasMajorGridlines = false;
			axis = (Microsoft.Office.Interop.Excel.Axis)chart.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlCategory);
			axis.HasTitle = true;
			axis.AxisTitle.Text = "Price Index";
			break;
		}
		case "Brand Interaction":
		{
			chart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlColumnStacked;
			oResizeRange = oSheet.get_Range(oSheet.Cells[3, 2], oSheet.Cells[chartTable.Rows.Count + 2, chartTable.Columns.Count]);
			oResizeRange.Cells.NumberFormat = "0.00%";
			oResizeRange = oSheet.get_Range(oSheet.Cells[3, 1], oSheet.Cells[chartTable.Rows.Count + 2, chartTable.Columns.Count]);
			chart.SetSourceData(oResizeRange, Microsoft.Office.Interop.Excel.XlRowCol.xlRows);
			Microsoft.Office.Interop.Excel.Axis axis = (Microsoft.Office.Interop.Excel.Axis)chart.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlValue);
			axis.Delete();
			axis.HasMajorGridlines = false;
			axis = (Microsoft.Office.Interop.Excel.Axis)chart.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlCategory);
			axis.TickLabelPosition = Microsoft.Office.Interop.Excel.XlTickLabelPosition.xlTickLabelPositionNone;
			break;
		}
		case "Fair Share":
		{
			chart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlBarClustered;
			oResizeRange = oSheet.get_Range(oSheet.Cells[3, 2], oSheet.Cells[chartTable.Rows.Count + 2, chartTable.Columns.Count]);
			oResizeRange.Cells.NumberFormat = "0.00";
			int num2 = 0;
			for (int i = 0; i < chartTable.Rows.Count; i++)
			{
				if (chartTable.Rows[i][1].ToString().Equals("0"))
				{
					num2 = i + 3;
					break;
				}
			}
			if (num2 == 3)
			{
				oResizeRange = oSheet.get_Range(oSheet.Cells[4, 1], oSheet.Cells[chartTable.Rows.Count + 2, 2]);
			}
			else
			{
				oResizeRange = oSheet.get_Range((object)("'" + oSheet.Name + "'!$A$" + (num2 - (num2 - 1) + 2) + ":$B$" + (num2 - 1) + ",'" + oSheet.Name + "'!$A$" + (num2 + 1) + ":$B$" + (chartTable.Rows.Count + 2)), (object)Missing.Value);
			}
			chart.SetSourceData(oResizeRange, Microsoft.Office.Interop.Excel.XlRowCol.xlColumns);
			Microsoft.Office.Interop.Excel.Axis axis = (Microsoft.Office.Interop.Excel.Axis)chart.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlCategory);
			axis.TickLabelPosition = Microsoft.Office.Interop.Excel.XlTickLabelPosition.xlTickLabelPositionLow;
			axis = (Microsoft.Office.Interop.Excel.Axis)chart.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlValue);
			axis.CrossesAt = 1.0;
			axis.HasMajorGridlines = false;
			series = (Microsoft.Office.Interop.Excel.Series)chart.SeriesCollection(1);
			series.Name = "Fair Share";
			series.Interior.Color = 16764006;
			break;
		}
		case "SOM VS Brand Share":
		{
			oResizeRange = oSheet.get_Range(oSheet.Cells[3, 2], oSheet.Cells[chartTable.Rows.Count + 2, chartTable.Columns.Count]);
			oResizeRange.Cells.NumberFormat = "0.00%";
			oResizeRange = oSheet.get_Range(oSheet.Cells[3, 1], oSheet.Cells[chartTable.Rows.Count + 2, chartTable.Columns.Count]);
			chart.SetSourceData(oResizeRange, Microsoft.Office.Interop.Excel.XlRowCol.xlColumns);
			series = (Microsoft.Office.Interop.Excel.Series)chart.SeriesCollection(1);
			series.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlColumnClustered;
			series.Interior.Color = 13395456;
			series.Name = "SOM";
			series = (Microsoft.Office.Interop.Excel.Series)chart.SeriesCollection(2);
			series.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLineMarkers;
			series.MarkerStyle = Microsoft.Office.Interop.Excel.XlMarkerStyle.xlMarkerStyleDiamond;
			series.Border.Color = 3342489;
			series.MarkerForegroundColor = 3342489;
			series.MarkerBackgroundColor = 3342489;
			series.Name = "Brand Share";
			Microsoft.Office.Interop.Excel.Axis axis = (Microsoft.Office.Interop.Excel.Axis)chart.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlValue);
			axis.Delete();
			axis.HasMajorGridlines = false;
			break;
		}
		case "Shares And R.Index":
		{
			chart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlColumnClustered;
			oResizeRange = oSheet.get_Range(oSheet.Cells[3, 2], oSheet.Cells[chartTable.Rows.Count + 2, chartTable.Columns.Count]);
			oResizeRange.Cells.NumberFormat = "0.0%";
			int num3 = 3;
			foreach (DataRow row in chartTable.Rows)
			{
				if (string.IsNullOrEmpty(row["Name"].ToString()))
				{
					break;
				}
				num3++;
			}
			if (num3 != 3)
			{
				if (chartTable.Rows.Count != num3 - 2)
				{
					oResizeRange = oSheet.get_Range((object)("'" + oSheet.Name + "'!$A$3:$" + GetLastColumnName(chartTable.Columns.Count) + "$" + (num3 - 1) + ",'" + oSheet.Name + "'!$A$" + (num3 + 1) + ":$" + GetLastColumnName(chartTable.Columns.Count) + "$" + (chartTable.Rows.Count + 2)), (object)Missing.Value);
				}
				else
				{
					oSheet.get_Range(oSheet.Cells[chartTable.Rows.Count + 2, 1], oSheet.Cells[chartTable.Rows.Count + 2, chartTable.Columns.Count]).Delete(Microsoft.Office.Interop.Excel.XlDeleteShiftDirection.xlShiftUp);
					oResizeRange = oSheet.get_Range((object)("'" + oSheet.Name + "'!$A$3:$" + GetLastColumnName(chartTable.Columns.Count) + "$" + (num3 - 1)), (object)Missing.Value);
				}
				chart.SetSourceData(oResizeRange, Microsoft.Office.Interop.Excel.XlRowCol.xlRows);
				series = (Microsoft.Office.Interop.Excel.Series)chart.SeriesCollection(1);
				series.XValues = oSheet.get_Range(oSheet.Cells[2, 2], oSheet.Cells[2, chartTable.Columns.Count]);
				Microsoft.Office.Interop.Excel.Axis axis = (Microsoft.Office.Interop.Excel.Axis)chart.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlValue);
				axis.TickLabels.NumberFormat = "0.0%";
				axis.HasMajorGridlines = false;
				axis.HasTitle = true;
				axis.AxisTitle.Text = "Volume Share (%)";
				if (num3 != chartTable.Rows.Count + 2)
				{
					oResizeRange = oSheet.get_Range(oSheet.Cells[num3 + 1, 2], oSheet.Cells[chartTable.Rows.Count + 2, chartTable.Columns.Count]);
					oResizeRange.Cells.NumberFormat = "0.00";
					for (int i = num3 - 2; i < chartTable.Rows.Count; i++)
					{
						series = (Microsoft.Office.Interop.Excel.Series)chart.SeriesCollection(i);
						series.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLine;
						series.AxisGroup = Microsoft.Office.Interop.Excel.XlAxisGroup.xlSecondary;
					}
					axis = (Microsoft.Office.Interop.Excel.Axis)chart.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlValue, Microsoft.Office.Interop.Excel.XlAxisGroup.xlSecondary);
					axis.TickLabels.NumberFormat = "0.00";
					axis.HasTitle = true;
					axis.AxisTitle.Text = "Revenue Index";
				}
			}
			else if (num3 == 3)
			{
				oSheet.get_Range(oSheet.Cells[3, 1], oSheet.Cells[3, chartTable.Columns.Count]).Delete(Microsoft.Office.Interop.Excel.XlDeleteShiftDirection.xlShiftUp);
				chart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLine;
				oResizeRange = oSheet.get_Range(oSheet.Cells[3, 2], oSheet.Cells[chartTable.Rows.Count + 1, chartTable.Columns.Count]);
				oResizeRange.Cells.NumberFormat = "0.00";
				oResizeRange = oSheet.get_Range(oSheet.Cells[3, 1], oSheet.Cells[chartTable.Rows.Count + 1, chartTable.Columns.Count]);
				chart.SetSourceData(oResizeRange, Microsoft.Office.Interop.Excel.XlRowCol.xlRows);
				series = (Microsoft.Office.Interop.Excel.Series)chart.SeriesCollection(1);
				series.XValues = oSheet.get_Range(oSheet.Cells[2, 2], oSheet.Cells[2, chartTable.Columns.Count]);
				Microsoft.Office.Interop.Excel.Axis axis = (Microsoft.Office.Interop.Excel.Axis)chart.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlValue);
				axis.TickLabels.NumberFormat = "0.00";
				axis.HasTitle = true;
				axis.AxisTitle.Text = "Revenue Index";
			}
			break;
		}
		case "Elasticity_Product":
		case "Elasticity_Company":
		case "Elasticity_Group":
		{
			chart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLine;
			oResizeRange = oSheet.get_Range(oSheet.Cells[2, 2], oSheet.Cells[2, chartTable.Columns.Count + 1]);
			oResizeRange.Cells.NumberFormat = "0%";
			oResizeRange = oSheet.get_Range(oSheet.Cells[3, 2], oSheet.Cells[chartTable.Rows.Count + 2, chartTable.Columns.Count]);
			oResizeRange.Cells.NumberFormat = "0.00%";
			oResizeRange = oSheet.get_Range(oSheet.Cells[3, 1], oSheet.Cells[chartTable.Rows.Count + 2, chartTable.Columns.Count]);
			oResizeRange.Sort(oSheet.Columns[2, Missing.Value], XlSortOrder.xlDescending, Missing.Value, Missing.Value, XlSortOrder.xlDescending, Missing.Value, XlSortOrder.xlDescending, XlYesNoGuess.xlNo, Missing.Value, Missing.Value, XlSortOrientation.xlSortColumns);
			chart.SetSourceData(oResizeRange, Microsoft.Office.Interop.Excel.XlRowCol.xlRows);
			series = (Microsoft.Office.Interop.Excel.Series)chart.SeriesCollection(1);
			series.XValues = oSheet.get_Range(oSheet.Cells[2, 2], oSheet.Cells[2, chartTable.Columns.Count]);
			Microsoft.Office.Interop.Excel.Axis axis = (Microsoft.Office.Interop.Excel.Axis)chart.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlValue);
			axis.TickLabels.NumberFormat = "0.0%";
			break;
		}
		case "CUM Intro Market Share":
		case "CUM Intro Revenue":
		case "CUM Removal Market Share":
		case "CUM Removal Revenue":
		case "EX Intro":
		case "EX Removal":
		{
			chart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLineMarkers;
			int num = chartTable.Rows.Count - 1;
			while (chartTable.Rows.Count > 0 && string.Equals(chartTable.Rows[num][1].ToString(), "Reversal Effect Happened"))
			{
				num--;
			}
			oResizeRange = oSheet.get_Range(oSheet.Cells[3, 2], oSheet.Cells[num + 1 + 2, chartTable.Columns.Count]);
			oResizeRange.Cells.NumberFormat = "0.00";
			oResizeRange = oSheet.get_Range(oSheet.Cells[3, 1], oSheet.Cells[num + 1 + 2, 3]);
			chart.SetSourceData(oResizeRange, Microsoft.Office.Interop.Excel.XlRowCol.xlColumns);
			oResizeRange = oSheet.get_Range(oSheet.Cells[3, 3], oSheet.Cells[num + 1 + 2, 3]);
			oResizeRange.Cells.NumberFormat = "0.00%";
			series = (Microsoft.Office.Interop.Excel.Series)chart.SeriesCollection(1);
			series.Border.ColorIndex = 43;
			series.Border.Weight = 3;
			series.MarkerStyle = Microsoft.Office.Interop.Excel.XlMarkerStyle.xlMarkerStyleSquare;
			series.MarkerForegroundColor = 52377;
			series.MarkerBackgroundColor = 52377;
			series.AxisGroup = Microsoft.Office.Interop.Excel.XlAxisGroup.xlSecondary;
			series.Name = "Revenue";
			series = (Microsoft.Office.Interop.Excel.Series)chart.SeriesCollection(2);
			series.Border.ColorIndex = 47;
			series.Border.Weight = 3;
			series.MarkerStyle = Microsoft.Office.Interop.Excel.XlMarkerStyle.xlMarkerStyleTriangle;
			series.MarkerForegroundColor = 10053222;
			series.MarkerBackgroundColor = 10053222;
			series.Name = "Market Share";
			Microsoft.Office.Interop.Excel.Axis axis = (Microsoft.Office.Interop.Excel.Axis)chart.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlValue);
			axis.TickLabels.NumberFormat = "0.0%";
			axis.HasMajorGridlines = true;
			axis.MajorGridlines.Border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlDot;
			axis.HasTitle = true;
			axis.AxisTitle.Text = "Volume Share (%)";
			axis = (Microsoft.Office.Interop.Excel.Axis)chart.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlValue, Microsoft.Office.Interop.Excel.XlAxisGroup.xlSecondary);
			axis.TickLabels.NumberFormat = "0.00";
			axis.HasTitle = true;
			axis.AxisTitle.Text = "Revenue Indicator (in millions)";
			axis = (Microsoft.Office.Interop.Excel.Axis)chart.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlCategory);
			axis.HasMajorGridlines = true;
			axis.MajorGridlines.Border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlDot;
			axis.TickLabels.Orientation = Microsoft.Office.Interop.Excel.XlTickLabelOrientation.xlTickLabelOrientationAutomatic;
			break;
		}
		case "Validation":
		{
			chart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLine;
			oResizeRange = oSheet.get_Range(oSheet.Cells[3, 2], oSheet.Cells[chartTable.Rows.Count + 2, chartTable.Columns.Count]);
			oResizeRange.Cells.NumberFormat = "0.00%";
			oResizeRange = oSheet.get_Range(oSheet.Cells[3, 1], oSheet.Cells[chartTable.Rows.Count + 2, chartTable.Columns.Count]);
			chart.SetSourceData(oResizeRange, Microsoft.Office.Interop.Excel.XlRowCol.xlColumns);
			series = (Microsoft.Office.Interop.Excel.Series)chart.SeriesCollection(1);
			series.Name = "Audit Share";
			series.ErrorBar(Microsoft.Office.Interop.Excel.XlErrorBarDirection.xlY, Microsoft.Office.Interop.Excel.XlErrorBarInclude.xlErrorBarIncludeBoth, Microsoft.Office.Interop.Excel.XlErrorBarType.xlErrorBarTypePercent, 10, Missing.Value);
			series = (Microsoft.Office.Interop.Excel.Series)chart.SeriesCollection(2);
			series.Name = "Rebased Simulator Share";
			Microsoft.Office.Interop.Excel.Axis axis = (Microsoft.Office.Interop.Excel.Axis)chart.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlValue);
			axis.TickLabels.NumberFormat = "0.0%";
			axis.HasMajorGridlines = true;
			axis.MajorGridlines.Border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlDot;
			axis.HasTitle = true;
			axis.AxisTitle.Text = "Volume Share (%)";
			axis = (Microsoft.Office.Interop.Excel.Axis)chart.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlCategory);
			axis.HasMajorGridlines = true;
			axis.MajorGridlines.Border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlDot;
			chart.HasTitle = true;
			chart.ChartTitle.Text = oSheet.Name;
			chart.ChartTitle.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
			break;
		}
		}
		chart.ChartArea.Font.Name = "Tw Cen MT";
		chart.ChartArea.Font.Size = 12;
		chart.ChartArea.Font.Bold = true;
		chart.ChartArea.Border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
		chart.PlotArea.Interior.ColorIndex = 2;
		chart.PlotArea.Border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
		chart.HasLegend = true;
		if (!string.Equals(chartTable.TableName, "Brand Interaction") && !chartTable.TableName.Contains("Elasticity"))
		{
			chart.Legend.Position = Microsoft.Office.Interop.Excel.XlLegendPosition.xlLegendPositionTop;
		}
		chart.Legend.Border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
		chart.Legend.Font.Size = 10;
		chart.ApplyDataLabels(Microsoft.Office.Interop.Excel.XlDataLabelsType.xlDataLabelsShowValue, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
		if (string.Equals(chartTable.TableName, "SOM VS Brand Share"))
		{
			series = (Microsoft.Office.Interop.Excel.Series)chart.SeriesCollection(2);
			Microsoft.Office.Interop.Excel.DataLabels dataLabels = (Microsoft.Office.Interop.Excel.DataLabels)series.DataLabels(Missing.Value);
			dataLabels.Font.Color = 3342489;
		}
		formatSeriesCollection(chart);
		chart.Name = "c" + oSheet.Name;
	}

	private void createPowerPointSlide()
	{
		Slide slide = null;
		powerpointApplication = new Microsoft.Office.Interop.PowerPoint.ApplicationClass();
		powerpointApplication.Visible = MsoTriState.msoTrue;
		pptPresentation = powerpointApplication.Presentations.Add();
		foreach (Microsoft.Office.Interop.Excel.Chart chart3 in oWB.Charts)
		{
			slide = pptPresentation.Slides.Add(1, PpSlideLayout.ppLayoutBlank);
			Microsoft.Office.Interop.Graph.Chart chart2 = (Microsoft.Office.Interop.Graph.Chart)slide.Shapes.AddOLEObject(20f, 20f, 680f, 500f, "MSGraph.Chart.8", "", MsoTriState.msoFalse, "", 0, "", MsoTriState.msoTrue).OLEFormat.Object;
			chart2.Application.ImportChart(Path.GetTempPath() + "Automated Charts.xls", Missing.Value, Missing.Value, chart3.Name, Missing.Value);
		}
	}

	private void formatSeriesCollection(Microsoft.Office.Interop.Excel.Chart oChart)
	{
		Microsoft.Office.Interop.Excel.SeriesCollection seriesCollection = (Microsoft.Office.Interop.Excel.SeriesCollection)oChart.SeriesCollection(Missing.Value);
		for (int i = 1; i <= seriesCollection.Count; i++)
		{
			Microsoft.Office.Interop.Excel.Series series = (Microsoft.Office.Interop.Excel.Series)oChart.SeriesCollection(i);
			if (!ProjectCore.ExcelColorNumberFormat.ContainsKey(series.Name))
			{
				continue;
			}
			if (series.ChartType == Microsoft.Office.Interop.Excel.XlChartType.xlLine || series.ChartType == Microsoft.Office.Interop.Excel.XlChartType.xlLineMarkers)
			{
				series.Border.Color = int.Parse(ProjectCore.ExcelColorNumberFormat[series.Name]);
				series.Border.Weight = 3;
				if (series.ChartType == Microsoft.Office.Interop.Excel.XlChartType.xlLineMarkers)
				{
					series.MarkerForegroundColor = int.Parse(ProjectCore.ExcelColorNumberFormat[series.Name]);
					series.MarkerBackgroundColor = int.Parse(ProjectCore.ExcelColorNumberFormat[series.Name]);
				}
			}
			else if (series.ChartType == Microsoft.Office.Interop.Excel.XlChartType.xlColumnClustered || series.ChartType == Microsoft.Office.Interop.Excel.XlChartType.xlColumnStacked)
			{
				series.Interior.Color = int.Parse(ProjectCore.ExcelColorNumberFormat[series.Name]);
			}
		}
	}

	private string GetLastColumnName(int lastColumnIndex)
	{
		string text = "";
		if (lastColumnIndex > 26)
		{
			char c = Convert.ToChar(64 + (lastColumnIndex - 1) / 26);
			char c2 = Convert.ToChar(64 + ((lastColumnIndex % 26 == 0) ? 26 : (lastColumnIndex % 26)));
			return c.ToString() + c2;
		}
		return Convert.ToChar(64 + lastColumnIndex).ToString();
	}
}
