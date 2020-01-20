using System.Collections.Generic;

namespace DigitalMedicine.Models
{
    public class DataChart
    {
        public DataChart()
        {
            datasets = new List<Dataset>();
            labels = new List<string>();
        }
        public class Dataset
        {
            public Dataset()
            {
                data = new List<double>();
                backgroundColor = new List<string>();
                borderColor = new List<string>();
                borderWidth = 1;
            }

            public string label { get; set; }
            public List<double> data { get; set; }
            public List<string> backgroundColor { get; set; }
            public List<string> borderColor { get; set; }
            public int borderWidth { get; set; }
        }

        public List<string> labels{ get; set; }
        public List<Dataset> datasets { get; set; }
    }
}