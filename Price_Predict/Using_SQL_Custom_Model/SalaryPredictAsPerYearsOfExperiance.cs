using Microsoft.ML.Data;
using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Using_SQL_Custom_Model
{
    internal class SalaryPredictAsPerYearsOfExperiance
    {
       public static void predictSalaryPredictAsPerYearsOfExperiance()
        {
            List<ModelInput> list = getEmployeeSalary();

            var context = new MLContext();

            IDataView traingdata = context.Data.LoadFromEnumerable(list);

            var estimator = context.Transforms.Concatenate("Features", new[] { "YearsOfExperience" });

            var pipeline = estimator.Append(context.Regression.Trainers.Sdca(labelColumnName: "Salary", maximumNumberOfIterations: 100));

            var model = pipeline.Fit(traingdata);

            var predictionEngine = context.Model.CreatePredictionEngine<ModelInput, ModelOutput>(model);

            var sampleStatement = new ModelInput { YearsOfExperience = 10 };

            var prediction = predictionEngine.Predict(sampleStatement);

            Console.WriteLine($"estimated salary as per {sampleStatement.YearsOfExperience} is : {prediction.Score}");

        }
        private static List<ModelInput> getEmployeeSalary()
        {
            List<ModelInput> modelInputs = new List<ModelInput>();

            modelInputs.Add(new ModelInput { YearsOfExperience = 37, Salary = 24000 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 35, Salary = 17000 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 31, Salary = 17000 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 34, Salary = 9000 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 33, Salary = 6000 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 27, Salary = 4800 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 26, Salary = 4800 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 25, Salary = 4200 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 30, Salary = 12000 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 30, Salary = 9000 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 27, Salary = 8200 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 27, Salary = 7700 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 26, Salary = 7800 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 25, Salary = 6900 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 30, Salary = 11000 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 29, Salary = 3100 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 27, Salary = 2900 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 27, Salary = 2800 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 26, Salary = 2600 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 25, Salary = 2500 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 28, Salary = 8000 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 27, Salary = 8200 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 29, Salary = 7900 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 27, Salary = 6500 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 26, Salary = 2700 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 28, Salary = 14000 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 27, Salary = 13500 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 26, Salary = 8600 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 26, Salary = 8400 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 25, Salary = 7000 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 24, Salary = 6200 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 28, Salary = 4000 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 27, Salary = 3900 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 37, Salary = 4400 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 28, Salary = 13000 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 27, Salary = 6000 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 30, Salary = 6500 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 30, Salary = 10000 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 30, Salary = 12000 });
            modelInputs.Add(new ModelInput { YearsOfExperience = 30, Salary = 8300 });

            return modelInputs;
        }
        private static List<ModelInput> getEmployeeTimeSheet(string employeeID)
        {

            DataSet dataSet = new DataSet();
            // throw new NotImplementedException();
            string connectionString = "Data Source=DESKTOP-568QRFQ;Initial Catalog=ML;Integrated Security=True";

            //string queryString = "  SELECT EmployeeID, CAST(LoginDate as VARCHAR) AS LoginDate,StartTime,EndTime FROM EmployeeTimeSheet where EmployeeID='" + employeeID + "'";
            string queryString = " SELECT DATEDIFF(YEAR, [hire_date], GETDATE()) AS YearsOfExperience, [salary] AS Salary FROM [ML].[dbo].[employees] ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {


                    DataTable dataTable = new DataTable();
                    dataSet.Tables.Add(dataTable);

                    dataTable.Load(reader);
                    reader.Close();

                }
            }

            var employeeRoster = new List<ModelInput>();
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                ModelInput student = new ModelInput
                {
                    //  EmployeeID = row["EmployeeID"].ToString(),
                    YearsOfExperience = float.Parse(row["YearsOfExperience"].ToString()),
                    Salary = float.Parse(row["Salary"].ToString())
                    //  EndTime = row["EndTime"].ToString(),
                };
                employeeRoster.Add(student);
            }

            return employeeRoster;

        }

    }

    public class ModelInput
    {
        [ColumnName("YearsOfExperience"), LoadColumn(0)]
        public float YearsOfExperience { get; set; }


        [ColumnName("Salary"), LoadColumn(1)]
        public float Salary { get; set; }

    }

    public class ModelOutput
    {
        public float Score { get; set; }
    }
}



