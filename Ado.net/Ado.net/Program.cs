using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace Ado.net
{
    class Program
    {
        static void Main(string[] args)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //string filepath = @"c:\users\svoto\downloads\cat.jpg";
            //string title = "cats";
            //addimagetodatabase(filepath, title, connectionstring);
            //GetImageFromDatabase(ConnectionString);
            string Univerlab4ConnectionString = ConfigurationManager.ConnectionStrings["SecondConnection"].ConnectionString;
            TestSqlCommandBuilder(Univerlab4ConnectionString);
        }
        static void AddImageToDatabase(string FilePath, string Title, string ConnectionString)
        {
            byte[] ImageData = ReadImage(FilePath);
            if (ImageData != null)
            {
                InsertImage(FilePath, Title, ImageData, ConnectionString);
            }
        }
        static byte[] ReadImage(string FilePath)
        {
            byte[] ImageData = null;
            using (FileStream ReadImage = new FileStream(FilePath, FileMode.Open))
            {
                ImageData = new byte[ReadImage.Length];
                ReadImage.Read(ImageData, 0, ImageData.Length);
            }
            if (ImageData == null)
            {
                Console.WriteLine("Ошибка загрузки изображения");
            }
            return ImageData;
        }
        static void InsertImage(string FilePath, string Title, byte[] ImageData, string ConnectionString)
        {
            string ShortFileName = Path.GetFileName(FilePath);
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string SQLExpression = "Insert into Images(FileName, Title, ImageData) values (@ShortFileName, @Title, @ImageData)";
                SqlCommand command = new SqlCommand(SQLExpression, connection);
                command.Parameters.Add("@ShortFileName", SqlDbType.NVarChar, 50).Value = ShortFileName;
                command.Parameters.Add("@Title", SqlDbType.NVarChar, 50).Value = Title;
                command.Parameters.Add("@ImageData", SqlDbType.Image, byte.MaxValue).Value = ImageData;

                Console.WriteLine("Задействовано строк: " + command.ExecuteNonQuery());
            }
        }
        static void GetImageFromDatabase(string ConnectionString)
        {
            SelectImage(ConnectionString);
        }
        public class Image
        {
            public Image(int id, string filename, string title, byte[] data)
            {
                Id = id;
                FileName = filename;
                Title = title;
                Data = data;
            }
            public int Id { get; private set; }
            public string FileName { get; private set; }
            public string Title { get; private set; }
            public byte[] Data { get; private set; }
        }
        static void SelectImage(string ConnectionString)
        {
            List<Image> images = new List<Image>();
            string SQLExtension = "Select * from Images";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(SQLExtension, connection);
                SqlDataReader sqlData = command.ExecuteReader();
                if (sqlData.HasRows)
                {
                    while (sqlData.Read())
                    {
                        int id = sqlData.GetInt32(0);
                        string FileName = sqlData.GetString(1);
                        string Title = sqlData.GetString(2);
                        byte[] ImageData = (byte[])sqlData.GetValue(3);
                        Image img = new Image(id, FileName, Title, ImageData);
                        images.Add(img);
                    }
                }
            }
            if(images.Count>0)
            {
                string FilePath = @"C:\Users\svoto\Downloads" + images[0].FileName;
                using (FileStream WriteImage = new FileStream(FilePath, FileMode.CreateNew))
                {
                    WriteImage.Write(images[0].Data, 0, images[0].Data.Length);
                    Console.WriteLine("Изображение '{0}' сохранено!", images[0].Title);
                }
            }
        }
        static void TestSqlCommandBuilder(string ConnectionString)
        {
            string SelectString = "SELECT * FROM SUBJECT";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(SelectString, connection);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);

                DataTable dataTable = dataSet.Tables[0];
                DataRow newRow = dataTable.Rows[7];
                dataTable.Rows.Remove(newRow);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(dataAdapter);
                dataAdapter.Update(dataSet);
            }
        }
    }
}
