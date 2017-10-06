//
// XML Data Access Layer (DAL) class
//     Table: Movie
//     Class: Movie
//     Database: Portfolio
//     Created: 7/30/2013 5:38:49 PM
//
using System;
using System.Xml;
using System.Data;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Windows;

namespace mmMovieCollection.Model
{
	public class Movie
	{
		public System.String ID { get; set; }
		public System.String ImdbURL { get; set; }
		public System.String Title { get; set; }
		public System.String RunTime { get; set; }
		public System.String Year { get; set; }
		public System.String Rating { get; set; }
		public System.String Gener { get; set; }
		public System.String Summary { get; set; }
		public System.String Cast { get; set; }
		public System.String CurrentVideo { get; set; }
		public System.String AllVideo { get; set; }
		public System.String Oscar { get; set; }
		public System.String MultipleVideo { get; set; }
		public System.String Poster { get; set; }

		public System.Int32 ColCount { get; set; }
		public List<string> ColName { get; set; }
		public List<string> ColType { get; set; }

		public Movie()
		{
			ColName = new List<string>();
			ColType = new List<string>();
			ID = "";
			ColName.Add("ID");
			ColType.Add("System.String");
			ImdbURL = "";
			ColName.Add("ImdbURL");
			ColType.Add("System.String");
			Title = "";
			ColName.Add("Title");
			ColType.Add("System.String");
			RunTime = "";
			ColName.Add("RunTime");
			ColType.Add("System.String");
			Year = "";
			ColName.Add("Year");
			ColType.Add("System.String");
			Rating = "";
			ColName.Add("Rating");
			ColType.Add("System.String");
			Gener = "";
			ColName.Add("Gener");
			ColType.Add("System.String");
			Summary = "";
			ColName.Add("Summary");
			ColType.Add("System.String");
			Cast = "";
			ColName.Add("Cast");
			ColType.Add("System.String");
			CurrentVideo = "";
			ColName.Add("CurrentVideo");
			ColType.Add("System.String");
			AllVideo = "";
			ColName.Add("AllVideo");
			ColType.Add("System.String");
			Oscar = "";
			ColName.Add("Oscar");
			ColType.Add("System.String");
			MultipleVideo = "";
			ColName.Add("MultipleVideo");
			ColType.Add("System.String");
			Poster = "";
			ColName.Add("Poster");
			ColType.Add("System.String");
			ColCount = 14;
		}
	}
	public class MovieTable : Movie
	{
		public string FilePath { get; set; }   //file path to xml database file

		public List<Movie> Items { get; set; }   //Row retrieved into a list
		public int RowCount { get; set; }   //Count rows in the table

		DataSet dataSet;
		DataTable dataTable;
		
		string tableName;
		
		public MovieTable (string filePath)
		{
			dataSet = new DataSet();
			dataTable = new DataTable();

			Items = new List<Movie>();
			RowCount = 0;

			FilePath = filePath;
			LoadTables();
		}
		public MovieTable ()
		{
			FilePath = DBLayer.Connection + @"\Movie.xml";

			dataSet = new DataSet();
			dataTable = new DataTable();

			Items = new List<Movie>();
			RowCount = 0;

			LoadTables();
		}
		//
		// Initialize the data table
		//
		void LoadTables()
		{
			dataSet.Clear();
			dataTable.Clear();
			//
			// Read XML file 
			//
            try
            {
                dataSet.ReadXml(FilePath);
                if (dataSet.Tables.Count != 1)
                {
                    dataTable = new DataTable("DBRecord");
                    dataSet.Tables.Add(dataTable);
                }
                //
                // Find table name and record count in dataset 
                //
                tableName = dataSet.Tables[0].TableName;      //Table name found
                dataTable = dataSet.Tables[tableName];        //Fill actual table
                RowCount = dataTable.Rows.Count;              //Record count
                //
                // If schema is missing, add columns 
                //
                if (dataTable.Columns.Count < ColCount)
                {
                    for (int i = 0; i < ColCount; i++)
                    {
                        DataColumn colItem = new DataColumn(ColName[i], Type.GetType(ColType[i]));
                        dataTable.Columns.Add(colItem);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
		}

		/// <summary>
		/// Set the Current Record in database
		/// </summary>
		public void Record(Movie rec)
		{
			if (rec == null)
				return;

			ID = rec.ID;
			ImdbURL = rec.ImdbURL;
			Title = rec.Title;
			RunTime = rec.RunTime;
			Year = rec.Year;
			Rating = rec.Rating;
			Gener = rec.Gener;
			Summary = rec.Summary;
			Cast = rec.Cast;
			CurrentVideo = rec.CurrentVideo;
			AllVideo = rec.AllVideo;
			Oscar = rec.Oscar;
			MultipleVideo = rec.MultipleVideo;
			Poster = rec.Poster;
		}
		#region // Insert, Delete, Update \

		public void Insert(Movie rec)
		{
			if (rec == null)
				return;

			//
			// Set the current record 
			//
			Record(rec);
			Insert();
		}
		public void Insert()
		{
			if (dataTable == null) return;
			if (dataTable.Columns.Count == 0) return;

			DataRow _dr = dataTable.NewRow();

			_dr[0] = this.ID;
			_dr[1] = this.ImdbURL;
			_dr[2] = this.Title;
			_dr[3] = this.RunTime;
			_dr[4] = this.Year;
			_dr[5] = this.Rating;
			_dr[6] = this.Gener;
			_dr[7] = this.Summary;
			_dr[8] = this.Cast;
			_dr[9] = this.CurrentVideo;
			_dr[10] = this.AllVideo;
			_dr[11] = this.Oscar;
			_dr[12] = this.MultipleVideo;
			_dr[13] = this.Poster;

			dataTable.Rows.Add(_dr);
			dataSet.AcceptChanges();
		}

		public void Update(Movie rec)
		{
			if (rec == null)
				return;

			//
			// Set the current record 
			//
			Record(rec);
			this.Update(this.ID);
		}
		public void Update(String pkey)
		{
			foreach (DataRow _dr in dataTable.Rows)
			{
				if (_dr[0].ToString().Equals(pkey))
				{
					_dr[0] = this.ID;
					_dr[1] = this.ImdbURL;
					_dr[2] = this.Title;
					_dr[3] = this.RunTime;
					_dr[4] = this.Year;
					_dr[5] = this.Rating;
					_dr[6] = this.Gener;
					_dr[7] = this.Summary;
					_dr[8] = this.Cast;
					_dr[9] = this.CurrentVideo;
					_dr[10] = this.AllVideo;
					_dr[11] = this.Oscar;
					_dr[12] = this.MultipleVideo;
					_dr[13] = this.Poster;
					break;
				}
			}
			dataSet.AcceptChanges();
		}

		/// <summary>
		/// MUST use Save method to update the XML file, else all changes are lost
		/// </summary>
		public void Save()
		{
			dataSet.WriteXml(FilePath);
		}

		public void Delete(Movie rec)
		{
			if (rec == null)
				return;

			//
			// Set the current record 
			//
			Record(rec);
			this.Delete(this.ID);
		}

		public void Delete(String pkey)
		{
			foreach (DataRow _dr in dataTable.Rows)
			{
				if (_dr[0].Equals(pkey))
				{
					dataTable.Rows.Remove(_dr);
					break;
				}
			}
			dataSet.AcceptChanges();
		}

		public void DeleteAll()
		{
			dataSet.Clear();
		}

		#endregion

		private void ConvertItems(DataRow dr)
		{
			Movie rec = new Movie();
			
			rec.ID = Convert.ToString(dr[0]);
			rec.ImdbURL = Convert.ToString(dr[1]);
			rec.Title = Convert.ToString(dr[2]);
			rec.RunTime = Convert.ToString(dr[3]);
			rec.Year = Convert.ToString(dr[4]);
			rec.Rating = Convert.ToString(dr[5]);
			rec.Gener = Convert.ToString(dr[6]);
			rec.Summary = Convert.ToString(dr[7]);
			rec.Cast = Convert.ToString(dr[8]);
			rec.CurrentVideo = Convert.ToString(dr[9]);
			rec.AllVideo = Convert.ToString(dr[10]);
			rec.Oscar = Convert.ToString(dr[11]);
			rec.MultipleVideo = Convert.ToString(dr[12]);
			rec.Poster = Convert.ToString(dr[13]);
			
			Items.Add(rec);
		}

		/// <summary>
		/// Get a list of all records from xml table
		/// </summary>
		public List<Movie> GetAllList()
		{
			Items.Clear();
			try
			{
				foreach (DataRow _dr in dataTable.Rows)
					ConvertItems(_dr);
			}
			catch
			{
				Items.Clear();
			}
			
			RowCount = Items.Count;
			return Items;
		}

		/// <summary>
		/// Get a table of all records from xml table
		/// </summary>
		public DataTable GetAllTable()
		{
			//
			//  Create temp table just like the 'dataTable' of this xml file
			//
			DataTable myTable = new DataTable("temp");
			myTable = dataTable.Clone();
			//
			//  Get a list of all rows (Items holds them)
			//
			this.GetAllList();
			myTable = Items.ToDataTable();                   //Extension method
			return myTable;;
		}

		/// <summary>
		/// Search and get a list of records using a DataTable select criteria.
		/// e.g. "Size >= 230 AND Sex = 'm'" or 
		/// "Date > #1/1/2012#" or 
		/// ColumnName Like 'Abc%'
		/// You can also 'override' this method with your own.
		/// </summary>
		public virtual List<Movie> Search(string selectCriteria)
		{
			Items.Clear();
			try
			{
				foreach (DataRow _dr in dataTable.Select(selectCriteria))
					ConvertItems(_dr);
			}
			catch
			{
				Items.Clear();
			}
			
			RowCount = Items.Count;
			return Items;
		}

		/// <summary>
		/// Find a record by its primary key, if multiple field pkey, then must supply all
		/// </summary>
		public Movie Find(System.String _id)
		{
			Items.Clear();
			string _searchCriteria = "ID = '" + _id.ToString() + "'";
			this.Search(_searchCriteria);
			if (Items.Count <= 0)
				return null;

			//
			// Primary key finds only ONE record
			//
			return Items[0];
		}
	}
 
}
