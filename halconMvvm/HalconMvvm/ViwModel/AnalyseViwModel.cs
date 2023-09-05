using CommunityToolkit.Mvvm.Input;
using HalconDotNet;
using HalconMvvm.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;

namespace HalconMvvm.ViwModel
{
    public class AnalyseViwModel:ViewModelBase<AnalyseModel>
    {
        HWindow hWindow ;

        public IRelayCommand<HSmartWindowControlWPF> FileSelect { get; set; }

        public IRelayCommand DrawCircle { get; set; }

        public IRelayCommand DrawTrangle { get; set; }

        public IRelayCommand Mate { get; set; }

        public AnalyseViwModel()
        {
            Model = new AnalyseModel();

            FileSelect = new RelayCommand<HSmartWindowControlWPF>(OnFileSelect);
            DrawCircle = new RelayCommand(OnDrawCircle);
            DrawTrangle = new RelayCommand(OnDrawTrangle);
            Mate = new RelayCommand(OnMate);
        }

        private void OnMate()
        {
            var htuple = Model.HDrawings[0].HTuples;

            HObject hoShap;

            if (htuple.Count() == 3)
            {
                HOperatorSet.GenCircle(out hoShap, htuple[0], htuple[1], htuple[2]);
            }
           else
            {
                HOperatorSet.GenRectangle1(out hoShap, htuple[0], htuple[1], htuple[2], htuple[3]);
            }

            if (hoShap != null)
            {
                HOperatorSet.ReduceDomain(Model.HImage,hoShap,out var imageReduced);
                HOperatorSet.CreateScaledShapeModel(imageReduced, "auto", -.39, 3.15, "auto"
               , 0.9, 1.1, "auto", "auto", "use_polarity", "auto", "auto", out var hv_ModelID);

                MateTemplet.Execute(hWindow, Model.HImage, hv_ModelID);
            }
        }

        private void OnDrawTrangle()
        {
            var htuples = new HTuple[] { 100, 100, 150,200 };
            var drawingObject = HDrawingObject.CreateDrawingObject(HDrawingObject.HDrawingObjectType.RECTANGLE1
                , htuples);

            hWindow.AttachDrawingObjectToWindow(drawingObject);

            Model.HDrawings.Add(new DataModel.HDrawingObjectModel()
            {
                HTuples = htuples,
                HDrawingObject = drawingObject
            });

            drawingObject.OnDrag(OnHDrawingDrag);
            drawingObject.OnResize(OnHDrawingResize);
        }

        private void OnDrawCircle()
        {
            var htuples = new HTuple[] { 300, 300, 50 };

            var drawingObject = HDrawingObject.CreateDrawingObject(HDrawingObject.HDrawingObjectType.CIRCLE
                , htuples);

            drawingObject.OnDrag(OnHDrawingDrag);
            drawingObject.OnResize(OnHDrawingResize);

            Model.HDrawings.Add(new DataModel.HDrawingObjectModel()
            {
                HTuples = htuples,
                 HDrawingObject = drawingObject
            });

            hWindow.AttachDrawingObjectToWindow(drawingObject);
        }

        private void OnFileSelect(HSmartWindowControlWPF ob)
        {
            hWindow = ob.HalconWindow;
            var dialog = new OpenFileDialog();
            dialog.Filter = "图片|*.gif;*.jpg;*.jpeg;*.bmp;*.jfif;*.png;";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Model.FilePath = dialog.FileName;
                Model.HImage.ReadImage(Model.FilePath);

                Model.HImage.GetImageSize(out int width, out var height);
                hWindow.SetPart(0, 0, height, width);
                hWindow.DispObj(Model.HImage); 
            }
        }


        public void OnHDrawingDrag(HDrawingObject drawid, HWindow window, string type)
        {
            RefershElement(drawid);
        }

        public void OnHDrawingResize(HDrawingObject drawid, HWindow window, string type)
        {
            RefershElement(drawid);
        }

        private void RefershElement(HDrawingObject drawid)
        {
            try
            {
                var radius = drawid.GetDrawingObjectParams("radius");
                var htuples = new HTuple[3];
                htuples[0] = drawid.GetDrawingObjectParams("row");
                htuples[1] = drawid.GetDrawingObjectParams("column");
                htuples[2] = drawid.GetDrawingObjectParams("radius");
                var drawobj = Model.HDrawings.FirstOrDefault(x => x.HDrawingObject.ID.Equals(drawid.ID));
                if (drawobj != null)
                {
                    drawobj.HTuples = htuples;
                }
            }
            catch (Exception)
            {
                var htuples = new HTuple[4];
                htuples[0] = drawid.GetDrawingObjectParams("row1");
                htuples[1] = drawid.GetDrawingObjectParams("column1");
                htuples[2] = drawid.GetDrawingObjectParams("row2");
                htuples[3] = drawid.GetDrawingObjectParams("column2");
                var drawobj = Model.HDrawings.FirstOrDefault(x => x.HDrawingObject.ID.Equals(drawid.ID));
                if (drawobj != null)
                {
                    drawobj.HTuples = htuples;
                }
            }
            
        }
    }
}
