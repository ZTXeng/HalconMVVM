using HalconDotNet;
using HalconMvvm.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalconMvvm.Model
{
    public class AnalyseModel
    {
        public string FilePath { get; set; }

        public HImage HImage { get; set; }

        public List<HDrawingObjectModel>  HDrawings { get; set; }

        public AnalyseModel()
        {
            HImage = new HImage();
            HDrawings = new List<HDrawingObjectModel>();
        }
    }
}
