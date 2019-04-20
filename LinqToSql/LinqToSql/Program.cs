using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Configuration;
using System.Reflection;

namespace LinqToSql
{
    class Program
    {
       static string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        static void Main(string[] args)
        {
            DataContext dbContext = new DataContext(ConnectionString);

            Table<Student> students = dbContext.GetTable<Student>();
            Console.WriteLine("Until update:");
            foreach (var student in students)
            {
                Console.WriteLine("{0} \t{1} \t{2} \t{3}", student.Id, student.GroupId, student.Name, student.BirthDay.ToShortDateString());
            }
            Student stud = students.FirstOrDefault();
            stud.Name = "Upd424141ated";
            dbContext.SubmitChanges();
            Console.WriteLine("After update:");
            foreach (var student in students)
            {
                Console.WriteLine("{0} \t{1} \t{2} \t{3}", student.Id, student.GroupId, student.Name, student.BirthDay.ToShortDateString());
            }

            StudentDataContext StudentdbContext = new StudentDataContext(ConnectionString);
            int MinGroupId = 0, MaxGroupId = 0;
            StudentdbContext.GetGroupRange(ref MinGroupId, ref MaxGroupId);
            Console.WriteLine("Min: " + MinGroupId);
            Console.WriteLine("Max: " + MaxGroupId);
        }
        [Table(Name ="STUDENT")]
        public class Student
        {
            [Column(IsPrimaryKey = true, IsDbGenerated = true)]
            public int Id { get; set; }
            [Column(Name="GROUPID")]
            public int GroupId { get; set; }
            [Column(Name = "NAME")]
            public string Name { get; set; }
            [Column(Name = "BDAY")]
            public DateTime BirthDay { get; set; }
        }

        public class StudentDataContext : DataContext
        {
            public StudentDataContext(string ConnectionString) : base(ConnectionString)
            {
            }
            public Table<Student> Students { get => GetTable<Student>(); }
            [Function(Name ="SP_GetGroupIdRange")]
            [return: Parameter(DbType ="Int")]
            public int GetGroupRange([Parameter(Name ="MinGroupId",DbType ="Int")] ref int MinGroupId,
                                     [Parameter(Name ="MaxGroupId", DbType ="Int")] ref int MaxGroupId)
            {
                IExecuteResult result = ExecuteMethodCall(this, (MethodInfo)MethodInfo.GetCurrentMethod(), MinGroupId, MaxGroupId);
                MinGroupId = (int)result.GetParameterValue(0);
                MaxGroupId = (int)result.GetParameterValue(1);
                return (int)result.ReturnValue;
            }
        }
    }
}
