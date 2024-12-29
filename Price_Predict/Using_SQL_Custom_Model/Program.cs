using Microsoft.ML;
using Microsoft.ML.Data;
using System.Data;
using System.Data.SqlClient;
using Using_SQL_Custom_Model;

namespace PatternMatching
{
    class Program
    {
        static void Main(string[] args)
        {

            SalaryPredictAsPerYearsOfExperiance.predictSalaryPredictAsPerYearsOfExperiance();
            // Create MLContext
            var context = new MLContext();

            // Load training data

       //     var data = context.Data.LoadFromTextFile<SentimentData>("./sentiment-training-data.tsv", separatorChar: '\t', hasHeader: true);
            
            var data = context.Data.LoadFromEnumerable<SentimentData>(getEmployeeTimeSheet("105"));

            // Define data processing pipeline
            var pipeline = context.Transforms.Text.FeaturizeText("Features", nameof(SentimentData.SentimentText))
                .Append(context.Transforms.Conversion.MapValueToKey("Label", nameof(SentimentData.Sentiment)))
                //.Append(context.Transforms.Conversion.MapKeyToValue("PredictedLabel"))//PredictedLabel"))
                .Append(context.Transforms.Text.NormalizeText("SentimentText", "SentimentText"))
                .Append(context.Transforms.Text.TokenizeIntoWords("Tokens", "SentimentText"))
                .Append(context.Transforms.Text.RemoveDefaultStopWords("Tokens"))
                .Append(context.Transforms.Text.FeaturizeText("Features", "Tokens"))
                .Append(context.Transforms.Conversion.MapValueToKey("Label"));

            // Train the model
            var model = pipeline.Fit(data);

            // Make predictions
            var predictionEngine = context.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(model);
            var sampleStatement = new SentimentData { SentimentText = "2024-12-05" };
            var prediction = predictionEngine.Predict(sampleStatement);

            Console.WriteLine($"Sentiment: {prediction.Sentiment} (0: Negative, 1: Positive)");




        }

        private static List<SentimentData> getEmployeeTimeSheet(string employeeID)
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

            var employeeRoster = new List<SentimentData>();
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                SentimentData student = new SentimentData
                {
                    //  EmployeeID = row["EmployeeID"].ToString(),
                    SentimentText = row["LoginDate"].ToString(),
                    Sentiment = Convert.ToInt32(row["StartTime"].ToString())
                    //  EndTime = row["EndTime"].ToString(),
                };
                employeeRoster.Add(student);
            }

            return employeeRoster;

        }

    }

    public class SentimentPrediction
    {
        [ColumnName("PredictedLabel")]
        public int Sentiment { get; set; }
    }
    public class SentimentData
    {
        public int Sentiment { get; set; }
        public string SentimentText { get; set; }
    }

}


