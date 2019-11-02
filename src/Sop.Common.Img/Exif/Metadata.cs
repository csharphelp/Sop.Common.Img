using System;
using System.Collections.Generic;
using System.Text;

namespace Sop.Common.Img.Exif
{
    /// <summary>
    /// 结构：存储EXIF元素信息
    /// </summary>
    public class Metadata
    {
        /// <summary>
        /// 设备制造商
        /// </summary>
        public MetadataDetail EquipmentMake { get; set; }
        /// <summary>
        /// 相机类型
        /// </summary>
        public MetadataDetail CameraModel { get; set; }
        /// <summary>
        /// 曝光时间
        /// </summary>
        public MetadataDetail ExposureTime{ get; set; }
        public MetadataDetail Fstop{ get; set; }
        /// <summary>
        /// 分辨率
        /// </summary>
        public MetadataDetail ResolutionUnit{ get; set; }
        public MetadataDetail DatePictureTaken{ get; set; }
        /// <summary>
        /// 快门速度
        /// </summary>
        public MetadataDetail ShutterSpeed{ get; set; }
        /// <summary>
        /// 曝光模式
        /// </summary>
        public MetadataDetail MeteringMode{ get; set; }
        /// <summary>
        /// 闪光灯
        /// </summary>
        public MetadataDetail Flash{ get; set; }
        public MetadataDetail XResolution{ get; set; }
        public MetadataDetail YResolution{ get; set; }
        /// <summary>
        /// 照片宽度
        /// </summary>
        public MetadataDetail ImageWidth{ get; set; }
        /// <summary>
        /// 照片高度
        /// </summary>
        public MetadataDetail ImageHeight{ get; set; }
        /// <summary>
        /// f值，光圈数
        /// </summary>
        public MetadataDetail FNumber{ get; set; }
        /// <summary>
        /// 曝光程序
        /// </summary>
        public MetadataDetail ExposureProg{ get; set; }
        public MetadataDetail SpectralSense{ get; set; }
        /// <summary>
        /// ISO感光度
        /// </summary>
        public MetadataDetail ISOSpeed{ get; set; }
        public MetadataDetail OECF{ get; set; }
        /// <summary>
        /// EXIF版本
        /// </summary>
        public MetadataDetail Ver{ get; set; }
        /// <summary>
        /// 色彩设置
        /// </summary>
        public MetadataDetail CompConfig{ get; set; }
        /// <summary>
        /// 压缩比率
        /// </summary>
        public MetadataDetail CompBPP{ get; set; }
        /// <summary>
        /// 光圈值
        /// </summary>
        public MetadataDetail Aperture{ get; set; }
        /// <summary>
        /// 亮度值Ev
        /// </summary>
        public MetadataDetail Brightness{ get; set; }
        /// <summary>
        /// 曝光补偿
        /// </summary>
        public MetadataDetail ExposureBias{ get; set; }
        /// <summary>
        /// 最大光圈值
        /// </summary>
        public MetadataDetail MaxAperture{ get; set; }
        /// <summary>
        /// 主体距离
        /// </summary>
        public MetadataDetail SubjectDist{ get; set; }
        /// <summary>
        /// 白平衡
        /// </summary>
        public MetadataDetail LightSource{ get; set; }
        /// <summary>
        /// 焦距
        /// </summary>
        public MetadataDetail FocalLength{ get; set; }
        /// <summary>
        /// FlashPix版本
        /// </summary>
        public MetadataDetail FPXVer{ get; set; }
        /// <summary>
        /// 色彩空间
        /// </summary>
        public MetadataDetail ColorSpace{ get; set; }
        public MetadataDetail Interop{ get; set; }
        public MetadataDetail FlashEnergy{ get; set; } 
        public MetadataDetail SpatialFR{ get; set; } 
        public MetadataDetail FocalXRes{ get; set; } 
        public MetadataDetail FocalYRes{ get; set; }
        public MetadataDetail FocalResUnit{ get; set; }
        /// <summary>
        /// 曝光指数
        /// </summary>
        public MetadataDetail ExposureIndex{ get; set; }
        public MetadataDetail SensingMethod{ get; set; }
        /// <summary>
        /// 感应方式
        /// </summary>
        public MetadataDetail SceneType{ get; set; }
        public MetadataDetail CfaPattern{ get; set; }
    }
}
