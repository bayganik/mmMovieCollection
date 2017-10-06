//
// Data Access Layer (DAL) class for XML files that are structured as :
//
//      <NewDataSet>                    <<--  Database Element
//          <Table>                     <<--  Table Element
//              <val0>data 0</val0>     <<--  DB Fields[0]
//              <val1>data 1</val1>     <<--  DB Fields[1]
//              <val2>data 3</val2>     <<--  DB Fields[2]
//          </Table>
//      </NewDataSet>
//
//     DBLayer directly communicates with database using connection string, typically in the app.config
//     file.  The file will have a definition for "db" string.
//     Created: 6/19/2013 00:00:00
//
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System;
using System.Xml;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Media.Imaging;
//
// Used in ToDataTable converter
//
using System.Reflection;
using System.ComponentModel;

namespace mmMovieCollection.Model
{
    public class DBLayer
    {
        public static string Connection;
        public static string DBPosters;
        public static string MovieFiles;

        public static Movie AddMovie;
        public static BitmapImage AddImage;
    }

}