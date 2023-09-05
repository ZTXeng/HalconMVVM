using HalconDotNet;

public static class MateTemplet
{
    public static void Execute(HTuple hv_WindowHandle,HObject ho_GrayImage,HTuple hv_ModelID)
    {
        var hv_Row = new HTuple();
        HTuple hv_Column = new HTuple(), hv_Radius = new HTuple();
        HTuple  hv_Row1 = new HTuple();
        HTuple hv_Column1 = new HTuple(), hv_Angle = new HTuple();
        HTuple hv_Scale = new HTuple(), hv_Score = new HTuple();

        try
        {
            hv_Row1.Dispose(); hv_Column1.Dispose(); hv_Angle.Dispose(); hv_Scale.Dispose(); hv_Score.Dispose();

            HOperatorSet.FindScaledShapeModel(ho_GrayImage, hv_ModelID, -0.39, 3.14, 0.9,
                1.1, 0.3, 3, 0.5, "least_squares", 0, 0.9, out hv_Row1, out hv_Column1,
                out hv_Angle, out hv_Scale, out hv_Score);

            HOperatorSet.DispCross(hv_WindowHandle, hv_Row1, hv_Column1, 200, 0);

            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                disp_message(hv_WindowHandle, "½á¹û×ø±ê" + hv_Row1, "window", hv_Row1, hv_Column1,
                    "black", "false");
            }
        }
        catch (HalconException HDevExpDefaultException)
        {
            hv_Row.Dispose();
            hv_Column.Dispose();
            hv_Radius.Dispose();
            hv_ModelID.Dispose();
            hv_Row1.Dispose();
            hv_Column1.Dispose();
            hv_Angle.Dispose();
            hv_Scale.Dispose();
            hv_Score.Dispose();

            throw HDevExpDefaultException;
        }
        hv_WindowHandle.Dispose();
        hv_Row.Dispose();
        hv_Column.Dispose();
        hv_Radius.Dispose();
        hv_ModelID.Dispose();
        hv_Row1.Dispose();
        hv_Column1.Dispose();
        hv_Angle.Dispose();
        hv_Scale.Dispose();
        hv_Score.Dispose();

    }

    public static void dev_display_shape_matching_results(HTuple hv_ModelID, HTuple hv_Color,
        HTuple hv_Row, HTuple hv_Column, HTuple hv_Angle, HTuple hv_ScaleR, HTuple hv_ScaleC,
        HTuple hv_Model)
    {
        // Local iconic variables 

        HObject ho_ClutterRegion = null, ho_ModelContours = null;
        HObject ho_ContoursAffinTrans = null, ho_RegionAffineTrans = null;

        // Local control variables 

        HTuple hv_WindowHandle = new HTuple();
        HTuple hv_UseClutter = new HTuple(), hv_UseClutter0 = new HTuple();
        HTuple hv_HomMat2D = new HTuple(), hv_ClutterContrast = new HTuple();
        HTuple hv_Index = new HTuple(), hv_Exception = new HTuple();
        HTuple hv_NumMatches = new HTuple(), hv_GenParamValue = new HTuple();
        HTuple hv_HomMat2DInvert = new HTuple(), hv_Match = new HTuple();
        HTuple hv_HomMat2DTranslate = new HTuple(), hv_HomMat2DCompose = new HTuple();
        HTuple hv_Model_COPY_INP_TMP = new HTuple(hv_Model);
        HTuple hv_ScaleC_COPY_INP_TMP = new HTuple(hv_ScaleC);
        HTuple hv_ScaleR_COPY_INP_TMP = new HTuple(hv_ScaleR);

        // Initialize local and output iconic variables 
        HOperatorSet.GenEmptyObj(out ho_ClutterRegion);
        HOperatorSet.GenEmptyObj(out ho_ModelContours);
        HOperatorSet.GenEmptyObj(out ho_ContoursAffinTrans);
        HOperatorSet.GenEmptyObj(out ho_RegionAffineTrans);
        try
        {
            //This procedure displays the results of Shape-Based Matching.
            //
            //Ensure that the different models have the same use_clutter value.
            //
            //This procedure displays the results on the active graphics window.
            if (HDevWindowStack.IsOpen())
            {
                hv_WindowHandle = HDevWindowStack.GetActive();
            }
            //If no graphics window is currently open, nothing can be displayed.
            if ((int)(new HTuple(hv_WindowHandle.TupleEqual(-1))) != 0)
            {
                ho_ClutterRegion.Dispose();
                ho_ModelContours.Dispose();
                ho_ContoursAffinTrans.Dispose();
                ho_RegionAffineTrans.Dispose();

                hv_Model_COPY_INP_TMP.Dispose();
                hv_ScaleC_COPY_INP_TMP.Dispose();
                hv_ScaleR_COPY_INP_TMP.Dispose();
                hv_WindowHandle.Dispose();
                hv_UseClutter.Dispose();
                hv_UseClutter0.Dispose();
                hv_HomMat2D.Dispose();
                hv_ClutterContrast.Dispose();
                hv_Index.Dispose();
                hv_Exception.Dispose();
                hv_NumMatches.Dispose();
                hv_GenParamValue.Dispose();
                hv_HomMat2DInvert.Dispose();
                hv_Match.Dispose();
                hv_HomMat2DTranslate.Dispose();
                hv_HomMat2DCompose.Dispose();

                return;
            }
            //
            hv_UseClutter.Dispose();
            hv_UseClutter = "false";
            try
            {
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_ClutterRegion.Dispose(); hv_UseClutter0.Dispose(); hv_HomMat2D.Dispose(); hv_ClutterContrast.Dispose();
                    HOperatorSet.GetShapeModelClutter(out ho_ClutterRegion, hv_ModelID.TupleSelect(
                        0), "use_clutter", out hv_UseClutter0, out hv_HomMat2D, out hv_ClutterContrast);
                }
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_ModelID.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        ho_ClutterRegion.Dispose(); hv_UseClutter.Dispose(); hv_HomMat2D.Dispose(); hv_ClutterContrast.Dispose();
                        HOperatorSet.GetShapeModelClutter(out ho_ClutterRegion, hv_ModelID.TupleSelect(
                            hv_Index), "use_clutter", out hv_UseClutter, out hv_HomMat2D, out hv_ClutterContrast);
                    }
                    if ((int)(new HTuple(hv_UseClutter.TupleNotEqual(hv_UseClutter0))) != 0)
                    {
                        throw new HalconException("Shape models are not of the same clutter type");
                    }
                }
            }
            // catch (Exception) 
            catch (HalconException HDevExpDefaultException1)
            {
                HDevExpDefaultException1.ToHTuple(out hv_Exception);
            }
            if ((int)(new HTuple(hv_UseClutter.TupleEqual("true"))) != 0)
            {
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.SetDraw(HDevWindowStack.GetActive(), "margin");
                }
                //For clutter-enabled models, the Color tuple should have either
                //exactly 2 entries, or 2* the number of models. The first color
                //is used for the match and the second for the clutter region,
                //respectively.
                if ((int)((new HTuple((new HTuple(hv_Color.TupleLength())).TupleNotEqual(
                    2 * (new HTuple(hv_ModelID.TupleLength()))))).TupleAnd(new HTuple((new HTuple(hv_Color.TupleLength()
                    )).TupleNotEqual(2)))) != 0)
                {
                    throw new HalconException("Length of Color does not correspond to models with enabled clutter parameters");
                }
            }

            hv_NumMatches.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_NumMatches = new HTuple(hv_Row.TupleLength()
                    );
            }
            if ((int)(new HTuple(hv_NumMatches.TupleGreater(0))) != 0)
            {
                if ((int)(new HTuple((new HTuple(hv_ScaleR_COPY_INP_TMP.TupleLength())).TupleEqual(
                    1))) != 0)
                {
                    {
                        HTuple ExpTmpOutVar_0;
                        HOperatorSet.TupleGenConst(hv_NumMatches, hv_ScaleR_COPY_INP_TMP, out ExpTmpOutVar_0);
                        hv_ScaleR_COPY_INP_TMP.Dispose();
                        hv_ScaleR_COPY_INP_TMP = ExpTmpOutVar_0;
                    }
                }
                if ((int)(new HTuple((new HTuple(hv_ScaleC_COPY_INP_TMP.TupleLength())).TupleEqual(
                    1))) != 0)
                {
                    {
                        HTuple ExpTmpOutVar_0;
                        HOperatorSet.TupleGenConst(hv_NumMatches, hv_ScaleC_COPY_INP_TMP, out ExpTmpOutVar_0);
                        hv_ScaleC_COPY_INP_TMP.Dispose();
                        hv_ScaleC_COPY_INP_TMP = ExpTmpOutVar_0;
                    }
                }
                if ((int)(new HTuple((new HTuple(hv_Model_COPY_INP_TMP.TupleLength())).TupleEqual(
                    0))) != 0)
                {
                    hv_Model_COPY_INP_TMP.Dispose();
                    HOperatorSet.TupleGenConst(hv_NumMatches, 0, out hv_Model_COPY_INP_TMP);
                }
                else if ((int)(new HTuple((new HTuple(hv_Model_COPY_INP_TMP.TupleLength()
                    )).TupleEqual(1))) != 0)
                {
                    {
                        HTuple ExpTmpOutVar_0;
                        HOperatorSet.TupleGenConst(hv_NumMatches, hv_Model_COPY_INP_TMP, out ExpTmpOutVar_0);
                        hv_Model_COPY_INP_TMP.Dispose();
                        hv_Model_COPY_INP_TMP = ExpTmpOutVar_0;
                    }
                }
                //Redirect all display calls to a buffer window and update the
                //graphics window only at the end, to speed up the visualization.
                HOperatorSet.SetWindowParam(hv_WindowHandle, "flush", "false");
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_ModelID.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        ho_ModelContours.Dispose();
                        HOperatorSet.GetShapeModelContours(out ho_ModelContours, hv_ModelID.TupleSelect(
                            hv_Index), 1);
                    }
                    if ((int)(new HTuple(hv_UseClutter.TupleEqual("true"))) != 0)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            ho_ClutterRegion.Dispose(); hv_GenParamValue.Dispose(); hv_HomMat2D.Dispose(); hv_ClutterContrast.Dispose();
                            HOperatorSet.GetShapeModelClutter(out ho_ClutterRegion, hv_ModelID.TupleSelect(
                                hv_Index), new HTuple(), out hv_GenParamValue, out hv_HomMat2D, out hv_ClutterContrast);
                        }
                        hv_HomMat2DInvert.Dispose();
                        HOperatorSet.HomMat2dInvert(hv_HomMat2D, out hv_HomMat2DInvert);
                    }
                    if (HDevWindowStack.IsOpen())
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            HOperatorSet.SetColor(HDevWindowStack.GetActive(), hv_Color.TupleSelect(
                                hv_Index % (new HTuple(hv_Color.TupleLength()))));
                        }
                    }
                    HTuple end_val56 = hv_NumMatches - 1;
                    HTuple step_val56 = 1;
                    for (hv_Match = 0; hv_Match.Continue(end_val56, step_val56); hv_Match = hv_Match.TupleAdd(step_val56))
                    {
                        if ((int)(new HTuple(hv_Index.TupleEqual(hv_Model_COPY_INP_TMP.TupleSelect(
                            hv_Match)))) != 0)
                        {
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_HomMat2DTranslate.Dispose();
                                get_hom_mat2d_from_matching_result(hv_Row.TupleSelect(hv_Match), hv_Column.TupleSelect(
                                    hv_Match), hv_Angle.TupleSelect(hv_Match), hv_ScaleR_COPY_INP_TMP.TupleSelect(
                                    hv_Match), hv_ScaleC_COPY_INP_TMP.TupleSelect(hv_Match), out hv_HomMat2DTranslate);
                            }
                            ho_ContoursAffinTrans.Dispose();
                            HOperatorSet.AffineTransContourXld(ho_ModelContours, out ho_ContoursAffinTrans,
                                hv_HomMat2DTranslate);
                            if ((int)(new HTuple(hv_UseClutter.TupleEqual("true"))) != 0)
                            {
                                hv_HomMat2DCompose.Dispose();
                                HOperatorSet.HomMat2dCompose(hv_HomMat2DTranslate, hv_HomMat2DInvert,
                                    out hv_HomMat2DCompose);
                                ho_RegionAffineTrans.Dispose();
                                HOperatorSet.AffineTransRegion(ho_ClutterRegion, out ho_RegionAffineTrans,
                                    hv_HomMat2DCompose, "constant");
                                if ((int)(new HTuple((new HTuple(hv_Color.TupleLength())).TupleEqual(
                                    2))) != 0)
                                {
                                    if (HDevWindowStack.IsOpen())
                                    {
                                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                        {
                                            HOperatorSet.SetColor(HDevWindowStack.GetActive(), hv_Color.TupleSelect(
                                                1));
                                        }
                                    }
                                    if (HDevWindowStack.IsOpen())
                                    {
                                        HOperatorSet.DispObj(ho_RegionAffineTrans, HDevWindowStack.GetActive()
                                            );
                                    }
                                    if (HDevWindowStack.IsOpen())
                                    {
                                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                        {
                                            HOperatorSet.SetColor(HDevWindowStack.GetActive(), hv_Color.TupleSelect(
                                                0));
                                        }
                                    }
                                }
                                else
                                {
                                    if (HDevWindowStack.IsOpen())
                                    {
                                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                        {
                                            HOperatorSet.SetColor(HDevWindowStack.GetActive(), hv_Color.TupleSelect(
                                                (hv_Index * 2) + 1));
                                        }
                                    }
                                    if (HDevWindowStack.IsOpen())
                                    {
                                        HOperatorSet.DispObj(ho_RegionAffineTrans, HDevWindowStack.GetActive()
                                            );
                                    }
                                    if (HDevWindowStack.IsOpen())
                                    {
                                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                        {
                                            HOperatorSet.SetColor(HDevWindowStack.GetActive(), hv_Color.TupleSelect(
                                                hv_Index * 2));
                                        }
                                    }
                                }
                            }
                            if (HDevWindowStack.IsOpen())
                            {
                                HOperatorSet.DispObj(ho_ContoursAffinTrans, HDevWindowStack.GetActive()
                                    );
                            }
                        }
                    }
                }
                //Copy the content of the buffer window to the graphics window.
                HOperatorSet.SetWindowParam(hv_WindowHandle, "flush", "true");
                HOperatorSet.FlushBuffer(hv_WindowHandle);
            }
            ho_ClutterRegion.Dispose();
            ho_ModelContours.Dispose();
            ho_ContoursAffinTrans.Dispose();
            ho_RegionAffineTrans.Dispose();

            hv_Model_COPY_INP_TMP.Dispose();
            hv_ScaleC_COPY_INP_TMP.Dispose();
            hv_ScaleR_COPY_INP_TMP.Dispose();
            hv_WindowHandle.Dispose();
            hv_UseClutter.Dispose();
            hv_UseClutter0.Dispose();
            hv_HomMat2D.Dispose();
            hv_ClutterContrast.Dispose();
            hv_Index.Dispose();
            hv_Exception.Dispose();
            hv_NumMatches.Dispose();
            hv_GenParamValue.Dispose();
            hv_HomMat2DInvert.Dispose();
            hv_Match.Dispose();
            hv_HomMat2DTranslate.Dispose();
            hv_HomMat2DCompose.Dispose();

            return;
        }
        catch (HalconException HDevExpDefaultException)
        {
            ho_ClutterRegion.Dispose();
            ho_ModelContours.Dispose();
            ho_ContoursAffinTrans.Dispose();
            ho_RegionAffineTrans.Dispose();

            hv_Model_COPY_INP_TMP.Dispose();
            hv_ScaleC_COPY_INP_TMP.Dispose();
            hv_ScaleR_COPY_INP_TMP.Dispose();
            hv_WindowHandle.Dispose();
            hv_UseClutter.Dispose();
            hv_UseClutter0.Dispose();
            hv_HomMat2D.Dispose();
            hv_ClutterContrast.Dispose();
            hv_Index.Dispose();
            hv_Exception.Dispose();
            hv_NumMatches.Dispose();
            hv_GenParamValue.Dispose();
            hv_HomMat2DInvert.Dispose();
            hv_Match.Dispose();
            hv_HomMat2DTranslate.Dispose();
            hv_HomMat2DCompose.Dispose();

            throw HDevExpDefaultException;
        }
    }

    public static void disp_message(HTuple hv_WindowHandle, HTuple hv_String, HTuple hv_CoordSystem,
        HTuple hv_Row, HTuple hv_Column, HTuple hv_Color, HTuple hv_Box)
    {


        HTuple hv_GenParamName = new HTuple(), hv_GenParamValue = new HTuple();
        HTuple hv_Color_COPY_INP_TMP = new HTuple(hv_Color);
        HTuple hv_Column_COPY_INP_TMP = new HTuple(hv_Column);
        HTuple hv_CoordSystem_COPY_INP_TMP = new HTuple(hv_CoordSystem);
        HTuple hv_Row_COPY_INP_TMP = new HTuple(hv_Row);

        // Initialize local and output iconic variables 
        try
        {
            //This procedure displays text in a graphics window.
            //
            //Input parameters:
            //WindowHandle: The WindowHandle of the graphics window, where
            //   the message should be displayed
            //String: A tuple of strings containing the text message to be displayed
            //CoordSystem: If set to 'window', the text position is given
            //   with respect to the window coordinate system.
            //   If set to 'image', image coordinates are used.
            //   (This may be useful in zoomed images.)
            //Row: The row coordinate of the desired text position
            //   A tuple of values is allowed to display text at different
            //   positions.
            //Column: The column coordinate of the desired text position
            //   A tuple of values is allowed to display text at different
            //   positions.
            //Color: defines the color of the text as string.
            //   If set to [], '' or 'auto' the currently set color is used.
            //   If a tuple of strings is passed, the colors are used cyclically...
            //   - if |Row| == |Column| == 1: for each new textline
            //   = else for each text position.
            //Box: If Box[0] is set to 'true', the text is written within an orange box.
            //     If set to' false', no box is displayed.
            //     If set to a color string (e.g. 'white', '#FF00CC', etc.),
            //       the text is written in a box of that color.
            //     An optional second value for Box (Box[1]) controls if a shadow is displayed:
            //       'true' -> display a shadow in a default color
            //       'false' -> display no shadow
            //       otherwise -> use given string as color string for the shadow color
            //
            //It is possible to display multiple text strings in a single call.
            //In this case, some restrictions apply:
            //- Multiple text positions can be defined by specifying a tuple
            //  with multiple Row and/or Column coordinates, i.e.:
            //  - |Row| == n, |Column| == n
            //  - |Row| == n, |Column| == 1
            //  - |Row| == 1, |Column| == n
            //- If |Row| == |Column| == 1,
            //  each element of String is display in a new textline.
            //- If multiple positions or specified, the number of Strings
            //  must match the number of positions, i.e.:
            //  - Either |String| == n (each string is displayed at the
            //                          corresponding position),
            //  - or     |String| == 1 (The string is displayed n times).
            //
            //
            //Convert the parameters for disp_text.
            if ((int)((new HTuple(hv_Row_COPY_INP_TMP.TupleEqual(new HTuple()))).TupleOr(
                new HTuple(hv_Column_COPY_INP_TMP.TupleEqual(new HTuple())))) != 0)
            {

                hv_Color_COPY_INP_TMP.Dispose();
                hv_Column_COPY_INP_TMP.Dispose();
                hv_CoordSystem_COPY_INP_TMP.Dispose();
                hv_Row_COPY_INP_TMP.Dispose();
                hv_GenParamName.Dispose();
                hv_GenParamValue.Dispose();

                return;
            }
            if ((int)(new HTuple(hv_Row_COPY_INP_TMP.TupleEqual(-1))) != 0)
            {
                hv_Row_COPY_INP_TMP.Dispose();
                hv_Row_COPY_INP_TMP = 12;
            }
            if ((int)(new HTuple(hv_Column_COPY_INP_TMP.TupleEqual(-1))) != 0)
            {
                hv_Column_COPY_INP_TMP.Dispose();
                hv_Column_COPY_INP_TMP = 12;
            }
            //
            //Convert the parameter Box to generic parameters.
            hv_GenParamName.Dispose();
            hv_GenParamName = new HTuple();
            hv_GenParamValue.Dispose();
            hv_GenParamValue = new HTuple();
            if ((int)(new HTuple((new HTuple(hv_Box.TupleLength())).TupleGreater(0))) != 0)
            {
                if ((int)(new HTuple(((hv_Box.TupleSelect(0))).TupleEqual("false"))) != 0)
                {
                    //Display no box
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_GenParamName = hv_GenParamName.TupleConcat(
                                "box");
                            hv_GenParamName.Dispose();
                            hv_GenParamName = ExpTmpLocalVar_GenParamName;
                        }
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_GenParamValue = hv_GenParamValue.TupleConcat(
                                "false");
                            hv_GenParamValue.Dispose();
                            hv_GenParamValue = ExpTmpLocalVar_GenParamValue;
                        }
                    }
                }
                else if ((int)(new HTuple(((hv_Box.TupleSelect(0))).TupleNotEqual(
                    "true"))) != 0)
                {
                    //Set a color other than the default.
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_GenParamName = hv_GenParamName.TupleConcat(
                                "box_color");
                            hv_GenParamName.Dispose();
                            hv_GenParamName = ExpTmpLocalVar_GenParamName;
                        }
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_GenParamValue = hv_GenParamValue.TupleConcat(
                                hv_Box.TupleSelect(0));
                            hv_GenParamValue.Dispose();
                            hv_GenParamValue = ExpTmpLocalVar_GenParamValue;
                        }
                    }
                }
            }
            if ((int)(new HTuple((new HTuple(hv_Box.TupleLength())).TupleGreater(1))) != 0)
            {
                if ((int)(new HTuple(((hv_Box.TupleSelect(1))).TupleEqual("false"))) != 0)
                {
                    //Display no shadow.
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_GenParamName = hv_GenParamName.TupleConcat(
                                "shadow");
                            hv_GenParamName.Dispose();
                            hv_GenParamName = ExpTmpLocalVar_GenParamName;
                        }
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_GenParamValue = hv_GenParamValue.TupleConcat(
                                "false");
                            hv_GenParamValue.Dispose();
                            hv_GenParamValue = ExpTmpLocalVar_GenParamValue;
                        }
                    }
                }
                else if ((int)(new HTuple(((hv_Box.TupleSelect(1))).TupleNotEqual(
                    "true"))) != 0)
                {
                    //Set a shadow color other than the default.
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_GenParamName = hv_GenParamName.TupleConcat(
                                "shadow_color");
                            hv_GenParamName.Dispose();
                            hv_GenParamName = ExpTmpLocalVar_GenParamName;
                        }
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_GenParamValue = hv_GenParamValue.TupleConcat(
                                hv_Box.TupleSelect(1));
                            hv_GenParamValue.Dispose();
                            hv_GenParamValue = ExpTmpLocalVar_GenParamValue;
                        }
                    }
                }
            }
            //Restore default CoordSystem behavior.
            if ((int)(new HTuple(hv_CoordSystem_COPY_INP_TMP.TupleNotEqual("window"))) != 0)
            {
                hv_CoordSystem_COPY_INP_TMP.Dispose();
                hv_CoordSystem_COPY_INP_TMP = "image";
            }
            //
            if ((int)(new HTuple(hv_Color_COPY_INP_TMP.TupleEqual(""))) != 0)
            {
                //disp_text does not accept an empty string for Color.
                hv_Color_COPY_INP_TMP.Dispose();
                hv_Color_COPY_INP_TMP = new HTuple();
            }
            //
            HOperatorSet.DispText(hv_WindowHandle, hv_String, hv_CoordSystem_COPY_INP_TMP,
                hv_Row_COPY_INP_TMP, hv_Column_COPY_INP_TMP, hv_Color_COPY_INP_TMP, hv_GenParamName,
                hv_GenParamValue);

            hv_Color_COPY_INP_TMP.Dispose();
            hv_Column_COPY_INP_TMP.Dispose();
            hv_CoordSystem_COPY_INP_TMP.Dispose();
            hv_Row_COPY_INP_TMP.Dispose();
            hv_GenParamName.Dispose();
            hv_GenParamValue.Dispose();

            return;
        }
        catch (HalconException HDevExpDefaultException)
        {

            hv_Color_COPY_INP_TMP.Dispose();
            hv_Column_COPY_INP_TMP.Dispose();
            hv_CoordSystem_COPY_INP_TMP.Dispose();
            hv_Row_COPY_INP_TMP.Dispose();
            hv_GenParamName.Dispose();
            hv_GenParamValue.Dispose();

            throw HDevExpDefaultException;
        }
    }

    public static void get_hom_mat2d_from_matching_result(HTuple hv_Row, HTuple hv_Column,
        HTuple hv_Angle, HTuple hv_ScaleR, HTuple hv_ScaleC, out HTuple hv_HomMat2D)
    {



        // Local control variables 

        HTuple hv_HomMat2DIdentity = new HTuple();
        HTuple hv_HomMat2DScale = new HTuple(), hv_HomMat2DRotate = new HTuple();
        // Initialize local and output iconic variables 
        hv_HomMat2D = new HTuple();
        try
        {
            //This procedure calculates the transformation matrix for the model contours
            //from the results of Shape-Based Matching.
            //
            hv_HomMat2DIdentity.Dispose();
            HOperatorSet.HomMat2dIdentity(out hv_HomMat2DIdentity);
            hv_HomMat2DScale.Dispose();
            HOperatorSet.HomMat2dScale(hv_HomMat2DIdentity, hv_ScaleR, hv_ScaleC, 0, 0,
                out hv_HomMat2DScale);
            hv_HomMat2DRotate.Dispose();
            HOperatorSet.HomMat2dRotate(hv_HomMat2DScale, hv_Angle, 0, 0, out hv_HomMat2DRotate);
            hv_HomMat2D.Dispose();
            HOperatorSet.HomMat2dTranslate(hv_HomMat2DRotate, hv_Row, hv_Column, out hv_HomMat2D);

            hv_HomMat2DIdentity.Dispose();
            hv_HomMat2DScale.Dispose();
            hv_HomMat2DRotate.Dispose();

            return;


        }
        catch (HalconException HDevExpDefaultException)
        {

            hv_HomMat2DIdentity.Dispose();
            hv_HomMat2DScale.Dispose();
            hv_HomMat2DRotate.Dispose();

            throw HDevExpDefaultException;
        }
    }


    
}


