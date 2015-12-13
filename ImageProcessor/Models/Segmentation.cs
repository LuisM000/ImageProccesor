using Accord.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessor.Models
{
    public class Segmentation
    {
        public enum Segmentations {Kmodes,Kmeans,MeanShift};

        public Segmentations TypeSegmentation { get; set; }
        public int Clusters{get;set;}
        public double Tolerance {get;set;}
        public string TypeDistance { get; set; }
    }
}
