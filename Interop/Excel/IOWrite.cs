using System;
using InteropExcel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
namespace Excel
{
	public class IOWrite
	{
		private DataStruct _data;
		private InteropExcel.Application excel;

		public IOWrite (DataStruct data)
		{
			_data = data;
		}
	
		public bool exportTable ()
		{
			try{
				//Podgotovka
				excel = new InteropExcel.ApplicationClass();
				if (excel == null) return false;

				excel.Visible = false;

				InteropExcel.Workbook workbook = excel.Workbooks.Add ();
				if (workbook == null) return false;

				InteropExcel.Worksheet sheet = (InteropExcel.Worksheet) workbook.Worksheets [1];
				sheet.Name = "Tablica 1";
				//Populvane na tablicata
				int i = 1;

				addRow (new DataRow ( "Purvo Ime","Familiq","Godini"),i++ ,true, 50); i++;

				foreach (DataRow row in _data.table)
				{
					addRow (row,i++,false , -1);
				}
				i++; addRow (new DataRow ( "Broi redove","",_data.table.Count.ToString()),i++, true , -1);
				//Zapametqvane i zatvarqne
				workbook.SaveCopyAs (getPath ());

				excel.DisplayAlerts=false;  //Izkl. vsichki suobshteniq na excel

				workbook.Close();
				excel.Quit();


				if (workbook != null) Marshal.ReleaseComObject (workbook);
				if (sheet != null) Marshal.ReleaseComObject (sheet);
				if (excel != null) Marshal.ReleaseComObject (excel);

				workbook = null;
				sheet = null;
				excel = null;

				GC.Collect ();

				return true;
			}catch{
			}

			return false;
		}
		public void addRow (DataRow _dataRow, int _indexRow, bool isBold , int color )
		{
			try {
				InteropExcel.Range range;
				//Formatirame
				range = excel.Range ["A" + _indexRow.ToString(), "C" + _indexRow.ToString()];
				if (color > 0 ) 	range.Interior.ColorIndex = color ; //-1
				if (isBold) 		range.Font.Bold = isBold ;

				//Vuvejdane na danni
				range = excel.Range ["A" + _indexRow.ToString(), "A" + _indexRow.ToString()];
				range.Value2 = _dataRow.firstName;

				range = excel.Range ["B" + _indexRow.ToString(), "B" + _indexRow.ToString()];
				range.Value2 = _dataRow.lastName;

				range = excel.Range ["C" + _indexRow.ToString(), "C" + _indexRow.ToString()];
				range.Value2 = _dataRow.age;
			}catch{
			}
		}
	

		public void runFile ()
		{
			try{
				System.Diagnostics.Process.Start (getPath ());
			}catch{
			}
		}

		private string getPath ()
		{
			return System.IO.Path.Combine (AppDomain.CurrentDomain.BaseDirectory, "Table1.xlsx");
		}
	}
}

