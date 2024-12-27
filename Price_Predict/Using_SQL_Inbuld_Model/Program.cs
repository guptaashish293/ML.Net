using System;
using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Using_SQL_Inbuld_Model;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ML;
using static Using_SQL_Inbuld_Model.PredictEmployeeLogInTime;
using System.Xml.Linq;
using Microsoft.ML.Trainers.LightGbm;
using System.Collections.Immutable;
using Microsoft.ML.Data;

namespace PatternMatching
{
    class Program
    {
        static void Main(string[] args)
        {

            //LoadWithOutModelBuilder("100");

            var predictEmployeeLogInTime = new PredictEmployeeLogInTime.ModelInput()
            {
                EmployeeID = 105, // change value
                LoginDate = DateTime.Parse("05-12-2024"),
                //  EndTime = 75549,
            };


            //Load model and predict output
            var StartTimePredictResult = PredictEmployeeLogInTime.Predict(predictEmployeeLogInTime);


            Console.WriteLine(Convert.ToInt64(StartTimePredictResult.Score));




        }

        private static void LoadWithOutModelBuilder(string employeeID)
        {
            DataSet dataSet = new DataSet();
            // throw new NotImplementedException();
            string connectionString = "Data Source=DESKTOP-568QRFQ;Initial Catalog=ML;Integrated Security=True";

            string queryString = "  SELECT EmployeeID, CAST(LoginDate as VARCHAR) AS LoginDate,StartTime,EndTime FROM EmployeeTimeSheet where EmployeeID='" + employeeID + "'";

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

            var employeeRoster = new List<EmployeeRoster>();
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                EmployeeRoster student = new EmployeeRoster
                {
                    EmployeeID = row["EmployeeID"].ToString(),
                    LoginDate = row["LoginDate"].ToString(),
                    StartTime = row["StartTime"].ToString(),
                    EndTime = row["EndTime"].ToString(),
                };
                employeeRoster.Add(student);
            }

            var mlContext = new MLContext();
            var dataView = mlContext.Data.LoadFromEnumerable<EmployeeRoster>(employeeRoster.ToImmutableList());
            var previewdata = dataView.Preview();
            //var pipeline = mlContext.Transforms.ReplaceMissingValues(new[] { new InputOutputColumnPair(@"EmployeeID", @"EmployeeID"), new InputOutputColumnPair(@"EndTime", @"EndTime") })
            //                        .Append(mlContext.Transforms.Conversion.ConvertType(@"LoginDate", @"LoginDate"))
            //                        .Append(mlContext.Transforms.Concatenate(@"Features", new[] { @"EmployeeID", @"EndTime", @"LoginDate" }))
            //                        .Append(mlContext.Regression.Trainers.LightGbm(new LightGbmRegressionTrainer.Options()
            //                        {
            //                            NumberOfLeaves = 26,
            //                            NumberOfIterations = 4,
            //                            MinimumExampleCountPerLeaf = 20,
            //                            LearningRate = 0.10726258019889,
            //                            LabelColumnName = @"StartTime",
            //                            FeatureColumnName = @"Features",
            //                            ExampleWeightColumnName = null,
            //                            Booster = new GradientBooster.Options()
            //                            {
            //                                SubsampleFraction = 0.999999776672986,
            //                                FeatureFraction = 0.99999999,
            //                                L1Regularization = 3.55466288954644E-07,
            //                                L2Regularization = 0.309025130817048
            //                            },
            //                            MaximumBinCountPerFeature = 516
            //                        }));

            var pipeline = mlContext.Transforms.SelectColumns(new[] { @"EmployeeID", @"EndTime", @"LoginDate" })
                                    .Append(mlContext.Regression.Trainers.LightGbm(new LightGbmRegressionTrainer.Options()
                                    {
                                        NumberOfLeaves = 26,
                                        NumberOfIterations = 4,
                                        MinimumExampleCountPerLeaf = 20,
                                        LearningRate = 0.10726258019889,
                                        LabelColumnName = @"StartTime",
                                        FeatureColumnName = @"Features",
                                        ExampleWeightColumnName = null,
                                        Booster = new GradientBooster.Options()
                                        {
                                            SubsampleFraction = 0.999999776672986,
                                            FeatureFraction = 0.99999999,
                                            L1Regularization = 3.55466288954644E-07,
                                            L2Regularization = 0.309025130817048
                                        },
                                        MaximumBinCountPerFeature = 516
                                    }));




            var model = pipeline.Fit(dataView);
            var predEngine = mlContext.Model.CreatePredictionEngine<string, object>(model);

            var predictEmployeeLogInTime = new PredictEmployeeLogInTime.ModelInput()
            {
                LoginDate = DateTime.Parse("05-12-2024"),
                //  EndTime = 75549,
            };
            var result = predEngine.Predict("05-12-2024");


            foreach (var item in dataSet.Tables[0].Rows)
            {

            }

        }
    }

    public class EmployeeRoster
    {
        [ColumnName(@"EmployeeID")]
        public string EmployeeID { get; set; }

        [ColumnName(@"LoginDate")]
        public string LoginDate { get; set; }

        [ColumnName(@"StartTime")]
        public string StartTime { get; set; }

        [ColumnName(@"EndTime")]
        public string EndTime { get; set; }

    }
}