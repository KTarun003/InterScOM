// This file was auto-generated by ML.NET Model Builder. 

using Microsoft.ML.Data;

namespace InterScOMML.Model
{
    public class ModelInput
    {
        [ColumnName("StudentId"), LoadColumn(0)]
        public float StudentId { get; set; }


        [ColumnName("AnnualIncome"), LoadColumn(1)]
        public float AnnualIncome { get; set; }


        [ColumnName("CGPA"), LoadColumn(2)]
        public float CGPA { get; set; }


        [ColumnName("Fees"), LoadColumn(3)]
        public float Fees { get; set; }


    }
}
