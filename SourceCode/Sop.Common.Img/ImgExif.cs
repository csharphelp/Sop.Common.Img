using System;
using System.DrawingCore;
using System.DrawingCore.Imaging;
using System.IO;
using System.Text;
using Sop.Common.Img.Exif;

namespace Sop.Common.Img
{
    /// <summary>
    /// EXIF(EXchangeable Image File Format)是专门为数码相机的照片设定的可交换图像文件格式，通过在图片下载URL后附加exif指示符（区分大小写）获取。  
    /// </summary>
    public class ImgExif
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <returns></returns>
        public static string GetExifDateTime(string sourceFile)
        {
            if (!File.Exists(sourceFile))
            {
                throw new Exception(" sourceFile is not exist");
            }
            string result = DateTime.MinValue.ToString();
            using (Bitmap sourceImage = new Bitmap(sourceFile))
            {
                Encoding ascii = Encoding.ASCII;
                //遍历图像文件元数据，检索所有属性
                foreach (PropertyItem pp in sourceImage.PropertyItems)
                {
                    //如果是PropertyTagDateTime，则返回该属性所对应的值
                    var isd = ascii.GetString(pp.Value);
                    if (pp.Id == 0x0132)
                    {
                        result = ascii.GetString(pp.Value);
                    }
                }
            }
            return result;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Description"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static string SetExifValue(string Description, string Value)
        {
            string DescriptionValue = null;

            switch (Description)
            {
                case "MeteringMode":

                    #region  MeteringMode
                    {
                        switch (Value)
                        {
                            case "0":
                                DescriptionValue = "Unknown"; break;
                            case "1":
                                DescriptionValue = "Average"; break;
                            case "2":
                                DescriptionValue = "Center Weighted Average"; break;
                            case "3":
                                DescriptionValue = "Spot"; break;
                            case "4":
                                DescriptionValue = "Multi-spot"; break;
                            case "5":
                                DescriptionValue = "Multi-segment"; break;
                            case "6":
                                DescriptionValue = "Partial"; break;
                            case "255":
                                DescriptionValue = "Other"; break;
                        }
                    }
                    #endregion

                    break;
                case "ResolutionUnit":

                    #region ResolutionUnit
                    {
                        switch (Value)
                        {
                            case "1":
                                DescriptionValue = "No Units"; break;
                            case "2":
                                DescriptionValue = "Inch"; break;
                            case "3":
                                DescriptionValue = "Centimeter"; break;
                        }
                    }

                    #endregion

                    break;
                case "Flash":

                    #region Flash
                    {
                        switch (Value)
                        {
                            case "0":
                                DescriptionValue = "未使用"; break;
                            case "1":
                                DescriptionValue = "闪光"; break;
                            case "5":
                                DescriptionValue = "Flash fired but strobe return light not detected"; break;
                            case "7":
                                DescriptionValue = "Flash fired and strobe return light detected"; break;
                        }
                    }
                    #endregion

                    break;
                case "ExposureProg":

                    #region ExposureProg
                    {
                        switch (Value)
                        {
                            case "0":
                                DescriptionValue = "没有定义"; break;
                            case "1":
                                DescriptionValue = "手动控制"; break;
                            case "2":
                                DescriptionValue = "程序控制"; break;
                            case "3":
                                DescriptionValue = "光圈优先"; break;
                            case "4":
                                DescriptionValue = "快门优先"; break;
                            case "5":
                                DescriptionValue = "夜景模式"; break;
                            case "6":
                                DescriptionValue = "运动模式"; break;
                            case "7":
                                DescriptionValue = "肖像模式"; break;
                            case "8":
                                DescriptionValue = "风景模式"; break;
                            case "9":
                                DescriptionValue = "保留的"; break;
                        }
                    }

                    #endregion

                    break;
                case "CompConfig":

                    #region CompConfig
                    {
                        switch (Value)
                        {

                            case "513":
                                DescriptionValue = "YCbCr"; break;
                        }
                    }
                    #endregion

                    break;
                case "Aperture":

                    #region Aperture
                    DescriptionValue = Value;
                    #endregion

                    break;
                case "LightSource":

                    #region LightSource
                    {
                        switch (Value)
                        {
                            case "0":
                                DescriptionValue = "未知"; break;
                            case "1":
                                DescriptionValue = "日光"; break;
                            case "2":
                                DescriptionValue = "荧光灯"; break;
                            case "3":
                                DescriptionValue = "白炽灯"; break;
                            case "10":
                                DescriptionValue = "闪光灯"; break;
                            case "17":
                                DescriptionValue = "标准光A"; break;
                            case "18":
                                DescriptionValue = "标准光B"; break;
                            case "19":
                                DescriptionValue = "标准光C"; break;
                            case "20":
                                DescriptionValue = "标准光D55"; break;
                            case "21":
                                DescriptionValue = "标准光D65"; break;
                            case "22":
                                DescriptionValue = "标准光D75"; break;
                            case "255":
                                DescriptionValue = "其它"; break;
                        }
                    }


                    #endregion
                    break;

            }
            return DescriptionValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <returns></returns>
        public static Metadata GetExifInfo(string sourceFile)
        {
            // 创建一个图片的实例 
            Image image = Image.FromFile(sourceFile);
            // 创建一个整型数组来存储图像中属性数组的ID 
            int[] MyPropertyIdList = image.PropertyIdList;
            //创建一个封闭图像属性数组的实例 
            PropertyItem[] propertyItems = new PropertyItem[MyPropertyIdList.Length];
            //创建一个图像EXIT信息的实例结构对象，并且赋初值 

            #region 创建一个图像EXIT信息的实例结构对象，并且赋初值
            Metadata info = new Metadata();
            info.EquipmentMake.Hex = "10f";
            info.CameraModel.Hex = "110";
            info.DatePictureTaken.Hex = "9003";
            info.ExposureTime.Hex = "829a";
            info.Fstop.Hex = "829d";
            info.ShutterSpeed.Hex = "9201";
            info.MeteringMode.Hex = "9207";
            info.Flash.Hex = "9209";
            info.FNumber.Hex = "829d"; //added  
            info.ExposureProg.Hex = ""; //added  
            info.SpectralSense.Hex = "8824"; //added  
            info.ISOSpeed.Hex = "8827"; //added  
            info.OECF.Hex = "8828"; //added  
            info.Ver.Hex = "9000"; //added  
            info.CompConfig.Hex = "9101"; //added  
            info.CompBPP.Hex = "9102"; //added  
            info.Aperture.Hex = "9202"; //added  
            info.Brightness.Hex = "9203"; //added  
            info.ExposureBias.Hex = "9204"; //added  
            info.MaxAperture.Hex = "9205"; //added  
            info.SubjectDist.Hex = "9206"; //added  
            info.LightSource.Hex = "9208"; //added  
            info.FocalLength.Hex = "920a"; //added  
            info.FPXVer.Hex = "a000"; //added  
            info.ColorSpace.Hex = "a001"; //added  
            info.FocalXRes.Hex = "a20e"; //added  
            info.FocalYRes.Hex = "a20f"; //added  
            info.FocalResUnit.Hex = "a210"; //added  
            info.ExposureIndex.Hex = "a215"; //added  
            info.SensingMethod.Hex = "a217"; //added  
            info.SceneType.Hex = "a301";
            info.CfaPattern.Hex = "a302";
            #endregion

            // ASCII编码 
            System.Text.ASCIIEncoding Value = new System.Text.ASCIIEncoding();

            int index = 0;
            int MyPropertyIdListCount = MyPropertyIdList.Length;
            if (MyPropertyIdListCount != 0)
            {
                foreach (int MyPropertyId in MyPropertyIdList)
                {
                    string hexVal = "";
                    propertyItems[index] = image.GetPropertyItem(MyPropertyId);

                    #region 初始化各属性值
                    string myPropertyIdString = image.GetPropertyItem(MyPropertyId).Id.ToString("x");
                    switch (myPropertyIdString)
                    {
                        case "10f":
                            {
                                info.EquipmentMake.RawValueAsString = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value);
                                info.EquipmentMake.DisplayValue = Value.GetString(propertyItems[index].Value);
                                break;
                            }

                        case "110":
                            {
                                info.CameraModel.RawValueAsString = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value);
                                info.CameraModel.DisplayValue = Value.GetString(propertyItems[index].Value);
                                break;

                            }

                        case "9003":
                            {
                                info.DatePictureTaken.RawValueAsString = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value);
                                info.DatePictureTaken.DisplayValue = Value.GetString(propertyItems[index].Value);
                                break;
                            }

                        case "9207":
                            {
                                info.MeteringMode.RawValueAsString = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value);
                                info.MeteringMode.DisplayValue = SetExifValue("MeteringMode", BitConverter.ToInt16(image.GetPropertyItem(MyPropertyId).Value, 0).ToString());
                                break;
                            }

                        case "9209":
                            {
                                info.Flash.RawValueAsString = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value);
                                info.Flash.DisplayValue = SetExifValue("Flash", BitConverter.ToInt16(image.GetPropertyItem(MyPropertyId).Value, 0).ToString());
                                break;
                            }

                        case "829a":
                            {
                                info.ExposureTime.RawValueAsString = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value);
                                string StringValue = "";
                                for (int Offset = 0; Offset < image.GetPropertyItem(MyPropertyId).Len; Offset = Offset + 4)
                                {
                                    StringValue += BitConverter.ToInt32(image.GetPropertyItem(MyPropertyId).Value, Offset).ToString() + "/";
                                }
                                info.ExposureTime.DisplayValue = StringValue.Substring(0, StringValue.Length - 1);
                                break;
                            }
                        case "829d":
                            {
                                info.Fstop.RawValueAsString = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value);
                                int int1;
                                int int2;
                                int1 = BitConverter.ToInt32(image.GetPropertyItem(MyPropertyId).Value, 0);
                                int2 = BitConverter.ToInt32(image.GetPropertyItem(MyPropertyId).Value, 4);
                                info.Fstop.DisplayValue = "F/" + (int1 / int2);

                                info.FNumber.RawValueAsString = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value);
                                info.FNumber.DisplayValue = BitConverter.ToInt16(image.GetPropertyItem(MyPropertyId).Value, 0).ToString();

                                break;
                            }
                        case "9201":
                            {
                                info.ShutterSpeed.RawValueAsString = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value);
                                string StringValue = BitConverter.ToInt32(image.GetPropertyItem(MyPropertyId).Value, 0).ToString();
                                info.ShutterSpeed.DisplayValue = "1/" + StringValue;
                                break;
                            }

                        case "8822":
                            {
                                info.ExposureProg.RawValueAsString = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value);
                                info.ExposureProg.DisplayValue = SetExifValue("ExposureProg", BitConverter.ToInt16(image.GetPropertyItem(MyPropertyId).Value, 0).ToString());
                                break;
                            }

                        case "8824":
                            {
                                info.SpectralSense.RawValueAsString = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value);
                                info.SpectralSense.DisplayValue = Value.GetString(propertyItems[index].Value);
                                break;
                            }
                        case "8827":
                            {
                                hexVal = "";
                                info.ISOSpeed.RawValueAsString = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value);
                                hexVal = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value).Substring(0, 2);
                                info.ISOSpeed.DisplayValue = Convert.ToInt32(hexVal, 16).ToString();//Value.GetString(MyPropertyItemList[index].Value); 
                                break;
                            }

                        case "8828":
                            {
                                info.OECF.RawValueAsString = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value);
                                info.OECF.DisplayValue = Value.GetString(propertyItems[index].Value);
                                break;
                            }

                        case "9000":
                            {
                                info.Ver.RawValueAsString = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value);
                                info.Ver.DisplayValue = Value.GetString(propertyItems[index].Value).Substring(1, 1) + "." + Value.GetString(propertyItems[index].Value).Substring(2, 2);
                                break;
                            }

                        case "9101":
                            {
                                info.CompConfig.RawValueAsString = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value);
                                info.CompConfig.DisplayValue = SetExifValue("CompConfig", BitConverter.ToInt16(image.GetPropertyItem(MyPropertyId).Value, 0).ToString());
                                break;
                            }

                        case "9102":
                            {
                                info.CompBPP.RawValueAsString = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value);
                                info.CompBPP.DisplayValue = BitConverter.ToInt16(image.GetPropertyItem(MyPropertyId).Value, 0).ToString();
                                break;
                            }

                        case "9202":
                            {
                                hexVal = "";
                                info.Aperture.RawValueAsString = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value);
                                hexVal = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value).Substring(0, 2);
                                hexVal = Convert.ToInt32(hexVal, 16).ToString();
                                hexVal = hexVal + "00";
                                info.Aperture.DisplayValue = hexVal.Substring(0, 1) + "." + hexVal.Substring(1, 2);
                                break;
                            }

                        case "9203":
                            {
                                hexVal = "";
                                info.Brightness.RawValueAsString = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value);
                                hexVal = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value).Substring(0, 2);
                                hexVal = Convert.ToInt32(hexVal, 16).ToString();
                                hexVal = hexVal + "00";
                                info.Brightness.DisplayValue = hexVal.Substring(0, 1) + "." + hexVal.Substring(1, 2);
                                break;
                            }

                        case "9204":
                            {
                                info.ExposureBias.RawValueAsString = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value);
                                info.ExposureBias.DisplayValue = BitConverter.ToInt16(image.GetPropertyItem(MyPropertyId).Value, 0).ToString();
                                break;
                            }

                        case "9205":
                            {
                                hexVal = "";
                                info.MaxAperture.RawValueAsString = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value);
                                hexVal = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value).Substring(0, 2);
                                hexVal = Convert.ToInt32(hexVal, 16).ToString();
                                hexVal = hexVal + "00";
                                info.MaxAperture.DisplayValue = hexVal.Substring(0, 1) + "." + hexVal.Substring(1, 2);
                                break;
                            }

                        case "9206":
                            {
                                info.SubjectDist.RawValueAsString = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value);
                                info.SubjectDist.DisplayValue = Value.GetString(propertyItems[index].Value);
                                break;
                            }

                        case "9208":
                            {
                                info.LightSource.RawValueAsString = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value);
                                info.LightSource.DisplayValue = SetExifValue("LightSource", BitConverter.ToInt16(image.GetPropertyItem(MyPropertyId).Value, 0).ToString());
                                break;
                            }

                        case "920a":
                            {
                                hexVal = "";
                                info.FocalLength.RawValueAsString = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value);
                                hexVal = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value).Substring(0, 2);
                                hexVal = Convert.ToInt32(hexVal, 16).ToString();
                                hexVal = hexVal + "00";
                                info.FocalLength.DisplayValue = hexVal.Substring(0, 1) + "." + hexVal.Substring(1, 2);
                                break;
                            }

                        case "a000":
                            {
                                info.FPXVer.RawValueAsString = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value);
                                info.FPXVer.DisplayValue = Value.GetString(propertyItems[index].Value).Substring(1, 1) + "." + Value.GetString(propertyItems[index].Value).Substring(2, 2);
                                break;
                            }

                        case "a001":
                            {
                                info.ColorSpace.RawValueAsString = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value);
                                if (BitConverter.ToInt16(image.GetPropertyItem(MyPropertyId).Value, 0).ToString() == "1")
                                    info.ColorSpace.DisplayValue = "RGB";
                                if (BitConverter.ToInt16(image.GetPropertyItem(MyPropertyId).Value, 0).ToString() == "65535")
                                    info.ColorSpace.DisplayValue = "Uncalibrated";
                                break;
                            }

                        case "a20e":
                            {
                                info.FocalXRes.RawValueAsString = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value);
                                info.FocalXRes.DisplayValue = BitConverter.ToInt16(image.GetPropertyItem(MyPropertyId).Value, 0).ToString();
                                break;
                            }

                        case "a20f":
                            {
                                info.FocalYRes.RawValueAsString = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value);
                                info.FocalYRes.DisplayValue = BitConverter.ToInt16(image.GetPropertyItem(MyPropertyId).Value, 0).ToString();
                                break;
                            }

                        case "a210":
                            {
                                string aa;
                                info.FocalResUnit.RawValueAsString = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value);
                                aa = BitConverter.ToInt16(image.GetPropertyItem(MyPropertyId).Value, 0).ToString(); ;
                                if (aa == "1") info.FocalResUnit.DisplayValue = "没有单位";
                                if (aa == "2") info.FocalResUnit.DisplayValue = "英尺";
                                if (aa == "3") info.FocalResUnit.DisplayValue = "厘米";
                                break;
                            }

                        case "a215":
                            {
                                info.ExposureIndex.RawValueAsString = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value);
                                info.ExposureIndex.DisplayValue = Value.GetString(propertyItems[index].Value);
                                break;
                            }

                        case "a217":
                            {
                                string aa;
                                aa = BitConverter.ToInt16(image.GetPropertyItem(MyPropertyId).Value, 0).ToString();
                                info.SensingMethod.RawValueAsString = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value);
                                if (aa == "2") info.SensingMethod.DisplayValue = "1 chip color area sensor";
                                break;
                            }

                        case "a301":
                            {
                                info.SceneType.RawValueAsString = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value);
                                info.SceneType.DisplayValue = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value);
                                break;
                            }

                        case "a302":
                            {
                                info.CfaPattern.RawValueAsString = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value);
                                info.CfaPattern.DisplayValue = BitConverter.ToString(image.GetPropertyItem(MyPropertyId).Value);
                                break;
                            }



                    }
                    #endregion

                    index++;
                }
            }

            info.XResolution.DisplayValue = image.HorizontalResolution.ToString();
            info.YResolution.DisplayValue = image.VerticalResolution.ToString();
            info.ImageHeight.DisplayValue = image.Height.ToString();
            info.ImageWidth.DisplayValue = image.Width.ToString();
            image.Dispose();
            return info;
        }







    }



}
